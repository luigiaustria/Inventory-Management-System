using Microsoft.VisualBasic.ApplicationServices;
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

namespace WinFormsApp1
{
    public partial class EmployeeEL : Form
    {
        static string conString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=db_users.mdb";
        OleDbConnection con = new OleDbConnection(conString);
        OleDbCommand cmd;
        OleDbDataAdapter adapter;
        DataTable dt = new DataTable();
        OleDbDataReader dr;
        bool value2;

        public EmployeeEL(string value)
        {
            InitializeComponent();
            this.Value = value; //get the username
        }
        public string Value { get; set; }

        private void EmployeeEL_Load(object sender, EventArgs e)
        {
            lblCont.Text = Value;
            retrieve();
        }

        private void retrieve()
        {
            cmd = new OleDbCommand("SELECT fullName, userName, email, address, contactInformation, birthday FROM tbl_users WHERE userName=@username", con);
            cmd.Parameters.AddWithValue("@username", lblCont.Text);

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string fullName = dr["fullName"].ToString();
                string userName = dr["userName"].ToString();
                string email = dr["email"].ToString();
                string address = dr["address"].ToString();
                string contactInformation = dr["contactInformation"].ToString();
                string birthday = dr["birthday"].ToString();

                txtFullName.Text = fullName;
                txtUsername.Text = userName;
                txtEmail.Text = email;
                txtAddress.Text = address;
                txtCI.Text = contactInformation;
                txtBirthday.Text = birthday;
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (value2)
            {
                txtFullName.Enabled = false;
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
                txtEmail.Enabled = false;
                txtAddress.Enabled = false;
                txtCI.Enabled = false;
                txtBirthday.Enabled = false;
                cbStatus.Enabled = false;


                value2 = false;
            }
            else
            {
                value2 = true;
                txtFullName.Enabled = true;
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
                txtEmail.Enabled = true;
                txtAddress.Enabled = true;
                txtCI.Enabled = true;
                txtBirthday.Enabled = true;
                cbStatus.Enabled = true;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFullName.Text == "" || txtUsername.Text == "" || txtPassword.Text == "" || txtEmail.Text == "" || txtAddress.Text == "" || txtCI.Text == "" || txtBirthday.Text == "" || cbStatus.Text == "")
                {
                    MessageBox.Show("Please fill up all empty fields.", "Update Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    update(lblCont.Text, txtFullName.Text, txtUsername.Text, txtPassword.Text, txtEmail.Text, txtAddress.Text, txtCI.Text, txtBirthday.Text, cbStatus.Text);

                }

            }
            catch
            {
                MessageBox.Show("Select the data that you want to edit.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void update(string lblid, string fname, string user, string pass, string email, string address, string ci, string bday, string status)
        {

            //SQL STMT
            string cmdTxt = "UPDATE tbl_users SET [fullName] = '" + fname + "', [userName] ='" + user + "', [password] ='" + pass + "', [email] ='" + email + "', [address] ='" + address + "', [contactInformation] ='" + ci + "', [birthday] ='" + bday + "', [status] ='" + status + "' WHERE [userName]='" + lblid + "'";
            cmd.Parameters.AddWithValue("@username", lblCont.Text);
            cmd = new OleDbCommand(cmdTxt, con);

            /*
            cmd.Parameters.AddWithValue("@FULLNAME", fname);
            cmd.Parameters.AddWithValue("@USERNAME", username);
            cmd.Parameters.AddWithValue("@PASSWORD", password);
            cmd.Parameters.AddWithValue("@EMAIL", email);
            cmd.Parameters.AddWithValue("@ADDRESS", address);
            cmd.Parameters.AddWithValue("@CI", ci);
            */

            //OPEN CON, UPDATE, RETRIEVE LISTVIEW
            try
            {
                con.Open();
                adapter = new OleDbDataAdapter(cmd);
                adapter.UpdateCommand = con.CreateCommand();
                adapter.UpdateCommand.CommandText = cmdTxt;

                if (adapter.UpdateCommand.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Successfuly Updated!");
                }
                con.Close();
                retrieve();
                txtPassword.Text = "";
                txtFullName.Enabled = false;
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
                txtEmail.Enabled = false;
                txtAddress.Enabled = false;
                txtCI.Enabled = false;
                txtBirthday.Enabled = false;
                cbStatus.Enabled = false;

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);

            }
        }
    }
}
