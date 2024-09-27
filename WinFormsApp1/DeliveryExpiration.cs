using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace WinFormsApp1
{
    public partial class DeliveryExpiration : Form
    {
        static string conString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=db_inventory.mdb";
        OleDbConnection con = new OleDbConnection(conString);
        OleDbCommand cmd;
        OleDbDataAdapter adapter;
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();

        public DeliveryExpiration()
        {
            InitializeComponent();
        }

        private void DeliveryExpiration_Load(object sender, EventArgs e)
        {
            //This is for the date and time: real time.
            timer1.Start();
            lblDate.Text = DateTime.Now.ToLongDateString();
            lblTime.Text = DateTime.Now.ToLongTimeString();

            //Load expiration dates table
            RetrieveExpirationDates(dtpCurrentDate.Text);

            //Load delivery dates table
            RetrieveDeliveryDates(dtpDelivery.Text);

            //Load list of Products
            RetrieveListOfProducts();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }
        
        //Filling up the Expiration Dates
        private void populateExpirationDates(String pname, String expd, String categ)
        {
            //filling up rows
            String[] row = { pname, expd, categ };
            ListViewItem item = new ListViewItem(row);

            lvExpirationDate.Items.Add(item);
        }
        private void RetrieveExpirationDates(string currentExpirationDate)
        {
            lvExpirationDate.Items.Clear();

            //sql codes
            string sql = "SELECT [productName], [expirationDate], [category] FROM tbl_inventory WHERE [expirationDate] = '" + currentExpirationDate + "'";
            cmd = new OleDbCommand(sql, con);

            try
            {
                con.Open();
                adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(dt);

                //Loop Through Database
                foreach (DataRow row in dt.Rows)
                {
                    populateExpirationDates(row[0].ToString(), row[1].ToString(), row[2].ToString());

                }
                con.Close();

                //clear DT
                dt.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Filling up the Delivery Dates
        private void populateDeliveryDates(String ProductName, String DeliveryTime)
        {
            //ROW
            String[] row = { ProductName, DeliveryTime };
            ListViewItem itemx = new ListViewItem(row);

            lvDeliveryDate.Items.Add(itemx);
        }
        private void RetrieveDeliveryDates(string CurrentDeliveryDate)
        {
            lvDeliveryDate.Items.Clear();

            //sqlcodes
            string sql = "SELECT [productName], [deliveryTime] FROM tbl_inventory WHERE [deliveryTime] = '" + CurrentDeliveryDate + "'";
            cmd = new OleDbCommand(sql, con);

            //Open, Retrieve, Fill DeliveryDate Table
            try
            {
                con.Open();
                adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(dt2);

                //Loop Through Database
                foreach (DataRow row in dt2.Rows)
                {
                    populateDeliveryDates(row[0].ToString(), row[1].ToString());
                }

                con.Close();

                //clear DT
                dt2.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Filling up the List of Products
        private void populateListOfProducts(String ProductName)
        {
            //ROW
            String[] row = { ProductName };
            ListViewItem item = new ListViewItem(row);

            lvSelectProducts.Items.Add(item);
        }
        private void RetrieveListOfProducts()
        {
            lvSelectProducts.Items.Clear();

            //sqlcodes
            string sql = "SELECT [productName] FROM tbl_inventory";
            cmd = new OleDbCommand(sql, con);

            //OPEN, RETRIEVE, FILL LISTVIEW
            try
            {
                con.Open();
                adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(dt3);

                //Loop Through Database
                foreach (DataRow row in dt3.Rows)
                {
                    populateListOfProducts(row[0].ToString());

                }

                con.Close();

                //clear DT
                dt3.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Search algorithm
        private void SearchAlgorithm(string SearchName)
        {
            lvSelectProducts.Items.Clear();

            //sqlcodes
            string sql = "SELECT [productName] FROM tbl_inventory WHERE [productName] LIKE '%" + SearchName + "%'";
            cmd = new OleDbCommand(sql, con);
            try
            {
                con.Open();
                adapter = new OleDbDataAdapter(sql, con);
                adapter.Fill(dt);

                //Loop Through Database
                foreach (DataRow row in dt.Rows)
                {
                    populateListOfProducts(row[0].ToString());

                }
                con.Close();
                dt.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //As you type, the list reload and sorts the products
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchAlgorithm(txtSearch.Text);
        }

        //Update Delivery Dates
        private void UpdateDeliveryDates(string CurrentDeliveryDate, string SelectedProduct, int stocks)
        {
            string cmdTxt = "UPDATE tbl_inventory SET [deliveryTime] = '" + CurrentDeliveryDate + "', [stocks] = stocks+'"+ stocks +"' WHERE [productName] = '" + SelectedProduct + "'";
            cmd = new OleDbCommand(cmdTxt, con);

            try
            {
                con.Open();
                adapter = new OleDbDataAdapter(cmd);
                adapter.UpdateCommand = con.CreateCommand();
                adapter.UpdateCommand.CommandText = cmdTxt;

                if (adapter.UpdateCommand.ExecuteNonQuery() > 0)
                {
                    txtSearch.Text = "";
                    dtpDelivery.Text = "";
                    MessageBox.Show("Successfuly Updated Stocks and Next Delivery Date!");
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnDeliveryUpdate_Click(object sender, EventArgs e)
        {

            if (txtStocks.Text == "")
            {
                string selectedProduct = lvSelectProducts.SelectedItems[0].SubItems[0].Text;
                string cmdTxt = "UPDATE tbl_inventory SET [deliveryTime] = '" + dtpDelivery.Text + "' WHERE [productName] = '" + selectedProduct + "'";
                cmd = new OleDbCommand(cmdTxt, con);

                try
                {
                    con.Open();
                    adapter = new OleDbDataAdapter(cmd);
                    adapter.UpdateCommand = con.CreateCommand();
                    adapter.UpdateCommand.CommandText = cmdTxt;

                    if (adapter.UpdateCommand.ExecuteNonQuery() > 0)
                    {
                        txtSearch.Text = "";
                        dtpDelivery.Text = "";
                        MessageBox.Show("Successfuly Updated Delivery Date!");
                    }
                    con.Close();
                    RetrieveDeliveryDates(dtpDeliveryDateTable.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                int x = Convert.ToInt32(txtStocks.Text);
                string selectedProduct = lvSelectProducts.SelectedItems[0].SubItems[0].Text;
                UpdateDeliveryDates(dtpDelivery.Text, selectedProduct, x);
                RetrieveDeliveryDates(dtpDeliveryDateTable.Text);

            }    

        }

        //When you change the expiration date, it updates the table
        private void dtpCurrentDate_ValueChanged(object sender, EventArgs e)
        {
            RetrieveExpirationDates(dtpCurrentDate.Text);
            txtSearch.Focus();
        }

        //When you change the delivery date, it updates the table
        private void dtpDelivery_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpDeliveryDateTable_ValueChanged(object sender, EventArgs e)
        {
            RetrieveDeliveryDates(dtpDeliveryDateTable.Text);
            txtSearch.Focus();
        }
    }
}
