using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebCrawlerForms
{
    public partial class Form2 : Form
    {
        public Zetels zetels;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            zetels = new Zetels(e.Graphics);
            zetels.TekenAs();
            if (!label1.Text.EndsWith("2015"))
            {
                label1.Text += zetels.DbDatum();
            }
            List<string> zetels_aantallen = zetels.DbAantalZetels();
            List<string> partijen = zetels.DbPartijen();
            List<string> verkiezingen2012 = zetels.DbAantalZetels2012();
            zetels.TekenLabels(partijen);
            zetels.TekenZetels2012(verkiezingen2012);
            zetels.TekenZetels(zetels_aantallen,verkiezingen2012);
        }
    }
}