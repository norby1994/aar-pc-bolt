using PcBolt.Beans.Aruk;
using PcBolt.DAO;
namespace PcBolt.Beans.Aruk
{
    public class Alaplap : AruCikk
    {
        #region Adattag definicio
        long cpuFoglalatId = -1;

        public long CpuFoglalatId
        {
            get { return cpuFoglalatId; }
            set { cpuFoglalatId = value; }
        }


        public string CpuFoglalat
        {
            get { return Adatbazis.Cpu_foglalatok[cpuFoglalatId].ToString(); }
        }
        long memoriaFoglalatId = -1;

        public long MemoriaFoglalatId
        {
            get { return memoriaFoglalatId; }
            set { memoriaFoglalatId = value; }
        }

        public string MemoriFoglalat
        {
            get { return Adatbazis.Cpu_foglalatok[memoriaFoglalatId].ToString(); }
        }

        int memoriaSzama = -1;

        public int MemoriaSzama
        {
            get { return MemoriaSzama; }
            set { MemoriaSzama = value; }
        }

        long videoFoglalatId = -1;

        public long VideoFoglalatId
        {
            get { return videoFoglalatId; }
            set { videoFoglalatId = value; }
        }

        public string VideoFoglalat
        {
            get { return Adatbazis.Video_foglalaok[videoFoglalatId].ToString(); }
        }
        int sataSzama = -1;

        public int SataSzama
        {
            get { return sataSzama; }
            set { sataSzama = value; }
        }

        int ideSzama = -1;

        public int IdeSzama
        {
            get { return ideSzama; }
            set { ideSzama = value; }
        }
        #endregion
    }
}