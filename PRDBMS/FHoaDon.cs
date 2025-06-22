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
    public partial class FHoaDon : Form
    {
        ConnectDatabase db = new ConnectDatabase();
        string manv;
        public FHoaDon(string manv)
        {
            InitializeComponent();
            this.manv = manv;
        }

        private void FHoaDon_Load(object sender, EventArgs e)
        {
            this.sanPhamTableAdapter.Fill(this.dATABASE_PROJECT_DBMSDataSet3.SanPham);
            this.hoaDonTableAdapter.Fill(this.dATABASE_PROJECT_DBMSDataSet2.HoaDon);
            this.khachHangTableAdapter.Fill(this.dATABASE_PROJECT_DBMSDataSet1.KhachHang);

            SqlCommand cmd = new SqlCommand("SELECT * FROM view_DSHoaDon", db.getConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private DataTable FHoaDon2_Load()
        {
            DataTable dataTable = new DataTable();
            SqlCommand command = new SqlCommand("SELECT * FROM func_ChiTietHoaDon_MaHD(@MaHD)", db.getConnection);
            command.Parameters.AddWithValue("@MaHD", comboBox4.Text);
            command.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            try
            {
                db.openConnection();
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }

            return dataTable;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage selected = tabControl1.SelectedTab;
            if (selected == tabPage1)
            {
                FHoaDon_Load(sender, e);
            }
            else if (selected == tabPage2)
            {
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                db.openConnection();
                SqlCommand cmd = new SqlCommand("AddChiTietHoaDon", db.getConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số vào procedure AddHoaDon
                cmd.Parameters.Add("@MaHD", SqlDbType.VarChar, 10).Value = comboBox4.Text;
                cmd.Parameters.Add("@MaSP", SqlDbType.VarChar, 10).Value = comboBox3.SelectedValue.ToString();  
                cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = Convert.ToInt32(numericUpDown2.Value);
                // Thực thi procedure AddHoaDon
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Thêm thấy bại.");
                }
                else
                {
                    MessageBox.Show("Thêm thành công.");
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
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                db.openConnection();
                SqlCommand cmd = new SqlCommand("AddHoaDon", db.getConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số vào procedure AddHoaDon
                cmd.Parameters.Add("@MaHD", SqlDbType.VarChar, 10).Value = textBox2.Text;
                cmd.Parameters.Add("@NgayGiaoDich", SqlDbType.Date).Value = dateTimePicker1.Value;
                cmd.Parameters.Add("@MaKH", SqlDbType.VarChar, 10).Value = comboBox1.SelectedValue.ToString();
                cmd.Parameters.Add("@MaNV", SqlDbType.VarChar, 10).Value = manv;

                // Thực thi procedure AddHoaDon
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Thêm thấy bại.");
                }
                else
                {
                    MessageBox.Show("Thêm thành công.");
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
            FHoaDon_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = FHoaDon2_Load();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBox1.Text.Trim();
            SqlCommand cmd = new SqlCommand("SELECT * FROM SearchHoaDon(@Keyword)", db.getConnection);
            cmd.Parameters.AddWithValue("@Keyword", keyword);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                db.openConnection();

                // Khai báo và khởi tạo một đối tượng SqlCommand
                SqlCommand cmd = new SqlCommand("UpdateHoaDon", db.getConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số vào procedure
                cmd.Parameters.AddWithValue("@MaHD", textBox2.Text);

                // Thực thi procedure
                int rowsAffected = cmd.ExecuteNonQuery();

                // Nếu có lỗi xảy ra, rowsAffected sẽ trả về -1
                if (rowsAffected == 0)
                {
                    MessageBox.Show("Cập nhật hóa đơn thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Cập nhật hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            FHoaDon_Load(sender, e);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}