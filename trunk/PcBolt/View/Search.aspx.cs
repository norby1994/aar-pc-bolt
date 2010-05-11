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

namespace PcBolt.View
{
    public partial class Search : Default
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            if (chk_alaplap.Checked)
            {
                placeholder1.Controls.Add(new Label { Text = "Alaplapok:" });
                foreach (var item in Adatbazis.GetAlaplapok())
                {
                    AlaplapControl ac = (AlaplapControl)LoadControl("~/View/AlaplapControl.ascx");
                    ac.Tartalom = item;
                    placeholder1.Controls.Add(ac);
                }
            }

            if (chk_cpu.Checked)
            {
                placeholder1.Controls.Add(new Label { Text = "Processzorok:" });
                foreach (var item in Adatbazis.GetProcesszorok())
                {
                  
                }
            }

            if (chk_hdd.Checked)
            {
                placeholder1.Controls.Add(new Label { Text = "Merevlemezek:" });
                foreach (var item in Adatbazis.GetHddk())
                {

                }
            }

            if (chk_vga.Checked)
            {
                placeholder1.Controls.Add(new Label { Text = "Videókártyák" });
                foreach (var item in Adatbazis.GetVideokartyak())
                {

                }
            }

            if (chk_memoria.Checked)
            {
                placeholder1.Controls.Add(new Label { Text = "Memóriák:" });
                foreach (var item in Adatbazis.GetMemoriaTipusok())
                {

                }
            }
        }
    }
}
