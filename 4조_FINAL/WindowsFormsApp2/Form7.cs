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

namespace WindowsFormsApp2
{
    public partial class Form7 : Form
    {
        string connect_info = "DATA SOURCE = 192.168.0.15/xe; User Id = MOVIE; password = 1234;";
        OracleConnection conn;
        OracleCommand comm;
        OracleCommand comma;
        OracleDataAdapter adt;

        DataSet data;
        Form2 f2;
        Form4 f4;
        public static string ID;
        string title;
        string genre;

        public Form7()
        {
            InitializeComponent();
        }


        public Form7(Form2 form)
        {
            f2 = form;
            InitializeComponent();
        }
        public Form7(Form4 form)
        {
            f4 = form;
            InitializeComponent();
        }

        public int radioBtn()
        {
            if (radioButton1.Checked) { return 1; }
            else if (radioButton2.Checked) { return 2; }
            else if (radioButton3.Checked) { return 3; }
            else if (radioButton4.Checked) { return 4; }
            else { return 5; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            data = new DataSet();
            string sql = "INSERT INTO MEMBERSHIP VALUES ('" + f2.textBox1.Text + "','" + title + "','" + genre + "','" + radioBtn() + "')";
            conn = new OracleConnection(connect_info);
            conn.Open();
            comm = new OracleCommand(sql, conn);
            comm.ExecuteNonQuery();

            this.Hide();

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            string sql = "SELECT MOVIE FROM MOVIELIST WHERE MOVIE LIKE ('" + f4.label7.Text + "')";

            conn = new OracleConnection(connect_info);
            conn.Open();
            comma = new OracleCommand(sql, conn);
            
            
            string sql2 = "SELECT GENRE FROM MOVIELIST WHERE MOVIE = '" + genre + "'";
            comm = new OracleCommand(sql2, conn);
          
            title = comma.ExecuteScalar().ToString();
            genre = comm.ExecuteScalar().ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
