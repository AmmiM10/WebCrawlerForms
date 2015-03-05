using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

namespace WebCrawlerForms
{
    public class Helper
    {
        private BronInterface adapter;
        private SQL sql;
        private Zetels zetels;
        private Wetsvoorstellen wv;

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

        public string ZetelsBackup()
        {
            string messagereturn = null;
            zetels = new Zetels();
            string datum = zetels.HaalDatumOp();
            DataTable dt = sql.Select("SELECT PeilingDatum FROM Zetels WHERE PeilingDatum = '" + datum + "'");
            if (dt.Rows.Count == 0)
            {
                sql.Update("UPDATE Zetels SET PeilingDatum = '" + datum + "' WHERE Id = '2'");
                List<string> partijen = zetels.HaalPartijenOp();
                List<string> aantalzetels = zetels.HaalZetelsOp();
                for (int i = 0; i < partijen.Count; i++)
                {
                    sql.Update("UPDATE Zetels SET Aantal = '" + Convert.ToInt32(aantalzetels[i]) + "' WHERE Partij = '" + partijen[i] + "'");
                    //sql.Insert("INSERT INTO Zetels (Partij, Aantal) VALUES ('"+ partijen[i] +"', '"+ Convert.ToInt32(aantalzetels[i]) +"')");
                }
                messagereturn = "Er zijn nieuwe peilingen";
            }
            else
                messagereturn = "Er zijn geen nieuwe peilingen";

            return messagereturn;
        }

        public string WetsvoorstellenBackup()
        {
            string messagereturn = null;
            wv = new Wetsvoorstellen();
            List<string> titels = wv.HaalTitelOp();
            List<string> links = wv.HaalLinkOp();
                for (int i = 0; i < titels.Count; i++)
                {
                    DataTable dt = sql.Select("SELECT * FROM Wetsvoorstellen WHERE Link = '" + links[i] + "'");
                    if (dt.Rows.Count == 0)
                    {
                        string[] lines = Regex.Split(titels[i].ToString(), "\n");
                        sql.Insert("INSERT INTO Wetsvoorstellen (Titel, Link) VALUES ('" + lines[0] + "', '" + links[i] + "')");
                        messagereturn = "Wetsvoorstellen zijn bijgewerkt";
                    }
                    else
                        messagereturn = "Wetsvoorstellen zijn niet bijgewerkt";
                }
            return messagereturn;
        }
    }
}
