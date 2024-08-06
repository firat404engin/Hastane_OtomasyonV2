using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Hastane_OtomasyonV2
{
    public partial class Form1 : Form
    {

        public bool dogrulamaYapildi = false;

        public Form1()
        {
            InitializeComponent();

            
            this.AcceptButton = button1;
        }

        private void captcha()
        {
            string[] harf = { "a", "b", "c", "d", "e", "f" };
            Random rnd = new Random();
            int s1, s2, s3, s4;
            s1 = rnd.Next(0, 9);
            s2 = rnd.Next(0, harf.Length);
            s3 = rnd.Next(0, 9);
            s4 = rnd.Next(0, harf.Length);
            label2.Text = s1.ToString() + harf[s2].ToString() + s3.ToString() + harf[s4].ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            captcha();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label2.Text == textBox1.Text)
            {
                MessageBox.Show("Tebrikler, onaylandı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dogrulamaYapildi = true;
                this.Hide();
            }
            else
            {
                textBox1.Text = "";
                MessageBox.Show("Tekrar deneyiniz.", "Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                captcha();
            }
        }
      
      /*  private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!dogrulamaYapildi)
            {
                MessageBox.Show("Doğrulama yapılmadan işaretlenemez.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                checkBox1.Checked = false; // Checkbox'u işaretsiz yap
            }
        }*/

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!dogrulamaYapildi)
            {
                MessageBox.Show("Doğrulama yapılmadan formun dışına tıklanamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            captcha();
        }
    }

}
