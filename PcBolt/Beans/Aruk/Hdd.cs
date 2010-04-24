using PcBolt.DAO;
using PcBolt.Beans.Aruk;
namespace PcBolt.Beans.Aruk
{
    public class Hdd : AruCikk
    {
        #region Adattag definicio
        int meret = -1;

        public int Meret
        {
            get { return meret; }
            set { meret = value; }
        }

        long csatoloId = -1;

        public long CsatoloId
        {
            get { return csatoloId; }
            set { csatoloId = value; }
        }

        public string Csatolo
        {
            get { return Adatbazis.Hdd_csatolok[csatoloId].ToString(); }
        }

        #endregion

    }
}