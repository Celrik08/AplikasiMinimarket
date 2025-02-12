namespace AplikasiMinimarket
{
    partial class SearchBarang
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
            Data_Barang = new DataGridView();
            Kode = new DataGridViewTextBoxColumn();
            Nama = new DataGridViewTextBoxColumn();
            TextSearchBarang = new TextBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)Data_Barang).BeginInit();
            SuspendLayout();
            // 
            // Data_Barang
            // 
            Data_Barang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Data_Barang.Columns.AddRange(new DataGridViewColumn[] { Kode, Nama });
            Data_Barang.Location = new Point(12, 106);
            Data_Barang.Name = "Data_Barang";
            Data_Barang.RowHeadersWidth = 51;
            Data_Barang.Size = new Size(772, 408);
            Data_Barang.TabIndex = 7;
            Data_Barang.CellClick += Data_Barang_CellClick;
            Data_Barang.KeyDown += Data_Barang_KeyDown;
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
            // TextSearchBarang
            // 
            TextSearchBarang.Location = new Point(12, 52);
            TextSearchBarang.Name = "TextSearchBarang";
            TextSearchBarang.Size = new Size(772, 28);
            TextSearchBarang.TabIndex = 5;
            TextSearchBarang.TextChanged += TextSearchBarang_TextChanged;
            TextSearchBarang.KeyDown += TextSearchBarang_KeyDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(316, 10);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(134, 23);
            label1.TabIndex = 4;
            label1.Text = "Search Barang";
            // 
            // SearchBarang
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(122, 178, 211);
            ClientSize = new Size(796, 525);
            Controls.Add(Data_Barang);
            Controls.Add(TextSearchBarang);
            Controls.Add(label1);
            Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4, 3, 4, 3);
            Name = "SearchBarang";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SearchBarang";
            Load += SearchBarang_Load;
            ((System.ComponentModel.ISupportInitialize)Data_Barang).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView Data_Barang;
        private TextBox TextSearchBarang;
        private Label label1;
        private DataGridViewTextBoxColumn Kode;
        private DataGridViewTextBoxColumn Nama;
    }
}