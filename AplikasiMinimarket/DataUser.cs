using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AplikasiMinimarket
{
    public partial class DataUser : Form
    {
        private List<Pegawai> pegawais = new List<Pegawai>(); // Menyimpan daftar anggota untuk pencarian
        private List<Gudang> gudangs = new List<Gudang>(); // Menyimpan daftar anggota untuk pencarian
        private List<Role> roles = new List<Role>(); // Menyimpan daftar anggota untuk pencarian
        private int roleId;
        private string loggedInUsername;
        private string loggedUserId;
        private int selectedRowIndex = -1;
        private bool isTambahMode = true;
        private bool isHandlingTextChanged = false; // Flag untuk menghindari loop rekursif
        private bool isSaveButtonClicked = false; // Flag untuk menandakan jika tombol simpan sudah ditekan
        public DataUser(int roleId)
        {
            InitializeComponent();
            this.roleId = roleId;
        }

        private void PerfromPegawai()
        {
            ComboPegawai.Items.Clear();
            pegawais.Clear();

            using (SqlCommand cmd = new SqlCommand("SELECT id_pegawai, nama_pegawai FROM tb_pegawai", Connect.conn))
            {
                Connect.conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Pegawai pegawai = new Pegawai
                    {
                        IdPegawai = (int)reader["id_pegawai"],
                        NamaPegawai = reader["nama_pegawai"].ToString()
                    };
                    pegawais.Add(pegawai);
                    ComboPegawai.Items.Add(pegawai);

                }
                reader.Close();
                Connect.conn.Close();
            }
        }

        private void PerfromGudang()
        {
            ComboGudang.Items.Clear();
            gudangs.Clear();

            using (SqlCommand cmd = new SqlCommand("SELECT id_gudang, nama_gudang FROM tb_gudang", Connect.conn))
            {
                Connect.conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Gudang gudang = new Gudang
                    {
                        IdGudang = (int)reader["id_gudang"],
                        NamaGudang = reader["nama_gudang"].ToString()
                    };
                    gudangs.Add(gudang);
                    ComboGudang.Items.Add(gudang);

                }
                reader.Close();
                Connect.conn.Close();
            }
        }

        private void PerfromRole()
        {
            ComboRole.Items.Clear();
            roles.Clear();

            using (SqlCommand cmd = new SqlCommand("SELECT id_role, nama_role FROM tb_role", Connect.conn))
            {
                Connect.conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Role role = new Role
                    {
                        IdRole = (int)reader["id_role"],
                        NamaRole = reader["nama_role"].ToString()
                    };
                    roles.Add(role);
                    ComboRole.Items.Add(role);
                }
                reader.Close();
                Connect.conn.Close();
            }
        }

        private void ComboPegawai_TextChanged(object sender, EventArgs e)
        {
            // Cegah event handler dari pemanggilan berulang saat tombol simpan ditekan
            if (isHandlingTextChanged || isSaveButtonClicked) return;

            // Aktifkan flag untuk mencegah rekursi
            isHandlingTextChanged = true;

            // Ambil posisi kursor saat ini
            int cursorPosition = ComboPegawai.SelectionStart;

            // Dapatkan teks yang input pengguna saat ini
            string currentText = ComboPegawai.Text;

            // Filter daftar pegawai berdasarkan nama_pegawai yang sesuai dengan teks pencarian
            var filteredPegawais = pegawais.FindAll(p =>
            {
                // Pisahkan nama pegawai menjadi array kata-kata
                var nameParts = p.NamaPegawai.Split(' ');
                // Periksa apakah salah satu bagian dari nama cocok dengan teks input
                return nameParts.Any(part => part.ToUpper().Contains(currentText.ToUpper()));
            });

            // Cek jika ada satu hasil yang sepenuhnya cocok dengan teks input pengguna
            if (filteredPegawais.Count == 1 &&
                filteredPegawais[0].NamaPegawai.Equals(currentText, StringComparison.OrdinalIgnoreCase))
            {
                // Jika hasilnya satu dan teksnya cocok sepenuhnya, jangan tampilkan dropdown lagi
                ComboPegawai.DroppedDown = false;
            }
            else
            {
                // Kosongkan ComboBox dan isi ulang dengan hasil filter
                ComboPegawai.Items.Clear();
                foreach (var pegawai in filteredPegawais)
                {
                    ComboPegawai.Items.Add(pegawai);
                }

                // Tampilkan kembali dropdown secara otomatis
                ComboPegawai.DroppedDown = true;
            }

            // Tampilkan kembali teks pencarian dan arahkan kursor ke posisi sebelumnya
            ComboPegawai.Text = currentText;
            ComboPegawai.SelectionStart = cursorPosition;

            // Matikan flag setelah operasi selesai
            isHandlingTextChanged = false;
        }

        private void ComboGudang_TextChanged(object sender, EventArgs e)
        {
            // Cegah event handler dari pemanggilan berulang saat tombol simpan ditekan
            if (isHandlingTextChanged || isSaveButtonClicked) return;

            // Aktifkan flag untuk mencegah rekursi
            isHandlingTextChanged = true;

            // Ambil posisi kursor saat ini
            int cursorPosition = ComboGudang.SelectionStart;

            // Dapatkan teks yang input pengguna saat ini
            string currentText = ComboGudang.Text;

            // Filter daftar gudang berdasarkan nama_gudang yang sesuai dengan teks pencarian
            var filteredGudangs = gudangs.FindAll(g =>
                g.NamaGudang.ToUpper().Contains(currentText.ToUpper()));

            // Cek jika ada satu hasil yang sepenuhnya cocok dengan teks input pengguna
            if (filteredGudangs.Count == 1 &&
                filteredGudangs[0].NamaGudang.Equals(currentText, StringComparison.OrdinalIgnoreCase))
            {
                // Jika hasilnya satu dan teksnya cocok sepenuhnya, jangan tampilkan dropdown lagi
                ComboGudang.DroppedDown = false;
            }
            else
            {
                // Kosongkan ComboBox dan isi ulang dengan hasil filter
                ComboGudang.Items.Clear();
                foreach (var gudang in filteredGudangs)
                {
                    ComboGudang.Items.Add(gudang);
                }

                // Tampilkan kembali dropdown secara otomatis
                ComboGudang.DroppedDown = true;
            }

            // Tampilkan kembali teks pencarian dan arahkan kursor ke posisi sebelumnya
            ComboGudang.Text = currentText;
            ComboGudang.SelectionStart = cursorPosition;

            // Matikan flag setelah operasi selesai
            isHandlingTextChanged = false;
        }


        private void ComboRole_TextChanged(object sender, EventArgs e)
        {
            // Cegah event handler dari pemanggilan berulang saat tombol simpan ditekan
            if (isHandlingTextChanged || isSaveButtonClicked) return;

            // Aktifkan flag untuk mencegah rekursi
            isHandlingTextChanged = true;

            // Ambil posisi kursor saat ini
            int cursorPosition = ComboRole.SelectionStart;

            // Dapatkan teks yang input pengguna saat ini
            string currentText = ComboRole.Text;

            // Filter daftar role berdasarkan nama_role yang sesuai dengan teks pencarian
            var filteredRoles = roles.FindAll(r =>
                r.NamaRole.ToUpper().Contains(currentText.ToUpper()));

            // Cek jika ada satu hasil yang sepenuhnya cocok dengan teks input pengguna
            if (filteredRoles.Count == 1 &&
                filteredRoles[0].NamaRole.Equals(currentText, StringComparison.OrdinalIgnoreCase))
            {
                // Jika hasilnya satu dan teksnya cocok sepenuhnya, jangan tampilkan dropdown lagi
                ComboRole.DroppedDown = false;
            }
            else
            {
                // Kosongkan ComboBox dan isi ulang dengan hasil filter
                ComboRole.Items.Clear();
                foreach (var role in filteredRoles)
                {
                    ComboRole.Items.Add(role);
                }

                // Tampilkan kembali dropdown secara otomatis
                ComboRole.DroppedDown = true;
            }

            // Tampilkan kembali teks pencarian dan arahkan kursor ke posisi sebelumnya
            ComboRole.Text = currentText;
            ComboRole.SelectionStart = cursorPosition;

            // Matikan flag setelah operasi selesai
            isHandlingTextChanged = false;
        }

        private void TextUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                TextPassword.Focus();
            }
        }

        private void TextPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                ComboPegawai.Focus();
            }
        }

        private void ComboPegawai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                ComboGudang.Focus();
            }
        }

        private void ComboGudang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                ComboRole.Focus();
            }
        }

        private void ComboRole_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                isSaveButtonClicked = true;
                e.Handled = true;
                PerfromUser();
                isSaveButtonClicked = false;
            }
        }

        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            // Tandai bahwa tombol simpan sudah ditekan
            isSaveButtonClicked = true;

            PerfromUser();

            // Setelah selesai, nonaktifkan flag isSaveButtonClicked
            isSaveButtonClicked = false;
        }

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private string HashPassword(string password, string salt)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] saltBytes = Convert.FromBase64String(salt);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] combinedBytes = saltBytes.Concat(passwordBytes).ToArray();
                byte[] hashBytes = sha256Hash.ComputeHash(combinedBytes);

                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool IsUsernameExists(string username)
        {
            bool exists = false;
            Connect.conn.Open();
            using (SqlCommand cmd = Connect.conn.CreateCommand())
            {
                cmd.CommandText = "SELECT COUNT(*) FROM tb_user WHERE username = @username";
                cmd.Parameters.AddWithValue("@username", username);
                exists = (int)cmd.ExecuteScalar() > 0;
            }
            Connect.conn.Close();
            return exists;
        }

        private bool IsFormValid()
        {
            if (string.IsNullOrWhiteSpace(TextUser.Text) ||
                string.IsNullOrWhiteSpace(TextPassword.Text) ||
                ComboPegawai.SelectedItem == null ||
                ComboGudang.SelectedItem == null ||
                ComboRole.SelectedItem == null)
            {
                MessageBox.Show("Semua kolom harus diisi", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void PerfromUser()
        {
            if (!IsFormValid())
            {
                return;
            }

            string username = TextUser.Text;
            string password = TextPassword.Text;

            // Generate salt dan hash password sebelum meyimpan ke database
            string salt = GenerateSalt();
            string hashedPassword = HashPassword(password, salt);

            // Cek apakah username sudah ada
            if (IsUsernameExists(username))
            {
                MessageBox.Show("Username sudah ada. Silahkan pilih username lain.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Ambil nilai dari ComboBox
            Pegawai selectedPegawai = ComboPegawai.SelectedItem as Pegawai;
            Gudang selectedGudang = ComboGudang.SelectedItem as Gudang;
            Role selectedRole = ComboRole.SelectedItem as Role;

            // Ambil nilai dari id_pegawai, id_gudang, dan id_role
            int idPegawai = selectedPegawai.IdPegawai;
            int idGudang = selectedGudang.IdGudang;
            int idRole = selectedRole.IdRole;

            // Simpan data ke database
            Connect.conn.Open();
            using (SqlCommand cmd = Connect.conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO tb_user (username, password, salt, id_pegawai, id_gudang, id_role) VALUES (@username, @password, @salt, @id_pegawai, @id_gudang, @id_role)";
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", hashedPassword);
                cmd.Parameters.AddWithValue("@salt", salt);
                cmd.Parameters.AddWithValue("@id_pegawai", idPegawai);
                cmd.Parameters.AddWithValue("@id_gudang", idGudang);
                cmd.Parameters.AddWithValue("@id_role", idRole);
                cmd.ExecuteNonQuery();
            }
            Connect.conn.Close();

            MessageBox.Show("Data berhasil di input! password acak: " + password, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reset form
            TextUser.Clear();
            TextPassword.Clear();
            ComboPegawai.SelectedIndex = -1;
            ComboGudang.SelectedIndex = -1;
            ComboRole.SelectedIndex = -1;

            BtnSimpan.Enabled = false;

            LoadDataToDataGridView();
        }

        private void DataUser_Load(object sender, EventArgs e)
        {
            PerfromPegawai();
            PerfromGudang();
            PerfromRole();
            LoadDataToDataGridView();
            DisableInputControls();
        }

        private void LoadDataToDataGridView()
        {
            // Kosongkan DataGridView terlebih dahulu
            Data_User.Rows.Clear();

            // Tulis kueri SQL Anda untuk mengambil data dari nilai
            string query = "SELECT tb_user.id_user, tb_user.username, CONCAT(tb_user.password, tb_user.salt) AS password, tb_pegawai.nama_pegawai, tb_gudang.nama_gudang, tb_role.nama_role " +
                            "FROM tb_user " +
                            "JOIN tb_pegawai ON tb_user.id_pegawai = tb_pegawai.id_pegawai " +
                            "JOIN tb_gudang ON tb_user.id_gudang = tb_gudang.id_gudang " +
                            "JOIN tb_role ON tb_user.id_role = tb_role.id_role ";

            // Buat SqlCommand untuk menjalankan kueri
            using (SqlCommand cmd = new SqlCommand(query, Connect.conn))
            {
                Connect.conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Menambahkan data ke DataGridView
                        string user = reader["id_user"].ToString();
                        string nama = reader["username"].ToString();
                        string password = reader["password"].ToString();
                        string pegawai = reader["nama_pegawai"].ToString();
                        string gudang = reader["nama_gudang"].ToString();
                        string role = reader["nama_role"].ToString();
                        Data_User.Rows.Add(user, nama, password, pegawai, gudang, role);
                    }
                }
                Connect.conn.Close();
            }
        }

        private void BtnTambah_Click(object sender, EventArgs e)
        {
            if (isTambahMode)
            {
                EnableInputControls();
                BtnTambah.Text = "Clear";
            }
        }

        private void DataUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Reset ke mode "Tambah" saat form ditutup
            BtnTambah.Text = "Tambah";
            isTambahMode = true;
        }

        private void Data_User_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRowIndex = e.RowIndex;

                // Periksa apakah sel yang diklik tidak null sebelum mengambil nilai
                if (Data_User.Rows[selectedRowIndex].Cells[0].Value != null)
                {
                    TextId.Text = Data_User.Rows[selectedRowIndex].Cells[0].Value.ToString();
                }
                else
                {
                    TextId.Text = string.Empty;
                }
                if (Data_User.Rows[selectedRowIndex].Cells[1].Value != null)
                {
                    TextUser.Text = Data_User.Rows[selectedRowIndex].Cells[1].Value.ToString();
                }

                if (Data_User.Rows[selectedRowIndex].Cells[2].Value != null)
                {
                    TextPassword.Text = Data_User.Rows[selectedRowIndex].Cells[2].Value.ToString();
                }
                else
                {
                    TextPassword.Text = string.Empty;
                }

                // Ambil id_pegawai dari kolom 4 dan cari nama_pegawai
                if (Data_User.Rows[selectedRowIndex].Cells[3].Value != null)
                {
                    string namaPegawai = Data_User.Rows[selectedRowIndex].Cells[3].Value.ToString();

                    // Tampilkan semua pegawai di ComboBox
                    ComboPegawai.Items.Clear();
                    Connect.conn.Open();
                    Connect.cmd = new SqlCommand("SELECT * FROM tb_pegawai", Connect.conn);
                    Connect.reader = Connect.cmd.ExecuteReader();
                    while (Connect.reader.Read())
                    {
                        Pegawai pegawai = new Pegawai
                        {
                            IdPegawai = (int)Connect.reader["id_pegawai"],
                            NamaPegawai = (string)Connect.reader["nama_pegawai"]
                        };
                        ComboPegawai.Items.Add(pegawai);
                    }
                    Connect.reader.Close();
                    Connect.conn.Close();
                    ComboPegawai.SelectedItem = ComboPegawai.Items.Cast<Pegawai>().FirstOrDefault(p => p.NamaPegawai == namaPegawai);
                }
                else
                {
                    ComboPegawai.SelectedIndex = -1;
                }

                // Ambil id_gudang dari kolom 5 dan cari nama_gudang
                if (Data_User.Rows[selectedRowIndex].Cells[4].Value != null)
                {
                    string namaGudang = Data_User.Rows[selectedRowIndex].Cells[4].Value.ToString();

                    // Tampilkan semua gudang di ComboBox
                    ComboGudang.Items.Clear();
                    Connect.conn.Open();
                    Connect.cmd = new SqlCommand("SELECT * FROM tb_gudang", Connect.conn);
                    Connect.reader = Connect.cmd.ExecuteReader();
                    while (Connect.reader.Read())
                    {
                        Gudang gudang = new Gudang
                        {
                            IdGudang = (int)Connect.reader["id_gudang"],
                            NamaGudang = (string)Connect.reader["nama_gudang"]
                        };
                        ComboGudang.Items.Add(gudang);
                    }
                    Connect.reader.Close();
                    Connect.conn.Close();
                    ComboGudang.SelectedItem = ComboGudang.Items.Cast<Gudang>().FirstOrDefault(g => g.NamaGudang == namaGudang);
                }
                else
                {
                    ComboGudang.SelectedIndex = -1;
                }

                // Ambil id_role dari kolom 6 dan cari nama_role
                if (Data_User.Rows[selectedRowIndex].Cells[5].Value != null)
                {
                    string namaRole = Data_User.Rows[selectedRowIndex].Cells[5].Value.ToString();

                    // Tampilkan semua role di ComboBox
                    ComboRole.Items.Clear();
                    Connect.conn.Open();
                    Connect.cmd = new SqlCommand("SELECT * FROM tb_role", Connect.conn);
                    Connect.reader = Connect.cmd.ExecuteReader();
                    while (Connect.reader.Read())
                    {
                        Role role = new Role
                        {
                            IdRole = (int)Connect.reader["id_role"],
                            NamaRole = (string)Connect.reader["nama_role"]
                        };
                        ComboRole.Items.Add(role);
                    }
                    Connect.reader.Close();
                    Connect.conn.Close();
                    ComboRole.SelectedItem = ComboRole.Items.Cast<Role>().FirstOrDefault(r => r.NamaRole == namaRole);
                }
                else
                {
                    ComboRole.SelectedIndex = -1;
                }


                BtnSimpan.Enabled = false;
                BtnUbah.Enabled = true;

                // Mengatifkan kolom "Hapus"
                EnableDeleteColumn(true);
            }
        }

        // Metode untuk mendapatkan username saat ini berdasarkan id_user
        private string GetCurrentUsername(string id)
        {
            string currentUsername = null;
            Connect.conn.Open();
            using (SqlCommand cmd = Connect.conn.CreateCommand())
            {
                cmd.CommandText = "SELECT username FROM tb_user WHERE id_user = @id_user";
                cmd.Parameters.AddWithValue("@id_user", id);
                currentUsername = cmd.ExecuteScalar()?.ToString();
            }
            Connect.conn.Close();
            return currentUsername;
        }

        private void BtnUbah_Click(object sender, EventArgs e)
        {
            // Tandai bahwa tombol simpan sudah ditekan
            isSaveButtonClicked = true;

            if (selectedRowIndex < 0)
            {
                MessageBox.Show("Pilih baris yang ingin diubah", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Nonaktifkan flag isSaveButtonClicked
                isSaveButtonClicked = false;
                return;
            }

            string id = TextId.Text;
            string username = TextUser.Text;
            string password = TextPassword.Text;

            Pegawai selectedPegawai = ComboPegawai.SelectedItem as Pegawai;
            Gudang selectedGudang = ComboGudang.SelectedItem as Gudang;
            Role selectedRole = ComboRole.SelectedItem as Role;

            if (string.IsNullOrWhiteSpace(id) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                selectedPegawai == null ||
                selectedGudang == null ||
                selectedRole == null)
            {
                MessageBox.Show("Semua kolom harus diisi, termasuk memilih pegawai, gudang, dan role", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Nonaktifkan flag isSaveButtonClicked
                isSaveButtonClicked = false;
                return;
            }

            int idPegawai = selectedPegawai.IdPegawai;
            int idGudang = selectedGudang.IdGudang;
            int idRole = selectedRole.IdRole;

            // Hash kata sandi baru
            string salt = GenerateSalt();
            string hashedPassword = HashPassword(password, salt);

            // Cek apakah username dan password sudah ada (kecuali untuk pengguna yang sama)
            if (IsUsernameExists(username) && username != GetCurrentUsername(id))
            {
                MessageBox.Show("Username sudah ada. Silahkan pilih username lain.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Nonaktifkan flag isSaveButtonClicked
                isSaveButtonClicked = false;
                return;
            }

            // Perbarui data pengguna di database
            Connect.conn.Open();
            using (SqlCommand cmd = Connect.conn.CreateCommand())
            {
                cmd.CommandText = "UPDATE tb_user SET username = @username, password = @password, salt = @salt, id_pegawai = @id_pegawai, id_gudang = @id_gudang, id_role = @id_role WHERE id_user = @id_user";
                cmd.Parameters.AddWithValue("@id_user", id);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", hashedPassword);
                cmd.Parameters.AddWithValue("@salt", salt);
                cmd.Parameters.AddWithValue("@id_pegawai", idPegawai);
                cmd.Parameters.AddWithValue("@id_gudang", idGudang);
                cmd.Parameters.AddWithValue("@id_role", idRole);
                cmd.ExecuteNonQuery();
            }
            Connect.conn.Close();

            MessageBox.Show("Data berhasil diubah!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Kosongkan semua input
            TextId.Clear();
            TextUser.Clear();
            TextPassword.Clear();
            ComboPegawai.SelectedIndex = -1;
            ComboGudang.SelectedIndex = -1;
            ComboRole.SelectedIndex = -1;

            // Reset tombol Simpan dan Ubah
            BtnSimpan.Enabled = true;
            BtnUbah.Enabled = false;
            selectedRowIndex = -1;
            EnableDeleteColumn(false);

            // Muat ulang data ke DataGridView
            LoadDataToDataGridView();

            // Nonaktifkan flag isSaveButtonClicked
            isSaveButtonClicked = false;
        }

        private void Data_User_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == Data_User.Columns["Hapus"].Index && e.RowIndex >= 0)
            {
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

                    // Ambil data dari kolom yang sesuai
                    string id = Data_User.Rows[e.RowIndex].Cells["Id"].Value?.ToString();

                    if (id != null)
                    {
                        // Hapus data dari database
                        Connect.conn.Open();
                        using (SqlCommand cmd = Connect.conn.CreateCommand())
                        {
                            cmd.CommandText = "DELETE FROM tb_user WHERE id_user = @id_user";
                            cmd.Parameters.AddWithValue("@id_user", id);
                            cmd.ExecuteNonQuery();
                        }
                        Connect.conn.Close();

                        MessageBox.Show("Data berhasil dihapus!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Kosongkan semua input
                        TextId.Clear();
                        TextUser.Clear();
                        TextPassword.Clear();
                        ComboPegawai.SelectedIndex = -1;
                        ComboGudang.SelectedIndex = -1;
                        ComboRole.SelectedIndex = -1;

                        // Reset tombol Simpan dan Ubah
                        BtnSimpan.Enabled = true;
                        BtnUbah.Enabled = false;
                        selectedRowIndex = -1;

                        // Muat ulang data ke DataGridView
                        LoadDataToDataGridView();
                    }
                    else
                    {
                        MessageBox.Show("ID pengguna tidak ditemukan", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // Nonaktifkan flag setelah proses selesai
                    isSaveButtonClicked = false;
                }
                else
                {
                    // Pengguna memilih No, tidak melakukan apa-apa
                }
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            MenuUtama MU = new MenuUtama(roleId, loggedInUsername, loggedUserId);
            MU.Show();
            Hide();
        }

        private void EnableDeleteColumn(bool enable)
        {
            // Mendapatkan indeks kolom "Hapus"
            int deleteColumnIndex = Data_User.Columns["Hapus"].Index;

            // Mengatur properti ReadOnly untuk setiap sel di kolom "Hapus"
            foreach (DataGridViewRow row in Data_User.Rows)
            {
                if (row.Cells[deleteColumnIndex].Value != null)
                {
                    // Mengatur properti ReadOnly sesuai kebutuhan
                    row.Cells[deleteColumnIndex].ReadOnly = !enable;
                }
            }
        }

        private void EnableInputControls()
        {
            TextId.Clear();
            TextUser.Clear();
            TextPassword.Clear();
            ComboPegawai.SelectedIndex = -1;
            ComboGudang.SelectedIndex = -1;
            ComboRole.SelectedIndex = -1;

            TextUser.Enabled = true;
            TextPassword.Enabled = true;
            ComboPegawai.Enabled = true;
            ComboGudang.Enabled = true;
            ComboRole.Enabled = true;

            BtnSimpan.Enabled = true;
            BtnUbah.Enabled = true;
            EnableDeleteColumn(true);
        }

        private void DisableInputControls()
        {
            TextId.Enabled = false;
            TextUser.Enabled = false;
            TextPassword.Enabled = false;
            ComboPegawai.Enabled = false;
            ComboGudang.Enabled = false;
            ComboRole.Enabled = false;

            BtnSimpan.Enabled = false;
            BtnUbah.Enabled = false;
            EnableDeleteColumn(false);
        }
    }
}