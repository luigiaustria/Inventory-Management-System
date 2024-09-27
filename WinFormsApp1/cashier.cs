using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;


namespace WinFormsApp1
{
    public partial class cashier : Form
    {
        static string conString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=db_inventory.mdb";
        static string conString2 = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=db_users.mdb";
        OleDbConnection con = new OleDbConnection(conString);
        OleDbConnection con2 = new OleDbConnection(conString2);
        OleDbCommand cmd;
        OleDbCommand cmd2;
        OleDbDataAdapter adapter;
        OleDbDataAdapter adapter2;
        DataTable dt = new DataTable();
        OleDbDataReader reader;




        //variables for cashier
        double totalPrice;
        double grandTotal;
        double grandTotalDiscount;
        double discount = 0;

        public cashier(string value)
        {
            InitializeComponent();
            this.Value = value; //get the username
        }

        public string Value { get; set; }

        private void populate(String pname, String price, String stocks)
        {
            //ROW
            String[] row = {pname, price, stocks};
            ListViewItem item = new ListViewItem(row);

            listView1.Items.Add(item);
        }

        private void retrieveSearch(string pname)
        {
            listView1.Items.Clear();
            //SQL STMT
            string sql = "SELECT [productName], [price], [stocks] FROM tbl_inventory WHERE [productName] LIKE '%" + pname + "%'";
           
            cmd = new OleDbCommand(sql, con);

            try
            {
                con.Open();
                adapter = new OleDbDataAdapter(sql, con);
                adapter.Fill(dt);

                //Loop Through Database
                foreach (DataRow row in dt.Rows)
                {
                    populate(row[0].ToString(), row[1].ToString(), row[2].ToString());

                }
                con.Close();
                dt.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //retrieve
        private void retrieve()
        {
            listView1.Items.Clear();
            //SQL STMT
            string sql = "SELECT [productName], [price], [stocks] FROM tbl_inventory ";
            cmd = new OleDbCommand(sql, con);

            //OPEN, RETRIEVE, FILL LISTVIEW
            try
            {
                con.Open();
                adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(dt);

                //Loop Through Database
                foreach (DataRow row in dt.Rows)
                {
                    populate(row[0].ToString(), row[1].ToString(), row[2].ToString());

                }

                con.Close();

                //clear DT
                dt.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }
        private void returnStocks(int stocks, String pname)
        {
            //SQL STMT
            string cmdTxt = "UPDATE tbl_inventory SET [stocks] = " + stocks + " WHERE [productName] = '" + pname + "'";
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
        }
        
        private void clearTxts()
        {
            txtReturnName.Text = "";
            txtReturnQty.Text = "";
            txtContainer.Text = "";
            txtContainer2.Text = "";

            txtPName.Text = "";
            txtPrice.Text = "";
            txtQty.Text = "";
            txtTotal.Text = "";

            txtSearch.Text = "";
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void cashier_Load(object sender, EventArgs e)
        {
            retrieve();
            lblCont.Text = Value;
            txtSearch.Focus();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            retrieveSearch(txtSearch.Text);
        }
         
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            txtPName.Text = listView1.SelectedItems[0].SubItems[0].Text;
            txtPrice.Text = listView1.SelectedItems[0].SubItems[1].Text;
            txtReturnName.Text = listView1.SelectedItems[0].SubItems[0].Text;
            txtContainer.Text = listView1.SelectedItems[0].SubItems[2].Text;
            txtContainer2.Text = listView1.SelectedItems[0].SubItems[2].Text;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtReturnName.Text == "" || txtReturnQty.Text == "")
                {
                    MessageBox.Show("Please fill up all empty fields.", "Update Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    String selected = listView1.SelectedItems[0].SubItems[0].Text;
                    int qty = Convert.ToInt32(txtReturnQty.Text);
                    int x = Convert.ToInt32(txtContainer.Text);
                    int totalqty = qty + x;
                    returnStocks(totalqty, selected);
                }
            }
            catch
            {
                MessageBox.Show("Select the data that you want to edit.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You clear your cart. Order Again.");
            clearTxts();
            txtGT.Text = "";
            BillDGV.Rows.Clear();
            grandTotal = 0;
            grandTotalDiscount = 0;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            if (txtQty.Text == "" || txtPName.Text == "" || txtPrice.Text == "")
            {
                MessageBox.Show("Please select a product first.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                /*scanning if it has same name
                if(BillDGV.Rows.Count >=3)
                {
                    for (int item = 0; item <= BillDGV.Rows.Count - 1; item++)
                    {
                        if (txtPName.Text == BillDGV.Rows[item].Cells[1].Value.ToString())
                        {
                            MessageBox.Show("The pruduct already exist");
                        }
                        MessageBox.Show(BillDGV.Rows.Count.ToString() + " ");
                    }
                }
                */

                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                /*
                BillDGV.Columns.Add("qty", "Quantity");
                BillDGV.Columns.Add("pname", "Product Name");
                BillDGV.Columns.Add("price", "Price");
                BillDGV.Columns.Add("total", "Total");
                */
                newRow.Cells[0].Value = txtQty.Text;
                newRow.Cells[1].Value = txtPName.Text;
                newRow.Cells[2].Value = txtPrice.Text;
                newRow.Cells[3].Value = txtTotal.Text;
                BillDGV.Rows.Add(newRow);

                //GrandTotal Codes and Discounts
                double container = Convert.ToDouble(txtTotal.Text);
                grandTotal += container;
                grandTotal = Math.Round(grandTotal, 2);

                txtGT.Text = "₱" + grandTotal.ToString();

                //sql decreasing stocks code
                String selected = listView1.SelectedItems[0].SubItems[0].Text;
                int qty = Convert.ToInt32(txtQty.Text);
                int x = Convert.ToInt32(txtContainer2.Text);
                int totalqty = x - qty;
                decreaseStocks(totalqty, selected);
                if(totalqty < 20)
                {
                    MessageBox.Show(selected + "is only " + totalqty + " in stocks, please restock soon.");
                }


                clearTxts();
                txtSearch.Focus();

                //DGV Button Properties
                btnColumn.Text = "Remove Item";
                btnColumn.UseColumnTextForButtonValue = true;
                
            }
        }
        private void totalText()
        {
            double p = Convert.ToDouble(txtPrice.Text);
            int x = Convert.ToInt32(txtQty.Text);


            totalPrice = p * x;
            totalPrice = Math.Round(totalPrice, 2);
            txtTotal.Text = totalPrice.ToString();
        }
        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            if (txtPrice.Text == "")
            {
                  totalPrice = 1;

            }
            else if (txtQty.Text == "")
            {
                MessageBox.Show("This field cannot be empty.");
            }
            else
            {
                  totalText();
            }



        }


        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string qty;
            string product;
            string price;
            string total;
            int pos = 150;





            //discounts
            if (cbDiscount.Checked)
            {
                //the entire receipt
                e.Graphics.DrawString("   SHOP RECEIPT", new Font("Arial", 20, FontStyle.Regular), Brushes.Black, new Point(11, 10));
                e.Graphics.DrawString("       Welcome to 5R's Grocery Store.", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(10, 40));
                e.Graphics.DrawString("0188 Sitio Sumilang Bagong Silang Plaridel Bulacan.", new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(40, 60));
                e.Graphics.DrawString("            Tel:0917-104-0505", new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(80, 78));
                e.Graphics.DrawString(DateTime.Now.ToString("MMMM dd, yyyy"), new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(109, 90));
                e.Graphics.DrawString("-----------------------------------------------------------------------", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, 120));
                e.Graphics.DrawString("Qty          Product Name                    Price             Total  ", new Font("Arial", 7, FontStyle.Bold), Brushes.Black, new Point(10, 130));
                e.Graphics.DrawString("-----------------------------------------------------------------------", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, 140));

                //foreach for datagridview data
                foreach (DataGridViewRow row in BillDGV.Rows)
                {
                    qty = "" + row.Cells["qty"].Value;
                    product = "" + row.Cells["pname"].Value;
                    price = "" + row.Cells["price"].Value;
                    total = "" + row.Cells["total"].Value;

                    e.Graphics.DrawString("" + qty, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, pos));
                    e.Graphics.DrawString("" + product, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(50, pos));
                    e.Graphics.DrawString("" + price, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(180, pos));
                    e.Graphics.DrawString("" + total, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(230, pos));
                    pos += 20;
                }

                //datetime
                DateTime dateTime2 = DateTime.Now;
                string currentdate2 = dateTime2.ToString("MMMM dd, yyyy HH:mm");

                //next part of the receipt
                e.Graphics.DrawString("-----------------------------------------------------------------------", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, pos));
                e.Graphics.DrawString("TOTAL", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(10, pos + 20));
                e.Graphics.DrawString("₱" + grandTotal.ToString(), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(230, pos + 20));
                e.Graphics.DrawString("-----------------------------------------------------------------------", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, pos + 40));
                e.Graphics.DrawString("Discount", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, pos + 60));
                e.Graphics.DrawString("₱" + discount.ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(230, pos + 60));
                e.Graphics.DrawString("TOTAL", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(10, pos + 80));
                e.Graphics.DrawString("₱" + grandTotalDiscount.ToString(), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(230, pos + 80));
                e.Graphics.DrawString("Cash", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, pos + 100));
                e.Graphics.DrawString("₱" + txtCash.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(230, pos + 100));
                e.Graphics.DrawString("-----------------------------------------------------------------------", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, pos + 120));
                e.Graphics.DrawString("Change", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(10, pos + 140));
                e.Graphics.DrawString(txtChange.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(230, pos + 140));
                e.Graphics.DrawString("-----------------------------------------------------------------------", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, pos + 160));
                e.Graphics.DrawString("Printed by: " + label10.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, pos + 175));
                e.Graphics.DrawString("Date and time: " + currentdate2, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, pos + 185));
                e.Graphics.DrawString("Thank you!", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(103, pos + 220));
                e.Graphics.DrawString("Have a Nice Day!", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(90, pos + 230));


            }
            else
            {
                //print grandtotal;
                //the entire receipt
                e.Graphics.DrawString("   SHOP RECEIPT", new Font("Arial", 20, FontStyle.Regular), Brushes.Black, new Point(11, 10));
                e.Graphics.DrawString("       Welcome to 5R's Grocery Store.", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(10, 40));
                e.Graphics.DrawString("0188 Sitio Sumilang Bagong Silang Plaridel Bulacan.", new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(40, 60));
                e.Graphics.DrawString("            Tel:0917-104-0505", new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(80, 78));
                e.Graphics.DrawString(DateTime.Now.ToString("MMMM dd, yyyy"), new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(109, 90));
                e.Graphics.DrawString("-----------------------------------------------------------------------", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, 120));
                e.Graphics.DrawString("Qty          Product Name                    Price             Total  ", new Font("Arial", 7, FontStyle.Bold), Brushes.Black, new Point(10, 130));
                e.Graphics.DrawString("-----------------------------------------------------------------------", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, 140));

                //foreach for datagridview data
                foreach (DataGridViewRow row in BillDGV.Rows)
                {
                    qty = "" + row.Cells["qty"].Value;
                    product = "" + row.Cells["pname"].Value;
                    price = "" + row.Cells["price"].Value;
                    total = "" + row.Cells["total"].Value;

                    e.Graphics.DrawString("" + qty, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, pos));
                    e.Graphics.DrawString("" + product, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(50, pos));
                    e.Graphics.DrawString("" + price, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(180, pos));
                    e.Graphics.DrawString("" + total, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(230, pos));
                    pos += 20;
                }

                //datetime
                DateTime dateTime2 = DateTime.Now;
                string currentdate2 = dateTime2.ToString("MMMM dd, yyyy HH:mm");
                //next part of the receipt
                e.Graphics.DrawString("-----------------------------------------------------------------------", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, pos));
                e.Graphics.DrawString("TOTAL", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(10, pos + 20));
                e.Graphics.DrawString(txtGT.Text, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(230, pos + 20));
                e.Graphics.DrawString("-----------------------------------------------------------------------", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, pos + 40));
                e.Graphics.DrawString("Cash", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, pos + 60));
                e.Graphics.DrawString("₱" + txtCash.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(230, pos + 60));
                e.Graphics.DrawString("-----------------------------------------------------------------------", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, pos + 80));
                e.Graphics.DrawString("Change", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(10, pos + 100));
                e.Graphics.DrawString(txtChange.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(230, pos + 100));
                e.Graphics.DrawString("-----------------------------------------------------------------------", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, pos + 120));
                e.Graphics.DrawString("Printed by: " + label10.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, pos + 135));
                e.Graphics.DrawString("Date and time: " + currentdate2, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, pos + 145));
                e.Graphics.DrawString("Thank you!", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(103, pos + 170));
                e.Graphics.DrawString("Have a Nice Day!", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(90, pos + 180));

            }

        }

        private void decreaseStocks(int stocks, string pname)
        {
            //SQL STMT
            string cmdTxt = "UPDATE tbl_inventory SET [stocks] = " + stocks + " WHERE [productName] = '" + pname + "'";
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
                }
                con.Close();
                retrieve();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (tbCustomer.Text == "")
            {
                MessageBox.Show("Please put the customer's name for the sales report.");
            }
            else if (txtGT.Text == "")
            {
                MessageBox.Show("Please Add your grocery.");
            }
            else if (txtCash.Text == "")
            {
                MessageBox.Show("Please Input the amount of cash.");
            }
            else if (txtChange.Text == "")
            {
                MessageBox.Show("Click the Change button to know how much is your change.");
            }
            else
            {
                //Get the full name of the user
                cmd2 = new OleDbCommand("SELECT [fullName] FROM tbl_users WHERE userName=@username", con2);
                cmd2.Parameters.AddWithValue("@username", lblCont.Text);

                con2.Open();
                reader = cmd2.ExecuteReader();
                if(label10.Text == "label10")
                {
                    if (reader.Read())
                    {
                        label10.Text = reader.GetValue(0).ToString();
                    }
                    con2.Close();
                }

                //print the receipt
                printPreviewDialog1.Document = printDocument1;
                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
                printPreviewDialog1.ShowDialog();
                MessageBox.Show("Sale Successful!");

                //current date
                DateTime dateTime = DateTime.UtcNow.Date;
                string currentdate = dateTime.ToString("MMMM/dd/yyyy");

                //input the sales to database
                string sql = "INSERT INTO tbl_sales([employeeUsername], [totalSale], [customerName], [saleDate]) VALUES (@euser, @tsale, @cname, @sdate)";
                cmd = new OleDbCommand(sql, con);

                //ADD PARAMS
                cmd.Parameters.AddWithValue("@euser", lblCont.Text);
                if(cbDiscount.Checked)
                {
                    cmd.Parameters.AddWithValue("@tsale", grandTotalDiscount);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@tsale", grandTotal);
                }
                cmd.Parameters.AddWithValue("@cname", tbCustomer.Text);
                cmd.Parameters.AddWithValue("@sdate", currentdate);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                retrieve();
                BillDGV.Rows.Clear();
                txtGT.Text = "";
                txtCash.Text = "";
                txtChange.Text = "";
                tbCustomer.Text = "";
                grandTotal = 0;
                grandTotalDiscount = 0;
            }

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(txtCash.Text == "")
            {
                MessageBox.Show("Please Input the amount of cash.");
            }
            else
            {
                double cashcheck = Convert.ToDouble(txtCash.Text);
                double change;


                if (grandTotal > cashcheck)
                {
                    MessageBox.Show("Please enter your cash amount.");
                }
                else
                {
                    if(cbDiscount.Checked)
                    {
                        change = cashcheck - grandTotalDiscount;
                        change = Math.Round(change, 2);
                        txtChange.Text = "₱" + change.ToString();
                    }
                    else
                    {
                        change = cashcheck - grandTotal;
                        change = Math.Round(change, 2);
                        txtChange.Text = "₱" + change.ToString();
                    }


                }
            }

        }

        double cashcheck;
        double change;
        private void cbDiscount_CheckedChanged(object sender, EventArgs e)
        {
            if(txtCash.Text == "")
            {
                MessageBox.Show("Please enter the cash amount first.");
                cbDiscount.Checked = false;
            }


            if(cbDiscount.Checked)
            {
                discount = grandTotal * .20;
                discount = Math.Round(discount, 2);
                grandTotalDiscount = grandTotal - discount;
                grandTotalDiscount = Math.Round(grandTotalDiscount, 2);
                txtGT.Text = "₱" + grandTotalDiscount.ToString();
            }
            else
            {
                grandTotal = Math.Round(grandTotal, 2);
                txtGT.Text = "₱" + grandTotal.ToString();
                discount = 0;
            }


            if (cbDiscount.Checked)
            {
                cashcheck = Convert.ToDouble(txtCash.Text);
                change = cashcheck - grandTotalDiscount;
                change = Math.Round(change, 2);
                txtChange.Text = "₱" + change.ToString();
            }
            else
            {
                change = cashcheck - grandTotal;
                change = Math.Round(change, 2);
                txtChange.Text = "₱" + change.ToString();
            }
        }

        private void BillDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==4)
            {
                int rowIndex = BillDGV.CurrentCell.RowIndex;
                DataGridViewRow row = BillDGV.Rows[rowIndex];

                string cont = row.Cells[3].Value.ToString();

                double x = Convert.ToDouble(cont);

                if(cbDiscount.Checked)
                {
                    double product = grandTotalDiscount - x;
                    txtGT.Text = "₱" + product.ToString();
                    grandTotalDiscount = product;
                    change = cashcheck - grandTotalDiscount;
                    change = Math.Round(change, 2);
                    txtChange.Text = "₱" + change.ToString();

                }
                else
                {
                    double product = grandTotal - x;
                    txtGT.Text = "₱" + product.ToString();
                    grandTotal = product;
                    change = cashcheck - grandTotal;
                    change = Math.Round(change, 2);
                    txtChange.Text = "₱" + change.ToString();
                }

                if (BillDGV.SelectedCells.Count > 0)
                {
                    int selectedrowindex = BillDGV.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = BillDGV.Rows[selectedrowindex];
                    string contofquantity = Convert.ToString(selectedRow.Cells["qty"].Value);
                    string contofproductname = Convert.ToString(selectedRow.Cells["pname"].Value);

                    int stocksconvert = Convert.ToInt32(contofquantity);


                    cmd = new OleDbCommand("UPDATE tbl_inventory SET [stocks]=stocks+ '"+stocksconvert+"'WHERE productName=@pName", con);
                    cmd.Parameters.AddWithValue("@pName", contofproductname);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    retrieve();


                }

                BillDGV.Rows.RemoveAt(rowIndex);
                MessageBox.Show("Item Removed.");



                /*cellitem
                give stocks again after removing the item
                string cont2 = row.Cells[0].Value.ToString();

                string selectedx = row.Cells[1].Value.ToString();//product name value
          
                int stocksx = Convert.ToInt32(tbStocksContainer.Text);
                int totalqtyx = qty + stocksx;
                returnStocks(totalqtyx, selectedx);

                int rowIndexx = BillDGV.CurrentCell.RowIndex;
                DataGridViewRow rows = BillDGV.Rows[rowIndexx];

                string conts = rows.Cells[1].Value.ToString();
                tbDGVCont.Text = conts;

                string sql = "SELECT [stocks] FROM tbl_inventory WHERE [productName] = '" + tbDGVCont.Text + "'";
                con.Open();
                cmd2 = new OleDbCommand(sql, con);
                reader = cmd2.ExecuteReader();

                dt2.Load(reader);
                if (dt2.Rows.Count > 0)
                {
                    tbStocksContainer.Text = Convert.ToString(dt.Rows[0]["stocks"]);
                }
                con.Close();

                */
            }
    }

        private void BillDGV_MouseClick(object sender, MouseEventArgs e)
        {



        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
