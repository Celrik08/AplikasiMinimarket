﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace AplikasiMinimarket
{
    public partial class DataTransaksi : Form
    {
        private List<Member> members = new List<Member>(); // Menyimpan daftar anggota untuk pencarian
        private List<Barang> barangList = new List<Barang>(); // Menyimpan daftar barang untuk pencarian
        private long currentCount = 1;
        private int roleId;
        private string loggedInUsername;
        private string loggedInUserId;
        private int selectedRowIndex = -1;
        private System.Windows.Forms.Timer timer;
        private bool isHandlingTextChanged = false; // Flag untuk menghindari loop rekrusif
        private bool isSaveButtonClicked = false; // Flag untuk menandakan jika tombol simpan sudah ditekan
        public DataTransaksi(int roleId, string loggedInUsername, string loggedInUserId)
        {
            InitializeComponent();
            this.roleId = roleId;
            this.loggedInUsername = loggedInUsername;
            this.loggedInUserId = loggedInUserId;
        }

        private void noOtomatis()
        {
            string query = "SELECT * FROM tb_transaksi WHERE id_transaksi IN (SELECT MAX(id_transaksi) FROM tb_transaksi)";
            string urutanKode;
            long hitung;

            using (SqlCommand cmd = new SqlCommand(query, Connect.conn))
            {
                Connect.conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read() && dr.HasRows)
                {
                    hitung = long.Parse(dr.GetString(0).Substring(dr.GetString(0).Length - 9));
                    // Jika tombol ditekan, tambahkan 1
                    urutanKode = "TRS" + DateTime.Now.ToString("ddMMyy") + (hitung + currentCount).ToString("D3");
                }
                else
                {
                    // Jika tidak ada data sebelumnya
                    urutanKode = "TRS" + DateTime.Now.ToString("ddMMyy") + "001";
                }
                dr.Close();
                Connect.conn.Close();
            }

            TextTransaksi.Text = urutanKode;
        }

        private void UpdateTextJam(Object sender, EventArgs e)
        {
            // Tentukan zona Waktu Indonesia Tengah (WITA)
            TimeZoneInfo witaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");
            DateTime witaTime = TimeZoneInfo.ConvertTime(DateTime.Now, witaTimeZone);

            // Pastikan kontrol TextJam ada di form dan sesuai dengan penamaannya
            TextJam.Text = witaTime.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
        }

        private void InitializeTime()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // 1 detik
            timer.Tick += new EventHandler(UpdateTextJam);
            timer.Start();
        }

        private void PerformMember()
        {
            ComboMember.Items.Clear();
            members.Clear();

            using (SqlCommand cmd = new SqlCommand("SELECT id_member, nama_member FROM tb_member", Connect.conn))
            {
                Connect.conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Member member = new Member
                    {
                        IdMember = reader["id_member"].ToString(),
                        NamaMember = reader["nama_member"].ToString()
                    };
                    members.Add(member);
                    ComboMember.Items.Add(member); // Hanya menampilkan IdMember
                }
                reader.Close();
                Connect.conn.Close();
            }
        }

        private void PerformBarang()
        {
            ComboBarang.Items.Clear();
            barangList.Clear();

            using (SqlCommand cmd = new SqlCommand("SELECT id_barang, nama_barang, harga_satuan FROM tb_barang", Connect.conn))
            {
                Connect.conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Barang barang = new Barang
                    {
                        IdBarang = reader["id_barang"].ToString(),
                        NamaBarang = reader["nama_barang"].ToString(),
                        HargaBarang = (int)reader["harga_satuan"]
                    };
                    barangList.Add(barang);
                    ComboBarang.Items.Add(barang.IdBarang); // Menampilkan id_barang di ComboBox
                }
                reader.Close();
                Connect.conn.Close();
            }
        }

        private void ComboMember_TextChanged(object sender, EventArgs e)
        {
            if (isHandlingTextChanged || isSaveButtonClicked) return;

            isHandlingTextChanged = true;

            int cursorPosition = ComboMember.SelectionStart;
            string currentText = ComboMember.Text;

            // Filter hanya berdasarkan IdMember
            var filteredMembers = members.FindAll(m =>
                m.IdMember.ToUpper().Contains(currentText.ToUpper()));

            if (filteredMembers.Count == 1 &&
                filteredMembers[0].IdMember.Equals(currentText, StringComparison.OrdinalIgnoreCase))
            {
                ComboMember.DroppedDown = false;
            }
            else
            {
                ComboMember.Items.Clear();
                foreach (var member in filteredMembers)
                {
                    ComboMember.Items.Add(member); // Tetap menampilkan IdMember
                }

                ComboMember.DroppedDown = true;
            }

            ComboMember.Text = currentText;
            ComboMember.SelectionStart = cursorPosition;

            isHandlingTextChanged = false;
        }


        private void ComboBarang_TextChanged(object sender, EventArgs e)
        {
            if (isHandlingTextChanged || isSaveButtonClicked) return;

            isHandlingTextChanged = true;

            int cursorPosition = ComboBarang.SelectionStart;
            string currentText = ComboBarang.Text;

            var filteredBarangs = barangList.FindAll(b =>
                b.IdBarang.ToUpper().Contains(currentText.ToUpper()) ||
                b.NamaBarang.ToUpper().Contains(currentText.ToUpper()));

            if (filteredBarangs.Count == 1 &&
                (filteredBarangs[0].IdBarang.Equals(currentText, StringComparison.OrdinalIgnoreCase) ||
                 filteredBarangs[0].NamaBarang.Equals(currentText, StringComparison.OrdinalIgnoreCase)))
            {
                ComboBarang.DroppedDown = false;
            }
            else
            {
                ComboBarang.Items.Clear();
                foreach (var barang in filteredBarangs)
                {
                    ComboBarang.Items.Add(barang.IdBarang); // Tetap menampilkan id_barang
                }
                ComboBarang.DroppedDown = true;
            }

            ComboBarang.Text = currentText;
            ComboBarang.SelectionStart = cursorPosition;

            isHandlingTextChanged = false;
        }

        private void ComboMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isHandlingTextChanged || isSaveButtonClicked) return;

            isHandlingTextChanged = true;

            if (ComboMember.SelectedItem is Member selectedMember)
            {
                // Menampilkan NamaMember di TextMember
                TextMember.Text = selectedMember.NamaMember;

                // Menutup dropdown ComboMember setelah item dipilih
                ComboMember.DroppedDown = false;
            }

            isHandlingTextChanged = false;
        }


        private void ComboBarang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBarang.SelectedItem is string selectedId)
            {
                var selectedBarang = barangList.Find(b => b.IdBarang == selectedId);
                if (selectedBarang != null)
                {
                    TextNama.Text = selectedBarang.NamaBarang;
                    TextHarga.Text = "Rp. " + selectedBarang.HargaBarang.ToString("N0", CultureInfo.InvariantCulture);
                }
            }
        }

        private void ComboMember_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Jika yang ditekan adalah tombol Enter
            if (e.KeyChar == (char)13)
            {
                e.Handled = true; // Menghentikan pengolahan input lebih lanjut
                ComboBarang.Focus(); // Berpindah ke ComboBarang
            }
        }

        private void ComboBarang_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Jika yang ditekan adalah tombol Enter
            if (e.KeyChar == (char)13)
            {
                e.Handled = true; // Menghentikan pengolahan input lebih lanjut
                TextTotal1.Focus(); // Berpindah ke TextTotal1
            }
            // Memastikan hanya angka dan karakter kontrol (seperti backspace) yang bisa dimasukkan
            else if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Mencegah input selain angka dan kontrol
            }
        }

        private void TextTotal1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Periksa jika tombol yang ditekan adalah Enter
            if (e.KeyChar == (char)13) // Jika tekan Enter
            {
                e.Handled = true; // Menghentikan pengolahan input lebih lanjut

                // Ambil nilai dari TextHarga dan TextTotal1
                string hargaText = TextHarga.Text;
                string totalText = TextTotal1.Text;

                // Menghapus "Rp." dan spasi dari harga
                hargaText = hargaText.Replace("Rp. ", "").Replace(" ", "");

                // Pastikan TextTotal1 berisi angka
                if (decimal.TryParse(hargaText, out decimal harga) &&
                    int.TryParse(totalText, out int jumlah))
                {
                    // Hitung total
                    decimal totalHarga = harga * jumlah;

                    // Tambahkan "Rp." ke hasil dan tampilkan di TextSub
                    TextSub.Text = "Rp. " + totalHarga;
                }
                else
                {
                    // Jika input tidak valid, tampilkan pesan error atau reset TextSub
                    TextSub.Text = "Input tidak valid!";
                }
            }
            // Memastikan hanya angka dan karakter kontrol (seperti backspace) yang bisa dimasukkan
            else if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Mencegah input selain angka dan kontrol
            }
        }


        private void BtnKlik_Click(object sender, EventArgs e)
        {
            PerfromTransaksi();
        }

        private void PerfromTransaksi()
        {
            if (ComboMember.SelectedItem == null || ComboBarang.SelectedItem == null ||
                string.IsNullOrWhiteSpace(TextTotal1.Text) || string.IsNullOrWhiteSpace(TextTotal2.Text))
            {
                MessageBox.Show("Id Member, Kode Barang, dan Total jangan di kosongin", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Member selectedMember = ComboMember.SelectedItem as Member;
            string selectedBarangId = ComboBarang.SelectedItem as string;

            if (selectedMember != null && selectedBarangId != null)
            {
                using (SqlCommand checkTransaksiCmd = new SqlCommand("SELECT COUNT(*) FROM tb_transaksi WHERE id_transaksi = @id_transaksi", Connect.conn))
                {
                    checkTransaksiCmd.Parameters.AddWithValue("@id_transaksi", TextTransaksi.Text);
                    Connect.conn.Open();
                    int count = (int)checkTransaksiCmd.ExecuteScalar();

                    if (count == 0)
                    {
                        // Insert ke tabel tb_transaksi
                        using (SqlCommand insertTransaksiCmd = new SqlCommand("INSERT INTO tb_transaksi (id_transaksi, tanggal_transaksi, id_user, grand_total, id_status_transaksi, id_member) VALUES (@id_transaksi, @tanggal_transaksi, @id_user, @grand_total, @id_status_transaksi, @id_member)", Connect.conn))
                        {
                            insertTransaksiCmd.Parameters.AddWithValue("@id_transaksi", TextTransaksi.Text);
                            DateTime tanggalTransaksi = DateTime.ParseExact(TextTanggal.Text + " " + TextJam.Text, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                            insertTransaksiCmd.Parameters.AddWithValue("@tanggal_transaksi", tanggalTransaksi);
                            insertTransaksiCmd.Parameters.AddWithValue("@id_user", loggedInUserId);

                            // Menghapus "Rp." dan spasi sebelum memasukkan nilai ke database
                            string grandTotalText = TextTotal2.Text.Replace("Rp. ", "").Replace(" ", "");
                            insertTransaksiCmd.Parameters.AddWithValue("@grand_total", decimal.Parse(grandTotalText));
                            insertTransaksiCmd.Parameters.AddWithValue("@id_status_transaksi", 0);
                            insertTransaksiCmd.Parameters.AddWithValue("@id_member", selectedMember.IdMember);
                            insertTransaksiCmd.ExecuteNonQuery();
                        }
                    }

                    // Insert ke tabel tb_detail_transaksi
                    using (SqlCommand insertDetailCmd = new SqlCommand("INSERT INTO tb_detail_transaksi (id_transaksi, id_barang, harga_satuan, qty, sub_total) VALUES (@id_transaksi, @id_barang, @harga_satuan, @qty, @sub_total)", Connect.conn))
                    {
                        insertDetailCmd.Parameters.AddWithValue("@id_transaksi", TextTransaksi.Text);
                        insertDetailCmd.Parameters.AddWithValue("@id_barang", selectedBarangId);

                        // Menghapus "Rp." dan spasi sebelum memasukkan nilai ke database
                        string hargaSatuanText = TextHarga.Text.Replace("Rp. ", "").Replace(" ", "");
                        insertDetailCmd.Parameters.AddWithValue("@harga_satuan", decimal.Parse(hargaSatuanText));

                        insertDetailCmd.Parameters.AddWithValue("@qty", int.Parse(TextTotal1.Text));

                        // Menghapus "Rp." dan spasi dari sub_total sebelum menyimpannya
                        string subTotalText = TextSub.Text.Replace("Rp. ", "").Replace(" ", "");
                        insertDetailCmd.Parameters.AddWithValue("@sub_total", decimal.Parse(subTotalText));

                        insertDetailCmd.ExecuteNonQuery();
                    }

                    // Update stok di tb_buku
                    using (SqlCommand updateCmd = new SqlCommand("UPDATE tb_barang SET total_stok = total_stok - @qty WHERE id_barang = @id_barang", Connect.conn))
                    {
                        updateCmd.Parameters.AddWithValue("@qty", int.Parse(TextTotal1.Text));
                        updateCmd.Parameters.AddWithValue("@id_barang", selectedBarangId);
                        updateCmd.ExecuteNonQuery();
                    }

                    // Reset komponen setelah transaksi
                    LoadDataToDataGridView();
                    ResetKomponen();
                    Connect.conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Mohon lengkapi semua data.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetKomponen()
        {
            ComboBarang.SelectedIndex = -1;
            TextNama.Clear();
            TextHarga.Clear();
            TextTotal1.Clear();
            TextSub.Clear();
        }

        private void DataTransaksi_Load(object sender, EventArgs e)
        {
            noOtomatis();
            // Set tanggal menggunakan zona WITA
            TimeZoneInfo witaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");
            DateTime witaDate = TimeZoneInfo.ConvertTime(DateTime.Now, witaTimeZone);
            TextTanggal.Text = witaDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            // Panggil fungsi ini agar timer aktif
            InitializeTime(); // Panggil fungsi ini agar timer aktif
            PerformMember();
            TextUser.Text = $"{loggedInUserId} - {loggedInUsername}";
            PerformBarang();
            TextTotal2.Text = "Rp. " + "0";
            LoadDataToDataGridView();
        }

        private void LoadDataToDataGridView()
        {
            // Kosongkann DataGridView terlebih dahulu
            Data_Transaksi.Rows.Clear();

            // Tulis kueri SQL Anda untuk mengambil data dari nilai
            string query = "SELECT tb_barang.nama_barang, tb_detail_transaksi.harga_satuan, tb_detail_transaksi.qty, tb_detail_transaksi.sub_total " +
                            "FROM tb_detail_transaksi " +
                            "JOIN tb_barang ON tb_detail_transaksi.id_barang = tb_barang.id_barang";

            // Buat SqlCommand untuk menjalankan kueri
            using (SqlCommand cmd = new SqlCommand(query, Connect.conn))
            {
                Connect.conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string nama = reader["nama_barang"].ToString();
                    string harga = reader["harga_satuan"].ToString();
                    string qty = reader["qty"].ToString();
                    string sub = reader["sub_total"].ToString();
                    Data_Transaksi.Rows.Add(nama, harga, qty, sub);
                }
                reader.Close();
                Connect.conn.Close();
            }
        }
    }
}