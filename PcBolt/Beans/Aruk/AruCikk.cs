using System;
using PcBolt.DAO;
using System.Data;

namespace PcBolt.Beans.Aruk
{
    public class AruCikk
    {
        public AruCikk()
        {

        }

        #region Adattag definialas
        long id = 0;

        public long Id
        {
            get { return id; }
            set { id = value; }
        }
        
        string nev = "";

        /// <summary>
        /// Arucikk elnevezese
        /// </summary>
        public string Nev
        {
            get { return nev; }
            set { nev = value; }
        }
        int ar = 0;

        /// <summary>
        /// Az arucikk alap ara, akcio nelkul
        /// </summary>
        public int Ar
        {
            get { return ar; }
            set { ar = value; }
        }
        string leiras = "";

        /// <summary>
        /// A termek leirasa, ami tartalmaz minden fontosabb adatot
        /// </summary>
        public string Leiras
        {
            get { return leiras; }
            set { leiras = value; }
        }
        double akcioSzazalek = 100;

        /// <summary>
        /// Mennyi szazalek akcio van a termeken
        /// </summary>
        public double AkcioSzazalek
        {
            get { return akcioSzazalek; }
            set { akcioSzazalek = value; }
        }
        
        Boolean akcios = false;

        /// <summary>
        /// Akcios-e a termek
        /// </summary>
        public Boolean Akcios
        {
            get { return akcios; }
            set { akcios = value; }
        }

        long gyartoId = 0;

        /// <summary>
        /// A gyarto ID-je
        /// </summary>
        public long GyartoId
        {
            get { return gyartoId; }
            set { gyartoId = value; }
        }

        /// <summary>
        /// Ki gyartotta az arucikket
        /// </summary>
        public string Gyarto
        {
            get { return Adatbazis.Gyartok[gyartoId].ToString(); }
        }

        int raktaronDarab = -1;

        /// <summary>
        /// Raktaron levo arucikkek szama
        /// </summary>
        public int RaktaronDarab
        {
            get { return raktaronDarab; }
            set { raktaronDarab = value; }
        }


        double atlag = -1;

        /// <summary>
        /// Ertekelesek atlag pontszama
        /// </summary>
        public double Atlag
        {
            get { return atlag; }
            set { atlag = value; }
        }

        int ertekelesekSzama = -1;

        /// <summary>
        /// Hanyan ertekeltek az arucikket
        /// </summary>
        public int ErtekelesekSzama
        {
            get { return ertekelesekSzama; }
            set { ertekelesekSzama = value; }
        }

        #endregion

        public DataSet GetAdatok(int valami)
        {

            return null;
        }
        
    }
}
