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
using WebcrawlerService;
using WebCrawlerForms.Forms;

namespace WebCrawlerForms
{
    public partial class NieuwsForm : Form
    {
        public List<string> ItemsLink;
        public bool zoek_vvd;
        public bool zoek_pvda;
        public bool zoek_pvv;
        public bool zoek_anders;
        public string andersText;
        public List<string> urls;
        public List<IGenericObject> NieuwsItems;
        public string[] videos;

        public NieuwsForm()
        {
            InitializeComponent();
            urls = new List<string>();
            NieuwsItems = new NieuwsItemsController().GetNieuwsItems();
            videos = new string[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Deze functie behoort eigenlijk alleen toe aan de service, maar staat nu hier voor makkelijker testen");
            WebcrawlerService.Webcrawler test = new WebcrawlerService.Webcrawler();
            test.CrawlContent();
            MessageBox.Show("Klaar");
        }

        private void listBox3_MouseClick(object sender, MouseEventArgs e)
        {
            videos = new string[0];
            textBox1.Text = NieuwsItems[listBox3.SelectedIndex].GetBeschrijving;
            Width = 762;
            checkBox1.Checked = false;
            axWindowsMediaPlayer2.Ctlcontrols.stop();

            string videos_string = NieuwsItems[listBox3.SelectedIndex].GetMedia;
            if (videos_string.Contains(';'))
            {
                videos = NieuwsItems[listBox3.SelectedIndex].GetMedia.Split(';');
                int videoscount = videos.Count();
                videos = videos.Take(videos.Count() - 1).ToArray();
            }
            if (videos.Count() == 0)
            {
                NullVideo();
            }
            else
            {
                HasVideo(videos);
                axWindowsMediaPlayer2.Ctlcontrols.stop();
            }

            label2.Visible = true;
            checkBox1.Visible = true;
            pictureBox2.Visible = true;
        }

        private void HasVideo(string[] VideoList)
        {
            button5.Visible = false;
            axWindowsMediaPlayer2.URL = VideoList[0];
            if (VideoList.Count() > 1)
            {
                button5.Visible = true;
                button5.Text = "Volgende " + "1/" + VideoList.Count().ToString();
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
            List<string> Titel = new List<string>();
            List<string> Link = new List<string>();

            if (NieuwsItems.Count > 0)
            {
                for (int i = 0; i < NieuwsItems.Count; i++)
                {
                    Titel.Add(NieuwsItems[i].GetBron + "|" + NieuwsItems[i].GetTitel);
                    Link.Add(NieuwsItems[i].GetLink);
                }
                listBox2.DataSource = Link;
                listBox3.DataSource = Titel;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer2.Ctlcontrols.play();
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            axWindowsMediaPlayer2.Ctlcontrols.stop();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            zoek_vvd = checkBox2.Checked;
            FilterNieuws(sender, e);
        }

        public void FilterNieuws(object sender, EventArgs e)
        {
            if (checkBox2.Checked || checkBox3.Checked || checkBox4.Checked || checkBox5.Checked)
            {
                List<string> nieuwsfilter = new List<string>();
                List<string> linksfilter = new List<string>();
                for (int i = 0; i < NieuwsItems.Count; i++)
                {
                    if (zoek_vvd)
                    {
                        if (NieuwsItems[i].GetTitel.Contains("VVD"))
                        {
                            nieuwsfilter.Add(NieuwsItems[i].GetBron + "|" + NieuwsItems[i].GetTitel);
                            linksfilter.Add(NieuwsItems[i].GetLink);
                        }
                    }
                    if (zoek_pvda)
                    {
                        if (NieuwsItems[i].GetTitel.Contains("PvdA"))
                        {
                            nieuwsfilter.Add(NieuwsItems[i].GetBron + "|" + NieuwsItems[i].GetTitel);
                            linksfilter.Add(NieuwsItems[i].GetLink);
                        }
                    }
                    if (zoek_pvv)
                    {
                        if (NieuwsItems[i].GetTitel.Contains("PVV"))
                        {
                            nieuwsfilter.Add(NieuwsItems[i].GetBron + "|" + NieuwsItems[i].GetTitel);
                            linksfilter.Add(NieuwsItems[i].GetLink);
                        }
                    }
                    if (zoek_anders)
                    {
                        if (NieuwsItems[i].GetTitel.Contains(andersText))
                        {
                            nieuwsfilter.Add(NieuwsItems[i].GetBron + "|" + NieuwsItems[i].GetTitel);
                            linksfilter.Add(NieuwsItems[i].GetLink);
                        }
                    }
                }
                listBox2.DataSource = linksfilter;
                listBox3.DataSource = nieuwsfilter;
            }
            else
            {
                Form1_Load(sender,e);
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            zoek_pvda = checkBox3.Checked;
            FilterNieuws(sender,e);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            zoek_pvv = checkBox4.Checked;
            FilterNieuws(sender,e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int bepaalInt = 0;
            for (int i = 0; i < videos.Count(); i++)
            {
                if (videos[i] == axWindowsMediaPlayer2.URL)
                {
                    if (i == videos.Count() - 1)
                    {
                        bepaalInt = 0;
                    }
                    else
                        bepaalInt = i + 1;
                }
            }
            axWindowsMediaPlayer2.URL = videos[bepaalInt];
            button5.Text = "Volgende " + (bepaalInt + 1).ToString() + "/" + videos.Count().ToString();
            axWindowsMediaPlayer2.Ctlcontrols.play();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ZetelForm zetelform = new ZetelForm();
            zetelform.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            WetsvoorstellenForm wetsvoorstellen = new WetsvoorstellenForm();
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
            FilterNieuws(sender,e);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AgendapuntenForm agenda = new AgendapuntenForm();
            agenda.Show();
        }
    }
}