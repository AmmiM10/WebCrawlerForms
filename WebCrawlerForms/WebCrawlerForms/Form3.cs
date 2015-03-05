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
    public partial class Form3 : Form
    {
        private Wetsvoorstellen wv;

        public Form3()
        {
            InitializeComponent();
            wv = new Wetsvoorstellen();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = wv.DbTitels();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> links = wv.DbLinks();
            System.Diagnostics.Process.Start("http://www.tweedekamer.nl"+links[listBox1.SelectedIndex]);
        }
    }
}
