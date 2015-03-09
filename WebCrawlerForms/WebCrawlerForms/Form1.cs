﻿using System;
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
        public bool zoek_vvd;
        public bool zoek_pvda;
        public bool zoek_pvv;
        public bool zoek_anders;
        public string andersText;
        public Helper helper;
        public List<string> urls;

        public Form1()
        {
            InitializeComponent();
            adapter = new BNRAdapter();
            helper = new Helper();
            urls = new List<string>();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }

        private void listBox3_MouseClick(object sender, MouseEventArgs e)
        {
            urls.RemoveRange(0, urls.Count);
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
            button5.Visible = false;
            urls.Add(nosAdapter.GetVideo(VideoList[0]));
            axWindowsMediaPlayer2.URL = urls[0];
            if (VideoList.Count > 1)
            {
                for (int i = 1; i < VideoList.Count; i++)
                {
                    urls.Add(nosAdapter.GetVideo(VideoList[i]));
                }
            }
            if (urls.Count > 1)
            {
                button5.Visible = true;
                button5.Text = "Volgende " + "1/" + urls.Count.ToString();
            }
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
            DataTable dt = SQL.Select("SELECT TOP 20 Titel, Link, Bron, Tijd, Dag FROM PolitiekNieuws ORDER BY Dag DESC, Tijd DESC");
            List<string> Titel = new List<string>();
            List<string> Link = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //dt.Rows[i][4] + "-" + dt.Rows[i][3] + "}" + 
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
            listBox3.DataSource = new List<string> { "Maikel is gay" };
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show(string.Format("NOS gebackupt, {0} nieuwe items", helper.NOSBackup().ToString()));
            MessageBox.Show(string.Format("BNR gebackupt, {0} nieuwe items", helper.BNRBackup().ToString()));
            MessageBox.Show(helper.ZetelsBackup());
            MessageBox.Show(helper.WetsvoorstellenBackup());
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
            if (zoek_anders)
                query = string.Format("{0} AND Titel LIKE '%{1}%'", query, andersText);

            query = string.Format("{0} ORDER BY Dag DESC, Tijd DESC", query);

            dt = SQL.Select(query);

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

        private void button5_Click(object sender, EventArgs e)
        {
            int bepaalInt = 0;
            for (int i = 0; i < urls.Count; i++)
            {
                if (urls[i] == axWindowsMediaPlayer2.URL)
                {
                    if (i == urls.Count - 1)
                    {
                        bepaalInt = 0;
                    }
                    else
                        bepaalInt = i + 1;
                }
            }
            axWindowsMediaPlayer2.URL = urls[bepaalInt];
            button5.Text = "Volgende " + (bepaalInt + 1).ToString() + "/" + urls.Count.ToString();
            axWindowsMediaPlayer2.Ctlcontrols.play();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 zetelsform = new Form2();
            zetelsform.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form3 wetsvoorstellen = new Form3();
            wetsvoorstellen.Show();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                andersText = Microsoft.VisualBasic.Interaction.InputBox("Vul een woord in om op te filteren:", "Filter", "Politiek");
                checkBox5.Text = andersText;
            }
            zoek_anders = checkBox5.Checked;
            FilterNieuws();
        }
    }
}