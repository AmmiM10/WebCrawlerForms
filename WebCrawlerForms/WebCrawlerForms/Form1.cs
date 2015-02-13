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

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }

        private void listBox3_MouseClick(object sender, MouseEventArgs e)
        {
            Tekst tekst = new Tekst();
            Video video = new Video();
            Width = 762;
            checkBox1.Checked = false;
            WebClient wb2 = new WebClient();
            string geheleUrl = "http://www.nos.nl"+listBox2.Items[listBox3.SelectedIndex].ToString();
            String x = wb2.DownloadString(geheleUrl);
            textBox1.Text = tekst.GetTekst(geheleUrl, "<div class=\"article_textwrap\"><p>\\s*(.+?)</p></div></div></section><footer class=\"container\">");
            List<string> VideoList = new List<string>();
            VideoList = tekst.getItems(geheleUrl, "<a href=\"/video/\\s*(.+?)\" class=\"video");
            if (VideoList.Count == 0)
                NullVideo();
            else
                HasVideo(video, VideoList);

            label2.Visible = true;
            checkBox1.Visible = true;
            pictureBox2.Visible = true;
        }

        private void HasVideo(Video video, List<string> VideoList)
        {
            axWindowsMediaPlayer1.URL = video.getVideo(VideoList[0], "preload=\"metadata\"><source src=\"(.+?)type=\"360p\" data-label"); 
            Width = 1106;
            axWindowsMediaPlayer1.Visible = true;
            button1.Visible = true;
            button4.Visible = true;
        }

        private void NullVideo()
        {
            axWindowsMediaPlayer1.Visible = false;
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
                Width = 460;
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
            //</span><time datetime=\"
            Tekst tekst = new Tekst();
            listBox3.DataSource = tekst.getItems("http://nos.nl/nieuws/politiek/archief/", "class=\"list-time__title link-hover\">(.+?)</div></a></li><li class=\"list-time__item\">");
            listBox2.DataSource = tekst.getItems("http://nos.nl/nieuws/politiek/archief/", "<li class=\"list-time__item\"><a href=\"(.+?)\" class=\"link-block\">");
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.uiMode = "mini";
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }
    }
}
