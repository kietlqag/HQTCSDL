using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRDBMS
{
    public partial class LOGIN : Form
    {
        ConnectDatabase db = new ConnectDatabase();

        public LOGIN()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                db.openConnectionADmin();
                string tk = textBox1.Text;
                string mk = textBox2.Text;
                SqlCommand cmd = new SqlCommand("SELECT dbo.checkLogin(@user, @pass)",
                                                db.getConnectionAdmin);
                cmd.Parameters.AddWithValue("@user", tk);
                cmd.Parameters.AddWithValue("@pass", mk);
                bool count = (bool)cmd.ExecuteScalar();
                if (count == true)
                {
                    GLOBAL.username = textBox1.Text;
                    GLOBAL.password = textBox2.Text;
                    FMain f = new FMain(textBox1.Text);
                    this.Hide();
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu chưa chính xác vui lòng nhập lại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
            finally
            {
                db.closeConnectionAdmin(); // Đảm bảo rằng kết nối sẽ được đóng ngay cả khi có ngoại lệ xảy ra
            }
        }
    }
}
