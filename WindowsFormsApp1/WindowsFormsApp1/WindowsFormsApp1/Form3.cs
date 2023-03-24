using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    

    public partial class FormDSNV : Form
    {
        private string chucNang = null;

        public FormDSNV()
        {
            InitializeComponent();
            this.Hide();
            loadTableNhanVien();
        }

        private void loadTableNhanVien()
        {
            SqlConnection ketnoi = ConnectionSQL.LayKetNoi();
            string sql = @"EXEC SP_SEL_ENCRYPT_NHANVIEN";

            SqlCommand scmd = new SqlCommand(sql, ketnoi);
            SqlDataReader sdr = scmd.ExecuteReader();

            DataTable dt = new DataTable();

            for (int i = 0; i < table_NhanVien.ColumnCount; ++i)
            {
                dt.Columns.Add(new DataColumn(table_NhanVien.Columns[i].Name));
                table_NhanVien.Columns[i].DataPropertyName = table_NhanVien.Columns[i].Name;
            }

            while (sdr.Read())
            {               
                string manv = sdr.GetString(0);//MaNV
                string hoTen = sdr.GetString(1);//HoTen
                string email = sdr.GetString(2);//Email
                byte[] luongEncrypted = (byte[])sdr.GetValue(3);//Luong
                string LuongDecrypted = Cryption.DecryptStringFromBytes_Aes(luongEncrypted);

                dt.Rows.Add(manv, hoTen, email, LuongDecrypted);
            }
            //table_NhanVien.Columns.Clear();
            table_NhanVien.DataSource = dt;
            

            ketnoi.Close();
        }

        private void themNhanVien(string maNv, string hoTen, string email, string luong, string tenDn, string matKhau)
        {
            SqlConnection ketnoi = ConnectionSQL.LayKetNoi();
            string sql = $@"EXEC SP_INS_ENCRYPT_NHANVIEN '{maNv}', '{hoTen}', '{email}', {luong}, '{tenDn}', {matKhau}";
            SqlCommand scmd = new SqlCommand(sql, ketnoi);
            scmd.ExecuteNonQuery();
            ketnoi.Close();
        }


        private void button_insert_Click(object sender, EventArgs e)
        {
            chucNang = "insert";
        }

        private void button_save_Click(object sender, EventArgs e)
        {

            //lay input
            string maNv = textBox_maNV.Text;
            string hoTen = textBox_hoTen.Text;
            string email = textBox_email.Text;
            string luong = textBox_luong.Text;
            string tenDn = textBox_tenDangNhap.Text;
            string matKhau = textBox_matkhau.Text;

            //ma hoa input
            byte[] luongEncrypted = Cryption.EncryptStringToBytes_Aes(luong);
            string luongEncryptedStr = "0x" + Cryption.ByteArrayToString(luongEncrypted);

            string passEncrypted = Cryption.Hash_SHA1(matKhau);

            //insert database NHANVIEN

            if (chucNang.Equals("insert")) {
                themNhanVien(maNv, hoTen, email, luongEncryptedStr, tenDn, passEncrypted);
                loadTableNhanVien();
            }
            
            
        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
