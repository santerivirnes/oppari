using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Crm.Sdk.Samples;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            checkBox1.Appearance = Appearance.Button;
            checkBox2.Appearance = Appearance.Button;
            checkBox3.Appearance = Appearance.Button;
            checkBox4.Appearance = Appearance.Button;
            checkBox5.Appearance = Appearance.Button;
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                MessageBox.Show("1 Valittu");
            }
            else
            {
                MessageBox.Show("1 Ei Valittu");
            }
            if (checkBox2.Checked == true)
            {
                MessageBox.Show("2 Valittu");
            }
            else
            {
                MessageBox.Show("2 Ei Valittu");
            }
            if (checkBox3.Checked == true)
            {
                MessageBox.Show("3 Valittu");
            }
            else
            {
                MessageBox.Show("3 Ei Valittu");
            }
            Start_Api(textBox1.Text);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Start_Api(textBox1.Text);
            (sender as Button).Enabled = false;
            button2.Enabled = true;
            textBox1.Enabled = false;
        }

        public void Start_Api(string var)
        {
            Timer MyTimer = new Timer();
            MyTimer.Interval = (Convert.ToInt32(var) * 60 * 1000); // 1 mins
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            MyTimer.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            (sender as Button).Enabled = false;
            textBox1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            (sender as Button).Enabled = false;


        }


    }
}
