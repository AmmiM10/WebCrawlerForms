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
    public partial class ZetelsForm : Form
    {
        public ZetelsController zc;
        public Graphics paper;

        public ZetelsForm()
        {
            InitializeComponent();
            paper = pictureBox1.CreateGraphics();
            zc = new ZetelsController();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            List<IGenericObject> zetelsitems = zc.GetZetelsItems();
            //TekenAs();
            paper.DrawLine(new Pen(new SolidBrush(Color.White)), 10, 10, 100,100);
            //TekenLabels(zetelsitems);
            //TekenZetels(zetelsitems);
        }

        public void TekenLabels(List<IGenericObject> partijen)
        {
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            for (int i = 0; i < partijen.Count; i++)
            {
                paper.DrawString(partijen[i].GetTitel, new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular), new SolidBrush(Color.White), (i * 40) + 40, 200, drawFormat);
            }
        }

        public void TekenZetels(List<IGenericObject> zetels)
        {
            for (int i = 0; i < zetels.Count; i++)
            {
                int cijfer = Convert.ToInt32(zetels[i].GetBeschrijving);
                paper.DrawLine(new Pen(new SolidBrush(Color.White), 2), (i * 40) + 45, 195, (i * 40) + 45, 195 - (cijfer * 5));
                paper.DrawString(zetels[i].GetBeschrijving, new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular), new SolidBrush(Color.White), (i * 40) + 40, 10);
            }
        }

        public void TekenAs()
        {
            paper.DrawLine(new Pen(new SolidBrush(Color.SeaGreen), 3), 30, 10, 30, 195);
            paper.DrawLine(new Pen(new SolidBrush(Color.SeaGreen), 3), 30, 195, 500, 195);
            Pen pen = new Pen(new SolidBrush(Color.White));
            float[] dashValues = { 5, 2, 15, 4 };
            pen.DashPattern = dashValues;
            paper.DrawLine(pen, 20, 195 - (30 * 5), 500, 195 - (30 * 5));
            paper.DrawLine(pen, 20, 195 - (20 * 5), 500, 195 - (20 * 5));
            paper.DrawLine(pen, 20, 195 - (10 * 5), 500, 195 - (10 * 5));
            paper.DrawString("30", new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular), new SolidBrush(Color.White), 3, 187 - (30 * 5));
            paper.DrawString("20", new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular), new SolidBrush(Color.White), 3, 187 - (20 * 5));
            paper.DrawString("10", new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular), new SolidBrush(Color.White), 3, 187 - (10 * 5));
        }
    }
}