using PcBolt.Beans.Aruk;
using PcBolt.DAO;

namespace PcBolt.Beans.Aruk
{
    public class Memoria : AruCikk
    {
        #region Adattagok deklaralasa
        long tipusId = -1;

        public long TipusId
        {
            get { return tipusId; }
            set { tipusId = value; }
        }


        public string Tipus
        {
            get { return Adatbazis.Memoria_tipusok[tipusId].ToString(); }

        }

        int sebesseg = 0;

        public int Sebesseg
        {
            get { return sebesseg; }
            set { sebesseg = value; }
        }

        int meret = 0;

        public int Meret
        {
            get { return meret; }
            set { meret = value; }
        }



        #endregion
    }
}