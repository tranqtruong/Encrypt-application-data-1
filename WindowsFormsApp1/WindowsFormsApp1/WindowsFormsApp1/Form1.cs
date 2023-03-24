using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;
using System.Collections;

namespace WindowsFormsApp1
{
    public partial class FormDangNhap : Form
    {

        public FormDangNhap()
        {
            InitializeComponent();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //nut dang nhap
            string username = textBox_dangNhap.Text.ToUpper();
            string passwd = textBox_Password.Text;
            //mã hóa mật khẩu
            string passwdHash = Cryption.Hash_SHA1(passwd);

            SqlConnection ketnoi = ConnectionSQL.LayKetNoi();
            string sql = $@"select * from NHANVIEN where TENDN = '{username}' AND MATKHAU = {passwdHash}";
            
            SqlCommand cmd = new SqlCommand(sql, ketnoi);
            SqlDataReader read = cmd.ExecuteReader();

            if (read.Read() == true)
            {
                //MessageBox.Show("Đăng nhập thành công thì hiển thị Form Danh sách nhân viên");
                FormDSNV nv = new FormDSNV();
                nv.ShowDialog();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không hợp lệ");
            }

            ketnoi.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Application.Exit();
        }

        private void FormDangNhap_Load(object sender, EventArgs e)
        {
            
        }
    }
}
