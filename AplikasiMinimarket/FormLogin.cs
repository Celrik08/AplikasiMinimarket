using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplikasiMinimarket
{
    public partial class FormLogin : Form
    {
        private string loggedInUsername;
        private string loggedInUserId;
        public FormLogin()
        {
            InitializeComponent();
        }

        private void TextUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                TextPassword.Focus();
                e.Handled = true;
            }
        }

        private void TextPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                PerfromLogin();
            }
        }

        private void BtnKlik_Click(object sender, EventArgs e)
        {
            PerfromLogin();
        }

        // Fungsi hashing
        private string HashPassword(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Konversi salt dan password ke byte array
                byte[] saltBytes = Convert.FromBase64String(salt);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Gabungkan salt dan password
                byte[] combinedBytes = saltBytes.Concat(passwordBytes).ToArray();

                // Hash gabungan salt dan password
                byte[] hashBytes = sha256.ComputeHash(combinedBytes);

                // Ubah hasil hash menjadi string heksadesimal
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2")); // konversi ke hexadecimal
                }
                return builder.ToString();
            }
        }

        private void PerfromLogin()
        {
            string username = TextUser.Text;
            string password = TextPassword.Text;

            // Cek jika inputan user memiliki spasi di awal atau di akhir
            if (username != username.Trim() || password != password.Trim())
            {
                MessageBox.Show("Username atau password tidak boleh memiliki spasi di awal atau di akhir", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Trim untuk menghapus spasi di awal/akhir input
            username = username.Trim();
            password = password.Trim();

            // Cek jika username atau password kosong
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username dan password tidak boleh kosong", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Query untuk mendapatkan hash password dan salt dari database
            string query = "SELECT password, salt, id_user, id_role FROM tb_user WHERE username COLLATE Latin1_General_BIN = @username";

            using (SqlConnection conn = new SqlConnection(Connect.conn.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Jika user ditemukan
                        if (reader.Read())
                        {
                            // Ambil password hash dan salt dari database
                            string storedPasswordHash = reader["password"].ToString();
                            string storedSalt = reader["salt"].ToString();
                            string userId = reader["id_user"].ToString();
                            int roleId = int.Parse(reader["id_role"].ToString());

                            // Hash password yang diinput user dengan salt yang diambil dari database
                            string hashedPassword = HashPassword(password, storedSalt);

                            // Bandingkan hash password inputan dengan hash password dari database
                            if (hashedPassword == storedPasswordHash)
                            {
                                // Jika password cocok, login berhasil
                                loggedInUsername = username;
                                loggedInUserId = userId;

                                // Buka MenuUtama dengan informasi roleId, loggedInUsername, dan loggedInUserId
                                MenuUtama MU = new MenuUtama(roleId, loggedInUsername, loggedInUserId);
                                MU.Show();
                                Hide();
                            }
                            else
                            {
                                // Jika password salah
                                MessageBox.Show("Username atau password salah", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                TextUser.Text = "";
                                TextPassword.Text = "";
                            }
                        }
                        else
                        {
                            // Jika username tidak ditemukan
                            MessageBox.Show("Username tidak ditemukan", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }


        private void FormLogin_Load(object sender, EventArgs e)
        {
            TextPassword.PasswordChar = '*';
        }

        private void CheckPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckPassword.Checked)
            {
                TextPassword.PasswordChar = '\0'; // Menampilkan teks biasa (tanpa karakter tersembunyi)
            }
            else
            {
                TextPassword.PasswordChar = '*'; // Menggunakan karakter tersembunyi (bintang)
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
