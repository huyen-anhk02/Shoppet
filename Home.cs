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

namespace ShopPet
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect= false;
            dataGridView1.RowHeadersVisible= false;
            loadDataCategory();
        }

        public void loadDataCategory()
        {
         
            SqlConnection conn = new Form1().conn;
            conn.Open();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("GET_ALL_CATEGORY", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader da = cmd.ExecuteReader();
            dt.Load(da);
            conn.Close();
            da.Close();
            cmd.Dispose();

            comboBox1.Items.Add("All");
            foreach (DataRow item in dt.Rows)
            {
                comboBox1.Items.Add(item["CategoryName"]);
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
           loadPet();
        }

        public void loadPet()
        {
            if (comboBox1.Text.Length > 0)
            {
                SqlConnection conn = new Form1().conn;
                conn.Open();
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("GET_PET_BY_CATEGORY", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (comboBox1.Text.Equals("All"))
                {
                    cmd.Parameters.Add("@CATE", SqlDbType.VarChar).Value = "%";
                }
                else cmd.Parameters.Add("@CATE", SqlDbType.VarChar).Value = comboBox1.Text;
                SqlDataReader da = cmd.ExecuteReader();
                dt.Load(da);
                conn.Close();
                da.Close();
                cmd.Dispose();

                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = null;
                dataGridView1.Refresh();
            }
        }
    }
}
