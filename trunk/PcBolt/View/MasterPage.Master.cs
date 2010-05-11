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
        private static MenuItem basketMenu = new MenuItem("Kosár", "Basket", null, "~/View/Basket.aspx");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isAdmin"] != null && (bool)Session["isAdmin"] && !IsPostBack)
            {
                menu.Items.Add(managementMenu);
                menu.Width = Unit.Pixel(650);
            }
            else if ((Session["isAdmin"] == null || !(bool)Session["isAdmin"]) && !IsPostBack)
                menu.Items.Remove(managementMenu);

            if (Session["user"] != null && !IsPostBack)
            {
                menu.Items.Add(basketMenu);
                menu.Width = Unit.Pixel(570);
            }
            else if (Session["user"] == null && !IsPostBack)
                menu.Items.Remove(basketMenu);
        }      
    }
}
