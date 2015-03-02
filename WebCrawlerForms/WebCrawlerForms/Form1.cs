using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;

namespace WebCrawlerForms
{
    public partial class Form1 : Form
    {
        public List<string> ItemsLink;
        public BronInterface adapter;
        public SQL sql;
        public bool zoek_vvd;
        public bool zoek_pvda;
        public bool zoek_pvv;
        public Helper helper;

        public Form1()
        {
            InitializeComponent();
            adapter = new BNRAdapter();
            sql = new SQL();
            helper = new Helper();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }

        private void listBox3_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox3.SelectedValue.ToString().StartsWith("NOS"))
                adapter = new NOSAdapter();
            else if (listBox3.SelectedValue.ToString().StartsWith("BNR"))
                adapter = new BNRAdapter();
            axWindowsMediaPlayer2.Ctlcontrols.stop();
            adapter.PropLink = listBox2.Items[listBox3.SelectedIndex].ToString();
            Width = 762;
            checkBox1.Checked = false;
            textBox1.Text = adapter.GetTekst();
            List<string> VideoList = new List<string>();
            VideoList = adapter.GetVideos();
            if (VideoList.Count == 0)
                NullVideo();
            else
            {
                HasVideo(adapter, VideoList);
                axWindowsMediaPlayer2.Ctlcontrols.stop();
            }
            
            label2.Visible = true;
            checkBox1.Visible = true;
            pictureBox2.Visible = true;
        }

        private void HasVideo(BronInterface nosAdapter, List<string> VideoList)
        {
            axWindowsMediaPlayer2.URL = nosAdapter.GetVideo(); 
            Width = 1106;
            axWindowsMediaPlayer2.Visible = true;
            button1.Visible = true;
            button4.Visible = true;
        }
        private void NullVideo()
        {
            axWindowsMediaPlayer2.Visible = false;
            button1.Visible = false;
            button4.Visible = false;
            Width = 761;
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show(textBox1.Text);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                axWindowsMediaPlayer2.Ctlcontrols.stop();
                Width = 456;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (textBox1.Font.Size == 12)
            {
                textBox1.Font = new Font(textBox1.Text, 8);
                label2.Text = "+";
            }
            else
            {
                textBox1.Font = new Font(textBox1.Text, 12);
                label2.Text = "-";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = sql.Select("SELECT Titel, Link, Bron FROM PolitiekNieuws ORDER BY Dag DESC, Tijd DESC");
            List<string> Titel = new List<string>();
            List<string> Link = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Titel.Add(dt.Rows[i][2].ToString() + " | " + dt.Rows[i][0].ToString());
                Link.Add(dt.Rows[i][1].ToString());
            }
            listBox2.DataSource = Link;
            listBox3.DataSource = Titel;
        }
        
         private void button1_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer2.Ctlcontrols.play();
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            axWindowsMediaPlayer2.Ctlcontrols.stop();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            listBox3.Font = new Font(listBox3.Text, 15);
            listBox3.DataSource = new List<string>{"Maikel is gay"};
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show(string.Format("NOS gebackupt, {0} nieuwe items", helper.NOSBackup().ToString()));
            MessageBox.Show(string.Format("BNR gebackupt, {0} nieuwe items", helper.BNRBackup().ToString()));
            Form1_Load(sender, e);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            zoek_vvd = checkBox2.Checked;
            FilterNieuws();
        }

        public void FilterNieuws()
        {
            DataTable dt = new DataTable();
            string query = "SELECT Titel, Link, Bron FROM PolitiekNieuws WHERE Titel LIKE '%%'";

            if (zoek_vvd)
                query = string.Format("{0} AND Titel LIKE '%vvd%'", query);
            if (zoek_pvda)
                query = string.Format("{0} AND Titel LIKE '%pvda%'", query);
            if (zoek_pvv)
                query = string.Format("{0} AND Titel LIKE '%pvv%'", query);

            query = string.Format("{0} ORDER BY Dag DESC, Tijd DESC", query);

            dt = sql.Select(query);

            List<string> Nieuws = new List<string>();
            List<string> Links = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Nieuws.Add(string.Format("{0} | {1}", dt.Rows[i][2], dt.Rows[i][0].ToString()));
                Links.Add(dt.Rows[i][1].ToString());
            }
            listBox2.DataSource = Links;
            listBox3.DataSource = Nieuws;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            zoek_pvda = checkBox3.Checked;
            FilterNieuws();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            zoek_pvv = checkBox4.Checked;
            FilterNieuws();
        }
    }
}