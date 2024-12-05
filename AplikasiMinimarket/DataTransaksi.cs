using System;
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
        private List<Barang> barangs = new List<Barang>(); // Menyimpan daftar barang untuk pencarian
        private long currentCount = 1;
        private int roleId;
        private string loggedInUsername;
        private string loggedInUserId;
        private int selectedRowIndex = -1;
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
            string query = "SELECT * FROM tb_transaksi WHERE id_transaksi IN (SELECT MAX(id_transaksi) FROM tb_transaksi)";
            string urutanKode;
            long hitung;

            using (SqlConnection conn = new SqlConnection(Connect.conn.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read() && dr.HasRows)
                        {
                            hitung = long.Parse(dr.GetString(0).Substring(dr.GetString(0).Length - 9));
                            urutanKode = "TRS" + DateTime.Now.ToString("ddMMyy") + (hitung + currentCount).ToString("D3");
                        }
                        else
                        {
                            urutanKode = "TRS" + DateTime.Now.ToString("ddMMyy") + "001";
                        }
                    }
                }
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

            using (SqlConnection conn = new SqlConnection(Connect.conn.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT id_member, nama_member FROM tb_member", conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Member member = new Member
                            {
                                IdMember = reader["id_member"].ToString(),
                                NamaMember = reader["nama_member"].ToString()
                            };
                            members.Add(member);
                            ComboMember.Items.Add(member); // Menampilkan IdMember
                        }
                    }
                }
            }
        }

        private void PerformBarang()
        {
            ComboBarang.Items.Clear();
            barangs.Clear();

            using (SqlConnection conn = new SqlConnection(Connect.conn.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT id_barang, nama_barang, harga_satuan, diskon FROM tb_barang", conn)) // Gantilah id_diskon dengan diskon
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Barang barang = new Barang
                            {
                                IdBarang = reader["id_barang"].ToString(),
                                NamaBarang = reader["nama_barang"].ToString(),
                                HargaBarang = (int)reader["harga_satuan"],
                                Diskon = reader["diskon"] == DBNull.Value ? 0 : Convert.ToInt32(reader["diskon"]) // Langsung ambil diskon dari kolom diskon
                            };
                            barangs.Add(barang);
                            ComboBarang.Items.Add(barang.IdBarang); // Menampilkan id_barang di ComboBox
                        }
                    }
                }
            }
        }

        private void ComboMember_TextChanged(object sender, EventArgs e)
        {
            if (isHandlingTextChanged || isSaveButtonClicked || ComboMember.SelectedItem != null)
            {
                return;
            }

            isHandlingTextChanged = true;

            int cursorPosition = ComboMember.SelectionStart;
            string currentText = ComboMember.Text;

            var filteredMembers = members.FindAll(b =>
                b.IdMember.ToUpper().Contains(currentText.ToUpper()));

            // Jika hanya satu item yang cocok, tutup dropdown
            if (filteredMembers.Count == 1 &&
                (filteredMembers[0].IdMember.Equals(currentText, StringComparison.OrdinalIgnoreCase)))
            {
                ComboMember.DroppedDown = false;
                ComboMember.SelectedItem = filteredMembers[0].IdMember; // Pilih item yang cocok
            }
            else
            {
                // Perbarui item di ComboBox
                ComboMember.Items.Clear();
                foreach (var member in filteredMembers)
                {
                    ComboMember.Items.Add(member.IdMember);
                }
                ComboMember.DroppedDown = true; // Tampilkan dropdown jika lebih dari 1
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

            var filteredBarangs = barangs.FindAll(b =>
                b.IdBarang.ToUpper().Contains(currentText.ToUpper()));

            if (filteredBarangs.Count == 1 &&
                (filteredBarangs[0].IdBarang.Equals(currentText, StringComparison.OrdinalIgnoreCase)))
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
            if (isHandlingTextChanged || ComboMember.SelectedItem == null)
            {
                return;
            }

            isHandlingTextChanged = true;

            // Pastikan item yang dipilih valid
            string selectedId = ComboMember.SelectedItem.ToString(); // Ambil teks dari item yang dipilih

            var selectedMember = members.Find(m =>
                string.Equals(m.IdMember, selectedId, StringComparison.OrdinalIgnoreCase));

            if (selectedMember != null)
            {
                // Jika member ditemukan, tampilkan nama member
                TextMember.Text = selectedMember.NamaMember;

                // Tutup dropdown setelah memilih item
                ComboMember.DroppedDown = false;
            }
            else
            {
                // Jika tidak ditemukan, kosongkan TextMember
                TextMember.Text = string.Empty;
            }

            isHandlingTextChanged = false;
        }


        private void ComboBarang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBarang.SelectedItem is string selectedId)
            {
                var selectedBarang = barangs.Find(b => b.IdBarang == selectedId);
                if (selectedBarang != null)
                {
                    TextNama.Text = selectedBarang.NamaBarang;

                    // Periksa apakah diskon memiliki nilai
                    if (selectedBarang.Diskon > 0) // Cek diskon langsung
                    {
                        // Hitung harga setelah diskon
                        decimal hargaAsli = selectedBarang.HargaBarang;
                        decimal hargaSetelahDiskon = hargaAsli - (hargaAsli * selectedBarang.Diskon / 100);

                        // Tampilkan harga setelah diskon
                        TextHarga.Text = string.Format("Rp. {0:N0}", hargaSetelahDiskon);
                    }
                    else
                    {
                        // Jika diskon tidak ada atau 0, tampilkan harga asli
                        TextHarga.Text = string.Format("Rp. {0:N0}", selectedBarang.HargaBarang);
                    }
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
            // Memastikan hanya angka dan karakter kontrol (seperti backspace) yang bisa dimasukkan
            else if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
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
                PerfromTransaksi();
            }
        }


        private void BtnKlik_Click(object sender, EventArgs e)
        {
            PerfromTransaksi();
        }


        private void PerfromTransaksi()
        {
            // Cegah eksekusi simultan
            if (isHandlingTextChanged || isSaveButtonClicked)
                return;

            isSaveButtonClicked = true; // Tandai bahwa tombol sedang diproses

            // Validasi input
            if (ComboMember.SelectedItem == null || ComboBarang.SelectedItem == null ||
                string.IsNullOrWhiteSpace(TextTotal1.Text) || string.IsNullOrWhiteSpace(TextTotal2.Text))
            {
                MessageBox.Show("Id Member, Kode Barang, dan Total jangan dikosongkan", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Member selectedMember = ComboMember.SelectedItem as Member;
            string selectedBarangId = ComboBarang.SelectedItem as string;

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

                                string grandTotalText = TextTotal2.Text.Replace("Rp.", "").Replace(" ", "").Replace(".", "");
                                insertTransaksiCmd.Parameters.AddWithValue("@grand_total", int.Parse(grandTotalText));
                                insertTransaksiCmd.Parameters.AddWithValue("@id_status_transaksi", 0);
                                insertTransaksiCmd.Parameters.AddWithValue("@id_member", selectedMember.IdMember);

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
                                updateDetailTransaksiCmd.Parameters.AddWithValue("@qty", int.Parse(TextTotal1.Text)); // Menambah qty
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
                                insertDetailTransaksiCmd.Parameters.AddWithValue("@qty", int.Parse(TextTotal1.Text));
                                insertDetailTransaksiCmd.Parameters.AddWithValue("@sub_total", int.Parse(TextSub.Text.Replace("Rp.", "").Replace(" ", "").Replace(".", "")));

                                insertDetailTransaksiCmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Mengurangi stok barang
                    using (SqlCommand updateStokCmd = new SqlCommand("UPDATE tb_barang SET total_stok = total_stok - @qty WHERE id_barang = @id_barang", conn))
                    {
                        updateStokCmd.Parameters.AddWithValue("@qty", int.Parse(TextTotal1.Text));
                        updateStokCmd.Parameters.AddWithValue("@id_barang", selectedBarangId);

                        updateStokCmd.ExecuteNonQuery();
                    }

                    LoadDataToDataGridView();
                    ResetKomponen();
                }
            }

            MessageBox.Show("Transaksi berhasil disimpan!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            isSaveButtonClicked = false; // Reset status klik tombol
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
            TextTotal2.Text = "Rp. " + 0;
            LoadDataToDataGridView();
            TextJumlah.Text = "Rp. ";
        }

        private void LoadDataToDataGridView()
        {
            Data_Transaksi.Rows.Clear();
            int totalSub = 0;

            string query = "SELECT tb_detail_transaksi.id_detail_transaksi, tb_detail_transaksi.id_barang, tb_barang.nama_barang, tb_detail_transaksi.harga_satuan, tb_detail_transaksi.qty, tb_detail_transaksi.sub_total " +
                            "FROM tb_detail_transaksi " +
                            "JOIN tb_barang ON tb_detail_transaksi.id_barang = tb_barang.id_barang";

            using (SqlConnection conn = new SqlConnection(Connect.conn.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
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
            TextTotal2.Text = "Rp. " + totalSub.ToString("N0");
        }

        private void Data_Transaksi_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Pastikan pengeditan terjadi di kolom qty
            if (e.RowIndex >= 0 && e.ColumnIndex == 4) // Kolom qty berada di index ke-4
            {
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
        }

        private void Data_Transaksi_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Pastikan perubahan terjadi di kolom qty
            if (e.RowIndex >= 0 && e.ColumnIndex == 4) // Kolom qty berada di index ke-4
            {
                // Ambil data dari baris yang diedit
                string idDetail = Data_Transaksi.Rows[e.RowIndex].Cells[0].Value.ToString(); // ID transaksi
                string hargaText = Data_Transaksi.Rows[e.RowIndex].Cells[3].Value.ToString().Replace("Rp. ", "").Replace(".", ""); // Harga satuan
                string qtyText = Data_Transaksi.Rows[e.RowIndex].Cells[4].Value.ToString(); // Qty baru
                string idBarang = Data_Transaksi.Rows[e.RowIndex].Cells[1].Value.ToString(); // ID Barang

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
                }
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
            TextTotal2.Text = "Rp. " + totalSub.ToString("N0");
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
            string angkaText = currentText.Replace("Rp. ", "").Replace(" ", "").Replace(".", "").Trim();

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
                string jumlahText = TextJumlah.Text.Replace("Rp. ", "").Replace(".", "").Trim();
                string total2Text = TextTotal2.Text.Replace("Rp. ", "").Replace(".", "").Trim();

                if (long.TryParse(jumlahText, out long jumlah) && long.TryParse(total2Text, out long total2))
                {
                    long total3 = jumlah - total2; // Lakukan pengurangan

                    // Tampilkan hasil di TextTotal3
                    TextTotal3.Text = "Rp. " + total3.ToString("N0");
                }
                else
                {
                    // Jika parsing gagal, tampilkan Rp. 0 di TextTotal3
                    TextTotal3.Text = "Rp. 0";
                }

                // Cegah bunyi "ding" ketika Enter ditekan
                e.Handled = true;
            }
        }
    }
}