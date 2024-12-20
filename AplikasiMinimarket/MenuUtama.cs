﻿using System;
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
    public partial class MenuUtama : Form
    {
        private int roleId; // Tambahkan variabel roleId
        private string loggedInUsername;
        private string loggedInUserId;
        public MenuUtama(int roleId, string loggedInUsername, string loggedInUserId)
        {
            InitializeComponent();
            this.roleId = roleId;
            this.loggedInUsername = loggedInUsername;
            this.loggedInUserId = loggedInUserId;

            User.Text = GetRoleName(roleId);
        }

        private string GetRoleName(int roleId)
        {
            switch (roleId)
            {
                case 1:
                    return "Admin";
                case 2:
                    return "Inventori Gudang";
                case 3:
                    return "Inventori Barang";
                case 4:
                    return "Kasir";
                default:
                    return "Unknown";
            }
        }

        private void MenuUtama_Load(object sender, EventArgs e)
        {
            LoadDataToDataGridView();
        }

        private void LoadDataToDataGridView()
        {
            // Kosongkan DataGridView terlebih dahulu
            Data_Barang.Rows.Clear();

            // Tulis kueri SQL Anda untuk mengambil data dari nilai
            string query = "SELECT tb_barang.id_barang, tb_barang.nama_barang, tb_kategori.nama_kategori, tb_barang.harga_satuan, tb_barang.total_stok, tb_barang.diskon " +
                            "FROM tb_barang " +
                            "JOIN tb_kategori ON tb_barang.id_kategori = tb_kategori.id_kategori ";

            using (SqlConnection conn = new SqlConnection(Connect.conn.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Menambahkan data ke DataGridView
                            string id = reader["id_barang"].ToString();
                            string nama = reader["nama_barang"].ToString();
                            string kategori = reader["nama_kategori"].ToString();
                            string total = reader["total_stok"].ToString();

                            // Mengambil harga_satuan dan memastikan format mata uang
                            int hargaSatuan = reader.GetInt32(reader.GetOrdinal("harga_satuan"));
                            // Format harga menjadi Rp dengan separator ribuan
                            string formattedHarga = string.Format(new System.Globalization.CultureInfo("id-ID"), "Rp. {0:N0}", hargaSatuan);

                            // Mengambil diskon dan menghitung harga setelah diskon
                            int diskon = reader.IsDBNull(reader.GetOrdinal("diskon")) ? 0 : reader.GetInt32(reader.GetOrdinal("diskon")); // Diskon dalam persen (misalnya 10 untuk 10%)

                            // Jika diskon adalah 0, hasilkan harga setelah diskon sebagai Rp. 0
                            string formattedHargaSetelahDiskon = "Rp. 0"; // Default jika diskon 0
                            if (diskon > 0)
                            {
                                int hargaSetelahDiskon = hargaSatuan - (hargaSatuan * diskon / 100);
                                formattedHargaSetelahDiskon = string.Format(new System.Globalization.CultureInfo("id-ID"), "Rp. {0:N0}", hargaSetelahDiskon);
                            }

                            // Menambahkan baris ke DataGridView
                            Data_Barang.Rows.Add(id, nama, kategori, formattedHarga, total, diskon.ToString() + "%", formattedHargaSetelahDiskon);
                        }
                    }
                }
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            FormLogin FL = new FormLogin();
            FL.Show();
            Hide();
        }

        private void DataLogin_Click(object sender, EventArgs e)
        {
            DataUser DU = new DataUser(roleId);
            DU.Show();
            Hide();
        }

        private void DataGudang_Click(object sender, EventArgs e)
        {
            DataGudang DG = new DataGudang();
            DG.Show();
            Hide();
        }

        private void Transaksi_Click(object sender, EventArgs e)
        {
            DataTransaksi DT = new DataTransaksi(roleId, loggedInUsername, loggedInUserId);
            DT.Show();
            Hide();

        }
    }
}
