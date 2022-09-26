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

namespace pesage
{
    public partial class MainWindow : Form
    {
        private SerialPort _serialPort1;
        private SerialPort _serialPort2;
        private const int BaudRate = 9600;
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void MainWindow_Load(object sender, EventArgs e)
        {
            string[] portNames = SerialPort.GetPortNames();
            foreach (var portName in portNames)
            {
                comboBox1.Items.Add(portName);
                comboBox2.Items.Add(portName);
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
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

        public void Read()
        {
            while (true)
            {
                try
                {
                    string message = _serialPort1.ReadLine();
                    //label11.Text = message;
                    //SetText(message.ToString());
                }
                catch (TimeoutException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        delegate void SetTextCallback(TextBox txtBox,string text);

        private void SetText(TextBox txtBox,string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
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
                SetText(textBox1, string.Format("{0:X2} ", _serialPort1.ReadLine()));
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            _serialPort2 = new SerialPort(comboBox2.Text, BaudRate, Parity.None, 8, StopBits.One);
            _serialPort2.Open();
            _serialPort2.Write(textBox2.Text);
            _serialPort2.Close();
        }
    }
}
