using System;
using PcBolt.DAO;
using PcBolt.Beans.Aruk;
using System.Collections.Generic;
namespace PcBolt.Beans.SzamlaBeans
{
    public class Szamla
    {
        #region Adattagok
        long id = -1;

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        long felhasznaloId = -1;
        Felhasznalo felh = null;

        public long FelhasznaloId
        {
            get { return felhasznaloId; }
            set { felhasznaloId = value; }
        }

        public Felhasznalo Felhasznalo
        {
            get
            {
                if (felh == null)
                {
                    felh = Adatbazis.GetFelhasznalo(felhasznaloId);
                }
                return felh;
            }
        }

        DateTime datum;
    

        public DateTime Datum
        {
            get { return datum; }
            set { datum = value; }
        }

        List<Tetel> tetelek = new List<Tetel>();

        public List<Tetel> Tetelek
        {
            get { return tetelek; }
            set { tetelek = value; }
        }

        List<AruCikk> aruk = new List<AruCikk>();

                

        #endregion

        /// <summary>
        /// Egy arut ad a tetelek koze
        /// </summary>
        /// <param name="aru"></param>
        public void AddAru(AruCikk aru)
        {
            AddAru(aru, 1);
        }

        /// <summary>
        /// A parameterben megadott arubol a megadott szamut adja a tetelek koze
        /// </summary>
        /// <param name="aru"></param>
        /// <param name="db"></param>
        public void AddAru(AruCikk aru, int db)
        {
            Tetel t = new Tetel(aru, db,this);
            Tetelek.Add(t);
        }

    }
}