using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using MessageBox = System.Windows.MessageBox;
using System.Threading;
using System.Drawing.Text;
using System.IO.Ports;
using System.Text.RegularExpressions;
using BinaryKits.Zpl.Label;
using BinaryKits.Zpl.Label.Elements;

namespace pesage
{
    public partial class MainWindow : Form
    {
        private SerialPort _serialPort1;
        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();

        private readonly Font _myFont;

        private Thread readThread;
        public MainWindow()
        {
            InitializeComponent();

            byte[] fontData = Properties.Resources.Inter_font;
            IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
            Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.Inter_font.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.Inter_font.Length, IntPtr.Zero, ref dummy);
            Marshal.FreeCoTaskMem(fontPtr);

            _myFont = new Font(fonts.Families[0], 12);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.conteneurTableAdapter.Fill(this.pesageDataSet.Conteneur);
            this.client_serviceTableAdapter.Fill(this.pesageDataSet.client_service);
            this.residuTableAdapter.Fill(this.pesageDataSet.Residu);
            this.c_ServiceTableAdapter.Fill(this.pesageDataSet.C_Service);
            this.clientTableAdapter.Fill(this.pesageDataSet.Client);
            //setting the font to inter
            if (clientLib.SelectedValue != null)
            {
                try
                {
                    this.c_ServiceTableAdapter.FillBy(this.pesageDataSet.C_Service,
                        (int)System.Convert.ChangeType(clientLib.SelectedValue, typeof(int)));
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }

            foreach (Control c in this.Controls)
            {
                c.Font = _myFont;
            }

            string[] portNames = SerialPort.GetPortNames();
            foreach (var portName in portNames)
            {
                comboBox1.Items.Add(portName);
            }

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
                _serialPort1 = new SerialPort();
                readThread = new Thread(Read);
                //// Allow the user to set the appropriate properties.
                _serialPort1.PortName = comboBox1.Text;

                //// Set the read/write timeouts
                _serialPort1.ReadTimeout = 500;
                _serialPort1.WriteTimeout = 500;

                try
                {
                    _serialPort1.Open();
                    readThread.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public void Read()
        {
            string pattern = @"(\w*)?,?(\w*)?,?([+-]?([0-9]*[.])?[0-9]+)(\w*)";
            while (true)
            {
                try
                {
                    string input = _serialPort1.ReadLine();
                    Match x = Regex.Match(input, pattern);
                    SetText(label11, x);
                }
                catch (TimeoutException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        delegate void SetTextCallback(Control txtBox, Match m);

        private void SetText(Control txtBox, Match m)
        {
            String status = m.Groups[1].Value;
            String type = m.Groups[2].Value;
            float weight = float.Parse(m.Groups[3].Value);
            String unit = m.Groups[5].Value;
            if (this.label11.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { txtBox, m });
            }
            else
            {
                txtBox.ForeColor = status == "US" ? Color.Red : Color.Green;
                type = m.Groups[2].Value == "GS" ? "Poids brut" : "Poids net";
                weight = float.Parse(m.Groups[3].Value);
                unit = m.Groups[5].Value.ToUpper();

                txtBox.Text = $@"{type} {weight} {unit}";
            }
        }
        
        private void clientLib_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clientLib.SelectedValue != null)
            {
                try
                {
                    this.c_ServiceTableAdapter.FillBy(this.pesageDataSet.C_Service, (int)System.Convert.ChangeType(clientLib.SelectedValue, typeof(int)));
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox9.SelectedValue != null)
            {
                //SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Conteneur WHERE id_client = " + comboBox9.SelectedValue, Connection);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var zplData = @"";
            //RawPrinterHelper.SendStringToPrinter(, zplData);
            var font = new ZplFont(fontWidth: 30, fontHeight: 30);
            var elements = new List<ZplElementBase>();
            int offset = 10;
            int x = 110;
            elements.Add(new ZplTextField($@"Client :{clientLib.Text}", x, 30+offset, font));
            elements.Add(new ZplTextField($@"Service : {serviceLib.Text}", x, 30*2+ offset, font));
            elements.Add(new ZplTextField($@"Residu : {residuLib.Text}", x, 30*3+ offset, font));
            elements.Add(new ZplTextField($@"Conteneur : {conteneurLib.Text}", x, 30*4+offset, font));
            elements.Add(new ZplTextField($@"Poids : {label11.Text}", x, 30*5+ offset, font));
            elements.Add(new ZplTextField($@"Date :", x, 30*6+offset, font));
            elements.Add(new ZplTextField($@"{DateTime.Now}", x*2, 30*6+offset, font));
            elements.Add(new ZplBarcode128("123ABC", 180, 30 * 8 + offset, 100, 3));
            var renderEngine = new ZplEngine(elements);
            var output = renderEngine.ToZplString(new ZplRenderOptions { AddEmptyLineBeforeElementStart = true });
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                if (printer.Contains("GK420t"))
                {
                    RawPrinterHelper.SendStringToPrinter(printer, output);
                }
            }
        }


        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            readThread.Abort();
            _serialPort1.Close();
        }
    }
}
