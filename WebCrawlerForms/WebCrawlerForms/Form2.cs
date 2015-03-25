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
    public partial class Form2 : Form
    {
        public ZetelsController zetels;

        public Form2()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            zetels = new ZetelsController(e.Graphics);
            List<IGenericObject> zetelsitems = zetels.GetZetelsItems();
            zetels.TekenAs();

            if (!label1.Text.EndsWith("2015"))
            {
                label1.Text += zetelsitems[0].GetDag.ToString();
            }
            zetels.TekenLabels(zetelsitems);
            zetels.TekenZetels(zetelsitems);
        }
    }
}