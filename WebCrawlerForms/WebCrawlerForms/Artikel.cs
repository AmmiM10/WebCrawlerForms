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
    public partial class Artikel : Form
    {
        private string _url;

        public Artikel(string linkUrl)
        {
            InitializeComponent();
            this._url = linkUrl; 
            Tekst tekst = new Tekst();
            List<string> ItemsList = new List<string>();

            //ItemsList = tekst.getItems();

            label1.Text = tekst.GetTekst("http://nos.nl" + _url, "<div class=\"media_caption show-medium-up\"><div class=\"caption_content\">\\s*(.+?)\\s* <span class=\"caption_source\">");
        }

        private void Artikel_Load(object sender, EventArgs e)
        {
            
        }
    }
}
