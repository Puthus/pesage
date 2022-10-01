using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Windows;
using MessageBox = System.Windows.MessageBox;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace pesage
{
    public partial class MainWindow : Form
    {
        private SerialPort _serialPort1;
        private SerialPort _serialPort2;
        private const int BaudRate = 9600;
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font myFont;
        public MainWindow()
        {
            InitializeComponent();

            byte[] fontData = Properties.Resources.Inter_font;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.Inter_font.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.Inter_font.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            myFont = new Font(fonts.Families[0], 12);
        }
        
        private void MainWindow_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pesageDataSet.DataTable1' table. You can move, or remove it, as needed.
            this.dataTable1TableAdapter.Fill(this.pesageDataSet.DataTable1);
            // TODO: This line of code loads data into the 'pesageDataSet.Conteneur' table. You can move, or remove it, as needed.
            this.conteneurTableAdapter.Fill(this.pesageDataSet.Conteneur);
            // TODO: This line of code loads data into the 'pesageDataSet.client_service' table. You can move, or remove it, as needed.
            this.client_serviceTableAdapter.Fill(this.pesageDataSet.client_service);
            // TODO: This line of code loads data into the 'pesageDataSet.Residu' table. You can move, or remove it, as needed.
            this.residuTableAdapter.Fill(this.pesageDataSet.Residu);
            // TODO: This line of code loads data into the 'pesageDataSet.C_Service' table. You can move, or remove it, as needed.
            this.c_ServiceTableAdapter.Fill(this.pesageDataSet.C_Service);
            // TODO: This line of code loads data into the 'pesageDataSet.Client' table. You can move, or remove it, as needed.
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
                    c.Font = myFont;
            }

            //string[] portNames = SerialPort.GetPortNames();
            //foreach (var portName in portNames)
            //{
            //    comboBox1.Items.Add(portName);
            //    comboBox2.Items.Add(portName);
            //}
            //comboBox1.SelectedIndex = 0;
            //comboBox2.SelectedIndex = 0;
            //Thread readThread = new Thread(Read);
            //_serialPort = new SerialPort();

            //// Allow the user to set the appropriate properties.
            //_serialPort.PortName = "COM1";

            //// Set the read/write timeouts
            //_serialPort.ReadTimeout = 500;
            //_serialPort.WriteTimeout = 500;

            //try
            //{
            //    _serialPort.Open();
            //    readThread.Start();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}

        }

        //public void Read()
        //{
        //    while (true)
        //    {
        //        try
        //        {
        //            string message = _serialPort1.ReadLine();
        //            //label11.Text = message;
        //            //SetText(message.ToString());
        //        }
        //        catch (TimeoutException exception)
        //        {
        //            Console.WriteLine(exception.Message);
        //        }
        //    }
        //}

        delegate void SetTextCallback(TextBox txtBox,string text);

        private void SetText(TextBox txtBox,string text)
        {

            if (this.label11.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { txtBox,text });
            }
            else
            {
                txtBox.Text = text;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            _serialPort1 = new SerialPort(comboBox1.Text, BaudRate, Parity.None, 8, StopBits.One);
            _serialPort1.DataReceived += SerialPortOnDataReceived;
            _serialPort1.Open();
            textBox1.Text = "Listening on " + comboBox1.Text + "...";
        }

        private void SerialPortOnDataReceived(object sender, SerialDataReceivedEventArgs serialDataReceivedEventArgs)
        {
            while (_serialPort1.BytesToRead > 0)
            {
                SetText(textBox1, string.Format("{0:X2} ", _serialPort1.ReadExisting()));
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //_serialPort2 = new SerialPort(comboBox2.Text, BaudRate, Parity.None, 8, StopBits.One);
            //_serialPort2.Open();
            _serialPort1.Write(textBox2.Text);
            //_serialPort2.Close();
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

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }
    }
}
