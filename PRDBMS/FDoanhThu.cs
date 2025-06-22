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
using System.Windows.Forms.DataVisualization.Charting;

namespace PRDBMS
{
    public partial class FDoanhThu : Form
    {
        ConnectDatabase db = new ConnectDatabase();
        public FDoanhThu()
        {
            InitializeComponent();
        }

        private void FDoanhThu_Load(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;

            // Lấy ngày đầu tiên của tháng hiện tại
            DateTime firstDayOfMonth = new DateTime(today.Year, today.Month, 1);

            // Gọi phương thức để tải dữ liệu hoặc vẽ biểu đồ với formdate là ngày đầu tiên của tháng và todate là hôm nay
            LoadData(firstDayOfMonth, today);
        }
        private void LoadData(DateTime formdate, DateTime todate)
        {
            Series series = chart1.Series.FindByName("DoanhThu");

            // Nếu chuỗi dữ liệu không tồn tại, thêm nó vào biểu đồ
            if (series == null)
            {
                // Thêm chuỗi dữ liệu mới vào biểu đồ
                series = chart1.Series.Add("DoanhThu");
            }
            // Mở kết nối đến cơ sở dữ liệu
            db.openConnection();

            // Thực thi stored procedure để tính toán doanh thu
            try
            {
                using (SqlCommand command = new SqlCommand("DoanhThu", db.getConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@formdate", formdate);
                    command.Parameters.AddWithValue("@todate", todate);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Xóa dữ liệu cũ trước khi thêm dữ liệu mới vào biểu đồ
                        chart1.Series["DoanhThu"].Points.Clear();

                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0) && !reader.IsDBNull(1) && !reader.IsDBNull(2))
                            {
                                decimal tongThanhTien = reader.GetDecimal(0);
                                decimal tongChiPhi = reader.GetDecimal(1);
                                decimal LoiNhuan = reader.GetDecimal(2);

                                // Thêm dữ liệu vào biểu đồ
                                chart1.Series["DoanhThu"].Points.AddXY("Doanh Thu", tongThanhTien);
                                chart1.Series["DoanhThu"].Points.AddXY("Lợi Nhuận", LoiNhuan);

                                // Đặt màu sắc cho các điểm dữ liệu
                                chart1.Series["DoanhThu"].Points[0].Color = Color.Blue; // Màu xanh cho cột "Doanh Thu"
                                chart1.Series["DoanhThu"].Points[1].Color = Color.Green; // Màu xanh lá cây cho cột "Lợi Nhuận"

                                labelDoanhThu.Text = tongThanhTien.ToString() + " VNĐ";
                                labelLoiNhuan.Text = LoiNhuan.ToString() + " VNĐ";
                            }
                            else
                            {
                                chart1.Series["DoanhThu"].Points.Clear();
                                labelDoanhThu.Text = "0 VNĐ";
                                labelLoiNhuan.Text = "0 VNĐ";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ ở đây
                // Ví dụ:
                MessageBox.Show("Bạn không được phép thực thi lệnh: " + ex.Message);
            }
            finally
            {
                // Đóng kết nối
                db.closeConnection();
            }

            chart1.Legends.Clear();
        }
        private void button2_Click(object sender, EventArgs e)
        {

            DateTime today = DateTime.Today;

            // Lấy ngày đầu tiên của tháng hiện tại
            DateTime firstDayOfMonth = new DateTime(today.Year, today.Month, 1);

            // Gọi phương thức để tải dữ liệu hoặc vẽ biểu đồ với formdate là ngày đầu tiên của tháng và todate là hôm nay
            LoadData(firstDayOfMonth, today);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;

            // Lấy ngày 7 ngày trước
            DateTime weekAgo = today.AddDays(-7);

            // Gọi phương thức để tải dữ liệu hoặc vẽ biểu đồ với formdate là 7 ngày trước và todate là hôm nay
            LoadData(weekAgo, today);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;

            // Gọi phương thức để tải dữ liệu hoặc vẽ biểu đồ với formdate và todate là hôm nay
            LoadData(today, today);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
