namespace WinFormsApp1
{
    partial class DeliveryExpiration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpCurrentDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.lvExpirationDate = new System.Windows.Forms.ListView();
            this.columnPName = new System.Windows.Forms.ColumnHeader();
            this.columnExpirationDate = new System.Windows.Forms.ColumnHeader();
            this.columnCategory = new System.Windows.Forms.ColumnHeader();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.dtpDeliveryDateTable = new System.Windows.Forms.DateTimePicker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDeliveryUpdate = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dtpDelivery = new System.Windows.Forms.DateTimePicker();
            this.lvDeliveryDate = new System.Windows.Forms.ListView();
            this.pnamecolumn = new System.Windows.Forms.ColumnHeader();
            this.columndelivery = new System.Windows.Forms.ColumnHeader();
            this.lvSelectProducts = new System.Windows.Forms.ListView();
            this.columnProduct = new System.Windows.Forms.ColumnHeader();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtStocks = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.dtpCurrentDate);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lvExpirationDate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(968, 524);
            this.panel1.TabIndex = 0;
            // 
            // dtpCurrentDate
            // 
            this.dtpCurrentDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpCurrentDate.CalendarFont = new System.Drawing.Font("Roboto Cn", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpCurrentDate.CustomFormat = "MMMM/dd/yyyy";
            this.dtpCurrentDate.Font = new System.Drawing.Font("Roboto Cn", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpCurrentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCurrentDate.Location = new System.Drawing.Point(720, 12);
            this.dtpCurrentDate.MaxDate = new System.DateTime(2999, 12, 31, 0, 0, 0, 0);
            this.dtpCurrentDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpCurrentDate.Name = "dtpCurrentDate";
            this.dtpCurrentDate.Size = new System.Drawing.Size(200, 23);
            this.dtpCurrentDate.TabIndex = 5;
            this.dtpCurrentDate.ValueChanged += new System.EventHandler(this.dtpCurrentDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto Cn", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(515, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Expiration Dates:";
            // 
            // lvExpirationDate
            // 
            this.lvExpirationDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvExpirationDate.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnPName,
            this.columnExpirationDate,
            this.columnCategory});
            this.lvExpirationDate.Font = new System.Drawing.Font("Roboto Cn", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lvExpirationDate.FullRowSelect = true;
            this.lvExpirationDate.GridLines = true;
            this.lvExpirationDate.Location = new System.Drawing.Point(515, 43);
            this.lvExpirationDate.Name = "lvExpirationDate";
            this.lvExpirationDate.Size = new System.Drawing.Size(439, 391);
            this.lvExpirationDate.TabIndex = 3;
            this.lvExpirationDate.UseCompatibleStateImageBehavior = false;
            this.lvExpirationDate.View = System.Windows.Forms.View.Details;
            // 
            // columnPName
            // 
            this.columnPName.Text = "Product Name";
            this.columnPName.Width = 170;
            // 
            // columnExpirationDate
            // 
            this.columnExpirationDate.Text = "Expiration Date";
            this.columnExpirationDate.Width = 110;
            // 
            // columnCategory
            // 
            this.columnCategory.Text = "Category";
            this.columnCategory.Width = 90;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(169)))), ((int)(((byte)(99)))));
            this.panel2.Controls.Add(this.lblDate);
            this.panel2.Controls.Add(this.lblTime);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 450);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(968, 74);
            this.panel2.TabIndex = 1;
            // 
            // lblDate
            // 
            this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Roboto Cn", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDate.Location = new System.Drawing.Point(12, 12);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(161, 29);
            this.lblDate.TabIndex = 3;
            this.lblDate.Text = "CURRENT DATE";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTime
            // 
            this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Roboto Cn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTime.Location = new System.Drawing.Point(14, 45);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(91, 19);
            this.lblTime.TabIndex = 2;
            this.lblTime.Text = "Current Time";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpDeliveryDateTable
            // 
            this.dtpDeliveryDateTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpDeliveryDateTable.CustomFormat = "MMMM/dd/yyyy";
            this.dtpDeliveryDateTable.Font = new System.Drawing.Font("Roboto Cn", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpDeliveryDateTable.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDeliveryDateTable.Location = new System.Drawing.Point(259, 35);
            this.dtpDeliveryDateTable.MaxDate = new System.DateTime(2999, 12, 31, 0, 0, 0, 0);
            this.dtpDeliveryDateTable.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpDeliveryDateTable.Name = "dtpDeliveryDateTable";
            this.dtpDeliveryDateTable.Size = new System.Drawing.Size(227, 22);
            this.dtpDeliveryDateTable.TabIndex = 13;
            this.dtpDeliveryDateTable.ValueChanged += new System.EventHandler(this.dtpDeliveryDateTable_ValueChanged);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(197)))), ((int)(((byte)(231)))));
            this.panel3.Controls.Add(this.txtStocks);
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.btnDeliveryUpdate);
            this.panel3.Controls.Add(this.dtpDeliveryDateTable);
            this.panel3.Controls.Add(this.txtSearch);
            this.panel3.Controls.Add(this.dtpDelivery);
            this.panel3.Controls.Add(this.lvDeliveryDate);
            this.panel3.Controls.Add(this.lvSelectProducts);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(504, 450);
            this.panel3.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WinFormsApp1.Properties.Resources.delivery;
            this.pictureBox1.Location = new System.Drawing.Point(7, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(66, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto Cn", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(358, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 23);
            this.label2.TabIndex = 6;
            this.label2.Text = "Delivery Dates:";
            // 
            // btnDeliveryUpdate
            // 
            this.btnDeliveryUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(127)))), ((int)(((byte)(90)))));
            this.btnDeliveryUpdate.FlatAppearance.BorderSize = 0;
            this.btnDeliveryUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeliveryUpdate.Font = new System.Drawing.Font("Roboto Bk", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnDeliveryUpdate.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnDeliveryUpdate.Location = new System.Drawing.Point(95, 402);
            this.btnDeliveryUpdate.Name = "btnDeliveryUpdate";
            this.btnDeliveryUpdate.Size = new System.Drawing.Size(81, 23);
            this.btnDeliveryUpdate.TabIndex = 12;
            this.btnDeliveryUpdate.Text = "Update";
            this.btnDeliveryUpdate.UseVisualStyleBackColor = false;
            this.btnDeliveryUpdate.Click += new System.EventHandler(this.btnDeliveryUpdate_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(7, 345);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "Search for a Product";
            this.txtSearch.Size = new System.Drawing.Size(169, 23);
            this.txtSearch.TabIndex = 11;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // dtpDelivery
            // 
            this.dtpDelivery.CustomFormat = "MMMM/dd/yyyy";
            this.dtpDelivery.Font = new System.Drawing.Font("Roboto Cn", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpDelivery.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDelivery.Location = new System.Drawing.Point(7, 374);
            this.dtpDelivery.MaxDate = new System.DateTime(2999, 12, 31, 0, 0, 0, 0);
            this.dtpDelivery.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpDelivery.Name = "dtpDelivery";
            this.dtpDelivery.Size = new System.Drawing.Size(169, 22);
            this.dtpDelivery.TabIndex = 8;
            this.dtpDelivery.ValueChanged += new System.EventHandler(this.dtpDelivery_ValueChanged);
            // 
            // lvDeliveryDate
            // 
            this.lvDeliveryDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvDeliveryDate.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.pnamecolumn,
            this.columndelivery});
            this.lvDeliveryDate.Font = new System.Drawing.Font("Roboto Cn", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lvDeliveryDate.FullRowSelect = true;
            this.lvDeliveryDate.GridLines = true;
            this.lvDeliveryDate.Location = new System.Drawing.Point(187, 65);
            this.lvDeliveryDate.Name = "lvDeliveryDate";
            this.lvDeliveryDate.Size = new System.Drawing.Size(299, 360);
            this.lvDeliveryDate.TabIndex = 10;
            this.lvDeliveryDate.UseCompatibleStateImageBehavior = false;
            this.lvDeliveryDate.View = System.Windows.Forms.View.Details;
            // 
            // pnamecolumn
            // 
            this.pnamecolumn.Text = "Product Name";
            this.pnamecolumn.Width = 170;
            // 
            // columndelivery
            // 
            this.columndelivery.Text = "Delivery Date";
            this.columndelivery.Width = 120;
            // 
            // lvSelectProducts
            // 
            this.lvSelectProducts.BackColor = System.Drawing.Color.Moccasin;
            this.lvSelectProducts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnProduct});
            this.lvSelectProducts.Font = new System.Drawing.Font("Roboto Cn", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lvSelectProducts.FullRowSelect = true;
            this.lvSelectProducts.GridLines = true;
            this.lvSelectProducts.Location = new System.Drawing.Point(7, 65);
            this.lvSelectProducts.Name = "lvSelectProducts";
            this.lvSelectProducts.Size = new System.Drawing.Size(169, 274);
            this.lvSelectProducts.TabIndex = 9;
            this.lvSelectProducts.UseCompatibleStateImageBehavior = false;
            this.lvSelectProducts.View = System.Windows.Forms.View.Details;
            // 
            // columnProduct
            // 
            this.columnProduct.Text = "List of Products";
            this.columnProduct.Width = 160;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtStocks
            // 
            this.txtStocks.Location = new System.Drawing.Point(7, 402);
            this.txtStocks.Name = "txtStocks";
            this.txtStocks.PlaceholderText = "Stocks";
            this.txtStocks.Size = new System.Drawing.Size(82, 23);
            this.txtStocks.TabIndex = 15;
            // 
            // DeliveryExpiration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 524);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "DeliveryExpiration";
            this.Text = "DeliveryExpiration";
            this.Load += new System.EventHandler(this.DeliveryExpiration_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Label lblDate;
        private Label lblTime;
        private ListView lvDeliveryDate;
        private ColumnHeader pnamecolumn;
        private ColumnHeader columndelivery;
        private ListView lvSelectProducts;
        private ColumnHeader columnProduct;
        private DateTimePicker dtpCurrentDate;
        private Label label1;
        private ListView lvExpirationDate;
        private ColumnHeader columnPName;
        private ColumnHeader columnExpirationDate;
        private ColumnHeader columnCategory;
        private System.Windows.Forms.Timer timer1;
        private DateTimePicker dtpDeliveryDateTable;
        private Button btnDeliveryUpdate;
        private TextBox txtSearch;
        private DateTimePicker dtpDelivery;
        private Label label2;
        private PictureBox pictureBox1;
        private TextBox txtStocks;
    }
}