using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebCrawlerForms.Forms
{
    public partial class ZetelForm : Form
    {
        public ZetelsController zetels;
        public Graphics paper;

        public ZetelForm()
        {
            InitializeComponent();
            paper = pictureBox1.CreateGraphics();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            zetels = new ZetelsController(e.Graphics);
            List<IGenericObject> zetelsitems = zetels.GetZetelsItems();
            zetels.TekenAs();
            zetels.TekenLabels(zetelsitems);
            zetels.TekenZetels(zetelsitems);
        }
    }
}
