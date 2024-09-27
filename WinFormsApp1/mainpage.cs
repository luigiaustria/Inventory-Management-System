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
using System.Runtime.InteropServices;
using FontAwesome.Sharp;

namespace WinFormsApp1
{
    public partial class mainpage : Form
    {
        //Fields
        private int borderSize = 2;
        private IconButton currentBtn;

        //constructor
        public mainpage(string value)
        {
            InitializeComponent();
            CollapseMenu();
            this.Padding = new Padding(borderSize);//border size
            this.BackColor = Color.FromArgb(221, 127, 90); // border color
            this.Value = value; //get the username
        }

        public string Value { get; set; }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("User32.DLL", EntryPoint ="SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        //overridden methods
        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;
            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                return;

            }
            base.WndProc(ref m);
        }

        private void mainpage_Resize(object sender, EventArgs e)
        {
            AdjustForm();
        }

        //private methods
        private void AdjustForm()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized:
                    this.Padding = new Padding(0, 8, 8, 0);
                    break;
                case FormWindowState.Normal:
                    if(this.Padding.Top != borderSize)
                    this.Padding = new Padding(borderSize);
                    break;
            }
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            CollapseMenu();
        }

        private void CollapseMenu()
        {
          if(this.panelMenu.Width > 200)
            {
                panelMenu.Width = 100;
                pictureBox1.Visible = false;
                menuButton.Dock = DockStyle.Top;
                foreach(Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = "";
                    menuButton.ImageAlign = ContentAlignment.MiddleCenter;
                    menuButton.Padding = new Padding(0);
                }
            }
            else
            {
                panelMenu.Width = 210;
                pictureBox1.Visible = true;
                menuButton.Dock = DockStyle.None;
                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = "   " + menuButton.Tag.ToString();
                    menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                    menuButton.Padding = new Padding(10,0,0,0);
                }
            }
        }

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Color.White);
            openChildForm(new employee());
            lblTitle.Text = "Employee List";
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Color.White);
            openChildForm(new about());
            lblTitle.Text = "About";
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Color.White);
            openChildForm(new cashier(lblUsername.Text));
            lblTitle.Text = "Cashier";
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Color.White);
            openChildForm(new inventory());
            lblTitle.Text = "Inventory";
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Color.White);
            openChildForm(new DeliveryExpiration());
            lblTitle.Text = "Schedule List";
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to Logout?", "Loging Out", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
                GroceryStore frm1 = new GroceryStore();
                frm1.Show();
            }
        }

        private void iconButton2_MouseHover(object sender, EventArgs e)
        {

        }

        private void iconButton2_MouseLeave(object sender, EventArgs e)
        {

        }

        private void iconButton2_MouseMove(object sender, MouseEventArgs e)
        {

        }

        //highlight color
        private void ActivateButton(object senderBtn, Color color)
        {
            if(senderBtn != null)
            {
                DisableButton();
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(240, 169, 99);

            }
            
        }
        
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(221, 127, 90);
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Color.White);
            openChildForm(new Sales());
            lblTitle.Text = "Sales";
        }

        private void mainpage_Load(object sender, EventArgs e)
        {
            iconButton2.PerformClick();
            lblUsername.Text = Value;
        }
    }
}
