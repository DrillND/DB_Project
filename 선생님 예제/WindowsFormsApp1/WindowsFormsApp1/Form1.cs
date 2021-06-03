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
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        OracleConnection oracleConnection; //속성(타입OracleConnection 변수이름oracleConnection)


        public Form1()
        {
            InitializeComponent();
            oracleConnection = new OracleConnection("Data Source=XE;User ID=sqlDB;Password=1234;Unicode=True") ;
        }

        private void open_conn()
        {
            if(oracleConnection.State == ConnectionState.Closed)
                oracleConnection.Open();
           
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //입력처리 텍스트 박스에서 사용자가 입력한 값을 데이터베이스에 입력한다 또는 아이디가 있는 경우 수정
            OracleCommand cmd;
            string query = "";
            int row;

            DataSet ds = new DataSet();
            OracleDataAdapter da = new OracleDataAdapter("select * from cartoon where cartoonid = '" + textBox1.Text + "'",oracleConnection);
            row = da.Fill(ds, "cartoon"); //da를 ds로 넣어 준다.
            
            if (textBox1.Text==""||textBox2.Text==""||textBox3.Text=="")
            {
               
                MessageBox.Show("타이틀","메세지", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
            }
            else
            {
                if (row == 0)//입력처리
                {
                    if (MessageBox.Show("sdd?", "ddd", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        query="insert into cartoon(cartoonid, cartoonname,"+"cartoondescription)" + "values('" +textBox1.Text+ "','" + textBox2.Text + "','" + textBox3.Text + "')";

                        MessageBox.Show("입력되었습니다.");
                    }
                }
                else //업데이트
                {
                    query = "update cartoon set cartonname = '" + textBox2.Text + "',cartoondescription='" + textBox3.Text + "'where cartoonid='" + textBox1.Text + "'";
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
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    textBox1.Focus();
                }

                grid();
            
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            grid();
        }

        private void grid()
        {
            //  그리드 함수를 만들어서 cartoon 테이블에 데이터를 가져와 그리드뷰에 출력한다. 

            OracleDataAdapter da;//로컬 변수
            DataSet ds;

            ds = new DataSet();
            string query;

            query = "select*from cartoon";
            da = new OracleDataAdapter(query, oracleConnection); //연결과 커리 필요함
            da.Fill(ds, "cartoon"); //출력

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "cartoon";
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
