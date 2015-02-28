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

        public Form1()
        {
            InitializeComponent();
            adapter = new BNRAdapter();
            sql = new SQL();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }

        private void listBox3_MouseClick(object sender, MouseEventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
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
            {
                axWindowsMediaPlayer1.Ctlcontrols.stop();
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
            List<string> Titels = adapter.GetHeadlines();
            listBox3.DataSource = Titels;
            List<string> Links = adapter.GetHeadlineLinks();
            List<string> time = adapter.GetTime(Links);
            for (int i = 1; i < time.Count; i++)
            {
                //time[i] = time[i].Split("2015")[0];
            }
            listBox2.DataSource = Links;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox3.Font = new Font(listBox3.Text, 15);
            listBox3.DataSource = new List<string>{"Maikel is gay"};
        }

        private void button5_Click(object sender, EventArgs e)
        {
            adapter = new NOSAdapter();
            Form1_Load(sender, e);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //BackupOffline(new NOSAdapter());

            //DataTable dt = sql.Select("SELECT * FROM PolitiekNieuws ORDER BY Dag DESC, Tijd DESC");
        }

        private void BackupOffline(BronInterface adapter)
        {
            List<string> Titels = adapter.GetHeadlines();
            List<string> Links = adapter.GetHeadlineLinks();
            List<string> dag = adapter.GetTime(Links);
            List<string> tijd = new List<string>();
            for (int i = 0; i < Titels.Count; i++)
            {
                DataTable dt = sql.Select("SELECT Titel FROM PolitiekNieuws WHERE Titel = '" + Titels[i].Replace("'", "`") + "'");
                if (dt.Rows.Count == 0)
                {
                    tijd.Add(dag[i].Split('T')[1]);
                    tijd[i] = tijd[i].Split('+')[0];
                    dag[i] = dag[i].Split('T')[0];

                    sql.Insert("insert into PolitiekNieuws (Titel, Tijd, Dag, Link, Bron) values ('" + Titels[i].Replace("'", "`") + "', '" + tijd[i] + "', '" + dag[i] + "', '"+ Links[i] +"', '"+ adapter.Naam +"') ");
                }
            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                zoek_vvd = true;
            }
            else
            {
                zoek_vvd = false;
            }
            FilterNieuws();
        }
        public void FilterNieuws()
        {
            DataTable dt = new DataTable();

            if (zoek_pvda && zoek_vvd)
            {
                dt = sql.Select("SELECT Titel, Link FROM PolitiekNieuws");
            }
            else if (!zoek_pvda && !zoek_vvd)
            {
                dt = sql.Select("SELECT Titel, Link FROM PolitiekNieuws");
            }
            else if (zoek_vvd)
            {
                dt = sql.Select("SELECT Titel, Link FROM PolitiekNieuws WHERE Titel LIKE '%vvd%'");
            }
            else if (zoek_pvda)
            {
                dt = sql.Select("SELECT Titel, Link FROM PolitiekNieuws WHERE Titel LIKE '%PvdA%'");
            }

            List<string> Nieuws = new List<string>();
            List<string> Links = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Nieuws.Add(dt.Rows[i][0].ToString());
                Links.Add(dt.Rows[i][1].ToString());
            }
            listBox2.DataSource = Links;
            listBox3.DataSource = Nieuws;
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                zoek_pvda = true;
            }
            else
            {
                zoek_pvda = false;
            }
            FilterNieuws();
        }
    }
}