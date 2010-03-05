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
    public class Videokartya
    {
        long foglalatId = -1;

        public long FoglalatId
        {
            get { return foglalatId; }
            set { foglalatId = value; }
        }

        string foglalat = "";

        public string Foglalat
        {
            get { return foglalat; }
            set { foglalat = value; }
        }

        int memoriaMeret = -1;

        public int MemoriaMeret
        {
            get { return memoriaMeret; }
            set { memoriaMeret = value; }
        }

        public string MemoriaToString
        {
            get { return MemoriaMeret + " MB"; }
        }


    }
}
