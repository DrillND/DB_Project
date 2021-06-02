using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string connect_info = "DATA SOURCE = 192.168.0.15/xe; User Id = MOVIE; password = 1234;";
        OracleConnection conn;
        OracleCommand comm;
        OracleDataAdapter adt;
        DataSet data = new DataSet();
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = string.Format(textBox1.Text);
            string password = string.Format(textBox2.Text);
            DataSet data = new DataSet();
            string sql = "SELECT * FROM BUYTBL3"; //회원정보 DB에  ID == ID, PASSWORD==PASSWORD면 로그인 출력
            conn = new OracleConnection(connect_info);
            conn.Open();
            adt = new OracleDataAdapter(sql, conn);
            

            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string genre = comboBox1.SelectedItem.ToString();
            int year = 1970;
            string country = comboBox3.SelectedItem.ToString();
            string title = string.Format(textBox2.Text);
            DataSet data = new DataSet();

            string sql = "SELECT * FROM MOVIELIST WHERE GENRE = '" + genre + "' AND COUNTRY = '" + country + "'" ;

            //"SELECT * FROM MOVIELIST WHERE GENRE = '" + genre + "'AND YEAR = " + year + " AND COUNTRY = '" + country + "' AND MOVIE = '" + title + "';";
            conn = new OracleConnection(connect_info);
            conn.Open();
            adt = new OracleDataAdapter(sql, conn);
            adt.Fill(data);
            dataGridView1.DataSource = data.Tables[0];

            conn.Close();
        }
    }
}
