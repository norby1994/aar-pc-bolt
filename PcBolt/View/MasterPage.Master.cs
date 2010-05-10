using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PcBolt.Beans;

namespace PcBolt.View
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        private static MenuItem managementMenu = new MenuItem("Termék kezelés", "Management", null, "~/View/Management.aspx");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isAdmin"] != null && (bool)Session["isAdmin"] && !IsPostBack)
            {
                menu.Items.Add(managementMenu);
                menu.Width = Unit.Percentage(40.0);
            }
            else if ((Session["isAdmin"] == null || !(bool)Session["isAdmin"]) && !IsPostBack)
                menu.Items.Remove(managementMenu);              
        }      
    }
}
