using System.Data.OleDb;

namespace WinFormsApp1
{
    public partial class GroceryStore : Form
    {
        public GroceryStore()
        {
            InitializeComponent();
        }
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=db_users.mdb");
        OleDbCommand cmd = new OleDbCommand();
        string AccessCode;

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void GroceryStore_Load(object sender, EventArgs e)
        {
            Username.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            string login = "SELECT * FROM tbl_users WHERE username = '"+Username.Text+ "' and password = '"+Password.Text+ "' and AccessType = '"+label3.Text+"'";
            cmd = new OleDbCommand(login, con);
            OleDbDataReader dr = cmd.ExecuteReader();


            if (dr.Read() == true)
            {

                if(rbAdmin.Checked == true)
                {
                    mainpage frm2 = new mainpage(Username.Text);
                    frm2.Show();
                    //new mainpage().Show();
                    this.Hide();
                    con.Close();


                }
                else if (rbEmployee.Checked == true)
                {
                    new EmployeeMainPage(Username.Text).Show();
                    this.Hide();
                    con.Close();
                }

            }

            else
            {
                MessageBox.Show("Invalid Username or Password, Please Try Again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Username.Text = "";
                Password.Text = "";
                Username.Focus();
                con.Close();
            }
        }

        private void CreateAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistrationForm frm2 = new RegistrationForm();
            frm2.Show();
            this.Hide();
        }

        private void button3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3_Click(this, new EventArgs());
            }
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3_Click(this, new EventArgs());
            }
        }

        private void Username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3_Click(this, new EventArgs());
            }
        }

        private void rbEmployee_CheckedChanged(object sender, EventArgs e)
        {

            AccessCode = "5REmployee";
            label3.Text = AccessCode;
        }

        private void rbAdmin_CheckedChanged(object sender, EventArgs e)
        {
            AccessCode = "5RAdmin";
            label3.Text = AccessCode;
        }
    }
}