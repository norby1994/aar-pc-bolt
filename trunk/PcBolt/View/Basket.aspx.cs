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
using System.Collections.Generic;
using PcBolt.Beans.Aruk;
using PcBolt.Beans;
using PcBolt.Beans.SzamlaBeans;
using PcBolt.DAO;

namespace PcBolt.View
{
    public partial class Basket : System.Web.UI.Page
    {
        private List<AruCikk> basket;
        private Felhasznalo user;
        long sum;

        protected void Page_Load(object sender, EventArgs e)
        {
            basket = (List<AruCikk>)Session["basket"];
            user = (Felhasznalo)Session["user"];

            label_nev.Text = user.Teljesnev + " kosara:";

            sum = 0;
            foreach (var item in basket)
            {
                AlaplapControl ac = (AlaplapControl)LoadControl("~/View/AlaplapControl.ascx");
                ac.VasarlasInaktiv = true;
                ac.Tartalom = (Alaplap)item;
                placeholder1.Controls.Add(ac);
                sum += item.Ar;
            }

            label_ar.Text = "Ár összesen: " + sum.ToString();           
        }

        protected void btn_order_Click(object sender, EventArgs e)
        {
            Szamla sz = new Szamla();
            sz.FelhasznaloId = user.Id;
            sz.Datum = DateTime.Now;

            foreach (var item in basket)
            {
                sz.Tetelek.Add(new Tetel(item, 1, sz));
            }

            Adatbazis.AddSzamla(sz);
        }
    }
}
