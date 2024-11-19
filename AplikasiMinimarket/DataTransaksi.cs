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

        private void ComboMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboMember.SelectedItem is Member selectedMember)
            {
                // Menampilkan NamaMember di TextMember
                TextMember.Text = selectedMember.NamaMember;
            }
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
        }
    }
}
