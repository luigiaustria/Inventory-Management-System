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
    public partial class newProduct : Form
    {
        static string conString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=db_inventory.mdb";
        OleDbConnection con = new OleDbConnection(conString);
        OleDbCommand cmd;
        OleDbDataAdapter adapter;

        public newProduct()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearTxts()
        {

            txtPName.Text = "";
            txtQty2.Text = "";
            txtPrice2.Text = "";
            dtpExpDate.Text = "";
            dtpDeliveryDate.Text = "";
            cbCateg.Text = "Select a Category.";
        }

        private void add(string pname, double price, int stocks, string expd, string categ)
        {
            //SQL STMT
            string sql = "INSERT INTO tbl_inventory([productName], [price], [stocks], [expirationDate], [category]) VALUES (@PNAME, @PRICE, @STOCKS, @EXPD, @CATEG)";
            cmd = new OleDbCommand(sql, con);

            //ADD PARAMS
            cmd.Parameters.AddWithValue("@PNAME", pname);
            cmd.Parameters.AddWithValue("@PRICE", price);
            cmd.Parameters.AddWithValue("@STOCKS", stocks);
            cmd.Parameters.AddWithValue("@EXPD", expd);
            cmd.Parameters.AddWithValue("@CATEG", categ);

            //OPEN CON AND EXEC
            try
            {
                if (txtPName.Text == "" || txtPrice2.Text == "" || txtQty2.Text == "" || cbCateg.Text == "Select a Category." || dtpExpDate.Text == "")
                {
                    MessageBox.Show("Please fill up all empty fields.", "Registration Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    con.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {

                        MessageBox.Show("Successfully Inserted!");
                        con.Close();
                        clearTxts();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            con.Open();
            string login = "SELECT * FROM tbl_inventory WHERE UCASE([productName]) = '" + txtPName.Text + "'";
            cmd = new OleDbCommand(login, con);
            OleDbDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)
            {
                MessageBox.Show("You already have this product. Please update your inventory instead.");
                clearTxts();
                con.Close();

            }
            else if (txtPName.Text == "" || txtPrice2.Text == "" || txtQty2.Text == "" || cbCateg.Text == "Select a Category.")
            {
                con.Close();
                MessageBox.Show("Please input all necessary details.");
                clearTxts();

            }
            else
            {
                con.Close();
                double x = Convert.ToDouble(txtPrice2.Text);
                int y = Convert.ToInt32(txtQty2.Text);
                add(txtPName.Text, x, y, dtpExpDate.Text, cbCateg.Text);

                this.Close();

            }
        }

        private void newProduct_Load(object sender, EventArgs e)
        {
            cbCateg.Text = "Select a Category.";
        }
    }
}
