using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
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
        private List<Barang> barangs = new List<Barang>(); // Menyimpan daftar barang untuk pencarian
        private long currentCount = 1;
        private int roleId;
        private string loggedInUsername;
        private string loggedInUserId;
        private System.Windows.Forms.Timer timer;
        private bool isHandlingTextChanged = false; // Flag untuk menghindari loop rekrusif
        private bool isSaveButtonClicked = false; // Flag untuk menandakan jika tombol simpan sudah ditekan
        int previousQty = 0;

        public DataTransaksi(int roleId, string loggedInUsername, string loggedInUserId)
        {
            InitializeComponent();
            this.roleId = roleId;
            this.loggedInUsername = loggedInUsername;
            this.loggedInUserId = loggedInUserId;
        }

        private void noOtomatis()
        {
            string query = "SELECT id_transaksi FROM tb_transaksi WHERE id_transaksi LIKE @datePattern ORDER BY id_transaksi DESC";
            string urutanKode;
            long hitung;

            string tanggalSekarang = DateTime.Now.ToString("ddMMyy"); // Format tanggal
            string prefix = "TRS" + tanggalSekarang; // Prefix transaksi

            using (SqlConnection conn = new SqlConnection(Connect.conn.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@datePattern", prefix + "%"); // Cari transaksi dengan format tanggal hari ini
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read() && dr.HasRows)
                        {
                            // Ambil nomor urut terakhir dan tambahkan 1
                            string lastTransaction = dr.GetString(0);
                            string lastNumber = lastTransaction.Substring(prefix.Length); // Ambil 3 digit terakhir
                            hitung = long.Parse(lastNumber) + 1;
                        }
                        else
                        {
                            hitung = 1; // Jika belum ada transaksi, mulai dari 001
                        }
                    }
                }
            }

            urutanKode = prefix + hitung.ToString("D3"); // Format nomor urut menjadi 3 digit
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

        private void TextIdMember_TextChanged(object sender, EventArgs e)
        {
            string inputId = TextIdMember.Text.Trim();

            if (!string.IsNullOrEmpty(inputId))
            {
                using (SqlConnection conn = new SqlConnection(Connect.conn.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT nama_member FROM tb_member WHERE id_member = @id_member";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_member", inputId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            TextMember.Text = reader.Read() ? reader["nama_member"].ToString() : "";
                        }
                    }
                }
            }
            else
            {
                TextMember.Text = "";
            }
        }

        private void BtnTambahMember_Click(object sender, EventArgs e)
        {
            PerfromMember();
        }

        public void SetTextIdMember(string idMember)
        {
            if (TextIdMember != null)
            {
                TextIdMember.Text = idMember;
            }
            else
            {
                // Opsional: Menambahkan log untuk menangani kasus jika TextIdMember belum diinisialisasi
                MessageBox.Show("TextIdMember tidak ditemukan.");
            }
        }

        private void PerfromMember()
        {
            SearchMember searchMember = new SearchMember();
            searchMember.ShowDialog();

            // Fokus ke TextSearchMember jika ada
            if (searchMember.Controls["TextSearchMember"] is TextBox textBox)
            {
                textBox.Focus();
            }
        }

        private void TextIdBarang_TextChanged(object sender, EventArgs e)
        {
            string inputIdBarang = TextIdBarang.Text.Trim();

            if (!string.IsNullOrEmpty(inputIdBarang))
            {
                using (SqlConnection conn = new SqlConnection(Connect.conn.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT nama_barang, harga_satuan, diskon FROM tb_barang WHERE id_barang = @id_barang";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_barang", inputIdBarang);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                TextNama.Text = reader["nama_barang"].ToString();

                                // Ambil harga satuan dan diskon
                                decimal hargaAsli = Convert.ToDecimal(reader["harga_satuan"]);
                                decimal diskon = reader["diskon"] != DBNull.Value ? Convert.ToDecimal(reader["diskon"]) : 0;

                                // Hitung harga setelah diskon
                                decimal hargaSetelahDiskon = hargaAsli - (hargaAsli * diskon / 100);

                                // Format harga dengan "Rp." dan titik pemisah ribuan
                                TextHarga.Text = string.Format("Rp. {0:N0}", hargaSetelahDiskon);
                            }
                            else
                            {
                                TextNama.Text = "";
                                TextHarga.Text = "";
                            }
                        }
                    }
                }
            }
            else
            {
                TextNama.Text = "";
                TextHarga.Text = "";
            }
        }

        private void BtnTambahBarang_Click(object sender, EventArgs e)
        {
            PerfromBarang();
        }

        public void SetTextIdBarang(string idBarang)
        {
            if (TextIdBarang != null)
            {
                TextIdBarang.Text = idBarang;
            }
            else
            {
                // Opsional: Menambahkan log untuk menangani kasus jika TextIdMember belum diinisialisasi
                MessageBox.Show("TextIdBarang tidak ditemukan.");
            }
        }

        private void PerfromBarang()
        {
            SearchBarang searchBarang = new SearchBarang();
            searchBarang.ShowDialog();

            // Fokus ke TextSearchMember jika ada
            if (searchBarang.Controls["TextSearchBarang"] is TextBox textBox)
            {
                textBox.Focus();
            }
        }

        private void TextQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Periksa jika tombol yang ditekan adalah Enter
            if (e.KeyChar == (char)13) // Jika tekan Enter
            {
                e.Handled = true; // Menghentikan pengolahan input lebih lanjut

                // Ambil nilai dari TextHarga dan TextTotal1
                string hargaText = TextHarga.Text;
                string totalText = TextQty.Text;

                // Menghapus "Rp.", spasi, dan semua titik pemisah ribuan dari harga
                hargaText = hargaText.Replace("Rp.", "").Replace(" ", "").Replace(".", "");

                // Pastikan TextTotal1 berisi angka
                if (int.TryParse(hargaText, out int harga) &&
                    int.TryParse(totalText, out int jumlah))
                {
                    // Hitung total
                    int totalHarga = harga * jumlah;

                    // Format totalHarga dengan "Rp.", spasi, dan titik pemisah ribuan
                    string formattedTotal = "Rp. " + totalHarga.ToString("N0");

                    // Tambahkan "Rp." ke hasil dan tampilkan di TextSub
                    TextSub.Text = formattedTotal;

                    TextSub.Focus();
                }
                else
                {
                    // Jika input tidak valid, tampilkan pesan error atau reset TextSub
                    TextSub.Text = "Input tidak valid!";
                }
            }
            // Memastikan hanya angka dan karakter kontrol (seperti backspace) yang bisa dimasukkan, dan mencegah angka '0' di awal input
            else if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) || TextQty.Text.Length == 0 && e.KeyChar == '0')
            {
                e.Handled = true; // Mencegah input selain angka dan kontrol
            }
        }

        private void TextSub_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Jika yang di tekan adalah tombol enter
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                PerfromDetailTransaksi();
            }
        }

        private void BtnKlik_Click(object sender, EventArgs e)
        {
            PerfromDetailTransaksi();
        }

        private void PerfromDetailTransaksi()
        {
            // Cegah eksekusi simultan
            if (isHandlingTextChanged || isSaveButtonClicked)
                return;

            isSaveButtonClicked = true; // Tandai bahwa tombol sedang diproses

            // Validasi input
            if (string.IsNullOrWhiteSpace(TextIdMember.Text) || string.IsNullOrWhiteSpace(TextMember.Text) || string.IsNullOrWhiteSpace(TextIdBarang.Text) || string.IsNullOrWhiteSpace(TextNama.Text) ||
                string.IsNullOrWhiteSpace(TextQty.Text) || string.IsNullOrWhiteSpace(TextSub.Text))
            {
                MessageBox.Show("Id Member, Kode Barang, Total, dan Sub Total jangan dikosongkan, dan data harus valid", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isSaveButtonClicked = false; // Reset status tombol
                return;
            }

            string selectedMember = TextIdMember.Text;
            string selectedBarangId = TextIdBarang.Text;

            if (selectedMember != null && selectedBarangId != null)
            {
                using (SqlConnection conn = new SqlConnection(Connect.conn.ConnectionString))
                {
                    conn.Open();

                    // Periksa apakah id_transaksi sudah ada di tb_transaksi
                    using (SqlCommand checkTransaksiCmd = new SqlCommand("SELECT COUNT(*) FROM tb_transaksi WHERE id_transaksi = @id_transaksi", conn))
                    {
                        checkTransaksiCmd.Parameters.AddWithValue("@id_transaksi", TextTransaksi.Text);
                        int count = (int)checkTransaksiCmd.ExecuteScalar();

                        if (count == 0)
                        {
                            // Insert ke tabel tb_transaksi jika belum ada
                            using (SqlCommand insertTransaksiCmd = new SqlCommand("INSERT INTO tb_transaksi (id_transaksi, tanggal_transaksi, id_user, grand_total, id_status_transaksi, id_member) VALUES (@id_transaksi, @tanggal_transaksi, @id_user, @grand_total, @id_status_transaksi, @id_member)", conn))
                            {
                                insertTransaksiCmd.Parameters.AddWithValue("@id_transaksi", TextTransaksi.Text);
                                DateTime tanggalTransaksi = DateTime.ParseExact(TextTanggal.Text + " " + TextJam.Text, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                                insertTransaksiCmd.Parameters.AddWithValue("@tanggal_transaksi", tanggalTransaksi);
                                insertTransaksiCmd.Parameters.AddWithValue("@id_user", loggedInUserId);

                                string grandTotalText = TextTotal1.Text.Replace("Rp.", "").Replace(" ", "").Replace(".", "");
                                insertTransaksiCmd.Parameters.AddWithValue("@grand_total", int.Parse(grandTotalText));
                                insertTransaksiCmd.Parameters.AddWithValue("@id_status_transaksi", 0);
                                insertTransaksiCmd.Parameters.AddWithValue("@id_member", selectedMember);

                                insertTransaksiCmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Periksa apakah id_barang sudah ada di tb_detail_transaksi untuk id_transaksi yang sama
                    using (SqlCommand checkDetailTransaksiCmd = new SqlCommand("SELECT COUNT(*) FROM tb_detail_transaksi WHERE id_transaksi = @id_transaksi AND id_barang = @id_barang", conn))
                    {
                        checkDetailTransaksiCmd.Parameters.AddWithValue("@id_transaksi", TextTransaksi.Text);
                        checkDetailTransaksiCmd.Parameters.AddWithValue("@id_barang", selectedBarangId);
                        int countDetail = (int)checkDetailTransaksiCmd.ExecuteScalar();

                        if (countDetail > 0)
                        {
                            // Jika id_barang sudah ada, update qty dan sub_total
                            using (SqlCommand updateDetailTransaksiCmd = new SqlCommand("UPDATE tb_detail_transaksi SET qty = qty + @qty, sub_total = sub_total + @sub_total WHERE id_transaksi = @id_transaksi AND id_barang = @id_barang", conn))
                            {
                                updateDetailTransaksiCmd.Parameters.AddWithValue("@qty", int.Parse(TextQty.Text)); // Menambah qty
                                updateDetailTransaksiCmd.Parameters.AddWithValue("@sub_total", int.Parse(TextSub.Text.Replace("Rp.", "").Replace(" ", "").Replace(".", ""))); // Menambah sub_total
                                updateDetailTransaksiCmd.Parameters.AddWithValue("@id_transaksi", TextTransaksi.Text);
                                updateDetailTransaksiCmd.Parameters.AddWithValue("@id_barang", selectedBarangId);

                                updateDetailTransaksiCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Jika id_barang belum ada, insert data baru ke tb_detail_transaksi
                            using (SqlCommand insertDetailTransaksiCmd = new SqlCommand("INSERT INTO tb_detail_transaksi (id_transaksi, id_barang, harga_satuan, qty, sub_total) VALUES (@id_transaksi, @id_barang, @harga_satuan, @qty, @sub_total)", conn))
                            {
                                insertDetailTransaksiCmd.Parameters.AddWithValue("@id_transaksi", TextTransaksi.Text);
                                insertDetailTransaksiCmd.Parameters.AddWithValue("@id_barang", selectedBarangId);
                                insertDetailTransaksiCmd.Parameters.AddWithValue("@harga_satuan", int.Parse(TextHarga.Text.Replace("Rp.", "").Replace(" ", "").Replace(".", "")));
                                insertDetailTransaksiCmd.Parameters.AddWithValue("@qty", int.Parse(TextQty.Text));
                                insertDetailTransaksiCmd.Parameters.AddWithValue("@sub_total", int.Parse(TextSub.Text.Replace("Rp.", "").Replace(" ", "").Replace(".", "")));

                                insertDetailTransaksiCmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Mengurangi stok barang
                    using (SqlCommand updateStokCmd = new SqlCommand("UPDATE tb_barang SET total_stok = total_stok - @qty WHERE id_barang = @id_barang", conn))
                    {
                        updateStokCmd.Parameters.AddWithValue("@qty", int.Parse(TextQty.Text));
                        updateStokCmd.Parameters.AddWithValue("@id_barang", selectedBarangId);

                        updateStokCmd.ExecuteNonQuery();
                    }

                    LoadDataToDataGridView();
                    ResetDetailTransaksi();
                }
            }

            MessageBox.Show("Transaksi berhasil disimpan!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            isSaveButtonClicked = false; // Reset status klik tombol
        }

        private void ResetDetailTransaksi()
        {
            TextIdBarang.Clear();
            TextNama.Clear();
            TextHarga.Clear();
            TextQty.Clear();
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
            TextUser.Text = $"{loggedInUserId} - {loggedInUsername}";
            TextTotal1.Text = "Rp. " + 0;
            LoadDataToDataGridView();
            LoadDataToDipending();
            TextJumlah.Text = "Rp. ";
        }

        private void LoadDataToDataGridView()
        {
            Data_Transaksi.Rows.Clear();
            int totalSub = 0;

            // Pastikan hanya menampilkan data untuk transaksi terbaru (TextTransaksi.Text)
            string query = "SELECT tb_detail_transaksi.id_detail_transaksi, tb_detail_transaksi.id_barang, " +
                            "tb_barang.nama_barang, tb_detail_transaksi.harga_satuan, tb_detail_transaksi.qty, " +
                            "tb_detail_transaksi.sub_total " +
                            "FROM tb_detail_transaksi " +
                            "JOIN tb_barang ON tb_detail_transaksi.id_barang = tb_barang.id_barang " +
                            "WHERE tb_detail_transaksi.id_transaksi = @id_transaksi"; // Filter transaksi terbaru

            using (SqlConnection conn = new SqlConnection(Connect.conn.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Menggunakan parameterized query untuk menghindari SQL Injection
                    cmd.Parameters.AddWithValue("@id_transaksi", TextTransaksi.Text);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["id_detail_transaksi"].ToString();
                            string barang = reader["id_barang"].ToString();
                            string nama = reader["nama_barang"].ToString();

                            // Ambil nilai harga dan sub_total, lalu format
                            if (int.TryParse(reader["harga_satuan"].ToString(), out int harga))
                            {
                                string formattedHarga = "Rp. " + harga.ToString("N0"); // Format dengan titik ribuan
                                if (int.TryParse(reader["qty"].ToString(), out int qty))
                                {
                                    // Ambil sub_total dan format
                                    if (int.TryParse(reader["sub_total"].ToString(), out int sub))
                                    {
                                        totalSub += sub;
                                        string formattedSub = "Rp. " + sub.ToString("N0");
                                        Data_Transaksi.Rows.Add(id, barang, nama, formattedHarga, qty, formattedSub);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Tampilkan total sub_total di TextTotal2
            TextTotal1.Text = "Rp. " + totalSub.ToString("N0");
        }

        private void Data_Transaksi_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Pastikan hanya kolom qty (kolom ke-4) yang bisa diedit
            if (e.ColumnIndex != 4)
            {
                e.Cancel = true; // Batalkan pengeditan
                return;
            }

            // Periksa apakah baris kosong (tidak ada nilai di kolom pertama)
            if (Data_Transaksi.Rows[e.RowIndex].Cells[0].Value == null ||
                string.IsNullOrWhiteSpace(Data_Transaksi.Rows[e.RowIndex].Cells[0].Value.ToString()))
            {
                e.Cancel = true; // Batalkan pengeditan
                return;
            }

            // Pastikan pengeditan terjadi di kolom qty
            string qtyText = Data_Transaksi.Rows[e.RowIndex].Cells[4].Value?.ToString() ?? "0";
            if (int.TryParse(qtyText, out int qty))
            {
                previousQty = qty; // Simpan nilai qty sebelumnya
            }
            else
            {
                previousQty = 0; // Default jika parsing gagal
            }
        }

        private void Data_Transaksi_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Pastikan perubahan terjadi di kolom qty (kolom ke-4)
            if (e.RowIndex >= 0 && e.ColumnIndex == 4)
            {
                // Ambil data dari sel yang diedit
                string qtyText = Data_Transaksi.Rows[e.RowIndex].Cells[4].Value?.ToString();

                // Periksa jika kolom qty kosong
                if (string.IsNullOrWhiteSpace(qtyText))
                {
                    // Tampilkan pesan kesalahan
                    MessageBox.Show("Kolom Total jangan dikosongkan!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Kembalikan nilai sebelumnya ke sel
                    Data_Transaksi.Rows[e.RowIndex].Cells[4].Value = previousQty;

                    return; // Hentikan proses lebih lanjut
                }

                // Ambil data lainnya
                string idDetail = Data_Transaksi.Rows[e.RowIndex].Cells[0].Value.ToString(); // ID transaksi
                string hargaText = Data_Transaksi.Rows[e.RowIndex].Cells[3].Value.ToString().Replace("Rp. ", "").Replace(".", ""); // Harga satuan
                string idBarang = Data_Transaksi.Rows[e.RowIndex].Cells[1].Value.ToString(); // ID Barang

                // Validasi input qty dan harga
                if (int.TryParse(qtyText, out int qty) && int.TryParse(hargaText, out int harga))
                {
                    // Hitung subtotal
                    int subTotal = harga * qty;

                    // Perbarui tampilan subtotal di DataGridView
                    Data_Transaksi.Rows[e.RowIndex].Cells[5].Value = "Rp. " + subTotal.ToString("N0");

                    // Perbarui database untuk transaksi
                    UpdateDatabase(idDetail, harga, qty, subTotal);

                    // Update stok di tabel tb_barang
                    UpdateStokBarang(idBarang, qty);

                    // Refresh total keseluruhan
                    RefreshTotalSub();

                    // Reset previousQty setelah selesai
                    previousQty = 0;
                }
                else
                {
                    MessageBox.Show("Input tidak valid. Pastikan nilai qty dan harga benar.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Kembalikan nilai sebelumnya jika input tidak valid
                    Data_Transaksi.Rows[e.RowIndex].Cells[4].Value = previousQty;
                }
            }
        }

        private void Data_Transaksi_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (Data_Transaksi.CurrentCell.ColumnIndex == 4) // Hanya untuk kolom qty
            {
                TextBox tb = e.Control as TextBox;

                if (tb != null)
                {
                    // Hapus event handler sebelumnya untuk menghindari duplikasi
                    tb.KeyPress -= QtyColumn_KeyPress;

                    // Tambahkan event handler untuk validasi input
                    tb.KeyPress += QtyColumn_KeyPress;
                }
            }
        }

        private void QtyColumn_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Periksa apakah karakter yang ditekan adalah angka
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) // Bukan angka atau kontrol
            {
                e.Handled = true; // Mencegah input
            }
            else if (e.KeyChar == '0' && ((TextBox)sender).Text.Length == 0) // Cegah angka 0 sebagai karakter pertama
            {
                e.Handled = true; // Mencegah input
            }
            else if (char.IsWhiteSpace(e.KeyChar)) // Cegah spasi
            {
                e.Handled = true; // Mencegah input
            }
        }

        private void UpdateDatabase(string idDetail, int harga, int qty, int subTotal)
        {
            string query = "UPDATE tb_detail_transaksi SET qty = @qty, sub_total = @subTotal WHERE id_detail_transaksi = @idDetail";

            using (SqlConnection conn = new SqlConnection(Connect.conn.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@qty", qty);
                    cmd.Parameters.AddWithValue("@subTotal", subTotal);
                    cmd.Parameters.AddWithValue("@idDetail", idDetail);

                    cmd.ExecuteNonQuery(); // Jalankan perintah SQL
                }
            }
        }

        private void UpdateStokBarang(string idBarang, int qty)
        {
            string query = @"
                UPDATE tb_barang
                SET 
                total_stok = total_stok + @previousQty - @qty
                WHERE id_barang = @idBarang";

            using (SqlConnection conn = new SqlConnection(Connect.conn.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@previousQty", previousQty); // Stok sebelum edit
                    cmd.Parameters.AddWithValue("@qty", qty); // Stok setelah edit
                    cmd.Parameters.AddWithValue("@idBarang", idBarang); // ID Barang

                    cmd.ExecuteNonQuery(); // Jalankan perintah SQL
                }
            }
        }

        private void RefreshTotalSub()
        {
            int totalSub = 0;

            foreach (DataGridViewRow row in Data_Transaksi.Rows)
            {
                if (row.Cells[5].Value != null)
                {
                    string subText = row.Cells[5].Value.ToString().Replace("Rp.", "").Replace(" ", "").Replace(".", ""); // Subtotal tanpa format Rp
                    if (int.TryParse(subText, out int sub))
                    {
                        totalSub += sub; // Tambahkan subtotal
                    }
                }
            }

            // Tampilkan total ke TextTotal2
            TextTotal1.Text = "Rp. " + totalSub.ToString("N0");
        }

        private void Data_Transaksi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == Data_Transaksi.Columns["Hapus"].Index && e.RowIndex >= 0)
            {
                // Cek apakah baris memiliki data lengkap
                string idDetail = Data_Transaksi.Rows[e.RowIndex].Cells[0].Value?.ToString();
                string idBarang = Data_Transaksi.Rows[e.RowIndex].Cells[1].Value?.ToString();
                string qtyText = Data_Transaksi.Rows[e.RowIndex].Cells[4].Value?.ToString();

                if (string.IsNullOrEmpty(idDetail) || string.IsNullOrEmpty(idBarang) || string.IsNullOrEmpty(qtyText))
                {
                    // Tidak melakukan apa-apa jika salah satu kolom di baris kosong
                    return;
                }

                // Cek apakah proses sedang berjalan
                if (isSaveButtonClicked)
                {
                    MessageBox.Show("Proses sedang berjalan. Mohon tunggu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tampilkan dialog konfirmasi
                DialogResult result = MessageBox.Show("Anda yakin ingin menghapus data ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Tandai bahwa tombol hapus sudah ditekan
                    isSaveButtonClicked = true;

                    if (int.TryParse(qtyText, out int qty))
                    {
                        using (SqlConnection conn = new SqlConnection(Connect.conn.ConnectionString))
                        {
                            conn.Open();

                            // Perbarui total stok di tb_barang
                            using (SqlCommand cmdUpdateStok = conn.CreateCommand())
                            {
                                cmdUpdateStok.CommandText = @"
                                UPDATE tb_barang
                                SET total_stok = total_stok + @qty
                                WHERE id_barang = @id_barang";
                                cmdUpdateStok.Parameters.AddWithValue("@qty", qty);
                                cmdUpdateStok.Parameters.AddWithValue("@id_barang", idBarang);
                                cmdUpdateStok.ExecuteNonQuery();
                            }

                            // Hapus data dari tb_detail_transaksi
                            using (SqlCommand cmdDeleteDetail = conn.CreateCommand())
                            {
                                cmdDeleteDetail.CommandText = "DELETE FROM tb_detail_transaksi WHERE id_detail_transaksi = @id_detail";
                                cmdDeleteDetail.Parameters.AddWithValue("@id_detail", idDetail);
                                cmdDeleteDetail.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Data berhasil dihapus!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Muat ulang data ke DataGridView
                        LoadDataToDataGridView();
                    }

                    // Nonaktifkan flag setelah proses selesai
                    isSaveButtonClicked = false;
                }
            }
        }

        private void BtnDipending_Click(object sender, EventArgs e)
        {
            PerfromDipending();
        }

        private void PerfromDipending()
        {
            // Cegah eksekusi simultan
            if (isHandlingTextChanged || isSaveButtonClicked)
                return;

            isSaveButtonClicked = true;

            // Validasi untuk memastikan Data_Transaksi tidak kosong
            if (Data_Transaksi.Rows.Count - 1 <= 0)
            {
                MessageBox.Show("Tabel Transaksi harus diisi terlebih dahulu!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isSaveButtonClicked = false;
                return;
            }

            using (SqlConnection conn = new SqlConnection(Connect.conn.ConnectionString))
            {
                conn.Open();

                // Hapus semua data dari Data_Transaksi di DataGridView (tanpa menghapus dari database)
                Data_Transaksi.Rows.Clear();

                // Parse tanggal transaksi dari input pengguna
                DateTime tanggalTransaksi = DateTime.ParseExact(TextTanggal.Text + " " + TextJam.Text, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                // Perbarui status transaksi menjadi 2 (dipending) dan set tanggal transaksi
                using (SqlCommand updateStatusCmd = new SqlCommand("UPDATE tb_transaksi SET tanggal_transaksi = @tanggal_transaksi, id_status_transaksi = @id_status_transaksi WHERE id_transaksi = @id_transaksi", conn))
                {
                    updateStatusCmd.Parameters.AddWithValue("@tanggal_transaksi", tanggalTransaksi);
                    updateStatusCmd.Parameters.AddWithValue("@id_status_transaksi", 2); // Status dipending
                    updateStatusCmd.Parameters.AddWithValue("@id_transaksi", TextTransaksi.Text);

                    updateStatusCmd.ExecuteNonQuery();
                }
            }

            noOtomatis();
            ResetTransaksi();
            LoadDataToDipending();

            MessageBox.Show("Transaksi telah dipending!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            isSaveButtonClicked = false; // Reset status klik tombol
        }

        private void LoadDataToDipending()
        {
            Data_Dipending.Rows.Clear(); // Kosongkan DaftarGridView sebelum mengisi data baru

            string query = "SELECT tb_transaksi.id_transaksi, tb_member.nama_member, tb_status_transaksi.nama_status_transaksi " +
                            "FROM tb_transaksi " +
                            "JOIN tb_member ON tb_transaksi.id_member = tb_member.id_member " +
                            "JOIN tb_status_transaksi ON tb_transaksi.id_status_transaksi = tb_status_transaksi.id_status_transaksi " +
                            "WHERE tb_transaksi.id_status_transaksi = 2"; // Hanya menampilkan data dengan id_status_transaki = 2

            using (SqlConnection conn = new SqlConnection(Connect.conn.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string idTransaksi = reader["id_transaksi"].ToString();
                            string namaMember = reader["nama_member"].ToString();
                            string namaStatus = reader["nama_status_transaksi"].ToString();

                            // Tambahkan data ke DataGridView
                            Data_Dipending.Rows.Add(idTransaksi, namaMember, namaStatus);
                        }
                    }
                }
            }
        }

        private void Data_Dipending_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Cegah eksekusi simultan
            if (isHandlingTextChanged || isSaveButtonClicked)
                return;

            isSaveButtonClicked = true;

            if (e.RowIndex >= 0 && e.RowIndex < Data_Dipending.Rows.Count) // Pastikan baris yang diklik valid
            {
                DataGridViewRow row = Data_Dipending.Rows[e.RowIndex];

                // Pastikan sel tidak null sebelum mengaksesnya
                if (row.Cells[0].Value != null)
                {
                    string selectedIdTransaksi = row.Cells[0].Value.ToString();
                    TextTransaksi.Text = selectedIdTransaksi;

                    // Query yang diperbaiki sesuai format yang diminta
                    string query = "SELECT tb_transaksi.id_member, tb_member.nama_member " +
                                   "FROM tb_transaksi " +
                                   "JOIN tb_member ON tb_transaksi.id_member = tb_member.id_member " +
                                   "WHERE tb_transaksi.id_transaksi = @id_transaksi";

                    using (SqlConnection conn = new SqlConnection(Connect.conn.ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id_transaksi", selectedIdTransaksi);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string idMember = reader["id_member"].ToString();
                                    string namaMember = reader["nama_member"].ToString();

                                    // Menampilkan id_member di TextIdMember (TextBox)
                                    TextIdMember.Text = idMember;

                                    // Menampilkan nama_member di TextMember (TextBox)
                                    TextMember.Text = namaMember;
                                }
                            } // Reader akan otomatis tertutup di sini
                        }
                    }

                    // Panggil ulang LoadDataToDataGridView untuk menampilkan detail transaksi terbaru
                    LoadDataToDataGridView();
                }
                else
                {
                    MessageBox.Show("Data transaksi tidak valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            isSaveButtonClicked = false;
        }

        private void TextTotal1_TextChanged(object sender, EventArgs e)
        {
            // Panggil fungsi untuk menghitung diskon setiap kali teks berubah
            ApplyDiscountBasedOnTotalAndMember();
        }

        private void ApplyDiscountBasedOnTotalAndMember()
        {
            // Ambil id_member dari ComboMember
            string idMember = TextIdMember.Text;

            // Ambil nilai total dari TextTotal2
            string totalText = TextTotal1.Text.Replace("Rp.", "").Replace(" ", "").Replace(".", "");

            if (int.TryParse(totalText, out int totalTransaksi))
            {
                double discount = 0;

                // Periksa diskon berdasarkan id_member dan total transaksi
                if (idMember == "0" || totalTransaksi < 50000)
                {
                    TextDiskon.Text = "Rp. 0";
                    return; // Keluar dari fungsi
                }

                // Periksa diskon berdasarkan total transaksi
                if (totalTransaksi <= 199000)
                {
                    discount = totalTransaksi * 0.02; // Diskon 
                }
                else if (totalTransaksi >= 200000)
                {
                    discount = totalTransaksi * 0.05; // Diskon 5%
                }

                // Hitung total setelah diskon
                double discountedTotal = totalTransaksi - discount;

                // Update TextDiskon
                TextDiskon.Text = "Rp. " + discountedTotal.ToString("N0");
            }
            else
            {
                // Jika id_member adalah 0 atau total transaksi di bawah 50000
                TextDiskon.Text = "Rp. 0";
            }
        }

        private void TextJumlah_TextChanged(object sender, EventArgs e)
        {
            if (isHandlingTextChanged) return;

            isHandlingTextChanged = true;

            string currentText = TextJumlah.Text;

            // Pastikan teks selalu dimulai dengan "Rp. "
            if (!currentText.StartsWith("Rp. "))
            {
                currentText = "Rp. "; // Menambahkan "Rp. " jika belum ada
                TextJumlah.Text = currentText; // Update teks di awal
                TextJumlah.SelectionStart = TextJumlah.Text.Length; // Pindahkan kursor ke akhir
            }

            // Ambil angka setelah "Rp. " dan hapus titik sebelumnya jika ada
            string angkaText = currentText.Replace("Rp.", "").Replace(" ", "").Replace(".", "").Trim();

            // Cek apakah input setelah "Rp. " valid dan tidak diawali dengan 0
            if (angkaText.Length > 0 && angkaText[0] == '0' && angkaText.Length > 1)
            {
                // Jika angka diawali dengan 0, hilangkan 0 tersebut
                angkaText = angkaText.Substring(1);
            }

            // Format ulang angka dengan pemisah ribuan dan menambah "Rp. " di depan
            if (long.TryParse(angkaText, out long angka))
            {
                // Format angka dengan pemisah ribuan
                TextJumlah.Text = "Rp. " + angka.ToString("N0");
                TextJumlah.SelectionStart = TextJumlah.Text.Length; // Pindahkan kursor ke akhir
            }
            else
            {
                TextJumlah.Text = "Rp. "; // Reset jika input bukan angka
                TextJumlah.SelectionStart = TextJumlah.Text.Length;
            }

            isHandlingTextChanged = false;
        }

        private void TextJumlah_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cegah karakter selain angka dan backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Jika bukan angka atau backspace, input dibatalkan
            }

            // Jika angka diawali dengan '0', hanya angka selain 0 yang bisa dimasukkan setelah "Rp. "
            string currentText = TextJumlah.Text;
            if (currentText.Length == 4 && e.KeyChar == '0') // Panjang "Rp. " = 4
            {
                e.Handled = true; // Cegah input angka 0 setelah "Rp. "
            }

            // Logika tambahan untuk menghitung nilai ketika tombol Enter ditekan
            if (e.KeyChar == (char)Keys.Enter) // Periksa apakah tombol Enter ditekan
            {
                // Ambil nilai dari TextJumlah dan TextTotal2
                string jumlahText = TextJumlah.Text.Replace("Rp.", "").Replace(" ", "").Replace(".", "").Trim();
                string total2Text = TextTotal1.Text.Replace("Rp.", "").Replace(" ", "").Replace(".", "").Trim();
                string discountText = TextDiskon.Text.Replace("Rp.", "").Replace(" ", "").Replace(".", "").Trim();

                if (long.TryParse(jumlahText, out long jumlah) && long.TryParse(total2Text, out long total2))
                {
                    long total3;

                    // Tentukan pengurangan berdasarkan nilai TextDiskon
                    if (long.TryParse(discountText, out long diskon) && diskon > 0)
                    {
                        total3 = jumlah - diskon; // Pengurangan dengan diskon
                    }
                    else
                    {
                        total3 = jumlah - total2; // Pengurangan dengan total transaksi
                    }

                    // Tampilkan hasil di TextTotal3
                    TextTotal2.Text = "Rp. " + total3.ToString("N0");

                    TextTotal2.Focus();
                }
                else
                {
                    // Jika parsing gagal, tampilkan Rp. 0 di TextTotal3
                    TextTotal2.Text = "Rp. 0";
                }

                // Cegah bunyi "ding" ketika Enter ditekan
                e.Handled = true;
            }
        }

        private void TextTotal3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Jika yanbg di tekan adalah tombol enter
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                PerfromTransaksi();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            PerfromTransaksi();
        }

        private void PerfromTransaksi()
        {
            // Cegah eksekusi simultan
            if (isHandlingTextChanged || isSaveButtonClicked)
                return;

            isSaveButtonClicked = true;

            // Validasi untuk TextJumlah (tidak boleh kosong) dan validasi untuk TextTotal3 tidak boleh minus
            string jumlahText = TextJumlah.Text.Replace("Rp.", "").Replace(" ", "").Replace(".", "");
            string total2Text = TextTotal2.Text.Replace("Rp.", "").Replace(" ", "").Replace(".", "");

            if (string.IsNullOrWhiteSpace(jumlahText) || jumlahText == "0" ||
                string.IsNullOrWhiteSpace(total2Text) || total2Text == "0" ||
                int.TryParse(total2Text, out int total2Value) && total2Value < 0)
            {
                MessageBox.Show("Jumlah Bayar tidak boleh kosong dan Total Kembalian tidak boleh minus!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isSaveButtonClicked = false;
                return;
            }

            // Proses transaksi jika semua validasi lulus
            using (SqlConnection conn = new SqlConnection(Connect.conn.ConnectionString))
            {
                conn.Open();

                // Hapus semua data dari tb_detail_transaksi berdasarkan id_transaksi
                using (SqlCommand deleteDetailTransaksiCmd = new SqlCommand("DELETE FROM tb_detail_transaksi WHERE id_transaksi = @id_transaksi", conn))
                {
                    deleteDetailTransaksiCmd.Parameters.AddWithValue("@id_transaksi", TextTransaksi.Text);
                    deleteDetailTransaksiCmd.ExecuteNonQuery();
                }

                // Periksa apakah TextDiskon memiliki nilai
                string grandTotalText;
                if (!string.IsNullOrWhiteSpace(TextDiskon.Text)) // Jika TextDiskon memiliki nilai, gunakan itu
                {
                    grandTotalText = TextDiskon.Text.Replace("Rp.", "").Replace(" ", "").Replace(".", "");
                }
                else // Jika tidak ada nilai di TextDiskon, gunakan TextTotal1 seperti sebelumnya
                {
                    grandTotalText = TextTotal1.Text.Replace("Rp.", "").Replace(" ", "").Replace(".", "");
                }

                int grandTotal = int.Parse(grandTotalText); // Konversi ke integer

                // Parsing tanggal_transaksi dari TextTanggal dan TextJam
                DateTime tanggalTransaksi = DateTime.ParseExact(TextTanggal.Text + " " + TextJam.Text, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                // Perbarui data di tb_transaksi
                using (SqlCommand updateTransaksiCmd = new SqlCommand("UPDATE tb_transaksi SET tanggal_transaksi = @tanggal_transaksi, grand_total = @grand_total, id_status_transaksi = @id_status_transaksi WHERE id_transaksi = @id_transaksi", conn))
                {
                    updateTransaksiCmd.Parameters.AddWithValue("@tanggal_transaksi", tanggalTransaksi);
                    updateTransaksiCmd.Parameters.AddWithValue("@grand_total", grandTotal);
                    updateTransaksiCmd.Parameters.AddWithValue("@id_status_transaksi", 1); // Ubah status transaksi menjadi 1
                    updateTransaksiCmd.Parameters.AddWithValue("@id_transaksi", TextTransaksi.Text);

                    updateTransaksiCmd.ExecuteNonQuery();
                }

                // Tambahkan logika untuk pemberian poin
                string idMemberStr = TextIdMember.Text; // Ambil id_member dari ComboMember

                if (!string.IsNullOrWhiteSpace(idMemberStr) && idMemberStr != "0") // Cek jika id_member tidak kosong dan bukan "0"
                {
                    // Hitung poin berdasarkan grand_total
                    int poin = 0;

                    if (grandTotal >= 50000 && grandTotal <= 199000)
                    {
                        poin = 400; // Poin untuk transaksi antara 50.000 dan 199.000
                    }
                    else if (grandTotal >= 200000)
                    {
                        poin = 1000; // Poin untuk transaksi di atas 200.000
                    }

                    // Update poin ke tabel tb_member
                    using (SqlCommand updateMemberCmd = new SqlCommand("UPDATE tb_member SET point = point + @point WHERE id_member = @id_member", conn))
                    {
                        updateMemberCmd.Parameters.AddWithValue("@point", poin);
                        updateMemberCmd.Parameters.AddWithValue("@id_member", idMemberStr);

                        updateMemberCmd.ExecuteNonQuery();
                    }
                }

                // Perbarui nomor transaksi setelah berhasil menyimpan
                noOtomatis();

                // Perbarui tampilan dan reset komponen
                LoadDataToDipending();
                LoadDataToDataGridView();
                ResetTransaksi();
            }

            MessageBox.Show("Transaksi berhasil diperbarui!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            isSaveButtonClicked = false; // Reset status klik tombol
        }

        private void ResetTransaksi()
        {
            TextIdMember.Clear();
            TextMember.Clear();
            TextIdBarang.Clear();
            TextNama.Clear();
            TextHarga.Clear();
            TextQty.Clear();
            TextSub.Clear();
            TextTotal1.Text = "Rp. 0";
            TextJumlah.Clear();
            TextTotal2.Clear();
        }
    }
}