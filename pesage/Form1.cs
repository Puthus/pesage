using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using BinaryKits.Zpl.Label;
using BinaryKits.Zpl.Label.Elements;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using pesage.Properties;
using Control = System.Windows.Forms.Control;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;

namespace pesage
{
    public partial class MainWindow : Form
    {
        private readonly PrivateFontCollection _fonts = new PrivateFontCollection();
        private readonly Font _myFont;
        private readonly Poids _weight;
        private DataSet _ds;
        private bool _isInserting;
        private bool _isCanceled;
        private readonly Dictionary<string, Dictionary<string,dynamic>> _pessageAdapters = new Dictionary<string,Dictionary<string,dynamic>>();
        private Thread _readThread;
        private SerialPort _serialPort1;
        private string _table;
        private bool _isRunning = true;
        private CodeBarre _codeBarre = new CodeBarre();
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
            _codeBarre.Label = codeBarreLib;
            _weight.Label = weightLib;
            _weight.TarLabel = tareLib;
        }

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //FONT
            foreach (Control c in Controls) c.Font = _myFont;
            foreach (string portName in SerialPort.GetPortNames()) comPortBox.Items.Add(portName);
            
            residuTableAdapter.Fill(pesageDataSet.Residu);
            clientTableAdapter.Fill(pesageDataSet.Client);
            c_ServiceTableAdapter.Fill(pesageDataSet.C_Service);
            operateurTableAdapter.Fill(pesageDataSet.Operateur);
            conteneurTableAdapter.Fill(pesageDataSet.Conteneur);
            client_serviceTableAdapter.Fill(pesageDataSet.client_service);

            _pessageAdapters.Add("Residu", new Dictionary<string, dynamic> { { "Adapter", residuTableAdapter }, { "Table", pesageDataSet.Residu } });
            _pessageAdapters.Add("Client", new Dictionary<string, dynamic> { { "Adapter", clientTableAdapter }, { "Table", pesageDataSet.Client } });
            _pessageAdapters.Add("C_Service", new Dictionary<string, dynamic> { { "Adapter", c_ServiceTableAdapter }, { "Table", pesageDataSet.C_Service } });
            _pessageAdapters.Add("Operateur", new Dictionary<string, dynamic> { { "Adapter", operateurTableAdapter }, { "Table", pesageDataSet.Operateur } });
            _pessageAdapters.Add("Conteneur", new Dictionary<string, dynamic> { { "Adapter", conteneurTableAdapter }, { "Table", pesageDataSet.Conteneur } });
            

            //_pessageAdapters.Add(new KeyValuePair<string, dynamic>("Conteneur", conteneurTableAdapter));
            //_pessageAdapters.Add(new KeyValuePair<string, dynamic>("Operateur", operateurTableAdapter));
            //_pessageAdapters.Add(new KeyValuePair<string, dynamic>("C_Service", c_ServiceTableAdapter));
            //_pessageAdapters.Add(new KeyValuePair<string, dynamic>("Client", clientTableAdapter));
            //_pessageAdapters.Add(new KeyValuePair<string, dynamic>("client_service", client_serviceTableAdapter));

            xOffsetNumeric.Value = Settings.Default.xOffsetPrint;
            yOffsetNumeric.Value = Settings.Default.yOffsetPrint;
            regexTxt.Text = Settings.Default.regex;
            
            _weight.Tare = conteneurLib.SelectedItem != null ? Convert.ToDouble(((DataRowView) conteneurLib.SelectedItem).Row["tare"]) : 0;
            
            _codeBarre.Client = Convert.ToInt32(((DataRowView)(clientId).SelectedItem).Row["id"]);
            _codeBarre.Service = Convert.ToInt32(((DataRowView)(serviceId).SelectedItem).Row["id"]);
            _codeBarre.Residu = Convert.ToInt32(((DataRowView)(residuId).SelectedItem).Row["id"]);
            _codeBarre.Conteneur = Convert.ToInt32(((DataRowView)(conteneurId).SelectedItem).Row["id"]);
            _codeBarre.Operateur = Convert.ToInt32(((DataRowView)(operateurId).SelectedItem).Row["id"]);

