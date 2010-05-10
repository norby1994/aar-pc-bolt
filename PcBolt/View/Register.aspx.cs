using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PcBolt.Beans;
using PcBolt.DAO;

namespace PcBolt.View
{
    public partial class Register : Default
    {
        protected new void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_ok_Click(object sender, EventArgs e)
        {
            string nev = tb_felhasznaloNev.Text;
            if (String.IsNullOrEmpty(nev))
                showMessage("A felhasználó név üres!");
            else if (Adatbazis.FelhasznaloLetezik(nev))
                showMessage("Már van ilyen felhasználó!");
            else if (String.IsNullOrEmpty(tb_teljesNev.Text))
                showMessage("A teljes név üres!");            
            else if (String.IsNullOrEmpty(tb_password.Text))
                showMessage("Adjon meg jelszót!");
            else if (!tb_password.Text.Equals(tb_password2.Text))
                showMessage("A két jelszó nem egyezik");
            else
            {
                Felhasznalo f = new Felhasznalo();
                f.FelhasznaloNev = nev;
                f.Teljesnev = tb_teljesNev.Text;
                f.Iranyitoszam = tb_irszam.Text;
                f.Varos = tb_varos.Text;
                f.Utca = tb_utca.Text;

                Adatbazis.AddFelhasznalo(f, tb_password.Text);
                Response.Redirect("~/View/Default.aspx");
            }                
        }
    }
}
