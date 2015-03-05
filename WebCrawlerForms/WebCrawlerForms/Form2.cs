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
            zetels = new Zetels(pictureBox1.CreateGraphics());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            zetels.TekenAs();
            label1.Text += zetels.DbDatum();
            List<string> zetels_aantallen = zetels.DbAantalZetels();
            List<string> partijen = zetels.DbPartijen();
            zetels.TekenLabels(partijen);
            zetels.TekenZetels(zetels_aantallen);
        }
    }
}
