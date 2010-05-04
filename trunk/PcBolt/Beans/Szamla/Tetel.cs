using PcBolt.Beans.Aruk;
namespace PcBolt.Beans.SzamlaBeans
{
    public class Tetel
    {
        public Tetel()
        {

        }

        public Tetel(AruCikk aru, int db, Szamla szamla)
        {
            this.AruId = aru.Id;
            this.szamlaId = szamla.Id;
            this.Ar = aru.Ar;
            this.darab = db;
        }

        #region Adattagok

        

        long aruId = -1;

        public long AruId
        {
            get { return aruId; }
            set { aruId = value; }
        }

        long szamlaId = -1;

        public long SzamlaId
        {
            get { return szamlaId; }
            set { szamlaId = value; }
        }



        int darab;

        public int Darab
        {
            get { return darab; }
            set { darab = value; }
        }

        int ar;

        public int Ar
        {
            get { return ar; }
            set { ar = value; }
        }

        #endregion

    }
}