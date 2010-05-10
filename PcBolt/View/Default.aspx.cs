using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PcBolt.Beans;

namespace PcBolt.View
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["action"] == "logout")
            {
                Session["user"] = null;
                Session["isAdmin"] = false;
            }

            lbl_welcome.Text = "Üdvözlünk a webshopunkban";
            if (Session["user"] != null)            
                lbl_welcome.Text += ", " + ((Felhasznalo)Session["user"]).FelhasznaloNev + "!";
            else if (Session["isAdmin"] != null && (bool)Session["isAdmin"])
                lbl_welcome.Text += ", oh, mindenható adminisztrátor!";
            else
                lbl_welcome.Text += "!";                
        }

        protected void showMessage(string message)
        {
            Response.Write("<script type='text/javascript'>alert('" + message + "')</script>");
        }
    }
}
