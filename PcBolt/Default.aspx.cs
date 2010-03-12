using System;
using System.Collections;
using System.Configuration;

using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using PcBolt.DAO;
using PcBolt.Beans;

namespace PcBolt
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void szovegCsere(object sender, EventArgs e)
        {
            //cimke.Text = Adatbazis.GetFelhasznaloNev();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Hashtable table = Adatbazis.GetProcesszorFoglalatok();

            foreach (long key in table.Keys)
            {
                TextBoxKonzol.Text += key + "   -   " + table[key] + "\n";
            }

        }

        
    }
}
