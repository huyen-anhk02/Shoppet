using System.Data.SqlClient;

namespace ShopPet
{
    public partial class Form1 : Form
    {
        public SqlConnection conn;
        public Form1()
        {
            InitializeComponent();
            conn = new SqlConnection("server=ANHCNTT\\SQLEXPRESS;database= ShopPet; " +
                "integrated security = true");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            conn.Open();
            string us = txtUsname.Text;
            string pass = txtPass.Text;
            try
            {
                string insert = "select COUNT(*) from Account wher" +
                    "e [AccountID] = '" + us + "' and [AccountPass] = '" + pass + "';";
                SqlCommand cmd = new SqlCommand(insert, conn);
                int result = (int)cmd.ExecuteScalar();
                if (result>0)
                {

                    conn.Close();

                    Home myForm = new Home();
                    this.Hide();
                    myForm.ShowDialog();
                    this.Close();

                }
                else
                {
                    err.Text = "Username or Password invalid!";
                    txtUsname.Text = "";
                    txtPass.Text = "";
                    conn.Close();
                }
            }
            catch
            {
                err.Text = "Error";
                conn.Close();
            }
        }
    }
}