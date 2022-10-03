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
using pesage.pesageDataSetTableAdapters;
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

        private readonly List<KeyValuePair<string, dynamic>> _pessageAdapters =
            new List<KeyValuePair<string, dynamic>>();

        private Thread _readThread;
        private SerialPort _serialPort1;
        private string _table;

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
            //setting the font to inter
            if (clientLib.SelectedValue != null)
                try
                {
                    c_ServiceTableAdapter.FillBy(pesageDataSet.C_Service,
                        (int)Convert.ChangeType(clientLib.SelectedValue, typeof(int)));
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
            _conn.Close();
            //_serialPort1.Close();
        }

        public void Read()
        {
            var pattern = @"(\w*)?,?(\w*)?,?([+-]?([0-9]*[.])?[0-9]+)(\w*)";
            while (true)
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
            var unit = m.Groups[5].Value;
            if (weightLib.InvokeRequired)
            {
                SetTextCallback d = SetText;
                Invoke(d, txtBox, m);
            }
            else
            {
                txtBox.ForeColor = _weight.IsStable ? Color.Green : Color.Red;
                unit = m.Groups[5].Value.ToUpper();
                tareLib.Text = $@"{_weight.Tare:F2} {unit}";
                txtBox.Text = $@"{_weight.Weight:F2} {unit}";
            }
        }

        private void clientLib_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clientLib.SelectedValue != null)
                try
                {
                    c_ServiceTableAdapter.FillBy(pesageDataSet.C_Service,
                        (int)Convert.ChangeType(clientLib.SelectedValue, typeof(int)));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            generateCodeBarre();
        }

        private void print_Click(object sender, EventArgs e)
        {
            var font = new ZplFont();
            var elements = new List<ZplElementBase>();
            var offset = 10;
            var x = 110;
            elements.Add(new ZplTextField($@"Client :{clientLib.Text}", x, 30 + offset, font));
            elements.Add(new ZplTextField($@"Service : {serviceLib.Text}", x, 30 * 2 + offset, font));
            elements.Add(new ZplTextField($@"Residu : {residuLib.Text}", x, 30 * 3 + offset, font));
            elements.Add(new ZplTextField($@"Conteneur : {conteneurLib.Text}", x, 30 * 4 + offset, font));
            elements.Add(new ZplTextField($@"Poids : {weightLib.Text}", x, 30 * 5 + offset, font));
            elements.Add(new ZplTextField(@"Date :", x, 30 * 6 + offset, font));
            elements.Add(new ZplTextField($@"{DateTime.Now}", x * 2, 30 * 6 + offset, font));
            elements.Add(new ZplBarcode128(codeBarreLib.Text, 130, 30 * 8 + offset, 100, 2));
            var renderEngine = new ZplEngine(elements);
            var output = renderEngine.ToZplString(new ZplRenderOptions { AddEmptyLineBeforeElementStart = true });
            foreach (string printer in PrinterSettings.InstalledPrinters)
                if (printer.Contains("GK420t"))
                    RawPrinterHelper.SendStringToPrinter(printer, output);
        }

        private void serviceEdit_Click(object sender, EventArgs e)
        {
            _table = "C_Service";
            _isInserting = false;
            groupBox1.Text = "Services";
            c_ServiceTableAdapter.FillBy(pesageDataSet.C_Service, Convert.ToInt32(clientId.SelectedValue));
            dataGridView2.DataSource = pesageDataSet.C_Service;
        }

        private void conteneurEdit_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "Conteneurs";
            _table = "Conteneur";
            _isInserting = false;
            conteneurTableAdapter.Fill(pesageDataSet.Conteneur);
            dataGridView2.DataSource = pesageDataSet.Conteneur;
        }

        private void residuEdit_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "Residus";
            _table = "Residu";
            _isInserting = false;
            residuTableAdapter.Fill(pesageDataSet.Residu);
            dataGridView2.DataSource = pesageDataSet.Residu;
        }

        private void serviceAdd_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "Services";
            _table = "C_Service";
            _isInserting = true;
            init_add_process();
        }

        private void conteneurAdd_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "Conteneurs";
            _table = "Conteneur";
            _isInserting = true;
            init_add_process();
        }


        private void residuAdd_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "Residus";
            _table = "Residu";
            _isInserting = true;
            init_add_process();
        }

        private void deleteRow_Click(object sender, EventArgs e)
        {
            var adapter = _pessageAdapters.Find(x => x.Key == _table).Value;

            foreach (DataGridViewRow row in dataGridView2.SelectedRows) dataGridView2.Rows.Remove(row);
        }

        private void addRow_Click(object sender, EventArgs e)
        {
            int last;
            if (_ds.Tables[_table].Rows.Count == 0)
                last = c_ServiceTableAdapter.GetData().AsEnumerable().Select(al => al.Field<int>("id")).Distinct()
                    .ToList().Max() + 1;
            else
                last = Convert.ToInt32(_ds.Tables[_table].Rows[_ds.Tables[_table].Rows.Count - 1][0]) + 1;

            _ds.Tables[_table].Rows.Add(last);
        }

        private void saveData_Click(object sender, EventArgs e)
        {
            try
            {
                var adapter = _pessageAdapters.Find(x => x.Key == _table).Value;
                if (_isInserting)
                {
                    foreach (DataRow row in _ds.Tables[_table].Rows)
                    { 
                        adapter.Insert(row["libelle"].ToString());
                        if (_table == "C_Service")
                        {
                            var max = c_ServiceTableAdapter.GetData().AsEnumerable().Select(al => al.Field<int>("id"))
                                .Distinct().ToList().Max();
                            client_serviceTableAdapter.Insert(Convert.ToInt32(clientId.SelectedValue), max);
                        }
                    }
                }
                else
                {
                    if (pesageDataSet.Tables[_table].GetChanges() != null)
                        foreach (DataRow row in pesageDataSet.Tables[_table].GetChanges().Rows)
                        {
                            if (row.RowState == DataRowState.Modified)
                                adapter.Update(row);
                            if (row.RowState == DataRowState.Deleted)
                                adapter.Delete(row["id", DataRowVersion.Original],
                                    row["libelle", DataRowVersion.Original]);
                        }
                }

                dataGridView2.DataSource = new DataSet();
                MessageBox.Show("Les données ont été enregistrées", "Succès", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void init_add_process()
        {
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
            MessageBox.Show("Ettiquette enregistrer", "Succès", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void serviceLib_SelectedIndexChanged(object sender, EventArgs e)
        {
            generateCodeBarre();
        }
        private void residuLib_SelectedIndexChanged(object sender, EventArgs e)
        {
            generateCodeBarre();
        }
        private void conteneurLib_SelectedIndexChanged(object sender, EventArgs e)
        {
            generateCodeBarre();
        }        
        private void operatorLib_SelectedIndexChanged(object sender, EventArgs e)
        {
            generateCodeBarre();
        }
        private void generateCodeBarre()
        {
            int cl = Convert.ToInt32(clientId.SelectedValue);
            int se = Convert.ToInt32(serviceId.SelectedValue);
            int co = Convert.ToInt32(conteneurId.SelectedValue);
            int re = Convert.ToInt32(residuId.SelectedValue);
            int op = Convert.ToInt32(operateurId.SelectedValue);
            string text= $@"{cl:0}{se:00}{co:00}{re:00}{op:00}";
            var result = etiquetteTableAdapter.likeCodeBarre($"%{text}%");
            int count = result.Count;
            text = $@"{text}{count:00000}";
            codeBarreLib.Text = text;
        }

    }
}