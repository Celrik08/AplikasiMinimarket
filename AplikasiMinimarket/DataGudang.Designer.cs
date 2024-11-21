namespace AplikasiMinimarket
{
    partial class DataGudang
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
            dataInventroiMasukToolStripMenuItem = new ToolStripMenuItem();
            dataMasukToolStripMenuItem = new ToolStripMenuItem();
            riwayatTransaksiMasukToolStripMenuItem = new ToolStripMenuItem();
            dataGudangToolStripMenuItem = new ToolStripMenuItem();
            dataGuToolStripMenuItem = new ToolStripMenuItem();
            dataGudangToolStripMenuItem1 = new ToolStripMenuItem();
            dataRiwayatGudangMasukToolStripMenuItem = new ToolStripMenuItem();
            dataGridView1 = new DataGridView();
            label1 = new Label();
            Id = new DataGridViewTextBoxColumn();
            Nama = new DataGridViewTextBoxColumn();
            Satuan = new DataGridViewTextBoxColumn();
            Harga = new DataGridViewTextBoxColumn();
            Jumlah = new DataGridViewTextBoxColumn();
            Rak = new DataGridViewTextBoxColumn();
            Gudang = new DataGridViewTextBoxColumn();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.Gray;
            menuStrip1.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { dataInventroiMasukToolStripMenuItem, dataGudangToolStripMenuItem, dataGuToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(8, 2, 0, 2);
            menuStrip1.Size = new Size(1924, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // dataInventroiMasukToolStripMenuItem
            // 
            dataInventroiMasukToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { dataMasukToolStripMenuItem, riwayatTransaksiMasukToolStripMenuItem });
            dataInventroiMasukToolStripMenuItem.Name = "dataInventroiMasukToolStripMenuItem";
            dataInventroiMasukToolStripMenuItem.Size = new Size(181, 24);
            dataInventroiMasukToolStripMenuItem.Text = "Data Inventroi Masuk";
            // 
            // dataMasukToolStripMenuItem
            // 
            dataMasukToolStripMenuItem.Name = "dataMasukToolStripMenuItem";
            dataMasukToolStripMenuItem.Size = new Size(335, 26);
            dataMasukToolStripMenuItem.Text = "Transaksi Masuk Barang";
            // 
            // riwayatTransaksiMasukToolStripMenuItem
            // 
            riwayatTransaksiMasukToolStripMenuItem.Name = "riwayatTransaksiMasukToolStripMenuItem";
            riwayatTransaksiMasukToolStripMenuItem.Size = new Size(335, 26);
            riwayatTransaksiMasukToolStripMenuItem.Text = "Riwayat Transaksi Masuk Barang";
            // 
            // dataGudangToolStripMenuItem
            // 
            dataGudangToolStripMenuItem.Name = "dataGudangToolStripMenuItem";
            dataGudangToolStripMenuItem.Size = new Size(118, 24);
            dataGudangToolStripMenuItem.Text = "Data Gudang";
            // 
            // dataGuToolStripMenuItem
            // 
            dataGuToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { dataGudangToolStripMenuItem1, dataRiwayatGudangMasukToolStripMenuItem });
            dataGuToolStripMenuItem.Name = "dataGuToolStripMenuItem";
            dataGuToolStripMenuItem.Size = new Size(111, 24);
            dataGuToolStripMenuItem.Text = "Data Masuk";
            // 
            // dataGudangToolStripMenuItem1
            // 
            dataGudangToolStripMenuItem1.Name = "dataGudangToolStripMenuItem1";
            dataGudangToolStripMenuItem1.Size = new Size(305, 26);
            dataGudangToolStripMenuItem1.Text = "Data Gudang Masuk";
            // 
            // dataRiwayatGudangMasukToolStripMenuItem
            // 
            dataRiwayatGudangMasukToolStripMenuItem.Name = "dataRiwayatGudangMasukToolStripMenuItem";
            dataRiwayatGudangMasukToolStripMenuItem.Size = new Size(305, 26);
            dataRiwayatGudangMasukToolStripMenuItem.Text = "Data Riwayat Gudang Masuk";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Id, Nama, Satuan, Harga, Jumlah, Rak, Gudang });
            dataGridView1.Location = new Point(13, 115);
            dataGridView1.Margin = new Padding(4, 3, 4, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1898, 928);
            dataGridView1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Cornsilk;
            label1.Location = new Point(30, 60);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(174, 25);
            label1.TabIndex = 2;
            label1.Text = "Barang Gudang";
            // 
            // Id
            // 
            Id.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Id.HeaderText = "Id Barang Gudang";
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
            // Satuan
            // 
            Satuan.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Satuan.HeaderText = "Nama Satuan";
            Satuan.MinimumWidth = 6;
            Satuan.Name = "Satuan";
            // 
            // Harga
            // 
            Harga.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Harga.HeaderText = "Harga Beli";
            Harga.MinimumWidth = 6;
            Harga.Name = "Harga";
            // 
            // Jumlah
            // 
            Jumlah.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Jumlah.HeaderText = "Jumlah Satuan";
            Jumlah.MinimumWidth = 6;
            Jumlah.Name = "Jumlah";
            // 
            // Rak
            // 
            Rak.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Rak.HeaderText = "Nama Rak";
            Rak.MinimumWidth = 6;
            Rak.Name = "Rak";
            // 
            // Gudang
            // 
            Gudang.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Gudang.HeaderText = "Nama Gudang";
            Gudang.MinimumWidth = 6;
            Gudang.Name = "Gudang";
            // 
            // DataGudang
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(122, 178, 211);
            ClientSize = new Size(1924, 1055);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip1);
            Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 3, 4, 3);
            Name = "DataGudang";
            Text = "DataGudang";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem dataInventroiMasukToolStripMenuItem;
        private ToolStripMenuItem dataMasukToolStripMenuItem;
        private ToolStripMenuItem riwayatTransaksiMasukToolStripMenuItem;
        private ToolStripMenuItem dataGudangToolStripMenuItem;
        private ToolStripMenuItem dataGuToolStripMenuItem;
        private ToolStripMenuItem dataGudangToolStripMenuItem1;
        private ToolStripMenuItem dataRiwayatGudangMasukToolStripMenuItem;
        private DataGridView dataGridView1;
        private Label label1;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn Nama;
        private DataGridViewTextBoxColumn Satuan;
        private DataGridViewTextBoxColumn Harga;
        private DataGridViewTextBoxColumn Jumlah;
        private DataGridViewTextBoxColumn Rak;
        private DataGridViewTextBoxColumn Gudang;
    }
}