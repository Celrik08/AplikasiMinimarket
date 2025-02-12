namespace AplikasiMinimarket
{
    partial class SearchMember
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
            label1 = new Label();
            TextSearchMember = new TextBox();
            Data_Member = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            Nama = new DataGridViewTextBoxColumn();
            No = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)Data_Member).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(316, 9);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(144, 23);
            label1.TabIndex = 0;
            label1.Text = "Search Member";
            // 
            // TextSearchMember
            // 
            TextSearchMember.Location = new Point(12, 51);
            TextSearchMember.Name = "TextSearchMember";
            TextSearchMember.Size = new Size(772, 28);
            TextSearchMember.TabIndex = 1;
            TextSearchMember.TextChanged += TextSearchMember_TextChanged;
            TextSearchMember.KeyDown += TextSearchMember_KeyDown;
            // 
            // Data_Member
            // 
            Data_Member.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Data_Member.Columns.AddRange(new DataGridViewColumn[] { Id, Nama, No });
            Data_Member.Location = new Point(12, 105);
            Data_Member.Name = "Data_Member";
            Data_Member.RowHeadersWidth = 51;
            Data_Member.Size = new Size(772, 408);
            Data_Member.TabIndex = 3;
            Data_Member.CellClick += Data_Member_CellClick;
            Data_Member.KeyDown += Data_Member_KeyDown;
            // 
            // Id
            // 
            Id.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Id.HeaderText = "Id Member";
            Id.MinimumWidth = 6;
            Id.Name = "Id";
            // 
            // Nama
            // 
            Nama.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Nama.HeaderText = "Nama Member";
            Nama.MinimumWidth = 6;
            Nama.Name = "Nama";
            // 
            // No
            // 
            No.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            No.HeaderText = "No Hp";
            No.MinimumWidth = 6;
            No.Name = "No";
            // 
            // SearchMember
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(122, 178, 211);
            ClientSize = new Size(796, 525);
            Controls.Add(Data_Member);
            Controls.Add(TextSearchMember);
            Controls.Add(label1);
            Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4, 3, 4, 3);
            Name = "SearchMember";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SearchMember";
            Load += SearchMember_Load;
            ((System.ComponentModel.ISupportInitialize)Data_Member).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox TextSearchMember;
        private DataGridView Data_Member;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn Nama;
        private DataGridViewTextBoxColumn No;
    }
}