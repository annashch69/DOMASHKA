using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;


namespace DOMASHKA
{
    public partial class Form1 : Form
    {
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();
           
        }

     

        private void Form1_Load(object sender, EventArgs e)
        {
     //Statement 2
            var cs = "Host=localhost;Username=postgres;Password=1111;Database=Tovar";

            //Statement 3
            NpgsqlConnection con = new NpgsqlConnection(cs);
            con.Open();

            //Statement 4
            var sql = "Select * from products";

            //Statement 5
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);

            //Statement 6
            var dataReader = cmd.ExecuteReader();

            //Statement 7
            DataTable dt = new DataTable();
            dt.Load(dataReader);

            //Statement 8
            con.Close();
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }
    }
}
