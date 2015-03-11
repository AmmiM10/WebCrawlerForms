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
        private Agenda ag;
        private bool status;

        public Form4()
        {
            InitializeComponent();
            ag = new Agenda();
            status = false;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            List<string> titels = ag.HaalTitelOp();
            List<string> tijd = ag.HaalTijdOp();
            List<string> type = ag.HaalTypeOp();

            List<string> samen = new List<string>();
            for (int i = 0; i < titels.Count; i++)
			{
			    samen.Add(tijd[i]+ " " +  type[i]+"|"+titels[i]);
			}

            listBox1.DataSource = samen;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (status)
            {
                List<string> links = ag.HaalLinkOp();
                System.Diagnostics.Process.Start("http://www.tweedekamer.nl" + links[listBox1.SelectedIndex]);
            }
            status = true;
        }


    }
}