            if (comPortBox.Items.Count <= 0) return;

            comPortBox.SelectedIndex = 0;
            _readThread = new Thread(Read);
            _serialPort1 = new SerialPort(comPortBox.SelectedItem.ToString(), 9600, Parity.None, 8, StopBits.One);

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
                _weight.IsStable = m.Groups[1].Value == "ST";
                _weight.Weight = Convert.ToDouble(m.Groups[3].Value);
                _weight.Tare = Convert.ToDouble(((DataRowView)conteneurLib.SelectedItem).Row["tare"]);
            }
        }
        #endregion


        private void print_Click(object sender, EventArgs e)
        {
            if(!IsValidTicket())return;
            var font = new ZplFont();
            var elements = new List<ZplElementBase>();
            int xoffset = (int)xOffsetNumeric.Value;
            int yoffset = (int)yOffsetNumeric.Value;
            var x = xoffset;
            var y = 30;
            DateTime now = DateTime.Now;
            elements.Add(new ZplTextField($@"Client : {clientLib.Text}", x, y * 1 + yoffset, font));
            elements.Add(new ZplTextField($@"Service : {serviceLib.Text}", x, y * 2 + yoffset, font));
            elements.Add(new ZplTextField($@"Residu : {residuLib.Text}", x, y * 3 + yoffset, font));
            elements.Add(new ZplTextField($@"Conteneur : {conteneurLib.Text}", x, y * 4 + yoffset, font));
            elements.Add(new ZplTextField($@"Poids : {weightLib.Text}", x, y * 5 + yoffset, font));
            elements.Add(new ZplTextField($@"Date : {now}", x, y * 6 + yoffset, font));
            elements.Add(new ZplBarcode128(codeBarreLib.Text, x, y * 8 + yoffset));
            var renderEngine = new ZplEngine(elements);
            var output = renderEngine.ToZplString(new ZplRenderOptions { AddEmptyLineBeforeElementStart = true });
            foreach (string printer in PrinterSettings.InstalledPrinters)
                if (printer.Contains("GK420t"))
                    RawPrinterHelper.SendStringToPrinter(printer, output);
            try
            {
                etiquetteTableAdapter.Insert(
                    codeBarreLib.Text,
                    now,
                    (float)(_weight.Weight - _weight.Tare),
                    _codeBarre.Client,
                    _codeBarre.Service,
                    _codeBarre.Conteneur,
                    _codeBarre.Residu,
                    _codeBarre.Operateur
                );
                _codeBarre.Ticket++;
                MessageBox.Show(@"Ettiquette enregistrer", @"Succès", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
            dataGridView2.DataSource = pesageDataSet.C_Service;
        }

        private void conteneurEdit_Click(object sender, EventArgs e)
        {
            _isCanceled = false;
            groupBox1.Text = @"Modifier Conteneurs";
            _table = "Conteneur";
            _isInserting = false;
            dataGridView2.DataSource = pesageDataSet.Conteneur;
        }

        private void residuEdit_Click(object sender, EventArgs e)
        {
            _isCanceled = false;
            groupBox1.Text = @"Modifier Residus";
            _table = "Residu";
            _isInserting = false;
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
                var adapter = _pessageAdapters[_table]["Adapter"];
                if (_isInserting)
                {
                    foreach (DataRow row in _ds.Tables[_table].Rows)
                    {
                        if(_table != "Conteneur")
                            adapter.Insert(row["libelle"].ToString());
                        else
                            adapter.Insert(row["libelle"].ToString(),Convert.ToDouble(row["tare"]));
                        
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
                foreach (var table in _pessageAdapters.Values)
                {
                    table["Adapter"].Fill(table["Table"]);
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
            if(_table == "Conteneur")
                _ds.Tables[_table].Columns.Add(new DataColumn("tare", typeof(double)));
            var keys = new DataColumn[1];
            keys[0] = _ds.Tables[_table].Columns["id"];
            _ds.Tables[_table].PrimaryKey = keys;
            dataGridView2.DataSource = _ds.Tables[_table].DefaultView;
            dataGridView2.Columns["id"].ReadOnly = true;
            dataGridView2.Columns["libelle"].ReadOnly = false;
        }

        private void search_Click(object sender, EventArgs e)
        {
            if (!pesageDataSet.tickets.Columns.Contains("Ligne"))
                pesageDataSet.tickets.Columns.Add("Ligne", typeof(int));
            pesageDataSet.tickets.Columns["Ligne"].SetOrdinal(0);
            pesageDataSet.tickets.Columns["Code barre"].SetOrdinal(1);
            pesageDataSet.tickets.Columns["Date"].SetOrdinal(2);
            pesageDataSet.tickets.Columns["Service"].SetOrdinal(3);
            pesageDataSet.tickets.Columns["Conteneur"].SetOrdinal(4);
            pesageDataSet.tickets.Columns["Residu"].SetOrdinal(5);
            pesageDataSet.tickets.Columns["Operateur"].SetOrdinal(6);
            pesageDataSet.tickets.Columns["Poid"].SetOrdinal(7);
            if(dateDebut.Value.Date == dateFin.Value.Date)
                dateFin.Value = dateFin.Value.AddDays(1);
            ticketTableAdapter.FillByDate(pesageDataSet.tickets, dateDebut.Value.Date, dateFin.Value.Date);
            if (pesageDataSet.tickets.Columns.Contains("Client"))
                pesageDataSet.tickets.Columns.Remove("Client");
            dataGridView1.DataSource = pesageDataSet.tickets.DefaultView;
            //set the nbr column to the index of the row
            for (int i = 0; i < pesageDataSet.tickets.Rows.Count; i++)
            {
                pesageDataSet.tickets.Rows[i]["Ligne"] = i + 1;
            }
        }

        private void searchCodeBarre_Click(object sender, EventArgs e)
        {
            ticketTableAdapter.FillByCode(pesageDataSet.tickets, codeBarre.Text);
        }

        private delegate void SetTextCallback(Control txtBox, Match m);
        private void clientLib_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                _codeBarre.Client = Convert.ToInt32(((DataRowView)((ComboBox)sender).SelectedItem).Row["id"]);
                if(c_ServiceTableAdapter.FillBy(pesageDataSet.C_Service, Convert.ToInt32(((sender as ComboBox).SelectedItem as DataRowView).Row["id"])) > 0)
                    _codeBarre.Service = Convert.ToInt32(((DataRowView)serviceLib.SelectedItem).Row["id"]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Something wrong happened in clientLib_SelectionChangeCommitted outer try " + ex.Message);
            }
        }

        private void serviceLib_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _codeBarre.Service = Convert.ToInt32(((DataRowView)((ComboBox)sender).SelectedItem).Row["id"]);
        }

        private void conteneurLib_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _codeBarre.Conteneur = Convert.ToInt32(((DataRowView)((ComboBox)sender).SelectedItem).Row["id"]);
            _weight.Tare = Convert.ToDouble(((DataRowView)((ComboBox)sender).SelectedItem).Row["tare"]);
        }
        private void residuLib_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _codeBarre.Residu = Convert.ToInt32(((DataRowView)((ComboBox)sender).SelectedItem).Row["id"]);
            //GenerateCodeBarre(6,sender);
        }
        private void operatorLib_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _codeBarre.Operateur = Convert.ToInt32(((DataRowView)((ComboBox)sender).SelectedItem).Row["id"]);
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

        private void comPortBox_SelectionChangeCommitted(object sender, EventArgs e)
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
            Settings.Default.xOffsetPrint = (int)xOffsetNumeric.Value;
            Settings.Default.yOffsetPrint = (int)yOffsetNumeric.Value;
            Settings.Default.comPort = comPortBox.Text;
            Settings.Default.regex = regexTxt.Text;
            Settings.Default.Save();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _codeBarre.Client = Convert.ToInt32(((DataRowView)(clientId).SelectedItem).Row["id"]);
            _codeBarre.Service = Convert.ToInt32(((DataRowView)(serviceId).SelectedItem).Row["id"]);
            _codeBarre.Residu = Convert.ToInt32(((DataRowView)(residuId).SelectedItem).Row["id"]);
            _codeBarre.Conteneur = Convert.ToInt32(((DataRowView)(conteneurId).SelectedItem).Row["id"]);
            _codeBarre.Operateur = Convert.ToInt32(((DataRowView)(operateurId).SelectedItem).Row["id"]);
        }

        private void regexTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if key pressed is F12
            if(e.KeyChar == (char)Keys.F12)
                regexTxt.ReadOnly = !regexTxt.ReadOnly;
        }
        public void SaveStreamToFile(string fileFullPath, Stream stream)
        {
            if (stream.Length == 0) return;

            // Create a FileStream object to write a stream to a file
            using (FileStream fileStream = System.IO.File.Create(fileFullPath, (int)stream.Length))
            {
                // Fill the bytes[] array with the stream data
                byte[] bytesInStream = new byte[stream.Length];
                stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

                // Use FileStream object to write to the specified file
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
        }
        private void exportData_Click(object sender, EventArgs e)
        {
            
            String excelPath = Path.Combine(Path.Combine(Application.StartupPath, "Resources"), "daily export.xlsx");
            using (var workbook = new XLWorkbook(excelPath))
            {
                var worksheet = workbook.Worksheets.Worksheet(1);
                worksheet.Name = "Rapport";
                worksheet.PageSetup.FirstPageNumber = 1;
                worksheet.PageSetup.Header.Center.AddText($"RELEVER JOURNALIER DES {clientLib.Text}");
                worksheet.PageSetup.Header.Right.AddText(Environment.NewLine);
                worksheet.PageSetup.Header.Right.AddText(XLHFPredefinedText.Date);
                //export dataGridView1 to worksheet
                //worksheet.Cell( 1,4).Value = clientLib.SelectedValue.ToString();
                worksheet.Cell(1, 1).InsertTable(pesageDataSet.tickets as DataTable,"Tickets");
                worksheet.Tables.Table("Tickets").Theme = XLTableTheme.TableStyleLight1;
                worksheet.Tables.Table("tickets").ShowAutoFilter = false;

                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();
                //add the total weight to the worksheet in the last row
                int last_row = worksheet.LastRowUsed().RowNumber() + 1;
                worksheet.Cell(last_row, 1).Value = "Nbr : ";
                worksheet.Cell(last_row, 2).Value = pesageDataSet.tickets.Rows.Count;
                worksheet.Cell(last_row,7).Value = "Total ";
                worksheet.Cell(last_row, 8).Value = pesageDataSet.tickets.Compute("Sum(poid)", "");
                worksheet.Cell(last_row, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                worksheet.Cell(last_row, 7).Style.Fill.BackgroundColor = XLColor.LightGray;
                worksheet.Cell(last_row, 8).Style.Fill.BackgroundColor = XLColor.LightGray;
                worksheet.PageSetup.SetRowsToRepeatAtTop(1, 1);
                //prompt user to save file and open it
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = @"Excel Workbook (*.xlsx)|*.xlsx",
                    FileName = $"Rapport Pesage {dateDebut.Value.Date:yy-MM-dd}-{dateFin.Value.Date:yy-MM-dd}"
                };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //try to save it to the selected path else show error message to retry or cancel operation
                    try
                    {
                        workbook.SaveAs(saveFileDialog.FileName);
                        Process.Start(saveFileDialog.FileName);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(@"Error saving file " + '\n' + ex.Message);
                    }
                    
                }
            }
        }
        public DataTable SetColumnsOrder(DataTable table, params String[] columnNames)
        {
            int columnIndex = 0;
            foreach(var columnName in columnNames)
            {
                table.Columns[columnName].SetOrdinal(columnIndex);
                columnIndex++;
            }

            return table;
        }
        private void dateDebut_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker d = (DateTimePicker)sender;
            if(d.Value.Date >= dateFin.Value.Date)
                dateFin.Value = ((DateTimePicker)sender).Value.Date.AddDays(1);
        }

        private void dateFin_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker d = (DateTimePicker)sender;
            if(d.Value.Date <= dateDebut.Value.Date)
                dateDebut.Value = ((DateTimePicker)sender).Value.Date.AddDays(-1);

        }
    }
}