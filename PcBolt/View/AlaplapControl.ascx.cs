using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PcBolt.Beans.Aruk;
using System.Web.UI.MobileControls;
using System.Collections.Generic;

namespace PcBolt.View
{
    public partial class AlaplapControl : System.Web.UI.UserControl
    {
        public Alaplap Tartalom { get; set; }
        public bool VasarlasInaktiv { get; set; }
        public bool ReszletekInaktiv { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (VasarlasInaktiv)
                btn_buy.Enabled = false;
            if (ReszletekInaktiv)
                btn_details.Enabled = false;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Tartalom != null)
            {
                try
                {
                    tb_akcio.Text = Tartalom.AkcioSzazalek.ToString();
                    tb_ar.Text = Tartalom.Ar.ToString();
                    tb_leiras.Text = Tartalom.Leiras;
                    Panel1.GroupingText = Tartalom.Nev;
                    tb_gyarto.Text = Tartalom.Gyarto;

                    tb_alaplap_ide.Text = Tartalom.IdeSzama.ToString();
                    tb_alaplap_mem.Text = Tartalom.MemoriaSzama.ToString();
                    tb_alaplap_sata.Text = Tartalom.MemoriaSzama.ToString();
                    tb_alaplap_cpu_fog.Text = Tartalom.CpuFoglalat;
                    tb_alaplap_mem_fog.Text = Tartalom.MemoriaFoglalat;
                    tb_alaplap_video_fog.Text = Tartalom.VideoFoglalat;
                }
                catch (Exception)
                {
                }
            }
        }

        protected void btn_buy_Click(object sender, EventArgs e)
        {
            if (Session["user"] == null)
                showMessage("Vásárláshoz be kell jelentkeznie");
            else
                ((List<AruCikk>)Session["basket"]).Add(Tartalom);
        }

        protected void btn_details_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/Details.aspx?type=alaplap&id="+Tartalom.Id.ToString());
        }

        protected void showMessage(string message)
        {
            Response.Write("<script type='text/javascript'>alert('" + message + "')</script>");
        }
    }
}