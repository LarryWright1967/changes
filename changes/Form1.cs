using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace changes
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll", SetLastError = false)]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);
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
            if (!decimal.TryParse(textBox1.Text, out decimal decValue)) { return; }
            double dubValue = (double)decValue;
            label13.Text = ((int)(Math.Truncate(Math.Log(dubValue, 2))) + 1).ToString();
            decimal redubValue = (decimal)dubValue;
            textBox3.Text = redubValue.ToString();

            int digits = 1;
            ulong multilpier = (ulong)Math.Pow(10, GetScale(decValue));
            label12.Text = multilpier.ToString();
            decValue = decValue * multilpier;
            textBox1.Text = decValue.ToString();
            while (CreateMaxDecimalFromBitCount(digits) < decValue) { digits++; }
            label15.Text = digits.ToString();
        }
        private byte GetScale(decimal dVal)
        {
            return BitConverter.GetBytes(Decimal.GetBits(dVal)[3])[2];
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
            //byte scale = (byte)((parts[3] >> 16) & 0x7F);
            //scale = BitConverter.GetBytes(decimal.GetBits(dVal)[3])[2];
            byte scale = BitConverter.GetBytes(parts[3])[2];
            decimal d = new decimal(lo: lo, mid: mid, hi: hi, isNegative: sign, scale: scale);
            label12.Text = scale.ToString();
        }
        private decimal CreateMaxDecimalFromBitCount(int bits)
        {
            if (bits > 95) { throw new ArgumentException($"The number of bits, {bits} can not be converted to a decimal value."); }
            int lo = 0;
            int mid = 0;
            int hi = 0;
            bool sign = false;
            byte scale = 0;
            if (bits > 64)
            { // 3 ints 2 ints full, 1 partial
                lo = CreateMaxIntFromBitCount(32);
                mid = CreateMaxIntFromBitCount(32);
                hi = CreateMaxIntFromBitCount(bits - 64);
            }
            else if (bits > 32)
            {// 2 ints 1 ints full, 1 partial
                lo = CreateMaxIntFromBitCount(32);
                mid = CreateMaxIntFromBitCount(bits - 32);
                hi = CreateMaxIntFromBitCount(0);
            }
            else if (bits > 0)
            { // 1 int, 1 partial
                lo = CreateMaxIntFromBitCount(bits);
                mid = CreateMaxIntFromBitCount(0);
                hi = CreateMaxIntFromBitCount(0);
            }
            else { throw new ArgumentException($"The number of bits, {bits} can not be converted to a decimal value."); }
            return new decimal(lo: lo, mid: mid, hi: hi, isNegative: sign, scale: scale);
        }
        private int CreateMaxIntFromBitCount(int bits)
        {
            if (bits < 1) { return 0; }
            int returnValue = 1;
            if (bits > 1) { for (int i = 0; i < bits - 1; i++) { returnValue = returnValue << 1; returnValue = returnValue | 1; } }
            return returnValue;
        }

        private void MinBits()
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
