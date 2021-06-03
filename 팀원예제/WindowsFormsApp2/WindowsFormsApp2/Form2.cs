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
    public partial class Form2 : Form
    {
        string connect_info = "DATA SOURCE = 192.168.0.15/xe; User Id = MOVIE; password = 1234;";
        OracleConnection conn;
        OracleCommand comm;
        OracleDataAdapter adt;
        DataSet data = new DataSet();

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string password = textBox2.Text;
            string sql = "SELECT ID FROM MEMBER;";
            conn = new OracleConnection(connect_info);
            adt = new OracleDataAdapter(sql, conn);
            adt.Fill(data, "member");
            DataTable dt = data.Tables[0];
            /*dt.
            MessageBox.Show("hi");*/
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }
    }
}
