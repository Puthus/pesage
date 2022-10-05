using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using BinaryKits.Zpl.Label;
using BinaryKits.Zpl.Label.Elements;
using pesage.Properties;

namespace pesage
{
    public partial class MainWindow : Form
    {
        private readonly PrivateFontCollection _fonts = new PrivateFontCollection();
        private readonly Font _myFont;
        private readonly Poids _weight;
        private readonly SqlConnection _conn = new SqlConnection(Settings.Default.pesageConn);
        private DataSet _ds;
        private bool _isInserting;
        private bool _isCanceled;
        private readonly List<KeyValuePair<string, dynamic>> _pessageAdapters = new List<KeyValuePair<string, dynamic>>();
#if DEBUG
        private TextBox debugBox;
#endif
        private Thread _readThread;
        private SerialPort _serialPort1;
        private string _table;
        private bool _isRunning = true;
        public MainWindow()
        {
            InitializeComponent();
            var fontData = Resources.Inter_font;
            var fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
            Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            _fonts.AddMemoryFont(fontPtr, Resources.Inter_font.Length);
            AddFontMemResourceEx(fontPtr, (uint)Resources.Inter_font.Length, IntPtr.Zero, ref dummy);
            Marshal.FreeCoTaskMem(fontPtr);
            _myFont = new Font(_fonts.Families[0], 12);
            _weight = new Poids();
        }

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //FONT
            foreach (Control c in Controls) c.Font = _myFont;

            residuTableAdapter.Fill(pesageDataSet.Residu);
            clientTableAdapter.Fill(pesageDataSet.Client);
            c_ServiceTableAdapter.Fill(pesageDataSet.C_Service);
            operateurTableAdapter.Fill(pesageDataSet.Operateur);
            conteneurTableAdapter.Fill(pesageDataSet.Conteneur);
            client_serviceTableAdapter.Fill(pesageDataSet.client_service);
            _pessageAdapters.Add(new KeyValuePair<string, dynamic>("Residu", residuTableAdapter));
            _pessageAdapters.Add(new KeyValuePair<string, dynamic>("Conteneur", conteneurTableAdapter));
            _pessageAdapters.Add(new KeyValuePair<string, dynamic>("Operateur", operateurTableAdapter));
            _pessageAdapters.Add(new KeyValuePair<string, dynamic>("C_Service", c_ServiceTableAdapter));
            _pessageAdapters.Add(new KeyValuePair<string, dynamic>("Client", clientTableAdapter));
            _pessageAdapters.Add(new KeyValuePair<string, dynamic>("client_service", client_serviceTableAdapter));
            xOffsetNumeric.Value = Settings.Default.xOffsetPrint;
            yOffsetNumeric.Value = Settings.Default.yOffsetPrint;
            regexTxt.Text = Settings.Default.regex == "" ? "(\\w*)?,?(\\w*)?,?([+-]?([0-9]*[.|,])?[0-9]+)(\\w*)" : Settings.Default.regex;
#if DEBUG
            regexTxt.ReadOnly = false;
            debugBox = new TextBox();
            tabPage4.Controls.Add(debugBox);
            debugBox.Location = new Point(175, 174);
            debugBox.Multiline = true;
            debugBox.Name = "debugBox";
            debugBox.Size = new Size(392, 284);
            debugBox.TabIndex = 24;
#endif
            try
            {
                _conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Connection to database failed! Line 75" + '\n' + ex.Message);
            }
            if (clientLib.SelectedValue != null)
                try
                {
                    c_ServiceTableAdapter.FillBy(pesageDataSet.C_Service, Convert.ToInt32(clientLib.SelectedValue));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Error Filling Client services Line 84" + '\n' + ex.Message);
                }

            foreach (string portName in SerialPort.GetPortNames())
            {
                comPortBox.Items.Add(portName);
                //MessageBox.Show("Adding Port " + portName);
            }

            if (comPortBox.Items.Count <= 0) return;

            comPortBox.SelectedIndex = 0;
            _readThread = new Thread(Read);
            _serialPort1 = new SerialPort(comPortBox.SelectedItem.ToString(), 9600, Parity.None, 8, StopBits.One);
            _serialPort1.ReadTimeout = 500;
            _serialPort1.WriteTimeout = 500;

