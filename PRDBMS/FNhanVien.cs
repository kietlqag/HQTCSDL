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
    public partial class FNhanVien : Form
    {
        ConnectDatabase db = new ConnectDatabase();
        public FNhanVien()
        {
            InitializeComponent();
        }

        private void FNhanVien_Load(object sender, EventArgs e)
        {
            try
            {
                this.cuaHangTableAdapter.Fill(this.dATABASE_PROJECT_DBMSDataSet5.CuaHang);
                SqlCommand cmd = new SqlCommand("SELECT * FROM view_DSNhanVien", db.getConnection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không thể truy cập dữ liệu. " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBox1.Text.Trim();
            SqlCommand cmd = new SqlCommand("SearchNhanVien", db.getConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Keyword", keyword);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                db.openConnection();
                SqlCommand cmd = new SqlCommand("AddNhanVien", db.getConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số vào procedure
                cmd.Parameters.Add("@MaNV", SqlDbType.NVarChar, 10).Value = textBox2.Text;
                cmd.Parameters.Add("@TenNV", SqlDbType.NVarChar, 255).Value = textBox3.Text;
                cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar, 10).Value = comboBox1.Text;
                cmd.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = dateTimePicker1.Value.ToString("MM/dd/yyyy");
                cmd.Parameters.Add("@QueQuan", SqlDbType.NVarChar, 255).Value = textBox4.Text;
                cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 255).Value = textBox5.Text;
                cmd.Parameters.Add("@SDT", SqlDbType.NVarChar, 20).Value = textBox6.Text;
                cmd.Parameters.Add("@Mail", SqlDbType.NVarChar, 255).Value = textBox7.Text;
                cmd.Parameters.Add("@ChucVu", SqlDbType.NVarChar, 255).Value = comboBox3.Text;
                cmd.Parameters.Add("@MaCH", SqlDbType.NVarChar, 10).Value = comboBox2.Text;

                // Thực thi procedure
                int rowsAffected = cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("proc_insertAccount", db.getConnection);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@username", SqlDbType.NVarChar, 10).Value = textBox2.Text;
                cmd2.Parameters.Add("@password", SqlDbType.NVarChar, 10).Value = "123";
                cmd2.Parameters.Add("@employee_id", SqlDbType.NVarChar, 10).Value = textBox2.Text;
                cmd2.Parameters.Add("@roles", SqlDbType.NVarChar, 10).Value = comboBox3.Text;
                cmd2.ExecuteNonQuery();
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
            FNhanVien_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                db.openConnection();
                SqlCommand cmd = new SqlCommand("UpdateNhanVien", db.getConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số vào procedure
                cmd.Parameters.Add("@MaNV", SqlDbType.NVarChar, 10).Value = textBox2.Text;
                cmd.Parameters.Add("@TenNV", SqlDbType.NVarChar, 255).Value = textBox3.Text;
                cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar, 10).Value = comboBox1.Text;
                cmd.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = dateTimePicker1.Value.ToString("MM/dd/yyyy");
                cmd.Parameters.Add("@QueQuan", SqlDbType.NVarChar, 255).Value = textBox4.Text;
                cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 255).Value = textBox5.Text;
                cmd.Parameters.Add("@SDT", SqlDbType.NVarChar, 20).Value = textBox6.Text;
                cmd.Parameters.Add("@Mail", SqlDbType.NVarChar, 255).Value = textBox7.Text;
                cmd.Parameters.Add("@ChucVu", SqlDbType.NVarChar, 255).Value = comboBox3.Text;
                cmd.Parameters.Add("@MaCH", SqlDbType.NVarChar, 10).Value = comboBox2.Text;

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
            FNhanVien_Load(sender, e);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy giá trị của ô được click
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox2.Text = row.Cells["Mã nhân viên"].Value.ToString();
                textBox3.Text = row.Cells["Tên nhân viên"].Value.ToString();
                comboBox1.Text = row.Cells["Giới tính"].Value.ToString();
                dateTimePicker1.Text = row.Cells["Ngày sinh"].Value.ToString();
                textBox4.Text = row.Cells["Quê quán"].Value.ToString();
                textBox5.Text = row.Cells["Nơi ở"].Value.ToString();
                textBox6.Text = row.Cells["SDT"].Value.ToString();
                textBox7.Text = row.Cells["Mail"].Value.ToString();
                comboBox3.Text = row.Cells["Chức vụ"].Value.ToString();
                comboBox2.Text = row.Cells["Cửa hàng"].Value.ToString();

            }
        }
    }
}
