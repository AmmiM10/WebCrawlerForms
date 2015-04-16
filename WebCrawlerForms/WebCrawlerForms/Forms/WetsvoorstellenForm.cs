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
    public partial class WetsvoorstellenForm : Form
    {
        private WetsvoorstellenController wv;
        private bool status;
        private List<IGenericObject> items;

        public WetsvoorstellenForm()
        {
            InitializeComponent();
            wv = new WetsvoorstellenController();
            status = false;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            items = wv.GetWetsvoorstellenItems();
            List<string> titels = new List<string>();
            for (int i = 0; i < items.Count; i++)
            {
                titels.Add(items[i].GetTijd.Day + "-" + items[i].GetTijd.Month + " | " + items[i].GetTitel);
            }
            listBox1.DataSource = titels;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (status)
            {
                System.Diagnostics.Process.Start("http://www.tweedekamer.nl" + items[listBox1.SelectedIndex].GetLink);
            }
            status = true;
        }
    }
}