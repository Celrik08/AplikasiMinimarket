using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization.DataContracts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplikasiMinimarket
{
    public partial class SearchBarang : Form
    {
        public SearchBarang()
        {
            InitializeComponent();
        }

        private void TextSearchBarang_TextChanged(object sender, EventArgs e)
        {
            string searchKeyword = TextSearchBarang.Text.Trim();
            LoadDataToDataGridView(searchKeyword);
        }

        private void TextSearchBarang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && Data_Barang.Rows.Count > 0)
            {
                // Pindahkan ke baris pertama jika masih di TextSearchBarang
                Data_Barang.Focus();
            }
        }

        private void Data_Barang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Dapatkan id_barang dari kolom 0 yang dipilih
                string idBarang = Data_Barang.CurrentRow.Cells[0].Value.ToString();

                // Kirim data langsung ke DataTransaksi
                DataTransaksi dataTransaksi = Application.OpenForms.OfType<DataTransaksi>().FirstOrDefault();
                if (dataTransaksi != null)
                {
                    dataTransaksi.SetTextIdBarang(idBarang);
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
                int rowCount = Data_Barang.Rows.Count;
                int currentIndex = Data_Barang.CurrentCell.RowIndex;
                int nextIndex = (currentIndex + 1) % rowCount;
                Data_Barang.CurrentCell = Data_Barang.Rows[nextIndex].Cells[0];
                e.Handled = true; // Mencegah efek tambahan
            }
            else if (e.KeyCode == Keys.Up)
            {
                // Jika sudah di baris pertama, kembali ke baris terakhir
                int rowCount = Data_Barang.Rows.Count;
                int currentIndex = Data_Barang.CurrentCell.RowIndex;
                int previousIndex = (currentIndex - 1 + rowCount) % rowCount;
                Data_Barang.CurrentCell = Data_Barang.Rows[previousIndex].Cells[0];
                e.Handled = true; // Mencegah efek tambahan
            }
        }

        private void Data_Barang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Pastikan baris yang diklik valid
            {
                // Ambil id_barang dari kolom pertama yang diklik
                string idBarang = Data_Barang.Rows[e.RowIndex].Cells[0].Value.ToString();

                // Kirim idMember ke TextIdMember di form DataTransaksi
                DataTransaksi dataTransaksi = Application.OpenForms.OfType<DataTransaksi>().FirstOrDefault();
                if (dataTransaksi != null)
                {
                    dataTransaksi.SetTextIdBarang(idBarang);
                }
                else
                {
                    MessageBox.Show("Form DataTransaksi tidak ditemukan.");
                }

                // Tutup form SearchMember
                this.Close();
            }
        }

        private void SearchBarang_Load(object sender, EventArgs e)
        {
            LoadDataToDataGridView("");
        }

        private void LoadDataToDataGridView(string searchKeyword)
        {
            // Kosongkan DataGridView terlebih dahulu
            Data_Barang.Rows.Clear();

            // Tulis kueri SQL Anda untuk mengambil data dari nilai
            string query = "SELECT id_barang, nama_barang FROM tb_barang " +
                            "WHERE id_barang LIKE @search OR nama_barang LIKE @search";

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
                            string id = reader["id_barang"].ToString();
                            string nama = reader["nama_barang"].ToString();

                            // Menambahkan baris ke DataGridView
                            Data_Barang.Rows.Add(id, nama);
                        }
                    }
                }
            }
        }
    }
}