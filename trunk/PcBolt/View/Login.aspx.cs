using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PcBolt.Beans;
using PcBolt.DAO;

namespace PcBolt.View
{
    public partial class Login : Default
    {
        protected new void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_ok_Click(object sender, EventArgs e)
        {
            string name = tb_felhasznaloNev.Text;
            string pass = tb_password.Text;

            if (name == "admin" && pass == "admin")
            {
                Session["user"] = null;
                Session["isAdmin"] = true;
                Response.Redirect("~/View/Default.aspx");
            }
            else if (Adatbazis.FelhasznaloEsJelszoLetezik(name, pass))
            {
                Session["user"] = Adatbazis.GetFelhasznalo(tb_felhasznaloNev.Text);
                Session["isAdmin"] = false;
                Response.Redirect("~/View/Default.aspx");
            }
            else
                showMessage("Hibás felhasználó név, vagy jelszó!");
        }
    }
}
