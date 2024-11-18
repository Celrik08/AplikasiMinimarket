namespace AplikasiMinimarket
{
    partial class DataUser
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
            panel1 = new Panel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            TextId = new TextBox();
            TextUser = new TextBox();
            TextPassword = new TextBox();
            ComboPegawai = new ComboBox();
            ComboGudang = new ComboBox();
            ComboRole = new ComboBox();
            BtnTambah = new Button();
            BtnSimpan = new Button();
            BtnUbah = new Button();
            BtnBack = new Button();
            Data_User = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            User = new DataGridViewTextBoxColumn();
            Password = new DataGridViewTextBoxColumn();
            Pegawai = new DataGridViewTextBoxColumn();
            Gudang = new DataGridViewTextBoxColumn();
            Role = new DataGridViewTextBoxColumn();
            Hapus = new DataGridViewButtonColumn();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Data_User).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Gray;
            panel1.Controls.Add(label1);
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(2404, 40);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            label1.Location = new Point(11, 8);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(96, 23);
            label1.TabIndex = 1;
            label1.Text = "Data User";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Cornsilk;
            label2.Location = new Point(11, 49);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(63, 20);
            label2.TabIndex = 1;
            label2.Text = "Id User";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.Cornsilk;
            label3.Location = new Point(11, 83);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(86, 20);
            label3.TabIndex = 2;
            label3.Text = "UserName";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.Cornsilk;
            label4.Location = new Point(11, 117);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(79, 20);
            label4.TabIndex = 3;
            label4.Text = "Password";
            // 
            // TextId
            // 
            TextId.Location = new Point(142, 46);
            TextId.Name = "TextId";
            TextId.Size = new Size(246, 28);
            TextId.TabIndex = 7;
            // 
            // TextUser
            // 
            TextUser.Location = new Point(142, 80);
            TextUser.Name = "TextUser";
            TextUser.Size = new Size(246, 28);
            TextUser.TabIndex = 8;
            TextUser.KeyPress += TextUser_KeyPress;
            // 
            // TextPassword
            // 
            TextPassword.Location = new Point(142, 114);
            TextPassword.Name = "TextPassword";
            TextPassword.Size = new Size(246, 28);
            TextPassword.TabIndex = 9;
            TextPassword.KeyPress += TextPassword_KeyPress;
            // 
            // ComboPegawai
            // 
            ComboPegawai.FormattingEnabled = true;
            ComboPegawai.Location = new Point(142, 148);
            ComboPegawai.Name = "ComboPegawai";
            ComboPegawai.Size = new Size(246, 28);
            ComboPegawai.TabIndex = 10;
            ComboPegawai.TextChanged += ComboPegawai_TextChanged;
            ComboPegawai.KeyPress += ComboPegawai_KeyPress;
            // 
            // ComboGudang
            // 
            ComboGudang.FormattingEnabled = true;
            ComboGudang.Location = new Point(142, 182);
            ComboGudang.Name = "ComboGudang";
            ComboGudang.Size = new Size(246, 28);
            ComboGudang.TabIndex = 11;
            ComboGudang.TextChanged += ComboGudang_TextChanged;
            ComboGudang.KeyPress += ComboGudang_KeyPress;
            // 
            // ComboRole
            // 
            ComboRole.FormattingEnabled = true;
            ComboRole.Location = new Point(142, 216);
            ComboRole.Name = "ComboRole";
            ComboRole.Size = new Size(246, 28);
            ComboRole.TabIndex = 12;
            ComboRole.TextChanged += ComboRole_TextChanged;
            ComboRole.KeyPress += ComboRole_KeyPress;
            // 
            // BtnTambah
            // 
            BtnTambah.Location = new Point(11, 270);
            BtnTambah.Name = "BtnTambah";
            BtnTambah.Size = new Size(94, 29);
            BtnTambah.TabIndex = 13;
            BtnTambah.Text = "Tambah";
            BtnTambah.UseVisualStyleBackColor = true;
            BtnTambah.Click += BtnTambah_Click;
            // 
            // BtnSimpan
            // 
            BtnSimpan.Location = new Point(124, 270);
            BtnSimpan.Name = "BtnSimpan";
            BtnSimpan.Size = new Size(94, 29);
            BtnSimpan.TabIndex = 14;
            BtnSimpan.Text = "Simpan";
            BtnSimpan.UseVisualStyleBackColor = true;
            BtnSimpan.Click += BtnSimpan_Click;
            // 
            // BtnUbah
            // 
            BtnUbah.Location = new Point(241, 270);
            BtnUbah.Name = "BtnUbah";
            BtnUbah.Size = new Size(94, 29);
            BtnUbah.TabIndex = 15;
            BtnUbah.Text = "Ubah";
            BtnUbah.UseVisualStyleBackColor = true;
            BtnUbah.Click += BtnUbah_Click;
            // 
            // BtnBack
            // 
            BtnBack.Location = new Point(359, 270);
            BtnBack.Name = "BtnBack";
            BtnBack.Size = new Size(94, 29);
            BtnBack.TabIndex = 16;
            BtnBack.Text = "Back";
            BtnBack.UseVisualStyleBackColor = true;
            BtnBack.Click += BtnBack_Click;
            // 
            // Data_User
            // 
            Data_User.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Data_User.Columns.AddRange(new DataGridViewColumn[] { Id, User, Password, Pegawai, Gudang, Role, Hapus });
            Data_User.Location = new Point(11, 330);
            Data_User.Name = "Data_User";
            Data_User.RowHeadersWidth = 51;
            Data_User.Size = new Size(1901, 713);
            Data_User.TabIndex = 17;
            Data_User.CellClick += Data_User_CellClick;
            Data_User.CellContentClick += Data_User_CellContentClick;
            // 
            // Id
            // 
            Id.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Id.HeaderText = "Id User";
            Id.MinimumWidth = 6;
            Id.Name = "Id";
            // 
            // User
            // 
            User.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            User.HeaderText = "UserName";
            User.MinimumWidth = 6;
            User.Name = "User";
            // 
            // Password
            // 
            Password.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Password.HeaderText = "Password";
            Password.MinimumWidth = 6;
            Password.Name = "Password";
            // 
            // Pegawai
            // 
            Pegawai.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Pegawai.HeaderText = "Nama Pegawai";
            Pegawai.MinimumWidth = 6;
            Pegawai.Name = "Pegawai";
            // 
            // Gudang
            // 
            Gudang.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Gudang.HeaderText = "Nama Gudang";
            Gudang.MinimumWidth = 6;
            Gudang.Name = "Gudang";
            // 
            // Role
            // 
            Role.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Role.HeaderText = "Nama Role";
            Role.MinimumWidth = 6;
            Role.Name = "Role";
            // 
            // Hapus
            // 
            Hapus.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Hapus.HeaderText = "";
            Hapus.MinimumWidth = 6;
            Hapus.Name = "Hapus";
            Hapus.Text = "Hapus";
            Hapus.UseColumnTextForButtonValue = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = Color.Cornsilk;
            label8.Location = new Point(11, 151);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(117, 20);
            label8.TabIndex = 19;
            label8.Text = "Nama Pegawai";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.Cornsilk;
            label9.Location = new Point(11, 185);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(113, 20);
            label9.TabIndex = 21;
            label9.Text = "Nama Gudang";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ForeColor = Color.Cornsilk;
            label10.Location = new Point(11, 219);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(90, 20);
            label10.TabIndex = 22;
            label10.Text = "Nama Role";
            // 
            // DataUser
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(122, 178, 211);
            ClientSize = new Size(1924, 1055);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(Data_User);
            Controls.Add(BtnBack);
            Controls.Add(BtnUbah);
            Controls.Add(BtnSimpan);
            Controls.Add(BtnTambah);
            Controls.Add(ComboRole);
            Controls.Add(ComboGudang);
            Controls.Add(ComboPegawai);
            Controls.Add(TextPassword);
            Controls.Add(TextUser);
            Controls.Add(TextId);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(panel1);
            Font = new Font("Times New Roman", 10.8F);
            Margin = new Padding(4, 3, 4, 3);
            Name = "DataUser";
            Text = "DataUser";
            FormClosing += DataUser_FormClosing;
            Load += DataUser_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Data_User).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox TextId;
        private TextBox TextUser;
        private TextBox TextPassword;
        private ComboBox ComboPegawai;
        private ComboBox ComboGudang;
        private ComboBox ComboRole;
        private Button BtnTambah;
        private Button BtnSimpan;
        private Button BtnUbah;
        private Button BtnBack;
        private DataGridView Data_User;
        private Label label8;
        private Label label9;
        private Label label10;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn User;
        private DataGridViewTextBoxColumn Password;
        private DataGridViewTextBoxColumn Pegawai;
        private DataGridViewTextBoxColumn Gudang;
        private DataGridViewTextBoxColumn Role;
        private DataGridViewButtonColumn Hapus;
    }
}