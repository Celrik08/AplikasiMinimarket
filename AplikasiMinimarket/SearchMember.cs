using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplikasiMinimarket
{
    public partial class SearchMember : Form
    {
        public SearchMember()
        {
            InitializeComponent();
        }

        private void TextSearchMember_TextChanged(object sender, EventArgs e)
        {
            string searchKeyword = TextSearchMember.Text.Trim();
            LoadDataToDataGridView(searchKeyword);
        }

        private void TextSearchMember_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && Data_Member.Rows.Count > 0)
            {
                // Pindahkan ke baris pertama jika masih di TextSearchMember
                Data_Member.Focus();
            }
        }

        private void Data_Member_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Dapatkan id_member dari kolom 0 yang dipilih
                string idMember = Data_Member.CurrentRow.Cells[0].Value.ToString();

                // Kirim data langsung ke DataTransaksi
                DataTransaksi dataTransaksi = Application.OpenForms.OfType<DataTransaksi>().FirstOrDefault();
                if (dataTransaksi != null)
                {
                    dataTransaksi.SetTextIdMember(idMember);
                }
                else
                {
                    MessageBox.Show("Form DataTransaksi tidak ditemukan.");
                }

                // Menutup form SearchMember
                this.Close();
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Jika sudah di baris, kembali ke baris pertama
                int rowCount = Data_Member.Rows.Count;
                int currentIndex = Data_Member.CurrentCell.RowIndex;
                int nextIndex = (currentIndex + 1) % rowCount;
                Data_Member.CurrentCell = Data_Member.Rows[nextIndex].Cells[0];
                e.Handled = true; // Mencegah efek tambahan
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Jika sudah di baris pertama, kembali ke baris terakhir
                int rowCount = Data_Member.Rows.Count;
                int currentIndex = Data_Member.CurrentCell.RowIndex;
                int previousIndex = (currentIndex - 1 + rowCount) % rowCount;
                Data_Member.CurrentCell = Data_Member.Rows[previousIndex].Cells[0];
                e.Handled = true; // Mencegah efek tambahan
            }
        }

        private void Data_Member_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Pastikan baris yang diklik valid
            {
                // Ambil id_member dari kolom pertama yang diklik
                string idMember = Data_Member.Rows[e.RowIndex].Cells[0].Value.ToString();

                // Kirim idMember ke TextIdMember di form DataTransaksi
                DataTransaksi dataTransaksi = Application.OpenForms.OfType<DataTransaksi>().FirstOrDefault();
                if (dataTransaksi != null)
                {
                    dataTransaksi.SetTextIdMember(idMember);
                }
                else
                {
                    MessageBox.Show("Form DataTransaksi tidak ditemukan.");
                }

                // Tutup form SearchMember
                this.Close();
            }
        }

        private void SearchMember_Load(object sender, EventArgs e)
        {
            LoadDataToDataGridView("");
        }

        private void LoadDataToDataGridView(string searchKeyword)
        {
            // Kosongkan DataGridView terlebih dahulu
            Data_Member.Rows.Clear();

            // Tulis kueri SQL Anda untuk mengambil data dari nilai
            string query = "SELECT id_member, nama_member, no_hp FROM tb_member " +
                            "WHERE id_member LIKE @search OR nama_member LIKE @search OR no_hp LIKE @search";

            using (SqlConnection conn = new SqlConnection(Connect.conn.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + searchKeyword + "%");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Menambahkan data ke DataGridView
                            string id = reader["id_member"].ToString();
                            string nama = reader["nama_member"].ToString();
                            string no = reader["no_hp"].ToString();

                            // Menambahkan baris ke DataGridView
                            Data_Member.Rows.Add(id, nama, no);
                        }
                    }
                }
            }
        }
    }
}