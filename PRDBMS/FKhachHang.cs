using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PRDBMS
{
    public partial class FKhachHang : Form
    {
        ConnectDatabase db = new ConnectDatabase();
        public FKhachHang()
        {
            InitializeComponent();
        }

        private void FKhachHang_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM view_DSKhachHang", db.getConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                db.openConnection();
                string keyword = textBox1.Text.Trim() + "%"; // Thêm ký tự '%' vào sau từ khóa

                // Khai báo và khởi tạo một đối tượng SqlCommand
                SqlCommand cmd = new SqlCommand("SELECT * FROM SearchKhachHang(@Keyword)", db.getConnection);
                cmd.Parameters.AddWithValue("@Keyword", keyword);

                // Sử dụng SqlDataAdapter để lấy dữ liệu từ SQL Server vào DataTable
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Gán DataTable làm nguồn dữ liệu cho DataGridView
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.closeConnection();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                db.openConnection();
                SqlCommand cmd = new SqlCommand("AddKhachHang", db.getConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số vào procedure
                cmd.Parameters.Add("@MaKH", SqlDbType.NVarChar, 10).Value = textBox2.Text;
                cmd.Parameters.Add("@TenKH", SqlDbType.NVarChar, 255).Value = textBox3.Text;
                cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar, 10).Value = comboBox1.Text;
                cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 255).Value = textBox5.Text;
                cmd.Parameters.Add("@SDT", SqlDbType.NVarChar, 20).Value = textBox6.Text;

                // Thực thi procedure
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected < 0)
                {
                    MessageBox.Show("Thêm thành công.");
                }
                else
                {
                    MessageBox.Show("Thêm thất bại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.closeConnection();
            }
            FKhachHang_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                db.openConnection();
                SqlCommand cmd = new SqlCommand("UpdateKhachHang", db.getConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số vào procedure
                cmd.Parameters.Add("@MaKH", SqlDbType.NVarChar, 255).Value = textBox2.Text;
                cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 255).Value = textBox5.Text;
                cmd.Parameters.Add("@SDT", SqlDbType.NVarChar, 20).Value = textBox6.Text;

                // Thực thi procedure
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected < 0)
                {
                    MessageBox.Show("Sửa thành công.");
                }
                else
                {
                    MessageBox.Show("Sửa thất bại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.closeConnection();
            }
            FKhachHang_Load(sender, e);
        }
    }
}
