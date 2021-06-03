using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OracleClient;

namespace Cartoon
{
    public partial class Form1 : Form
    {
        OracleConnection oracleConnection;
        public Form1()
        {
            InitializeComponent();
#pragma warning disable CS0618 // Type or member is obsolete
            oracleConnection = new OracleConnection("Data Source=XE;User ID=sqlDB;Password=1234;Unicode=True");
#pragma warning restore CS0618 // Type or member is obsolete
        }

        private void open_conn()
        {
            if (oracleConnection.State == ConnectionState.Closed) oracleConnection.Open();
        }

        private void grid()
        {
            OracleDataAdapter da;
            DataSet ds;
            ds = new DataSet();
            string query;

            query = "Select * from cartoon";
            da = new OracleDataAdapter(query, oracleConnection);
            da.Fill(ds, "cartoon");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "cartoon";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells["cartoonid"].Value.ToString() == "")
            {
                MessageBox.Show("first", "second", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["cartoonid"].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["cartoonname"].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["cartoondescription"].Value.ToString();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            grid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OracleCommand cmd;
            string de = "delete from cartoon where cartoonid = '" + textBox1.Text + "'";
            if (textBox1.Text == "") MessageBox.Show("delete!");
            if (MessageBox.Show("asdfasdf ? ", "ddd", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                open_conn();
                cmd = new OracleCommand();
                try
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = de;
                    cmd.Connection = oracleConnection;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    textBox1.Focus();
                }
            }
            grid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd;
            string query = "";
            int row;

            DataSet ds = new DataSet();
            OracleDataAdapter da = new OracleDataAdapter("select * from cartoon where cartoonid = '" + textBox1.Text + "'", oracleConnection);
            row = da.Fill(ds, "cartoon");

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("asdfasd", "asdfds", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
            }
            else
            {
                if(row == 0)
                {
                    if(MessageBox.Show("asdf ? ", "wodd",MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        query = "insert into cartoon(cartoonid, cartoonname, cartoondescription) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')";
                        MessageBox.Show("입력되었습");
                    }

                }
                else
                {
                    query = "update cartoon set cartoonname = '" + textBox2.Text + "',cartoondescription = '" + textBox3.Text + "' where cartoonid = '" + textBox1.Text + "'";
                    MessageBox.Show("updated");
                }
                cmd = new OracleCommand();
                open_conn();
                try
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Connection = oracleConnection;
                    row = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    textBox1.Focus();
                }
                grid();
            }
        }
    }
}
