using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace PcBolt.Beans
{
    

    public class Felhasznalo : Szemely
    {
        string varos = "";
        string iranyitoszam = "";
        string utca = "";

        public Felhasznalo() { }

        public Felhasznalo(int id, string felhasznaloNev, string varos, string utca, string iranyitoszam)
        {
            this.Id = id;
            this.FelhasznaloNev = felhasznaloNev;
            this.varos = varos;
            this.utca = utca;
            this.iranyitoszam = iranyitoszam;
        }


        public string Varos
        {
            get { return varos; }
            set { varos = value; }
        }       

        public string Utca
        {
            get { return utca; }
            set { utca = value; }
        }        

        public string Iranyitoszam
        {
            get { return iranyitoszam; }
            set { iranyitoszam = value; }
        }

        public string TeljesCim
        {
            get { return Iranyitoszam + " " + Varos + ", " + Utca; }
        }
        
    }
}
