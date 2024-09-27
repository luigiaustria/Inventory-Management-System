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
using Microsoft.VisualBasic.ApplicationServices;
using System.Xml.Linq;

namespace WinFormsApp1
{
    public partial class employee : Form
    {
        static string conString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=db_users.mdb";
        OleDbConnection con = new OleDbConnection(conString);
        OleDbCommand cmd;
        OleDbCommand cmd2;
        OleDbDataAdapter adapter;
        DataTable dt = new DataTable();
        bool value;

        public employee()
        {
            InitializeComponent();
        }
        //Insert Database
        /*
        private void add( string fullname, string username, string password, string email, string address, string ci, string birthday)
        {
            //SQL STMT
            string sql = "INSERT INTO tbl_users([fullName], [userName], [password], [email], [address], [contactInformation], [birthday]) VALUES (@FName, @Username, @Password, @Email, @Address, @ContactInformation, @Birthday)";
            cmd = new OleDbCommand(sql, con);

            //ADD PARAMS
            cmd.Parameters.AddWithValue("@FNAME", fullname);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Address", address);
            cmd.Parameters.AddWithValue("@ContactInformation", ci);
            cmd.Parameters.AddWithValue("@Birthday", birthday);

            //OPEN CON AND EXEC
            try
            {
                if(txtFullName.Text == "" || txtUsername.Text =="" || txtPassword.Text == "" || txtEmail.Text == "" || txtAddress.Text == "" || txtCI.Text == "" || txtBirthday.Text == "")
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
                        retrieve();
                        clearTxts();
                        txtFullName.Enabled = false;
                        txtUsername.Enabled = false;
                        txtPassword.Enabled = false;
                        txtEmail.Enabled = false;
                        txtAddress.Enabled = false;
                        txtCI.Enabled = false;
                        txtBirthday.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        */
        //fill listview
        private void populate(String id, String status, String fname, String username, String email, String address, String ci, String birthday)
        {
            //ROW
            String[] row = { id, status, fname, username, email, address, ci, birthday};
            ListViewItem item = new ListViewItem(row);

            listView1.Items.Add(item);
        }

        //FETCH FROM DATABASE
        public void retrieve()
        {
            listView1.Items.Clear();
            //SQL STMT
            string sql = "SELECT [IDNumber], [status], [fullName], [userName], [email], [address], [contactInformation], [birthday] FROM tbl_users ";
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
                    populate(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString() , row[6].ToString(), row[7].ToString());

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
        //Updat status
        private void updateStatus(string status, int id)
        {
            //SQL STMT
            string cmdText = "UPDATE tbl_users SET [status] ='" + status + "' WHERE [IDNumber]=" + id + "";
            cmd2 = new OleDbCommand(cmdText, con);

            try
            {
                con.Open();
                adapter = new OleDbDataAdapter(cmd2);
                adapter.UpdateCommand = con.CreateCommand();
                adapter.UpdateCommand.CommandText = cmdText;

                if (adapter.UpdateCommand.ExecuteNonQuery() > 0)
                {
                    clearTxts();
                    MessageBox.Show("Successfuly Updated!");
                }
                con.Close();
                retrieve();
                txtFullName.Enabled = false;
                txtUsername.Enabled = false;
                txtEmail.Enabled = false;
                txtAddress.Enabled = false;
                txtCI.Enabled = false;
                txtBirthday.Enabled = false;
                cbStatus.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }
        //Update Database
        private void update(int id, string fname, string pass, string user, string email, string address, string ci, string bday)
        {
            //SQL STMT
            string cmdTxt = "UPDATE tbl_users SET [fullName] = '" + fname + "', [password] = '" + pass + "', [userName] ='" + user + "', [email] ='" + email + "', [address] ='" + address + "', [contactInformation] ='" + ci + "', [birthday] ='" + bday + "' WHERE [IDNumber]=" + id + "";
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

                if(adapter.UpdateCommand.ExecuteNonQuery()>0)
                {
                    clearTxts();
                    MessageBox.Show("Successfuly Updated!");
                }
                con.Close();
                retrieve();
                txtFullName.Enabled = false;
                txtUsername.Enabled = false;
                txtEmail.Enabled = false;
                txtAddress.Enabled = false;
                txtCI.Enabled = false;
                txtBirthday.Enabled = false;
                cbStatus.Enabled = false;

            }
            catch (Exception ex)
            {
                clearTxts();
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        //Delete from DB
        private void delete(int id)
        {
            //SQL STMT
            string cmdTxt = "DELETE FROM tbl_users WHERE IDNumber = " + id + "";
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
                    if(cmd.ExecuteNonQuery()>0)
                    {
                        clearTxts();
                        MessageBox.Show("Successfuly Deleted!");
                        txtFullName.Enabled = false;
                        txtUsername.Enabled = false;
                        txtEmail.Enabled = false;
                        txtAddress.Enabled = false;
                        txtCI.Enabled = false;
                        txtBirthday.Enabled = false;
                    }
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

        //Clear texts
        private void clearTxts()
        {
            txtFullName.Text = "";
            txtUsername.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            txtCI.Text = "";
            txtBirthday.Text = "";
            cbStatus.Text = "";
            txtPassword.Text = "";


        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(value)
            {
                txtFullName.Enabled = false;
                txtUsername.Enabled = false;
                txtEmail.Enabled = false;
                txtAddress.Enabled = false;
                txtCI.Enabled = false;
                txtBirthday.Enabled = false;
                cbStatus.Enabled = false;
                txtPassword.Enabled = false;


                value = false;
            }
            else
            {
                value = true;
                txtFullName.Enabled = true;
                txtUsername.Enabled = true;
                txtEmail.Enabled = true;
                txtAddress.Enabled = true;
                txtCI.Enabled = true;
                txtBirthday.Enabled = true;
                cbStatus.Enabled = true;
                txtPassword.Enabled = true;

            }

        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            cbStatus.Text = listView1.SelectedItems[0].SubItems[1].Text;
            txtFullName.Text = listView1.SelectedItems[0].SubItems[2].Text;
            txtUsername.Text = listView1.SelectedItems[0].SubItems[3].Text;
            txtEmail.Text = listView1.SelectedItems[0].SubItems[4].Text;
            txtAddress.Text = listView1.SelectedItems[0].SubItems[5].Text;
            txtCI.Text = listView1.SelectedItems[0].SubItems[6].Text;
            txtBirthday.Text = listView1.SelectedItems[0].SubItems[7].Text;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFullName.Text == "" || txtUsername.Text == ""  || txtEmail.Text == "" || txtAddress.Text == "" || txtCI.Text == "" || txtBirthday.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("Please fill up all empty fields.", "Update Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    String selected = listView1.SelectedItems[0].SubItems[0].Text;
                    int id = Convert.ToInt32(selected);

                    update(id, txtFullName.Text, txtPassword.Text, txtUsername.Text, txtEmail.Text, txtAddress.Text, txtCI.Text, txtBirthday.Text);
                }

            }
            catch
            {
                MessageBox.Show("Select the data that you want to edit.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = listView1.SelectedItems[0].SubItems[0].Text;
                int id = Convert.ToInt32(selected);

                delete(id);
            }                  
            catch
            {
                MessageBox.Show("Select the data that you want to delete.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
}

        private void btnAdd_Click(object sender, EventArgs e)
        {
            RegistrationForm frm2 = new RegistrationForm();
            frm2.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void employee_Load(object sender, EventArgs e)
        {
            retrieve();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            String selected = listView1.SelectedItems[0].SubItems[0].Text;
            int id = Convert.ToInt32(selected);
            updateStatus(cbStatus.Text, id);
        }
    }
}
