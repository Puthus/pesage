using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using ExportJournalier.pesageExportTableAdapters;

namespace ExportJournalier
{
    public partial class Form1 : Form
    {
        ticketsTableAdapter _ticketsTableAdapter = new ticketsTableAdapter();
        C_ServiceTableAdapter c_serviceTA = new C_ServiceTableAdapter();
        EtiquetteTableAdapter ettiquetTA = new EtiquetteTableAdapter();
        ClientTableAdapter clientTA = new ClientTableAdapter();
        public Form1()
        {
            InitializeComponent();
            c_serviceTA.Fill(pesageExport.C_Service);
            ettiquetTA.Fill(pesageExport.Etiquette);
            clientTA.Fill(pesageExport.Client);
        }
        DataTable _tickets = new DataTable();
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
        private void searchButton_Click(object sender, EventArgs e)
        {
            //_ticketsTableAdapter.Fill(pesageExport.tickets,1, dateDebut.Value.Date, dateFin.Value.Date);
            //create a matrix of data with service as columns and date as rows
            _tickets = GenerateData();
            //bind datagridView1 to _tickets
            dataGridView1.DataSource = _tickets;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if(column.Name == "Date") continue;
                    column.DefaultCellStyle.Format = "0.00";
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            
            //String temp = dirInfo.Parent.Parent.FullName;
            //System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();
            String excelPath = Path.Combine(Path.Combine(Application.StartupPath, "Resources"), "Monthly export.xlsx");
            using (var workbook = new XLWorkbook(excelPath))
            {
                var worksheet = workbook.Worksheets.Worksheet(1);
                worksheet.Name = "Rapport";
                worksheet.PageSetup.FirstPageNumber = 1;
                worksheet.PageSetup.Header.Center.AddText($"RELEVER MENSUEL DU {pesageExport.Client.First().libelle}");
                worksheet.PageSetup.Header.Center.AddText(Environment.NewLine);
                worksheet.PageSetup.Header.Center.AddText(DateTime.Now.ToString());
                worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                worksheet.PageSetup.Header.Left.AddText(XLHFPredefinedText.Date);
                worksheet.PageSetup.Header.Right.AddText(XLHFPredefinedText.PageNumber);
                //export dataGridView1 to worksheet
                //worksheet.Cell( 1,4).Value = clientLib.SelectedValue.ToString();
                worksheet.Cell(1, 1).InsertTable(_tickets,"Tickets");
                worksheet.Tables.Table("Tickets").Theme = XLTableTheme.TableStyleLight1;
                worksheet.Tables.Table("tickets").ShowAutoFilter = false;
                //format numbers as 0.00
                worksheet.Tables.Table("tickets").Cells().Style.NumberFormat.Format = "0.00";
                worksheet.Column(1).Cells().Style.DateFormat.Format = "dd-MM-yy";
                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();
                //prompt user to save file and open it
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = @"Excel Workbook (*.xlsx)|*.xlsx",
                    FileName = $"Rapport {pesageExport.Client.First().libelle} {dateDebut.Value.Date:dd-MM-yy}-{dateFin.Value.Date:dd-MM-yy}"
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

        private DataTable GenerateData(){
            DataTable dt = new DataTable();
            DateTime firstDate = dateDebut.Value.Date;
            DateTime lastDate = dateFin.Value.Date;
            //for each C_Services as columns and dates as rows add the weight as value
            dt.Columns.Add("Date");
            foreach (var service in pesageExport.C_Service)
            {
                dt.Columns.Add(service.libelle, typeof(double));
            }
            dt.Columns.Add("Total",typeof(double));
            
            for (DateTime date = firstDate; date <= lastDate; date = date.AddDays(1))
            {
                DataRow row = dt.NewRow();
                row[0] = date.Date;
                foreach (var service in pesageExport.C_Service)
                {
                    row[service.libelle] = pesageExport.Etiquette.Where(t => t.e_date.Date == date.Date && t.service_id == service.id).Sum(t => t.poid);
                }
                row["Total"] = pesageExport.Etiquette.Where(t => t.e_date.Date == date).Sum(t => t.poid);
                dt.Rows.Add(row);
            }
            DataRow r = dt.NewRow();
            r["Date"] = "Total";
            foreach (var service in pesageExport.C_Service)
            {
                r[service.libelle] = pesageExport.Etiquette.Where(t => t.service_id == service.id && t.e_date.Date >= firstDate.Date && t.e_date.Date <= lastDate.Date).Sum(t => t.poid);
            }
            r["Total"] = pesageExport.Etiquette.Where(t=>t.e_date.Date >= firstDate.Date && t.e_date.Date <= lastDate.Date).Sum(t => t.poid);
            dt.Rows.Add(r);
            return dt;
        }
    }
}
