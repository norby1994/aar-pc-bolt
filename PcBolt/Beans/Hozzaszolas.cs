using System;
using PcBolt.Beans.Aruk;

namespace PcBolt.Beans
{
    public class Hozzaszolas
    {
        #region Adattagok definialasa
        long id = -1;

        public long Id
        {
            get { return id; }
            set { id = value; }
        }
        long aruCikkId = -1;

        public long AruCikkId
        {
            get { return aruCikkId; }
            set { aruCikkId = value; }
        }
        long felhasznaloId = -1;

        public long FelhasznaloId
        {
            get { return felhasznaloId; }
            set { felhasznaloId = value; }
        }
        bool ellenorzott = false;

        public bool Ellenorzott
        {
            get { return ellenorzott; }
            set { ellenorzott = value; }
        }
        string szoveg = "";

        public string Szoveg
        {
            get { return szoveg; }
            set { szoveg = value; }
        }

        DateTime datum;

        public DateTime Datum
        {
            get { return datum; }
            set { datum = value; }
        }
        #endregion

        public Hozzaszolas(Szemely szemely, AruCikk aru, DateTime datum, string szoveg)
        {
            this.FelhasznaloId = szemely.Id;
            this.aruCikkId = aru.Id;
            this.datum = datum;
            this.szoveg = szoveg;
        }
    }
}