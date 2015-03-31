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
    public partial class Form4 : Form
    {
        private AgendapuntenController ag;
        private bool status;
        private List<IGenericObject> items;

        public Form4()
        {
            InitializeComponent();
            ag = new AgendapuntenController();
            status = false;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            items = ag.GetAgendapuntenItems();
            List<string> titels = new List<string>();
            List<string> tijd = new List<string>();
            List<string> type = new List<string>();
            List<string> samen = new List<string>();
            for (int i = 0; i < items.Count; i++)
            {
                titels.Add(items[i].GetTitel);
                tijd.Add(items[i].GetDag);
                type.Add(items[i].GetMedia);
                samen.Add(tijd[i] + " " + type[i] + "|" + titels[i]);
            }

            listBox1.DataSource = samen;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (status)
            {
                System.Diagnostics.Process.Start("http://www.tweedekamer.nl" + items[listBox1.SelectedIndex].GetLink);
            }
            status = true;
        }


    }
}
