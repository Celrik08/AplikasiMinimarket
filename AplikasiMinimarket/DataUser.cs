using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplikasiMinimarket
{
    public partial class DataUser : Form
    {
        //private int roleId;
        private int selectedRowIndex = -1;
        private bool isTambahMode = true;
        public DataUser()
        {
            InitializeComponent();
            // this.roleId = roleId;
        }

        private void PerfromPegawai()
        {
            ComboPegawai.Items.Clear();

            using (SqlCommand cmd = new SqlCommand("SELECT id_pegawai, nama_pegawai FROM tb_pegawai", Connect.conn))
            {
                Connect.conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Pegawai pegawai = new Pegawai
                    {
                        IdPegawai = reader["id_pegawai"].ToString(),
                        NamaPegawai = reader["nama_pegawai"].ToString()
                    };
                    ComboPegawai.Items.Add(pegawai);

                }
                reader.Close();
                Connect.conn.Close();
            }
        }

        private void ComboPegawai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboPegawai.SelectedItem is Pegawai selectedPegawai)
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tb_pegawai WHERE id_pegawai = @id_pegawai", Connect.conn))
                {
                    cmd.Parameters.AddWithValue("@id_pegawai", selectedPegawai.IdPegawai);
                    Connect.conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        TextPegawai.Text = reader["nama_pegawai"].ToString();
                    }
                    reader.Close();
                    Connect.conn.Close();
                }
            }
        }

        private void PerfromGudang()
        {
            ComboGudang.Items.Clear();

            using (SqlCommand cmd = new SqlCommand("SELECT id_gudang, nama_gudang FROM tb_gudang", Connect.conn))
            {
                Connect.conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Gudang gudang = new Gudang
                    {
                        IdGudang = reader["id_gudang"].ToString(),
                        NamaGudang = reader["nama_gudang"].ToString()
                    };
                    ComboGudang.Items.Add(gudang);

                }
                reader.Close();
                Connect.conn.Close();
            }
        }

        private void ComboGudang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboGudang.SelectedItem is Gudang selectedGudang)
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tb_gudang WHERE id_gudang = @id_gudang", Connect.conn))
                {
                    cmd.Parameters.AddWithValue("@id_gudang", selectedGudang.IdGudang);
                    Connect.conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        TextGudang.Text = reader['nama_gudang'].ToString();
                    }
                }
            }
        }

        private void TextNama_KeyPress(object sender, KeyPressEventArgs e)
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
                e.Handled = true;
                PerfromUser();
            }
        }

        private bool IsFormValid()
        {
            if (string.IsNullOrWhiteSpace(TextNama.Text) ||
                string.IsNullOrWhiteSpace(TextPassword.Text) ||
                ComboPegawai.SelectedValue == null ||
                ComboGudang.SelectedValue == null ||
                ComboRole.SelectedValue == null)
            {
                MessageBox.Show("Semua kolom harus diisi", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
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

        private void PerfromUser()
        {
            if (!IsFormValid())
            {
                return;
            }

            string nama = TextNama.Text;
            string password = TextPassword.Text;
        }
    }
}
