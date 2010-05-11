using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using PcBolt.DAO;
using PcBolt.Beans.Aruk;
using PcBolt.Beans;

namespace PcBolt.View
{
    public partial class Details : Default
    {
        private AruCikk cikk;

        protected new void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["type"] == "alaplap")
            {
                AlaplapControl ac = (AlaplapControl)LoadControl("~/View/AlaplapControl.ascx");
                ac.ReszletekInaktiv = true;
                cikk = ac.Tartalom = Adatbazis.GetAlaplap(Int64.Parse(Request.Params["id"]));
                placeholder1.Controls.Add(ac);
            }
            else if (Request.Params["type"] == "cpu")
            {
                
            }
            else if (Request.Params["type"] == "hdd")
            {

            }
            else if (Request.Params["type"] == "vg")
            {

            }
            else if (Request.Params["type"] == "memoria")
            {

            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (cikk != null)
                foreach (var item in Adatbazis.GetHozzaszolasokTermekhez(cikk.Id))
                {
                    HozzaszolasControl hc = (HozzaszolasControl)LoadControl("~/View/HozzaszolasControl.ascx");
                    hc.Hozzaszolas = item;
                    placeholder2.Controls.Add(hc);
                }
        }

        protected void btn_ok_Click(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                Hozzaszolas h = new Hozzaszolas();
                h.FelhasznaloId = ((Felhasznalo)Session["user"]).Id;
                h.AruCikkId = Int64.Parse(Request.Params["id"]);
                h.Datum = DateTime.Now;
                h.Ellenorzott = true;
                h.Szoveg = tb_comment.Text;

                Adatbazis.AddUjHozzaszolas(h);
            }
            else
                showMessage("Hozzászólás íráshoz be kell lépni!");
        }
    }
}
