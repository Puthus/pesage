using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
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
        private SqlConnection _conn;
        private DataSet _ds;
        private bool _isInserting;
        private bool _isCanceled;

        private readonly List<KeyValuePair<string, dynamic>> _pessageAdapters =
            new List<KeyValuePair<string, dynamic>>();

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
        private static extern IntPtr
            AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        private void MainWindow_Load(object sender, EventArgs e)
        {
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
            //setting the font to inter
            try
            {
                _conn = new SqlConnection(Settings.Default.pesageConnectionString);
                _conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Connection to database failed!",ex.Message);
            }
            if (clientLib.SelectedValue != null)
                try
                {
                    c_ServiceTableAdapter.FillBy(pesageDataSet.C_Service,
                        (int)Convert.ChangeType(clientLib.SelectedValue, typeof(int)));
                    GenerateCodeBarre();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            foreach (Control c in Controls) c.Font = _myFont;

            var portNames = SerialPort.GetPortNames();
            foreach (var portName in portNames) comPortBox.Items.Add(portName);

            if (comPortBox.Items.Count > 0)
            {
                comPortBox.SelectedIndex = 0;
                _serialPort1 = new SerialPort();
                _readThread = new Thread(Read);
                //// Allow the user to set the appropriate properties.
                _serialPort1.PortName = comPortBox.Text;

                //// Set the read/write timeouts
                _serialPort1.ReadTimeout = 500;
                _serialPort1.WriteTimeout = 500;

                try
                {
                    _serialPort1.Open();
                    _readThread.Start();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString());
                }
            }

            _conn = new SqlConnection(
                $"Data Source={Environment.MachineName};Initial Catalog=pesage;Integrated Security=True");
            try
            {
                _conn.Open();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.xOffsetPrint = (int)xOffsetNumeric.Value;
            Settings.Default.yOffsetPrint = (int)yOffsetNumeric.Value;
            Settings.Default.Save();
        }

        public void Read()
        {
            var pattern = @"(\w*)?,?(\w*)?,?([+-]?([0-9]*[.])?[0-9]+)(\w*)";
            while (_isRunning)
                try
                {
                    var input = _serialPort1.ReadLine();
                    var x = Regex.Match(input, pattern);
                    SetText(weightLib, x);
                }
                catch (TimeoutException exception)
                {
                    Console.WriteLine(exception.Message);
                }
        }

        private void SetText(Control txtBox, Match m)
        {
            _weight.IsStable = m.Groups[1].Value == "ST";
            _weight.IsNet = m.Groups[2].Value == "NT";
            _weight.Weight = float.Parse(m.Groups[3].Value);
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

        private void clientLib_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (clientLib.Items.Count > 0 && clientLib.SelectedValue != null)
                    try
                    {
                        c_ServiceTableAdapter.FillBy(pesageDataSet.C_Service,
                            (int)Convert.ChangeType(clientLib.SelectedValue, typeof(int)));
                        serviceLib.SelectedIndex = -1;
                        if (pesageDataSet.C_Service.Count > 0)
                            serviceLib.SelectedIndex = 0;
                        //GenerateCodeBarre();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        // Log.Error("Something wrong happened in clientLib_SelectedIndexChanged", ex);    
                    }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Log.Error("Something wrong happened in clientLib_SelectedIndexChanged", ex);
            }
        }

        private void print_Click(object sender, EventArgs e)
        {
            var font = new ZplFont();
            var elements = new List<ZplElementBase>();
            int xoffset = Settings.Default.xOffsetPrint;
            int yoffset = Settings.Default.yOffsetPrint;
            var x = 110 + xoffset;
            var y = 30 + yoffset;
            elements.Add(new ZplTextField($@"Client : {clientLib.Text}"      , x , y * 1 + yoffset, font));
            elements.Add(new ZplTextField($@"Service : {serviceLib.Text}"    , x , y * 2 + yoffset, font));
            elements.Add(new ZplTextField($@"Residu : {residuLib.Text}"      , x , y * 3 + yoffset, font));
            elements.Add(new ZplTextField($@"Conteneur : {conteneurLib.Text}", x , y * 4 + yoffset, font));
            elements.Add(new ZplTextField($@"Poids : {weightLib.Text}"       , x , y * 5 + yoffset, font));
            elements.Add(new ZplTextField($@"Date : {DateTime.Now}"          , x , y * 6 + yoffset, font));
            elements.Add(new ZplBarcode128(codeBarreLib.Text                 , x + 48 , y * 8 + yoffset));
            var renderEngine = new ZplEngine(elements);
            var output = renderEngine.ToZplString(new ZplRenderOptions { AddEmptyLineBeforeElementStart = true });
            foreach (string printer in PrinterSettings.InstalledPrinters)
                if (printer.Contains("GK420t"))
                    RawPrinterHelper.SendStringToPrinter(printer, output);
            saveTicket_Click(sender, e);
        }

        private void serviceEdit_Click(object sender, EventArgs e)
        {
            _isCanceled = false;
            _table = "C_Service";
            _isInserting = false;
            groupBox1.Text = @"Modifier Services";
            c_ServiceTableAdapter.FillBy(pesageDataSet.C_Service, Convert.ToInt32(clientId.SelectedValue));
            dataGridView2.DataSource = pesageDataSet.C_Service;
        }

        private void conteneurEdit_Click(object sender, EventArgs e)
        {
            _isCanceled = false;
            groupBox1.Text = @"Modifier Conteneurs";
            _table = "Conteneur";
            _isInserting = false;
            conteneurTableAdapter.Fill(pesageDataSet.Conteneur);
            dataGridView2.DataSource = pesageDataSet.Conteneur;
        }

        private void residuEdit_Click(object sender, EventArgs e)
        {
            _isCanceled = false;
            groupBox1.Text = @"Modifier Residus";
            _table = "Residu";
            _isInserting = false;
            residuTableAdapter.Fill(pesageDataSet.Residu);
            dataGridView2.DataSource = pesageDataSet.Residu;
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
                                    adapter.Delete(row["id", DataRowVersion.Original], row["libelle", DataRowVersion.Original]);
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            MessageBox.Show(@"Ettiquette enregistrer", @"Succès", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            GenerateCodeBarre();
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
            int cl = Convert.ToInt32(clientId.SelectedValue);
            int se = Convert.ToInt32(serviceId.SelectedValue) == 0 ? 1 : Convert.ToInt32(serviceId.SelectedValue);
            int co = Convert.ToInt32(conteneurId.SelectedValue);
            int re = Convert.ToInt32(residuId.SelectedValue);
            int op = Convert.ToInt32(operateurId.SelectedValue);
            string text= $@"{cl:0}{se:00}{co:00}{re:00}{op:00}";
            etiquetteTableAdapter.likeCodeBarre($"%{text}%");
            SqlCommand cmd = new SqlCommand("select count(*) from etiquette where num_serie like @codeBarre", _conn);
            cmd.Parameters.Add("@codeBarre", SqlDbType.VarChar).Value = $"%{text}%";
            int count = 0;
            if (_conn.State == ConnectionState.Open)
                count = (Int32)cmd.ExecuteScalar();

            text = $@"{text}{count:00000}";
            codeBarreLib.Text = text;
        }

        private void codeBarre_TextChanged(object sender, EventArgs e)
        {
            
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

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.xOffsetPrint = (int)xOffsetNumeric.Value;
            Settings.Default.yOffsetPrint = (int)yOffsetNumeric.Value;
            Settings.Default.Save();
            if (_conn != null &&_conn.State == ConnectionState.Open)
                _conn.Close();
            if (_serialPort1 != null && _serialPort1.IsOpen)
                _serialPort1.Close();
            _isRunning = false;
            if(_readThread != null)
                _readThread.Abort();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "";
            dataGridView2.DataSource = new DataSet();
            dataGridView2.Refresh();
            _isCanceled = true;
        }
    }
}