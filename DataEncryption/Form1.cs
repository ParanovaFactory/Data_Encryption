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

namespace DataEncryption
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=predator;Initial Catalog=Encryption;Integrated Security=True;TrustServerCertificate=True");

        void listCrypted()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from TblData", conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            byte[] bName = ASCIIEncoding.ASCII.GetBytes(name);
            string cName = Convert.ToBase64String(bName);

            string surname = txtSurname.Text;
            byte[] bSurname = ASCIIEncoding.ASCII.GetBytes(surname);
            string cSurname = Convert.ToBase64String(bSurname);

            string mail = txtMail.Text;
            byte[] bMail = ASCIIEncoding.ASCII.GetBytes(mail);
            string cMail = Convert.ToBase64String(bMail);

            string password = txtPassword.Text;
            byte[] bPassword = ASCIIEncoding.ASCII.GetBytes(password);
            string cPassword = Convert.ToBase64String(bPassword);

            string accountNo = txtAccountNo.Text;
            byte[] bAccountNo = ASCIIEncoding.ASCII.GetBytes(accountNo);
            string cAccountNo = Convert.ToBase64String(bAccountNo);

            conn.Open();
            SqlCommand command = new SqlCommand("insert into TblData (Name,Surname,Mail,Password,AccountNo) values (@p1,@p2,@p3,@p4,@p5)", conn);
            command.Parameters.AddWithValue("@p1", cName);
            command.Parameters.AddWithValue("@p2", cSurname);
            command.Parameters.AddWithValue("@p3", cMail);
            command.Parameters.AddWithValue("@p4", cPassword);
            command.Parameters.AddWithValue("@p5", cAccountNo);
            command.ExecuteNonQuery();
            conn.Close();
            listCrypted();
        }

        private void btnDecoing_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            byte[] bName = Convert.FromBase64String(name);
            string cName = ASCIIEncoding.ASCII.GetString(bName);

            string surname = txtSurname.Text;
            byte[] bSurname = Convert.FromBase64String(surname);
            string cSurname = ASCIIEncoding.ASCII.GetString(bSurname);

            string mail = txtMail.Text;
            byte[] bMail = Convert.FromBase64String(mail);
            string cMail = ASCIIEncoding.ASCII.GetString(bMail);

            string password = txtPassword.Text;
            byte[] bPassword = Convert.FromBase64String(password);
            string cPassword = ASCIIEncoding.ASCII.GetString(bPassword);

            string accountNo = txtAccountNo.Text;
            byte[] bAccountNo = Convert.FromBase64String(accountNo);
            string cAccountNo = ASCIIEncoding.ASCII.GetString(bAccountNo);

            richTextBox1.Text = cName + " " + cSurname + " " + cMail + " " + cPassword + " " + cAccountNo;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSurname.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtMail.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtPassword.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtAccountNo.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listCrypted();
        }
    }
}
