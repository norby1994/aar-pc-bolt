using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace PcBolt.Beans.Aruk
{
    public abstract class AruCikk
    {
        public AruCikk()
        {

        }




        long id = -1;

        public long Id
        {
            get { return id; }
            set { id = value; }
        }
        string nev = "";

        public string Nev
        {
            get { return nev; }
            set { nev = value; }
        }
        int ar = -1;

        public int Ar
        {
            get { return ar; }
            set { ar = value; }
        }
        string leiras = "";

        public string Leiras
        {
            get { return leiras; }
            set { leiras = value; }
        }
        double akcioSzazalek = -1;

        public double AkcioSzazalek
        {
            get { return akcioSzazalek; }
            set { akcioSzazalek = value; }
        }
        Boolean akcios = false;

        public Boolean Akcios
        {
            get { return akcios; }
            set { akcios = value; }
        }

        long gyartoId = -1;

        public long GyartoId
        {
            get { return gyartoId; }
            set { gyartoId = value; }
        }
        string gyarto = "";

        public string Gyarto
        {
            get { return gyarto; }
            set { gyarto = value; }
        }

        int raktaronDarab = -1;

        public int RaktaronDarab
        {
            get { return raktaronDarab; }
            set { raktaronDarab = value; }
        }


        double atlag = -1;

        public double Atlag
        {
            get { return atlag; }
            set { atlag = value; }
        }

        int ertekelesekSzama = -1;

        public int ErtekelesekSzama
        {
            get { return ertekelesekSzama; }
            set { ertekelesekSzama = value; }
        }


    }
}
