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
using System.Windows.Controls;

namespace pesage
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=TITAN-LEGION;Initial Catalog=pesage;Integrated Security=True");
            SqlDataReader dr = null;
            SqlCommand myCommand = new SqlCommand("select * from Conteneur", con);
            //con.Open();


            //dr = myCommand.ExecuteReader();
            //add the items to the conteneurId combobox with id as the index and libelle as the value
            //while (dr.Read())
            //{

            //}
        }
    }
}