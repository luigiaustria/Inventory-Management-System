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


namespace WinFormsApp1
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=db_users.mdb");
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter adap = new OleDbDataAdapter();

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            string login = "SELECT * FROM tbl_users WHERE [userName] = '" + userNameTextBox.Text + "'";
            cmd = new OleDbCommand(login, con);
            OleDbDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)
            {
                con.Close();
                MessageBox.Show("Sorry, this username already exist.");
                userNameTextBox.Text = "";
                passwordTextBox.Text = "";
                txtConfirm.Text = "";
                userNameTextBox.Focus();
            }
            else if (txtConfirm.Text != passwordTextBox.Text)
            {
                con.Close();
                MessageBox.Show("The password confirmation does not match.", "Registration Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fullNameTextBox.Text = "";
                userNameTextBox.Text = "";
                passwordTextBox.Text = "";
                emailTextBox.Text = "";
                addressTextBox.Text = "";
                contactInformationTextBox.Text = "";
                BirthdayDatePicker.Text = "";
                txtConfirm.Text = "";
                fullNameTextBox.Focus();
            }
            else if (userNameTextBox.Text == "" || passwordTextBox.Text == "" || fullNameTextBox.Text == "" || emailTextBox.Text == "" || addressTextBox.Text == "" || contactInformationTextBox.Text == "" || BirthdayDatePicker.Text == "")
            {
                con.Close();
                MessageBox.Show("Please fill up all empty fields.", "Registration Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fullNameTextBox.Text = "";
                userNameTextBox.Text = "";
                passwordTextBox.Text = "";
                emailTextBox.Text = "";
                txtConfirm.Text = "";
                addressTextBox.Text = "";
                contactInformationTextBox.Text = "";
                BirthdayDatePicker.Text = "";
                fullNameTextBox.Focus();
            }
            else if(rbAdmin.Checked == true)
            {
                con.Close();
                string adminTxtCode = "5RAdmin";
                con.Open();
                string register = "INSERT INTO tbl_users ([fullName], [userName], [password], [email], [address], [contactInformation], [birthday], [AccessType] ) VALUES ('" + fullNameTextBox.Text + "','" + userNameTextBox.Text + "','" + passwordTextBox.Text + "','" + emailTextBox.Text + "','" + addressTextBox.Text + "','" + contactInformationTextBox.Text + "','" + BirthdayDatePicker.Text + "','" + adminTxtCode + "', '" + cbStatus.Text + "')";
                cmd = new OleDbCommand(register, con);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Your Account has been registered as admin!", "Registration Successful!", MessageBoxButtons.OK, MessageBoxIcon.Information);


                fullNameTextBox.Text = "";
                userNameTextBox.Text = "";
                passwordTextBox.Text = "";
                emailTextBox.Text = "";
                addressTextBox.Text = "";
                contactInformationTextBox.Text = "";
                BirthdayDatePicker.Text = "";
                txtConfirm.Text = "";
                fullNameTextBox.Focus();
                cbStatus.Text = "Present";
                this.Hide();
            }
            else if(rbEmployee.Checked == true)
            {
                con.Close();
                string adminTxtCode = "5REmployee";
                con.Open();
                string register = "INSERT INTO tbl_users ([fullName], [userName], [password], [email], [address], [contactInformation], [birthday], [AccessType], [status] ) VALUES ('" + fullNameTextBox.Text + "','" + userNameTextBox.Text + "','" + passwordTextBox.Text + "','" + emailTextBox.Text + "','" + addressTextBox.Text + "','" + contactInformationTextBox.Text + "','" + BirthdayDatePicker.Text + "','" + adminTxtCode + "', '" + cbStatus.Text + "')";
                cmd = new OleDbCommand(register, con);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Your Account has been registered!", "Registration Successful!", MessageBoxButtons.OK, MessageBoxIcon.Information);


                fullNameTextBox.Text = "";
                userNameTextBox.Text = "";
                passwordTextBox.Text = "";
                emailTextBox.Text = "";
                addressTextBox.Text = "";
                contactInformationTextBox.Text = "";
                BirthdayDatePicker.Text = "";
                txtConfirm.Text = "";
                fullNameTextBox.Focus();
                cbStatus.Text = "Present";
                this.Hide();

            }
            con.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();

        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
