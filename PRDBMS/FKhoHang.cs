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

namespace PRDBMS
{
    public partial class FKhoHang : Form
    {
        ConnectDatabase db = new ConnectDatabase();
        public FKhoHang()
        {
            InitializeComponent();
        }

        private void FKhoHang_Load(object sender, EventArgs e)
        {
            try
            {
                this.phieuNhapTableAdapter.Fill(this.dATABASE_PROJECT_DBMSDataSet4.PhieuNhap);
                SqlCommand cmd = new SqlCommand("SELECT * FROM view_DSPhieuNhap", db.getConnection);
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

        private DataTable FKhoHang2_Load()
        {
            DataTable dataTable = new DataTable();
            SqlCommand command = new SqlCommand("SELECT * FROM func_ChiTietPhieuNhap(@MaPN)", db.getConnection);
            command.Parameters.AddWithValue("@MaPN", comboBox4.Text);
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

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = FKhoHang2_Load();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBox1.Text.Trim();
            SqlCommand cmd = new SqlCommand("SearchPhieuNhap", db.getConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Keyword", keyword);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }
    }
}
