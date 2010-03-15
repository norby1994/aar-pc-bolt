using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

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

        string foglalat = "";

        public string Foglalat
        {
            get { return foglalat; }
            set { foglalat = value; }
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
