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
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            TextUser = new TextBox();
            TextNama = new TextBox();
            TextPassword = new TextBox();
            ComboPegawai = new ComboBox();
            ComboGudang = new ComboBox();
            ComboRole = new ComboBox();
            BtnTambah = new Button();
            BtnSimpan = new Button();
            BtnUbah = new Button();
            BtnBack = new Button();
            dataGridView1 = new DataGridView();
            User = new DataGridViewTextBoxColumn();
            Nama = new DataGridViewTextBoxColumn();
            Password = new DataGridViewTextBoxColumn();
            Pegawai = new DataGridViewTextBoxColumn();
            Gudang = new DataGridViewTextBoxColumn();
            Role = new DataGridViewTextBoxColumn();
            Hapus = new DataGridViewButtonColumn();
            TextPegawai = new TextBox();
            label8 = new Label();
            TextGudang = new TextBox();
            label9 = new Label();
            label10 = new Label();
            TextRole = new TextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
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
            label1.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
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
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.Cornsilk;
            label5.Location = new Point(11, 185);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(117, 20);
            label5.TabIndex = 4;
            label5.Text = "Nama Pegawai";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.Cornsilk;
            label6.Location = new Point(11, 253);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(113, 20);
            label6.TabIndex = 5;
            label6.Text = "Nama Gudang";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.Cornsilk;
            label7.Location = new Point(13, 321);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(90, 20);
            label7.TabIndex = 6;
            label7.Text = "Nama Role";
            // 
            // TextUser
            // 
            TextUser.Location = new Point(142, 46);
            TextUser.Name = "TextUser";
            TextUser.Size = new Size(151, 28);
            TextUser.TabIndex = 7;
            // 
            // TextNama
            // 
            TextNama.Location = new Point(142, 80);
            TextNama.Name = "TextNama";
            TextNama.Size = new Size(151, 28);
            TextNama.TabIndex = 8;
            TextNama.KeyPress += TextNama_KeyPress;
            // 
            // TextPassword
            // 
            TextPassword.Location = new Point(142, 114);
            TextPassword.Name = "TextPassword";
            TextPassword.Size = new Size(151, 28);
            TextPassword.TabIndex = 9;
            TextPassword.KeyPress += TextPassword_KeyPress;
            // 
            // ComboPegawai
            // 
            ComboPegawai.FormattingEnabled = true;
            ComboPegawai.Location = new Point(142, 148);
            ComboPegawai.Name = "ComboPegawai";
            ComboPegawai.Size = new Size(151, 28);
            ComboPegawai.TabIndex = 10;
            ComboPegawai.SelectedIndexChanged += ComboPegawai_SelectedIndexChanged;
            ComboPegawai.KeyPress += ComboPegawai_KeyPress;
            // 
            // ComboGudang
            // 
            ComboGudang.FormattingEnabled = true;
            ComboGudang.Location = new Point(142, 216);
            ComboGudang.Name = "ComboGudang";
            ComboGudang.Size = new Size(151, 28);
            ComboGudang.TabIndex = 11;
            ComboGudang.SelectedIndexChanged += ComboGudang_SelectedIndexChanged;
            ComboGudang.KeyPress += ComboGudang_KeyPress;
            // 
            // ComboRole
            // 
            ComboRole.FormattingEnabled = true;
            ComboRole.Location = new Point(142, 284);
            ComboRole.Name = "ComboRole";
            ComboRole.Size = new Size(151, 28);
            ComboRole.TabIndex = 12;
            ComboRole.KeyPress += ComboRole_KeyPress;
            // 
            // BtnTambah
            // 
            BtnTambah.Location = new Point(13, 376);
            BtnTambah.Name = "BtnTambah";
            BtnTambah.Size = new Size(94, 29);
            BtnTambah.TabIndex = 13;
            BtnTambah.Text = "Tambah";
            BtnTambah.UseVisualStyleBackColor = true;
            // 
            // BtnSimpan
            // 
            BtnSimpan.Location = new Point(126, 376);
            BtnSimpan.Name = "BtnSimpan";
            BtnSimpan.Size = new Size(94, 29);
            BtnSimpan.TabIndex = 14;
            BtnSimpan.Text = "Simpan";
            BtnSimpan.UseVisualStyleBackColor = true;
            // 
            // BtnUbah
            // 
            BtnUbah.Location = new Point(243, 376);
            BtnUbah.Name = "BtnUbah";
            BtnUbah.Size = new Size(94, 29);
            BtnUbah.TabIndex = 15;
            BtnUbah.Text = "Ubah";
            BtnUbah.UseVisualStyleBackColor = true;
            // 
            // BtnBack
            // 
            BtnBack.Location = new Point(361, 376);
            BtnBack.Name = "BtnBack";
            BtnBack.Size = new Size(94, 29);
            BtnBack.TabIndex = 16;
            BtnBack.Text = "Back";
            BtnBack.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { User, Nama, Password, Pegawai, Gudang, Role, Hapus });
            dataGridView1.Location = new Point(11, 440);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(1901, 603);
            dataGridView1.TabIndex = 17;
            // 
            // User
            // 
            User.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            User.HeaderText = "Id User";
            User.MinimumWidth = 6;
            User.Name = "User";
            // 
            // Nama
            // 
            Nama.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Nama.HeaderText = "UserName";
            Nama.MinimumWidth = 6;
            Nama.Name = "Nama";
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
            // TextPegawai
            // 
            TextPegawai.Location = new Point(142, 182);
            TextPegawai.Name = "TextPegawai";
            TextPegawai.Size = new Size(151, 28);
            TextPegawai.TabIndex = 18;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = Color.Cornsilk;
            label8.Location = new Point(11, 151);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(89, 20);
            label8.TabIndex = 19;
            label8.Text = "Id Pegawai";
            // 
            // TextGudang
            // 
            TextGudang.Location = new Point(142, 250);
            TextGudang.Name = "TextGudang";
            TextGudang.Size = new Size(151, 28);
            TextGudang.TabIndex = 20;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.Cornsilk;
            label9.Location = new Point(11, 219);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(85, 20);
            label9.TabIndex = 21;
            label9.Text = "Id Gudang";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ForeColor = Color.Cornsilk;
            label10.Location = new Point(13, 287);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(62, 20);
            label10.TabIndex = 22;
            label10.Text = "Id Role";
            // 
            // TextRole
            // 
            TextRole.Location = new Point(142, 318);
            TextRole.Name = "TextRole";
            TextRole.Size = new Size(151, 28);
            TextRole.TabIndex = 23;
            // 
            // DataUser
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 127, 255);
            ClientSize = new Size(1924, 1055);
            Controls.Add(TextRole);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(TextGudang);
            Controls.Add(label8);
            Controls.Add(TextPegawai);
            Controls.Add(dataGridView1);
            Controls.Add(BtnBack);
            Controls.Add(BtnUbah);
            Controls.Add(BtnSimpan);
            Controls.Add(BtnTambah);
            Controls.Add(ComboRole);
            Controls.Add(ComboGudang);
            Controls.Add(ComboPegawai);
            Controls.Add(TextPassword);
            Controls.Add(TextNama);
            Controls.Add(TextUser);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(panel1);
            Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(4, 3, 4, 3);
            Name = "DataUser";
            Text = "DataUser";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox TextUser;
        private TextBox TextNama;
        private TextBox TextPassword;
        private ComboBox ComboPegawai;
        private ComboBox ComboGudang;
        private ComboBox ComboRole;
        private Button BtnTambah;
        private Button BtnSimpan;
        private Button BtnUbah;
        private Button BtnBack;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn User;
        private DataGridViewTextBoxColumn Nama;
        private DataGridViewTextBoxColumn Password;
        private DataGridViewTextBoxColumn Pegawai;
        private DataGridViewTextBoxColumn Gudang;
        private DataGridViewTextBoxColumn Role;
        private DataGridViewButtonColumn Hapus;
        private TextBox TextPegawai;
        private Label label8;
        private TextBox TextGudang;
        private Label label9;
        private Label label10;
        private TextBox TextRole;
    }
}