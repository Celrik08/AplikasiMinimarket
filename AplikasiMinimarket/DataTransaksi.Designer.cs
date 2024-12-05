namespace AplikasiMinimarket
{
    partial class DataTransaksi
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            panel1 = new Panel();
            BtnBack = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            TextTanggal = new TextBox();
            TextTransaksi = new TextBox();
            TextJam = new TextBox();
            label4 = new Label();
            label5 = new Label();
            ComboMember = new ComboBox();
            Data_Transaksi = new DataGridView();
            No = new DataGridViewTextBoxColumn();
            Kode = new DataGridViewTextBoxColumn();
            Nama = new DataGridViewTextBoxColumn();
            Harga = new DataGridViewTextBoxColumn();
            Total = new DataGridViewTextBoxColumn();
            Sub = new DataGridViewTextBoxColumn();
            Hapus = new DataGridViewButtonColumn();
            TextJumlah = new TextBox();
            label6 = new Label();
            TextTotal2 = new TextBox();
            label7 = new Label();
            TextTotal3 = new TextBox();
            label8 = new Label();
            BtnSave = new Button();
            label9 = new Label();
            ComboBarang = new ComboBox();
            TextNama = new TextBox();
            label10 = new Label();
            TextHarga = new TextBox();
            label11 = new Label();
            TextTotal1 = new TextBox();
            label12 = new Label();
            TextSub = new TextBox();
            label13 = new Label();
            BtnDipending = new Button();
            BtnKlik = new Button();
            Data_Dipending = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            Member = new DataGridViewTextBoxColumn();
            TextUser = new TextBox();
            TextMember = new TextBox();
            label14 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Data_Transaksi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Data_Dipending).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Gray;
            panel1.Controls.Add(BtnBack);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(2404, 40);
            panel1.TabIndex = 0;
            // 
            // BtnBack
            // 
            BtnBack.ForeColor = SystemColors.ControlText;
            BtnBack.Location = new Point(1802, 6);
            BtnBack.Name = "BtnBack";
            BtnBack.Size = new Size(94, 29);
            BtnBack.TabIndex = 34;
            BtnBack.Text = "Back";
            BtnBack.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(9, 8);
            label1.Name = "label1";
            label1.Size = new Size(92, 23);
            label1.TabIndex = 1;
            label1.Text = "Transaksi";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Cornsilk;
            label2.Location = new Point(12, 70);
            label2.Name = "label2";
            label2.Size = new Size(105, 20);
            label2.TabIndex = 1;
            label2.Text = "No Transkasi";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.Cornsilk;
            label3.Location = new Point(12, 108);
            label3.Name = "label3";
            label3.Size = new Size(139, 20);
            label3.TabIndex = 2;
            label3.Text = "Tanggal Transaksi";
            // 
            // TextTanggal
            // 
            TextTanggal.Location = new Point(157, 105);
            TextTanggal.Name = "TextTanggal";
            TextTanggal.Size = new Size(193, 28);
            TextTanggal.TabIndex = 3;
            // 
            // TextTransaksi
            // 
            TextTransaksi.Location = new Point(157, 67);
            TextTransaksi.Name = "TextTransaksi";
            TextTransaksi.Size = new Size(193, 28);
            TextTransaksi.TabIndex = 4;
            // 
            // TextJam
            // 
            TextJam.Location = new Point(496, 72);
            TextJam.Name = "TextJam";
            TextJam.Size = new Size(193, 28);
            TextJam.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.Cornsilk;
            label4.Location = new Point(372, 75);
            label4.Name = "label4";
            label4.Size = new Size(38, 20);
            label4.TabIndex = 5;
            label4.Text = "Jam";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.Cornsilk;
            label5.Location = new Point(372, 110);
            label5.Name = "label5";
            label5.Size = new Size(90, 20);
            label5.TabIndex = 8;
            label5.Text = "Id Member";
            // 
            // ComboMember
            // 
            ComboMember.FormattingEnabled = true;
            ComboMember.Location = new Point(496, 110);
            ComboMember.Name = "ComboMember";
            ComboMember.Size = new Size(193, 28);
            ComboMember.TabIndex = 9;
            ComboMember.SelectedIndexChanged += ComboMember_SelectedIndexChanged;
            ComboMember.TextChanged += ComboMember_TextChanged;
            ComboMember.KeyPress += ComboMember_KeyPress;
            // 
            // Data_Transaksi
            // 
            Data_Transaksi.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Data_Transaksi.Columns.AddRange(new DataGridViewColumn[] { No, Kode, Nama, Harga, Total, Sub, Hapus });
            Data_Transaksi.Location = new Point(12, 295);
            Data_Transaksi.Name = "Data_Transaksi";
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Data_Transaksi.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Data_Transaksi.RowHeadersWidth = 51;
            dataGridViewCellStyle2.ForeColor = Color.Black;
            Data_Transaksi.RowsDefaultCellStyle = dataGridViewCellStyle2;
            Data_Transaksi.Size = new Size(1244, 646);
            Data_Transaksi.TabIndex = 10;
            // 
            // No
            // 
            No.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            No.HeaderText = "No";
            No.MinimumWidth = 6;
            No.Name = "No";
            // 
            // Kode
            // 
            Kode.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Kode.HeaderText = "Kode Barang";
            Kode.MinimumWidth = 6;
            Kode.Name = "Kode";
            // 
            // Nama
            // 
            Nama.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Nama.HeaderText = "Nama Barang";
            Nama.MinimumWidth = 6;
            Nama.Name = "Nama";
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
            Total.HeaderText = "Total";
            Total.MinimumWidth = 6;
            Total.Name = "Total";
            // 
            // Sub
            // 
            Sub.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Sub.HeaderText = "Sub Total";
            Sub.MinimumWidth = 6;
            Sub.Name = "Sub";
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
            // TextJumlah
            // 
            TextJumlah.Location = new Point(1429, 770);
            TextJumlah.Name = "TextJumlah";
            TextJumlah.Size = new Size(184, 28);
            TextJumlah.TabIndex = 12;
            TextJumlah.TextChanged += TextJumlah_TextChanged;
            TextJumlah.KeyPress += TextJumlah_KeyPress;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.Cornsilk;
            label6.Location = new Point(1284, 773);
            label6.Name = "label6";
            label6.Size = new Size(108, 20);
            label6.TabIndex = 11;
            label6.Text = "Jumlah Bayar";
            // 
            // TextTotal2
            // 
            TextTotal2.Location = new Point(1429, 736);
            TextTotal2.Name = "TextTotal2";
            TextTotal2.Size = new Size(184, 28);
            TextTotal2.TabIndex = 14;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.Cornsilk;
            label7.Location = new Point(1284, 739);
            label7.Name = "label7";
            label7.Size = new Size(94, 20);
            label7.TabIndex = 13;
            label7.Text = "Total Harga";
            // 
            // TextTotal3
            // 
            TextTotal3.Location = new Point(1429, 804);
            TextTotal3.Name = "TextTotal3";
            TextTotal3.Size = new Size(184, 28);
            TextTotal3.TabIndex = 16;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = Color.Cornsilk;
            label8.Location = new Point(1284, 807);
            label8.Name = "label8";
            label8.Size = new Size(128, 20);
            label8.TabIndex = 15;
            label8.Text = "Total Kembalian";
            // 
            // BtnSave
            // 
            BtnSave.ForeColor = SystemColors.ControlText;
            BtnSave.Location = new Point(1649, 866);
            BtnSave.Name = "BtnSave";
            BtnSave.Size = new Size(94, 29);
            BtnSave.TabIndex = 17;
            BtnSave.Text = "Save";
            BtnSave.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.Cornsilk;
            label9.Location = new Point(12, 188);
            label9.Name = "label9";
            label9.Size = new Size(104, 20);
            label9.TabIndex = 18;
            label9.Text = "Kode Barang";
            // 
            // ComboBarang
            // 
            ComboBarang.FormattingEnabled = true;
            ComboBarang.Location = new Point(157, 185);
            ComboBarang.Name = "ComboBarang";
            ComboBarang.Size = new Size(193, 28);
            ComboBarang.TabIndex = 22;
            ComboBarang.SelectedIndexChanged += ComboBarang_SelectedIndexChanged;
            ComboBarang.TextChanged += ComboBarang_TextChanged;
            ComboBarang.KeyPress += ComboBarang_KeyPress;
            // 
            // TextNama
            // 
            TextNama.Location = new Point(496, 185);
            TextNama.Name = "TextNama";
            TextNama.Size = new Size(193, 28);
            TextNama.TabIndex = 24;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ForeColor = Color.Cornsilk;
            label10.Location = new Point(360, 194);
            label10.Name = "label10";
            label10.Size = new Size(108, 20);
            label10.TabIndex = 23;
            label10.Text = "Nama Barang";
            // 
            // TextHarga
            // 
            TextHarga.Location = new Point(819, 191);
            TextHarga.Name = "TextHarga";
            TextHarga.Size = new Size(193, 28);
            TextHarga.TabIndex = 26;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.ForeColor = Color.Cornsilk;
            label11.Location = new Point(695, 194);
            label11.Name = "label11";
            label11.Size = new Size(106, 20);
            label11.TabIndex = 25;
            label11.Text = "Harga Satuan";
            // 
            // TextTotal1
            // 
            TextTotal1.Location = new Point(157, 219);
            TextTotal1.Name = "TextTotal1";
            TextTotal1.Size = new Size(193, 28);
            TextTotal1.TabIndex = 28;
            TextTotal1.KeyPress += TextTotal1_KeyPress;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.ForeColor = Color.Cornsilk;
            label12.Location = new Point(12, 225);
            label12.Name = "label12";
            label12.Size = new Size(46, 20);
            label12.TabIndex = 27;
            label12.Text = "Total";
            // 
            // TextSub
            // 
            TextSub.Location = new Point(496, 219);
            TextSub.Name = "TextSub";
            TextSub.Size = new Size(193, 28);
            TextSub.TabIndex = 30;
            TextSub.KeyPress += TextSub_KeyPress;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.ForeColor = Color.Cornsilk;
            label13.Location = new Point(360, 225);
            label13.Name = "label13";
            label13.Size = new Size(79, 20);
            label13.TabIndex = 29;
            label13.Text = "Sub Total";
            // 
            // BtnDipending
            // 
            BtnDipending.ForeColor = SystemColors.ControlText;
            BtnDipending.Location = new Point(1122, 246);
            BtnDipending.Name = "BtnDipending";
            BtnDipending.Size = new Size(94, 29);
            BtnDipending.TabIndex = 31;
            BtnDipending.Text = "Dipending";
            BtnDipending.UseVisualStyleBackColor = true;
            // 
            // BtnKlik
            // 
            BtnKlik.ForeColor = SystemColors.ControlText;
            BtnKlik.Location = new Point(989, 246);
            BtnKlik.Name = "BtnKlik";
            BtnKlik.Size = new Size(94, 29);
            BtnKlik.TabIndex = 32;
            BtnKlik.Text = "Klik";
            BtnKlik.UseVisualStyleBackColor = true;
            BtnKlik.Click += BtnKlik_Click;
            // 
            // Data_Dipending
            // 
            Data_Dipending.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Data_Dipending.Columns.AddRange(new DataGridViewColumn[] { Id, Member });
            Data_Dipending.Location = new Point(1284, 70);
            Data_Dipending.Name = "Data_Dipending";
            Data_Dipending.RowHeadersWidth = 51;
            Data_Dipending.Size = new Size(597, 393);
            Data_Dipending.TabIndex = 33;
            // 
            // Id
            // 
            Id.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Id.HeaderText = "Id Tranaksi";
            Id.MinimumWidth = 6;
            Id.Name = "Id";
            // 
            // Member
            // 
            Member.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Member.HeaderText = "Nama Member";
            Member.MinimumWidth = 6;
            Member.Name = "Member";
            // 
            // TextUser
            // 
            TextUser.Location = new Point(819, 110);
            TextUser.Name = "TextUser";
            TextUser.Size = new Size(193, 28);
            TextUser.TabIndex = 34;
            // 
            // TextMember
            // 
            TextMember.Location = new Point(819, 72);
            TextMember.Name = "TextMember";
            TextMember.Size = new Size(193, 28);
            TextMember.TabIndex = 35;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.ForeColor = Color.Cornsilk;
            label14.Location = new Point(695, 75);
            label14.Name = "label14";
            label14.Size = new Size(118, 20);
            label14.TabIndex = 36;
            label14.Text = "Nama Member";
            // 
            // DataTransaksi
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(122, 178, 211);
            ClientSize = new Size(1924, 1055);
            Controls.Add(label14);
            Controls.Add(TextMember);
            Controls.Add(TextUser);
            Controls.Add(Data_Dipending);
            Controls.Add(BtnKlik);
            Controls.Add(BtnDipending);
            Controls.Add(TextSub);
            Controls.Add(label13);
            Controls.Add(TextTotal1);
            Controls.Add(label12);
            Controls.Add(TextHarga);
            Controls.Add(label11);
            Controls.Add(TextNama);
            Controls.Add(label10);
            Controls.Add(ComboBarang);
            Controls.Add(label9);
            Controls.Add(BtnSave);
            Controls.Add(TextTotal3);
            Controls.Add(label8);
            Controls.Add(TextTotal2);
            Controls.Add(label7);
            Controls.Add(TextJumlah);
            Controls.Add(label6);
            Controls.Add(Data_Transaksi);
            Controls.Add(ComboMember);
            Controls.Add(label5);
            Controls.Add(TextJam);
            Controls.Add(label4);
            Controls.Add(TextTransaksi);
            Controls.Add(TextTanggal);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(panel1);
            Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.Cornsilk;
            Margin = new Padding(4, 3, 4, 3);
            Name = "DataTransaksi";
            Load += DataTransaksi_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Data_Transaksi).EndInit();
            ((System.ComponentModel.ISupportInitialize)Data_Dipending).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox TextTanggal;
        private TextBox TextTransaksi;
        private TextBox TextJam;
        private Label label4;
        private Label label5;
        private ComboBox ComboMember;
        private DataGridView Data_Transaksi;
        private TextBox TextJumlah;
        private Label label6;
        private TextBox TextTotal2;
        private Label label7;
        private TextBox TextTotal3;
        private Label label8;
        private Button BtnSave;
        private Label label9;
        private ComboBox ComboBarang;
        private TextBox TextNama;
        private Label label10;
        private TextBox TextHarga;
        private Label label11;
        private TextBox TextTotal1;
        private Label label12;
        private TextBox TextSub;
        private Label label13;
        private Button BtnDipending;
        private Button BtnKlik;
        private DataGridView Data_Dipending;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn Member;
        private Button BtnBack;
        private TextBox TextUser;
        private TextBox TextMember;
        private Label label14;
        private DataGridViewTextBoxColumn No;
        private DataGridViewTextBoxColumn Kode;
        private DataGridViewTextBoxColumn Nama;
        private DataGridViewTextBoxColumn Harga;
        private DataGridViewTextBoxColumn Total;
        private DataGridViewTextBoxColumn Sub;
        private DataGridViewButtonColumn Hapus;
    }
}