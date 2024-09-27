using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net;
using System.Windows.Controls;
using System.Data.SqlClient;

namespace WinFormsApp1
{
    public partial class inventory : Form
    {
        static string conString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=db_inventory.mdb";
        OleDbConnection con = new OleDbConnection(conString);
        OleDbCommand cmd;
        OleDbDataAdapter adapter;
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();

        public inventory()
        {
            InitializeComponent();
        }

        private void retrieveSearch(string pname)
        {
            string sql = "SELECT [ProductID], [productName], [price], [stocks], [expirationDate], [category], [brand] FROM tbl_inventory WHERE [productName] LIKE '%" + pname + "%'";
            cmd = new OleDbCommand(sql, con);

            adapter.SelectCommand = cmd;

            dt2.Clear();
            adapter.Fill(dt2);
            DGVInventory2.DataSource = dt2;
            DGVInventory2.AllowUserToAddRows = false;

            /*
            listView1.Items.Clear();
            //SQL STMT
            string sql = "SELECT [ProductID], [productName], [price], [stocks], [expirationDate], [category] FROM tbl_inventory WHERE [productName] LIKE '%" + pname + "%'";
            cmd = new OleDbCommand(sql, con);

            try
            {
                con.Open();
                adapter = new OleDbDataAdapter(sql,con);
                adapter.Fill(dt);

                //Loop Through Database
                foreach (DataRow row in dt.Rows)
                {
                    populate(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString());

                }
                con.Close();
                dt.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */
        }
        //retrieve
        private void retrieveInventory()
        {
            //SQL STMT
            string sql = "SELECT [ProductID], [productName], [price], [stocks], [expirationDate], [category], [brand] FROM tbl_inventory ";
            cmd = new OleDbCommand(sql, con);

            //OPEN, RETRIEVE, FILL LISTVIEW
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
                DGVInventory2.DataSource = dt;
                DGVInventory2.AllowUserToAddRows = false;

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

        //Update database
        private void update(double id, double stocks, double price, String expd)
        {


            /*
            string cmdTxt = "UPDATE tbl_inventory SET [stocks] = " + stocks + ", [price] = " + price + ", [expirationDate] = '" + expd + "' WHERE [ProductID] = " + id + "";
            cmd = new OleDbCommand(cmdTxt, con);

            try
            {
                con.Open();
                adapter = new OleDbDataAdapter(cmd);
                adapter.UpdateCommand = con.CreateCommand();
                adapter.UpdateCommand.CommandText = cmdTxt;

                if (adapter.UpdateCommand.ExecuteNonQuery() > 0)
                {
                    clearTxts();
                    MessageBox.Show("Successfuly Updated!");
                }
                con.Close();
                retrieve();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
            */
        }

        private void delete(int id)
        {
            //SQL STMT
            string cmdTxt = "DELETE FROM tbl_inventory WHERE ProductID = " + id + "";
            cmd = new OleDbCommand(cmdTxt, con);
            

            //'OPEN CON, EXECUTE, DELETE, CLOSE CON
            try
            {
                con.Open();
                adapter = new OleDbDataAdapter(cmd);
                adapter.DeleteCommand = con.CreateCommand();
                adapter.DeleteCommand.CommandText = cmdTxt;

                //Prompt for Confirmation
                if (MessageBox.Show("Are you sure you want to delete the account?", "DELETE", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        con.Close();
                        retrieveInventory();
                        MessageBox.Show("Successfuly Deleted!");
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        //clear texts

         private void inventory_Load(object sender, EventArgs e)
        {
            dt2.Clear();
            retrieveInventory();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {


        }
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            retrieveInventory();
        }

        private void btnClear2_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            dt.Clear();
            retrieveSearch(txtSearch.Text);
            txtSearch.Text = "";
        }

        private void txtSearch_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                iconButton1_Click(this, new EventArgs());
            }
        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnUpdate_Click(this, new EventArgs());
            }
        }

        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnUpdate_Click(this, new EventArgs());
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void updateToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            if (DGVInventory2.SelectedCells.Count > 0)
            {
                int selectedrowindex = DGVInventory2.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = DGVInventory2.Rows[selectedrowindex];
                string contofproductname = Convert.ToString(selectedRow.Cells["productName"].Value);

                EditInventory envontory = new EditInventory(contofproductname);
                envontory.Show();
            }
            else
            {
                MessageBox.Show("Select a product to edit.");
            }


            /*
            try
            {
                if (txtQuantity.Text == "" || txtPrice.Text == "" || dateTimePicker1.Text == "")
                {
                    MessageBox.Show("Please fill up all empty fields.", "Update Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    String selected = listView1.SelectedItems[0].SubItems[0].Text;
                    double id = Convert.ToDouble(selected);
                    double x = Convert.ToDouble(txtQuantity.Text);
                    double y = Convert.ToDouble(txtPrice.Text);

                    update(id, x, y, dateTimePicker1.Text);
                }
            }
            catch
            {
                MessageBox.Show("Select the data that you want to edit.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int selected_id = Convert.ToInt32(DGVInventory2.CurrentRow.Cells[0].Value);

            delete(selected_id);
        }

        private void btnNewProduct_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 400, 600);
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string pname;
            string price;
            string stocks;
            string categ;
            int pos = 80;

            e.Graphics.DrawString("                         INVENTORY LIST                                           ", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(10, 20));
            e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, 50));
            e.Graphics.DrawString("        Product Name                          Price               Stocks               Category    ", new Font("Arial", 7, FontStyle.Bold), Brushes.Black, new Point(10, 60));
            e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, 70));

            foreach (DataGridViewRow row in DGVInventory2.Rows)
            {


                pname = "" + row.Cells["productName"].Value;
                price = "" + row.Cells["price"].Value;
                stocks = "" + row.Cells["stocks"].Value;
                categ = "" + row.Cells["category"].Value;

                e.Graphics.DrawString("" + pname, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, pos));
                e.Graphics.DrawString("₱" + price, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(170, pos));
                e.Graphics.DrawString("" + stocks, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(240, pos));
                e.Graphics.DrawString("" + categ, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(310, pos));
                pos += 20;

            }

            DateTime dateTime = DateTime.UtcNow.Date;

            string date = dateTime.ToString("MMMM / dd / yyyy");

            e.Graphics.DrawString("DATE: ", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(100, pos + 20));
            e.Graphics.DrawString("" + date, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(180, pos + 20));
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            newProduct frm = new newProduct();
            frm.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void DGVInventory2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DGVInventory2_SelectionChanged(object sender, EventArgs e)
        {

        }

        public void refresh()
        {
            retrieveInventory();
        }

        private void inventory_Activated(object sender, EventArgs e)
        {

        }
    }

}