            try
            {
                _serialPort1.Open();
                _readThread.Start();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($@"Error Opening COM Port {_serialPort1.PortName}" + '\n' + ex.Message);
            }
            catch (OutOfMemoryException ex)
            {
                MessageBox.Show($@"Error Opening Thread, you are out of memory" + '\n' + ex.Message);
            }
            GenerateCodeBarre();
        }
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.xOffsetPrint = (int)xOffsetNumeric.Value;
            Settings.Default.yOffsetPrint = (int)yOffsetNumeric.Value;
            Settings.Default.comPort = comPortBox.Text;
            Settings.Default.regex = regexTxt.Text;
            Settings.Default.Save();
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.xOffsetPrint = (int)xOffsetNumeric.Value;
            Settings.Default.yOffsetPrint = (int)yOffsetNumeric.Value;
            Settings.Default.comPort = comPortBox.Text;
            Settings.Default.regex = regexTxt.Text;
            Settings.Default.Save();
            if (_conn != null && _conn.State == ConnectionState.Open)
                _conn.Close();
            if (_serialPort1 != null && _serialPort1.IsOpen)
                _serialPort1.Close();
            _isRunning = false;
            if (_readThread != null)
                _readThread.Abort();
        }

        #region read com port

        public void Read()
        {
            while (_isRunning)
                if (_serialPort1.IsOpen)
                {
                    try
                    {
                        var pattern = Settings.Default.regex == "" ? "(\\w*)?,?(\\w*)?,?([+-]?([0-9]*[.|,])?[0-9]+)(\\w*)" : Settings.Default.regex;
                        var input = _serialPort1.ReadLine();
                        var x = Regex.Match(input, pattern);
                        if (x.Success)
                        {
                            SetText(weightLib, x);
#if DEBUG
                            SetText(debugBox, x);
#endif
                        }
                    }
                    catch (TimeoutException exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
        }

        private void SetText(Control txtBox, Match m)
        {
            try
            {
                _weight.IsStable = m.Groups[1].Value == "ST";
                _weight.IsNet = m.Groups[2].Value == "NT";
                _weight.Weight = float.Parse(m.Groups[3].Value);
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
            }
            if (weightLib.InvokeRequired)
            {
                SetTextCallback d = SetText;
                try
                {
                    Invoke(d, txtBox, m);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                txtBox.ForeColor = _weight.IsStable ? Color.Green : Color.Red;
                var unit = m.Groups[5].Value.ToUpper();
                tareLib.Text = $@"{_weight.Tare:F2} {unit}";
                txtBox.Text = $@"{_weight.Weight:F2} {unit}";
            }
        }
#if DEBUG
        private void SetText(TextBox txtBox, Match m)
        {
            if (weightLib.InvokeRequired)
            {
                SetTextCallback d = SetText;
                try
                {
                    Invoke(d, txtBox, m);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                txtBox.AppendText(m.Groups[0].Value);
            }
        }
#endif

        #endregion

        private void clientLib_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (clientLib.Items.Count > 0 && clientLib.SelectedValue != null)
                    try
                    {
                        if(c_ServiceTableAdapter.FillBy(pesageDataSet.C_Service, Convert.ToInt32(clientId.SelectedValue)) > 0)
                            GenerateCodeBarre();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(@"Something wrong happened in clientLib_SelectedIndexChanged inner try"+ex.Message);
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Something wrong happened in clientLib_SelectedIndexChanged outer try " + ex.Message);
            }
        }

        private void print_Click(object sender, EventArgs e)
        {
            if(!IsValidTicket())return;
            var font = new ZplFont();
            var elements = new List<ZplElementBase>();
            int xoffset = Settings.Default.xOffsetPrint;
            int yoffset = Settings.Default.yOffsetPrint;
            var x = 110 + xoffset;
            var y = 30 + yoffset;
            elements.Add(new ZplTextField($@"Client : {clientLib.Text}", x, y * 1 + yoffset, font));
            elements.Add(new ZplTextField($@"Service : {serviceLib.Text}", x, y * 2 + yoffset, font));
            elements.Add(new ZplTextField($@"Residu : {residuLib.Text}", x, y * 3 + yoffset, font));
            elements.Add(new ZplTextField($@"Conteneur : {conteneurLib.Text}", x, y * 4 + yoffset, font));
            elements.Add(new ZplTextField($@"Poids : {weightLib.Text}", x, y * 5 + yoffset, font));
            elements.Add(new ZplTextField($@"Date : {DateTime.Now}", x, y * 6 + yoffset, font));
            elements.Add(new ZplBarcode128(codeBarreLib.Text, x + 48, y * 8 + yoffset));
            var renderEngine = new ZplEngine(elements);
            var output = renderEngine.ToZplString(new ZplRenderOptions { AddEmptyLineBeforeElementStart = true });
            foreach (string printer in PrinterSettings.InstalledPrinters)
                if (printer.Contains("GK420t"))
                    RawPrinterHelper.SendStringToPrinter(printer, output);
            saveTicket_Click(sender, e);
        }
        public bool IsValidTicket(bool mboxs = true){
                if (mboxs && clientId.SelectedIndex == -1) MessageBox.Show("Impossible de Valider étiquettes : veulliez choisire un Client");
                if (mboxs && serviceId.SelectedIndex == -1) MessageBox.Show("Impossible de Valider étiquettes : veulliez choisire un Service");
                if (mboxs && residuId.SelectedIndex == -1) MessageBox.Show("Impossible de Valider étiquettes : veulliez choisire un Residu");
                if (mboxs && conteneurId.SelectedIndex == -1) MessageBox.Show("Impossible de Valider étiquettes : veulliez choisire un Conteneur");
                if (mboxs && operateurId.SelectedIndex == -1)  MessageBox.Show("Impossible de Valider étiquettes : veulliez choisire un Operateur");
            return (clientId.SelectedIndex != -1
                && serviceId.SelectedIndex != -1
                && residuId.SelectedIndex != -1
                && conteneurId.SelectedIndex != -1
                && operateurId.SelectedIndex != -1);
        }
        private void serviceEdit_Click(object sender, EventArgs e)
        {
            _isCanceled = false;
            _table = "C_Service";
            _isInserting = false;
            groupBox1.Text = @"Modifier Services";
            c_ServiceTableAdapter.FillBy(pesageDataSet.C_Service, Convert.ToInt32(clientId.SelectedValue));
            dataGridView2.DataSource = pesageDataSet.C_Service;
            //GenerateCodeBarre();
        }

        private void conteneurEdit_Click(object sender, EventArgs e)
        {
            _isCanceled = false;
            groupBox1.Text = @"Modifier Conteneurs";
            _table = "Conteneur";
            _isInserting = false;
            conteneurTableAdapter.Fill(pesageDataSet.Conteneur);
            dataGridView2.DataSource = pesageDataSet.Conteneur;
            //GenerateCodeBarre();
        }

        private void residuEdit_Click(object sender, EventArgs e)
        {
            _isCanceled = false;
            groupBox1.Text = @"Modifier Residus";
            _table = "Residu";
            _isInserting = false;
            residuTableAdapter.Fill(pesageDataSet.Residu);
            dataGridView2.DataSource = pesageDataSet.Residu;
            //GenerateCodeBarre();
        }

        private void serviceAdd_Click(object sender, EventArgs e)
        {
            groupBox1.Text = @"Ajouter Services";
            _table = "C_Service";
            _isInserting = true;
            init_add_process();
        }

        private void conteneurAdd_Click(object sender, EventArgs e)
        {
            groupBox1.Text = @"Ajouter Conteneurs";
            _table = "Conteneur";
            _isInserting = true;
            init_add_process();
        }


        private void residuAdd_Click(object sender, EventArgs e)
        {
            groupBox1.Text = @"Ajouter Residus";
            _table = "Residu";
            _isInserting = true;
            init_add_process();
        }

        private void deleteRow_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.SelectedRows) dataGridView2.Rows.Remove(row);
        }

        private void addRow_Click(object sender, EventArgs e)
        {
            if(!_isInserting){
                MessageBox.Show(@"Impossible d'ajouter une ligne dans ce mode");
                return;
            }
            int last;
            if (_ds.Tables[_table].Rows.Count == 0)
                try
                {
                    last = c_ServiceTableAdapter.GetData().AsEnumerable().Select(al => al.Field<int>("id")).Distinct()
                        .ToList().Max() + 1;
                }
                catch
                {
                    last = 1;
                }
            else
                last = Convert.ToInt32(_ds.Tables[_table].Rows[_ds.Tables[_table].Rows.Count - 1][0]) + 1;

            _ds.Tables[_table].Rows.Add(last);
        }

        private void saveData_Click(object sender, EventArgs e)
        {
            if (_isCanceled) return;
            try
            {
                var adapter = _pessageAdapters.Find(x => x.Key == _table).Value;
                if (_isInserting)
                {
                    foreach (DataRow row in _ds.Tables[_table].Rows)
                    {
                        adapter.Insert(row["libelle"].ToString());
                        if (_table != "C_Service") continue;
                        var max = c_ServiceTableAdapter.GetData().AsEnumerable().Select(al => al.Field<int>("id"))
                            .Distinct().ToList().Max();
                        client_serviceTableAdapter.Insert(Convert.ToInt32(clientId.SelectedValue), max);
                    }
                }
                else
                {
                    DataRowCollection dataRowCollection = pesageDataSet.Tables[_table].GetChanges()?.Rows;
                    if (dataRowCollection != null)
                        foreach (DataRow row in dataRowCollection)
                        {
                            switch (row.RowState)
                            {
                                case DataRowState.Modified:
                                    adapter.Update(row);
                                    break;
                                case DataRowState.Deleted:
                                    adapter.Delete((int)row["id", DataRowVersion.Original], (string)row["libelle", DataRowVersion.Original]);
                                    break;
                            }
                        }
                }

                dataGridView2.DataSource = new DataSet();
                MessageBox.Show(@"Les données ont été enregistrées", @"Succès", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void init_add_process()
        {
            _isCanceled = false;
            _ds = new DataSet();
            _ds.Tables.Add(new DataTable(_table));
            _ds.Tables[_table].Columns.Add(new DataColumn("id", typeof(int)));
            _ds.Tables[_table].Columns.Add(new DataColumn("libelle", typeof(string)));
            var keys = new DataColumn[1];
            keys[0] = _ds.Tables[_table].Columns["id"];
            _ds.Tables[_table].PrimaryKey = keys;
            dataGridView2.DataSource = _ds.Tables[_table].DefaultView;
            dataGridView2.Columns["id"].ReadOnly = true;
            dataGridView2.Columns["libelle"].ReadOnly = false;
        }

        private void search_Click(object sender, EventArgs e)
        {
            ticketTableAdapter.FillByDate(pesageDataSet.tickets, dateDebut.Value, dateFin.Value);
        }

        private void searchCodeBarre_Click(object sender, EventArgs e)
        {
            ticketTableAdapter.FillByCode(pesageDataSet.tickets, codeBarre.Text);
        }

        private delegate void SetTextCallback(Control txtBox, Match m);

        private void saveTicket_Click(object sender, EventArgs e)
        {
            if(!IsValidTicket())return;
            try
            {
                etiquetteTableAdapter.Insert(
                    codeBarreLib.Text,
                    DateTime.Now,
                    (float)_weight.Weight,
                    Convert.ToInt32(clientId.SelectedValue),
                    Convert.ToInt32(serviceId.SelectedValue),
                    Convert.ToInt32(conteneurId.SelectedValue),
                    Convert.ToInt32(residuId.SelectedValue),
                    Convert.ToInt32(operateurId.SelectedValue)
                );
                MessageBox.Show(@"Ettiquette enregistrer", @"Succès", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                GenerateCodeBarre();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void serviceLib_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateCodeBarre();
        }
        private void residuLib_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateCodeBarre();
        }
        private void conteneurLib_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateCodeBarre();
        }
        private void operatorLib_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateCodeBarre();
        }

        private void GenerateCodeBarre()
        {
            if (!IsValidTicket(false))
            {
                codeBarreLib.Text = "###################";
                return;
            }

            var cl = Convert.ToInt32(clientId.SelectedValue);
            var se = Convert.ToInt32(serviceId.SelectedValue);
            var co = Convert.ToInt32(conteneurId.SelectedValue);
            var re = Convert.ToInt32(residuId.SelectedValue);
            var op = Convert.ToInt32(operateurId.SelectedValue);
            var text = $@"{cl:00}{se:00}{co:00}{re:00}{op:00}";
            var count = etiquetteTableAdapter.ticketNumber($"%{text}%");
            text = $@"{text}{count:000000}";
            codeBarreLib.Text = text;
        }

        private void codeBarre_Click(object sender, EventArgs e)
        {
            codeBarre.SelectAll();
        }

        private void codeBarre_KeyPress(object sender, KeyPressEventArgs e)
        {
            // if key is enter 
            if (e.KeyChar == (char)13)
            {
                searchCodeBarre_Click(sender, e);
                codeBarre.SelectAll();
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "";
            dataGridView2.DataSource = new DataSet();
            dataGridView2.Refresh();
            _isCanceled = true;
        }

        private void comPortBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_serialPort1 == null) return;
                if (_serialPort1.IsOpen)
                    _serialPort1.Close();
                _serialPort1.PortName = comPortBox.SelectedItem.ToString();
                _serialPort1.Open();
            }
            catch (IOException ex)
            {
                MessageBox.Show(@"Error closing Com port " + '\n' + ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($@"Error Opening COM Port {_serialPort1.PortName}" + '\n' + ex.Message);
            }
        }

        private void saveConf_Click(object sender, EventArgs e)
        {
            Settings.Default.Save();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateCodeBarre();
        }
    }
}