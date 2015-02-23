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
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            adapter = new NOSAdapter();
            adapter.PropLink = "http://www.nos.nl"+listBox2.Items[listBox3.SelectedIndex].ToString();
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
                axWindowsMediaPlayer1.Ctlcontrols.stop();
            }

            label2.Visible = true;
            checkBox1.Visible = true;
            pictureBox2.Visible = true;
        }

        private void HasVideo(BronInterface nosAdapter, List<string> VideoList)
        {
            axWindowsMediaPlayer1.URL = nosAdapter.GetVideo(); 
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
                Width = 456;
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
            var nosAdapter = new NOSAdapter();
            listBox3.DataSource = nosAdapter.GetHeadlines();
            listBox2.DataSource = nosAdapter.GetHeadlineLinks();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }
    }
}