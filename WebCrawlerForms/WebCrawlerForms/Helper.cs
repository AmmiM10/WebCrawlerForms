using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WebCrawlerForms
{
    public class Helper
    {
        private BronInterface adapter;
        private SQL sql;

        public Helper()
        {
            sql = new SQL();
        }

        public int NOSBackup()
        {
            int teller = 0;
            DataTable dt = null;
            adapter = new NOSAdapter();
            adapter.Naam = "NOS";
            List<string> Titels = adapter.GetHeadlines();
            List<string> Links = adapter.GetHeadlineLinks();
            List<string> dag = adapter.GetTime(Links);
            List<string> tijd = new List<string>();
            for (int i = 0; i < Titels.Count; i++)
            {
                dt = sql.Select("SELECT Titel FROM PolitiekNieuws WHERE Titel = '" + Titels[i].Replace("'", "`") + "'");
                if (dt.Rows.Count == 0)
                {
                    tijd.Add(dag[i].Split('T')[1]);
                    tijd[i] = tijd[i].Split('+')[0];
                    dag[i] = dag[i].Split('T')[0];
                    sql.Insert("insert into PolitiekNieuws (Titel, Tijd, Dag, Link, Bron) values ('" + Titels[i].Replace("'", "`") + "', '" + tijd[i] + "', '" + dag[i] + "', '" + Links[i] + "', '" + adapter.Naam + "') ");
                    teller++;
                }
            }
            return teller;
        }

        public int BNRBackup()
        {
            int teller = 0;
            DataTable dt = null;
            adapter = new BNRAdapter();
            adapter.Naam = "BNR";
            List<string> Titels = adapter.GetHeadlines();
            List<string> Links = adapter.GetHeadlineLinks();
            List<string> time = adapter.GetTime(Links);
            List<string> tijd_split = new List<string>();
            List<string> dag_split = new List<string>();
            for (int i = 0; i < time.Count - 1; i++)
            {
                string maand = "01";
                if (time[i + 1].Split(' ')[2] == "Feb")
                {
                    maand = "02";
                }
                else if (time[i + 1].Split(' ')[2] == "Mar")
                {
                    maand = "03";
                }
                else if (time[i + 1].Split(' ')[2] == "Apr")
                {
                    maand = "04";
                }
                else if (time[i + 1].Split(' ')[2] == "Mei")
                {
                    maand = "05";
                }
                dag_split.Add(time[i + 1].Split(' ')[3] + "-" + maand + "-" + time[i + 1].Split(' ')[1]);
                tijd_split.Add(time[i + 1].Split(' ')[4]);
                dt = sql.Select("SELECT Titel FROM PolitiekNieuws WHERE Titel = '" + Titels[i].Replace("'", "`") + "'");
                if (dt.Rows.Count == 0)
                {
                    sql.Insert("insert into PolitiekNieuws (Titel, Tijd, Dag, Link, Bron) values ('" + Titels[i].Replace("'", "`") + "', '" + tijd_split[i] + "', '" + dag_split[i] + "', '" + Links[i + 1] + "', '" + adapter.Naam + "') ");
                    teller++;
                }
            }
            return teller;
        }
    }
}
