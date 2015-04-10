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
    public partial class AgendapuntenForm : Form
    {
        private AgendapuntenController ag;
        private bool status;
        private List<IGenericObject> items;
        private DateTime dt;

        public AgendapuntenForm()
        {
            InitializeComponent();
            ag = new AgendapuntenController();
            status = false;
            dt = DateTime.Now;
            dt = dt.Date + new TimeSpan(0, 0, 0);
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            items = ag.GetAgendapuntenItems();
        }

        private void Initializeren(DateTime dt)
        {
            List<string> titels = new List<string>();
            List<string> tijd = new List<string>();
            List<string> type = new List<string>();
            List<string> samen = new List<string>();
            for (int i = 0; i < items.Count; i++)
            {
                titels.Add(items[i].GetTitel);
                tijd.Add(items[i].GetBeschrijving);
                type.Add(items[i].GetMedia);
                samen.Add(tijd[i] + " " + type[i] + "|" + titels[i]);
            }

            listBox1.DataSource = samen;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime dt = Convert.ToDateTime(dateTimePicker1.Text);
            dt = dt.Date + new TimeSpan(0,0,0);
            VeranderDatum(dt);
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (status)
            {
                System.Diagnostics.Process.Start("http://www.tweedekamer.nl" + items[listBox1.SelectedIndex].GetLink);
            }
            status = true;
        }

        private void VeranderDatum(DateTime dt)
        {
            List<string> titels = new List<string>();
            List<string> tijd = new List<string>();
            List<string> type = new List<string>();
            List<string> samen = new List<string>();
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].GetTijd == dt.Date.ToString())
                {
                    titels.Add(items[i].GetTitel);
                    tijd.Add(items[i].GetBeschrijving);
                    type.Add(items[i].GetMedia);
                    samen.Add(tijd[i] + " " + type[i] + "|" + titels[i]);
                }
            }
            listBox1.DataSource = samen;
        }
    }
}
