using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BigIntLibrary;
using System.Numerics;
using System.Threading;

namespace BigIntFormTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            tb_res.Text = (BigInt.Parse(tb_num1.Text) + BigInt.Parse(tb_num2.Text)).ToString();
            if (tb_res.Text != (BigInteger.Parse(tb_num1.Text) + BigInteger.Parse(tb_num2.Text)).ToString())
            MessageBox.Show("Error!!!");

        }

        private void btn_sub_Click(object sender, EventArgs e)
        {
            tb_res.Text = (BigInt.Parse(tb_num1.Text) - BigInt.Parse(tb_num2.Text)).ToString();
            if (tb_res.Text != (BigInteger.Parse(tb_num1.Text) - BigInteger.Parse(tb_num2.Text)).ToString())
            MessageBox.Show("Error!!!");
        }

        private void btn_mul_Click(object sender, EventArgs e)
        {
            tb_res.Text = (BigInt.Parse(tb_num1.Text) * BigInt.Parse(tb_num2.Text)).ToString();
            if (tb_res.Text != (BigInteger.Parse(tb_num1.Text) * BigInteger.Parse(tb_num2.Text)).ToString())
            MessageBox.Show("Error!!!");
        }

        private void btn_div_Click(object sender, EventArgs e)
        {
            tb_res.Text = (BigInt.Parse(tb_num1.Text) / BigInt.Parse(tb_num2.Text)).ToString();
            if (tb_res.Text != (BigInteger.Parse(tb_num1.Text) / BigInteger.Parse(tb_num2.Text)).ToString())
            MessageBox.Show("Error!!!");
        }

        static int error = 0;
        private void btn_test_Click(object sender, EventArgs e)
        {
            tests = int.Parse(tb_Tests.Text);
            progressBar1.Minimum = 0;
            progressBar1.Maximum = tests;
            
            btn_test.Enabled = false;
            AsyncFullTest();
        }

        private void btn_gen_Click(object sender, EventArgs e)
        {
            min = int.Parse(tb_Min.Text);
            max = int.Parse(tb_Max.Text);
            tb_num1.Text = GenerateNum(rnd.Next(min, max), true);
            tb_num2.Text = GenerateNum(rnd.Next(min, max), true);
        }

        public static async void AsyncFullTest()
        {
            await Task.Run(() => FullTest(0));
           
        }

        static int min = 1;
        static int max = 800;
        static int[] index = new int[1];
        static int tests = 100000;
        public static int FullTest(object ind)
        {
            int inde = (int)ind;
            for (int i = 0; i < tests; i++)
            {
                index[inde] = i;
                //Clear();
                //WriteLine(i);
                bool Error = false;
                string s1 = GenerateNum(rnd.Next(min, max), true);
                string s2 = GenerateNum(rnd.Next(min, max), true);
                BigInt a1 = BigInt.Parse(s1);
                BigInt b1 = BigInt.Parse(s2);
                BigInteger a2 = BigInteger.Parse(s1);
                BigInteger b2 = BigInteger.Parse(s2);
                BigInt plus1 = a1 + b1;
                BigInteger plus2 = a2 + b2;
                BigInt minus1 = a1 - b1;
                BigInteger minus2 = a2 - b2;
                BigInt mul1 = a1 * b1;
                BigInteger mul2 = a2 * b2;
                BigInt div1 = a1 / b1;
                BigInteger div2 = a2 / b2;
                if (plus1.ToString() != plus2.ToString())
                    Error = true;
                if (minus1.ToString() != minus2.ToString())
                    Error = true;
                if (mul1.ToString() != mul2.ToString())
                    Error = true;
                if (div1.ToString() != div2.ToString())
                    Error = true;
                if (Error)
                {
                    i = -1;
                    return 1;
                }
            }
            return 0;
        }

        static Random rnd = new Random();
        static string chars = "0123456789";
        static string GenerateNum(int Length, bool WithNeg = true)
        {
            StringBuilder s = new StringBuilder(Length - 1);
            if (WithNeg)
                if (rnd.Next(0, 2) == 1)
                    s.Append('-');
            s.Append(chars[rnd.Next(1, 10)]);
            for (int i = 0; i < Length - 1; i++)
            {
                s.Append(chars[rnd.Next(0, 10)]);
            }
            return s.ToString();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (error == 1)
                MessageBox.Show("ERRorr");
            progressBar1.Value = index[0];


        }

       
    }
}
