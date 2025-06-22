using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRDBMS
{
    public partial class FSanPham : Form
    {
        ConnectDatabase db = new ConnectDatabase();
        public FSanPham()
        {
            InitializeComponent();
        }

        private void FSanPham_Load(object sender, EventArgs e)
        {
            this.cuaHangTableAdapter.Fill(this.dATABASE_PROJECT_DBMSDataSet.CuaHang);
            SqlCommand cmd = new SqlCommand("SELECT * FROM view_DSSanPham", db.getConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBox1.Text.Trim();
            SqlCommand cmd = new SqlCommand("SearchSanPham", db.getConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Keyword", keyword);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private DataTable Load2()
        {
            DataTable dataTable = new DataTable();
            SqlCommand command = new SqlCommand("SELECT * FROM DSSanPham_CuaHang(@MaCH)", db.getConnection);
            command.Parameters.AddWithValue("@MaCH", comboBox1.Text);
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
            if(selected == tabPage1) 
            {
                FSanPham_Load(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = Load2();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBox2.Text.Trim();
            SqlCommand cmd = new SqlCommand("SearchSanPham2", db.getConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Keyword", keyword);
            cmd.Parameters.AddWithValue("@MaCH", comboBox1.Text);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView2.DataSource = dataTable;
        }
    }
}
