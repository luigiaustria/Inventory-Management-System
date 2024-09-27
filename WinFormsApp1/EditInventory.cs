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

namespace WinFormsApp1
{
    public partial class EditInventory : Form
    {
        static string conString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=db_inventory.mdb";
        OleDbConnection con = new OleDbConnection(conString);
        OleDbCommand cmd;
        OleDbDataAdapter adapter;
        DataTable dt = new DataTable();
        OleDbDataReader dr;
        bool value2;

        public EditInventory(string value)
        {
            InitializeComponent();
            this.Value = value; //get the username
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string Value { get; set; }

        private void EditInventory_Load(object sender, EventArgs e)
        {
            lblProductCont.Text = Value;
            retrieve();
        }

        private void retrieve()
        {
            cmd = new OleDbCommand("SELECT productName, price, stocks, expirationDate, category, brand FROM tbl_inventory WHERE productName=@pname", con);
            cmd.Parameters.AddWithValue("@pname", lblProductCont.Text);

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string pname = dr["productName"].ToString();
                string price = dr["price"].ToString();
                string stocks = dr["stocks"].ToString();
                string expdate = dr["expirationDate"].ToString();
                string categ = dr["category"].ToString();
                string brand = dr["brand"].ToString();

                txtProductName.Text = pname;
                txtPrice.Text = price;
                txtStocks.Text = stocks;
                dtpExpirationDate.Text = expdate;
                cbCateg.Text = categ;
                txtBrand.Text = brand;
            }
            con.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (value2)
            {
                txtProductName.Enabled = false;
                txtPrice.Enabled = false;
                txtStocks.Enabled = false;
                dtpExpirationDate.Enabled = false;
                cbCateg.Enabled = false;
                txtBrand.Enabled = false;

                value2 = false;
            }
            else
            {
                value2 = true;
                txtProductName.Enabled = true;
                txtPrice.Enabled = true;
                txtStocks.Enabled = true;
                dtpExpirationDate.Enabled = true;
                cbCateg.Enabled = true;
                txtBrand.Enabled = true;

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(txtProductName.Text == "" || txtPrice.Text == "" || txtStocks.Text == "" || txtBrand.Text == "" || dtpExpirationDate.Text == "" || cbCateg.Text == "")
            {
                MessageBox.Show("Please fill up all empty fields.", "Update Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                cmd = new OleDbCommand("UPDATE tbl_inventory SET expirationDate=@ExpDate, productName=@pName, price=@price, stocks=@stocks, category=@categ, brand=@brand WHERE productName=@cont", con);

                int stocksconvert = Convert.ToInt32(txtStocks.Text);
                double priceconvert = Convert.ToInt32(txtPrice.Text);

                cmd.Parameters.AddWithValue("@ExpDate", dtpExpirationDate.Text);
                cmd.Parameters.AddWithValue("@pName", txtProductName.Text);
                cmd.Parameters.AddWithValue("@price", priceconvert);
                cmd.Parameters.AddWithValue("@stocks", stocksconvert);
                cmd.Parameters.AddWithValue("@categ", cbCateg.Text);
                cmd.Parameters.AddWithValue("@brand", txtBrand.Text);
                cmd.Parameters.AddWithValue("@cont", lblProductCont.Text);

                MessageBox.Show("Product has been updated!");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                this.Close();
            }

        }
    }
}
