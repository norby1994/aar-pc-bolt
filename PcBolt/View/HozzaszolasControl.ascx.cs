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
using PcBolt.Beans;

namespace PcBolt.View
{
    public partial class HozzaszolasControl : System.Web.UI.UserControl
    {
        public Hozzaszolas Hozzaszolas { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            panel.GroupingText = Hozzaszolas.Felhasznalo.FelhasznaloNev + ", " + Hozzaszolas.Datum;
            tb_comment.Text = Hozzaszolas.Szoveg;
        }
    }
}