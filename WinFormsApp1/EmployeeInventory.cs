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

    public partial class EmployeeInventory : Form
    {
        static string conString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=db_inventory.mdb";
        OleDbConnection con = new OleDbConnection(conString);
        OleDbCommand cmd;
        OleDbDataAdapter adapter;
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        public EmployeeInventory()
        {
            InitializeComponent();
        }

        private void retrieve()
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
                DGVInventory.DataSource = dt;
                DGVInventory.AllowUserToAddRows = false;

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

        private void EmployeeInventory_Load(object sender, EventArgs e)
        {
            retrieve();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            dt.Clear();
            retrieveSearch(txtSearch.Text);
            txtSearch.Text = "";
            txtSearch.Focus();
        }

        private void retrieveSearch(string pname)
        {
            string sql = "SELECT [ProductID], [productName], [price], [stocks], [expirationDate], [category] FROM tbl_inventory WHERE [productName] LIKE '%" + pname + "%'";
            cmd = new OleDbCommand(sql, con);

            adapter.SelectCommand = cmd;

            dt2.Clear();
            adapter.Fill(dt2);
            DGVInventory.DataSource = dt2;
            DGVInventory.AllowUserToAddRows = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            retrieve();
        }
    }
}
