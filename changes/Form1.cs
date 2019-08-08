using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace changes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.Shown += Form1_Shown;
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            button1.Click += Button1_Click;
            button3.Click += Button3_Click;
            label1.Click += Label1_Click;
            label1.Text = (ulong.MaxValue / (ulong)UInt16.MaxValue).ToString();
            label4.Text = ((ulong)1 << 48).ToString();
            label6.Text = decimal.MaxValue.ToString();
            label8.Text = long.MaxValue.ToString();
            label10.Text = (double.MaxValue * 10).ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(textBox1.Text, out decimal r)) { return; }
            double d = (double)r;
            label13.Text = ((int)(Math.Truncate(Math.Log(d, 2))) + 1).ToString();
            decimal r2d2 = (decimal)d;
            textBox3.Text = r2d2.ToString();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            // decimal places
            decimal dVal = 456.789M;
            if (decimal.TryParse(textBox2.Text, out decimal dd)) { dVal = dd; } else { return; };
            int[] parts = Decimal.GetBits(dVal);
            int lo = parts[0];
            int mid = parts[1];
            int hi = parts[2];
            bool sign = (parts[3] & 0x80000000) != 0;
            byte scale = (byte)((parts[3] >> 16) & 0x7F);
            //scale = BitConverter.GetBytes(decimal.GetBits(dVal)[3])[2];
            scale = BitConverter.GetBytes(parts[3])[2];
            decimal d = new decimal(lo: lo, mid: mid, hi: hi, isNegative: sign, scale: scale);
            label12.Text = scale.ToString();

        }

        private void Label1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
