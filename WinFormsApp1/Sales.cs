using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Sales : Form
    {

        static string conString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=db_inventory.mdb";
        OleDbConnection con = new OleDbConnection(conString);
        OleDbCommand cmd;
        OleDbDataAdapter adapter;
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();

        public Sales()
        {
            InitializeComponent();

        }

        private void Sales_Load(object sender, EventArgs e)
        {
            retrieve();
        }

        private void retrieve()
        {
            //SQL
            string sql = "SELECT [ID], [employeeUsername], [totalSale], [customerName], [saleDate] FROM tbl_sales ORDER BY [ID] DESC ";
            cmd = new OleDbCommand(sql, con);

            try
            {
                con.Open();
                adapter = new OleDbDataAdapter(cmd);

                /*Loop Through Database
                foreach (DataRow row in dt.Rows)
                {
                    populate(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString());

                }
                */
                dt.Clear();
                adapter.Fill(dt);
                DGVSales.DataSource = dt;
                DGVSales.AllowUserToAddRows = false;

                con.Close();

                //clear DT
                //dt.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //SQL
            string sql = "SELECT [ID], [employeeUsername], [totalSale], [customerName], [saleDate] FROM tbl_sales WHERE saleDate BETWEEN '"+dtpFrom.Text+"' AND '"+dtpTo.Text+"' ORDER BY [ID] DESC";
            cmd = new OleDbCommand(sql, con);

            try
            {
                con.Open();
                adapter = new OleDbDataAdapter(cmd);

                dt2.Clear();
                adapter.Fill(dt2);
                DGVSales.DataSource = dt2;
                DGVSales.AllowUserToAddRows = false;

                con.Close();

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            dt2.Clear();
            retrieve();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 400, 600);
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string employee;
            string totalsale;
            string customername;
            string saledate;
            double x;
            double totalx = 0;
            int pos = 150;

            e.Graphics.DrawString("                         SALES REPORT                                           ", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(10, 80));
            e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, 110));
            e.Graphics.DrawString("Username              Total Sale               Customer Name              Date of Sale    ", new Font("Arial", 7, FontStyle.Bold), Brushes.Black, new Point(10, 120));
            e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, 130));

            foreach (DataGridViewRow row in DGVSales.Rows)
            {;
                x = Convert.ToDouble(row.Cells["totalSale"].Value);
                totalx = totalx + x;

                employee = "" + row.Cells["employeeUsername"].Value;
                totalsale = "" + row.Cells["totalSale"].Value;
                customername = "" + row.Cells["customerName"].Value;
                saledate = "" + row.Cells["saleDate"].Value;

                e.Graphics.DrawString("" + employee, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, pos));
                e.Graphics.DrawString("₱" + totalsale, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(100, pos));
                e.Graphics.DrawString("" + customername, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(160, pos));
                e.Graphics.DrawString("" + saledate, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(280, pos));
                pos += 20;

            }
            totalx = Math.Round(totalx, 2);
            e.Graphics.DrawString("TOTAL SALE: " , new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(100, pos + 20));
            e.Graphics.DrawString("₱" + totalx.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(200, pos + 20));
        }
    }
}
