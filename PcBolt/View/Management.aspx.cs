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
using PcBolt.Beans.Aruk;

namespace PcBolt.View
{
    public partial class Management : Default
    {
        
        protected new void Page_Load(object sender, EventArgs e)
        {
            Session["isAdmin"] = true;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            switch (lb_type_del.SelectedValue)
            {
                case "CPU":
                    CreateTypeList(Adatbazis.GetProcesszorFoglalatok);
                    break;
                case "RAM":
                    CreateTypeList(Adatbazis.GetMemoriaTipusok);
                    break;
                case "HDD":
                    CreateTypeList(Adatbazis.GetHddCsatolok);
                    break;
                case "GFX":
                    CreateTypeList(Adatbazis.GetVideokartyaFoglalatok);
                    break;
                default:
                    showMessage("Internal error!");
                    break;
            }

            lb_hdd_csatolo.Items.Clear();
            for (IDictionaryEnumerator i = Adatbazis.GetHddCsatolok().GetEnumerator(); i.MoveNext(); )
            {
                lb_hdd_csatolo.Items.Add(new ListItem(i.Value.ToString(), i.Key.ToString()));
            }

            lb_video_foglalat.Items.Clear();
            lb_alaplap_video_fog.Items.Clear();
            for (IDictionaryEnumerator i = Adatbazis.GetVideokartyaFoglalatok().GetEnumerator(); i.MoveNext(); )
            {
                lb_video_foglalat.Items.Add(new ListItem(i.Value.ToString(), i.Key.ToString()));
                lb_alaplap_video_fog.Items.Add(new ListItem(i.Value.ToString(), i.Key.ToString()));
            }

            lb_ram_tipus.Items.Clear();
            lb_alaplap_mem_fog.Items.Clear();
            for (IDictionaryEnumerator i = Adatbazis.GetMemoriaTipusok().GetEnumerator(); i.MoveNext(); )
            {
                lb_ram_tipus.Items.Add(new ListItem(i.Value.ToString(), i.Key.ToString()));
                lb_alaplap_mem_fog.Items.Add(new ListItem(i.Value.ToString(), i.Key.ToString()));
            }

            lb_proc_foglalat.Items.Clear();
            lb_alaplap_cpu_fog.Items.Clear();
            for (IDictionaryEnumerator i = Adatbazis.GetProcesszorFoglalatok().GetEnumerator(); i.MoveNext(); )
            {
                lb_proc_foglalat.Items.Add(new ListItem(i.Value.ToString(), i.Key.ToString()));
                lb_alaplap_cpu_fog.Items.Add(new ListItem(i.Value.ToString(), i.Key.ToString()));
            }

            lb_gyarto.Items.Clear();
            for (IDictionaryEnumerator i = Adatbazis.GetGyartok().GetEnumerator(); i.MoveNext(); )
            {
                lb_gyarto.Items.Add(new ListItem(i.Value.ToString(), i.Key.ToString()));
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            switch (lb_type_add.SelectedValue)
            {
                case "CPU":
                    AddType(Adatbazis.AddProcesszorFoglalat);
                    break;
                case "RAM":
                    AddType(Adatbazis.AddMemoriaTipus);
                    break;
                case "HDD":
                    AddType(Adatbazis.AddHddCsatolo);
                    break;
                case "GFX":
                    AddType(Adatbazis.AddVideokartyaFoglalat);
                    break;
                default:
                    showMessage("Internal error!");
                    break;
            }
        }

        protected void btn_del_Click(object sender, EventArgs e)
        {
            switch (lb_type_del.SelectedValue)
            {
                case "CPU":
                    DeleteType(Adatbazis.TorolProcesszorFoglalat);
                    break;
                case "RAM":
                    DeleteType(Adatbazis.TorolMemoriaTipus);
                    break;
                case "HDD":
                    DeleteType(Adatbazis.TorolHddCsatolo);
                    break;
                case "GFX":
                    DeleteType(Adatbazis.TorolVideokartyaFoglalat);
                    break;
                default:
                    showMessage("Internal error!");
                    break;
            }
        }

        protected void btn_alaplap_Click(object sender, EventArgs e)
        {
            Alaplap a = new Alaplap();
            a.AkcioSzazalek = Double.Parse(tb_akcio.Text);
            a.Ar = Int32.Parse(tb_ar.Text);
            a.GyartoId = Int64.Parse(lb_gyarto.SelectedValue);
            a.Leiras = tb_leiras.Text;
            a.Nev = tb_nev.Text;
            a.RaktaronDarab = Int32.Parse(tb_darab.Text);

            a.CpuFoglalatId = Int64.Parse(lb_alaplap_cpu_fog.SelectedValue);
            a.IdeSzama = Int32.Parse(tb_alaplap_ide.Text);
            a.MemoriaFoglalatId = Int64.Parse(lb_alaplap_mem_fog.SelectedValue);
            a.MemoriaSzama = Int32.Parse(tb_alaplap_mem.Text);
            a.SataSzama = Int32.Parse(tb_alaplap_sata.Text);
            a.VideoFoglalatId = Int64.Parse(lb_alaplap_video_fog.SelectedValue);

            Adatbazis.AddTermek(a);
        }

        protected void btn_mem_Click(object sender, EventArgs e)
        {
            Memoria m = new Memoria();
            m.AkcioSzazalek = Double.Parse(tb_akcio.Text);
            m.Ar = Int32.Parse(tb_ar.Text);
            m.GyartoId = Int64.Parse(lb_gyarto.SelectedValue);
            m.Leiras = tb_leiras.Text;
            m.Nev = tb_nev.Text;
            m.RaktaronDarab = Int32.Parse(tb_darab.Text);

            m.Meret = Int32.Parse(tb_ram_meret.Text);
            m.Sebesseg = Int32.Parse(tb_ram_sebesseg.Text);
            m.TipusId = Int64.Parse(lb_ram_tipus.SelectedValue);

            Adatbazis.AddTermek(m);
        }

        protected void btn_proc_Click(object sender, EventArgs e)
        {
            Processzor p = new Processzor();
            p.AkcioSzazalek = Double.Parse(tb_akcio.Text);
            p.Ar = Int32.Parse(tb_ar.Text);
            p.GyartoId = Int64.Parse(lb_gyarto.SelectedValue);
            p.Leiras = tb_leiras.Text;
            p.Nev = tb_nev.Text;
            p.RaktaronDarab = Int32.Parse(tb_darab.Text);

            p.Sebesseg = Int32.Parse(tb_proc_sebesseg.Text);
            p.MagokSzama = Int32.Parse(tb_proc_magok.Text);
            p.Dobozos = chk_proc_dobozos.Checked;
            p.FoglalatID = Int64.Parse(lb_proc_foglalat.SelectedValue);

            Adatbazis.AddTermek(p);
        }

        protected void btn_video_Click(object sender, EventArgs e)
        {
            Videokartya v = new Videokartya();
            v.AkcioSzazalek = Double.Parse(tb_akcio.Text);
            v.Ar = Int32.Parse(tb_ar.Text);
            v.GyartoId = Int64.Parse(lb_gyarto.SelectedValue);
            v.Leiras = tb_leiras.Text;
            v.Nev = tb_nev.Text;
            v.RaktaronDarab = Int32.Parse(tb_darab.Text);

            v.MemoriaMeret = Int32.Parse(tb_video_memoria.Text);
            v.FoglalatId = Int64.Parse(lb_video_foglalat.Text);

            Adatbazis.AddTermek(v);
        }

        protected void btn_hdd_Click(object sender, EventArgs e)
        {
            Hdd h = new Hdd();
            h.AkcioSzazalek = Double.Parse(tb_akcio.Text);
            h.Ar = Int32.Parse(tb_ar.Text);
            h.GyartoId = Int64.Parse(lb_gyarto.SelectedValue);
            h.Leiras = tb_leiras.Text;
            h.Nev = tb_nev.Text;
            h.RaktaronDarab = Int32.Parse(tb_darab.Text);

            h.CsatoloId = Int64.Parse(lb_hdd_csatolo.Text);
            h.Meret = Int32.Parse(tb_hdd_meret.Text);

            Adatbazis.AddTermek(h);
        }

        protected void addGyarto(object sender, EventArgs e)
        {
            Adatbazis.AddGyarto(tb_uj_gyarto.Text);
        }

        private void AddType(Action<string> addFunc)
        {
            foreach (var row in tb_type_add.Text.Split(new string[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))
            {
                addFunc(row);
            }
        }

        private void DeleteType(Action<string> delFunc)
        {
            foreach (var item in lb_type_del_items.Items)
            {
                ListItem listItem = (ListItem)item;
                if (listItem.Selected)
                {
                    //showMessage(listItem.Value);
                    delFunc(listItem.Value);
                }
            }      
        }

        private void CreateTypeList(Func<Hashtable> getFunc)
        {
            lb_type_del_items.Items.Clear();
            foreach (var item in getFunc().Values)
            {
                string s = (string)item;                
                lb_type_del_items.Items.Add(new ListItem(s, s));
            }
        }
    }
}
