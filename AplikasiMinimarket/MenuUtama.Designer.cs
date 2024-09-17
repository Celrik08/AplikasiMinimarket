namespace AplikasiMinimarket
{
    partial class MenuUtama
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
            menuStrip1 = new MenuStrip();
            DataUser = new ToolStripMenuItem();
            label1 = new Label();
            User = new Label();
            Data_Barang = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            Nama = new DataGridViewTextBoxColumn();
            Kategori = new DataGridViewTextBoxColumn();
            Harga = new DataGridViewTextBoxColumn();
            Total = new DataGridViewTextBoxColumn();
            Back = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Data_Barang).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.Gray;
            menuStrip1.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { DataUser, Back });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(8, 2, 0, 2);
            menuStrip1.Size = new Size(1924, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // DataUser
            // 
            DataUser.Name = "DataUser";
            DataUser.Size = new Size(96, 24);
            DataUser.Text = "Data User";
            DataUser.Click += DataUser_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.Cornsilk;
            label1.Location = new Point(927, 72);
            label1.Name = "label1";
            label1.Size = new Size(133, 22);
            label1.TabIndex = 1;
            label1.Text = "Selamat Datang";
            // 
            // User
            // 
            User.AutoSize = true;
            User.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            User.ForeColor = Color.Cornsilk;
            User.Location = new Point(964, 120);
            User.Name = "User";
            User.Size = new Size(47, 22);
            User.TabIndex = 2;
            User.Text = "User";
            // 
            // Data_Barang
            // 
            Data_Barang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Data_Barang.Columns.AddRange(new DataGridViewColumn[] { Id, Nama, Kategori, Harga, Total });
            Data_Barang.Location = new Point(12, 160);
            Data_Barang.Name = "Data_Barang";
            Data_Barang.RowHeadersWidth = 51;
            Data_Barang.RowTemplate.Height = 29;
            Data_Barang.Size = new Size(1900, 883);
            Data_Barang.TabIndex = 3;
            // 
            // Id
            // 
            Id.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Id.HeaderText = "Id Barang";
            Id.MinimumWidth = 6;
            Id.Name = "Id";
            // 
            // Nama
            // 
            Nama.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Nama.HeaderText = "Nama Barang";
            Nama.MinimumWidth = 6;
            Nama.Name = "Nama";
            // 
            // Kategori
            // 
            Kategori.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Kategori.HeaderText = "Nama Kategori";
            Kategori.MinimumWidth = 6;
            Kategori.Name = "Kategori";
            // 
            // Harga
            // 
            Harga.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Harga.HeaderText = "Harga Satuan";
            Harga.MinimumWidth = 6;
            Harga.Name = "Harga";
            // 
            // Total
            // 
            Total.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Total.HeaderText = "Total Stok";
            Total.MinimumWidth = 6;
            Total.Name = "Total";
            // 
            // Back
            // 
            Back.Name = "Back";
            Back.Size = new Size(60, 24);
            Back.Text = "Back";
            Back.Click += Back_Click;
            // 
            // MenuUtama
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 127, 255);
            ClientSize = new Size(1924, 1055);
            Controls.Add(Data_Barang);
            Controls.Add(User);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = SystemColors.ControlText;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 3, 4, 3);
            Name = "MenuUtama";
            Text = "MenuUtama";
            Load += MenuUtama_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Data_Barang).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem DataUser;
        private Label label1;
        private Label User;
        private DataGridView Data_Barang;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn Nama;
        private DataGridViewTextBoxColumn Kategori;
        private DataGridViewTextBoxColumn Harga;
        private DataGridViewTextBoxColumn Total;
        private ToolStripMenuItem Back;
    }
}