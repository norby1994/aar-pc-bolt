using PcBolt.DAO;

namespace PcBolt.Beans.Aruk
{
    public class Processzor : AruCikk
    {
        #region Adattag definicio
        int sebesseg;

        public int Sebesseg
        {
            get { return sebesseg; }
            set { sebesseg = value; }
        }

        long foglalatID = -1;

        public long FoglalatID
        {
            get { return foglalatID; }
            set { foglalatID = value; }
        }


        public string Foglalat
        {
            get { return Adatbazis.Cpu_foglalatok[foglalatID].ToString(); }
        }

        int magokSzama = -1;

        public int MagokSzama
        {
            get { return magokSzama; }
            set { magokSzama = value; }
        }

        bool dobozos = false;

        public bool Dobozos
        {
            get { return dobozos; }
            set { dobozos = value; }
        }

        #endregion



    }
}
