using System;
using System.Data;
using System.Configuration;
using PcBolt.Beans;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using PcBolt.Exceptions;
using PcBolt.Beans.Aruk;
using System.Collections;
using System.Collections.Generic;

namespace PcBolt.DAO
{
    public static class Adatbazis
    {
        #region Kapcsolatok
        static string oradb = "Data Source=XE;User Id=peti; Password=peti;";
        static string felhasznalo_tab = "felhasznalo_tab";
        static string cpu_foglalat_tab = "cpu_foglalat_tab";
        static string video_foglalat_tab = "video_foglalat_tab";
        static string memoria_tipus_tab = "memoria_tip_tab";
        static string gyarto_tab = "gyarto_tab";
        static string raktar_tab = "raktar_tab";
        static OracleConnection connection = new OracleConnection(oradb);
        static string sqlKod;
        static OracleCommand command = new OracleCommand("", connection);
        #endregion

        #region Kiegeszito hashmapek
        static private Hashtable gyartok = new Hashtable();

        public static Hashtable Gyartok
        {
            get { return Adatbazis.gyartok; }
            set { Adatbazis.gyartok = value; }
        }

        static private Hashtable cpu_foglalatok = new Hashtable();

        public static Hashtable Cpu_foglalatok
        {
            get { return Adatbazis.cpu_foglalatok; }
            set { Adatbazis.cpu_foglalatok = value; }
        }

        static private Hashtable video_foglalaok = new Hashtable();

        public static Hashtable Video_foglalaok
        {
            get { return Adatbazis.video_foglalaok; }
            set { Adatbazis.video_foglalaok = value; }
        }

        static private Hashtable memoria_tipusok = new Hashtable();

        public static Hashtable Memoria_tipusok
        {
            get { return Adatbazis.memoria_tipusok; }
            set { Adatbazis.memoria_tipusok = value; }
        }

        #endregion

        
        public static void Init()
        {
            kiegeszitoFrissites();

        }


        public static void AddTermek(AruCikk aru)
        {
            if (aru is Processzor)
                AddProcesszor((Processzor)aru);
            if (aru is Videokartya)
                AddVideoKartya((Videokartya)aru);
            if (aru is Memoria)
                AddMemoriaTipus((Memoria)aru);

        }
       


        #region Felhasznalo

        /// <summary>
        /// Uj felhasznalot tesz az adatbazisba
        /// </summary>
        /// <param name="f">Uj felhasznalo</param>
        /// <param name="jelszo">felhasznalo jelszava</param>
        public static void AddFelhasznalo(Felhasznalo f, string jelszo)
        {
            try
            {
                connection.Open();
                sqlKod = "insert into " + felhasznalo_tab +
                    "(felhasznev, jelszo, teljesnev, varos, utca, iranyitoszam) " +
                    "values( :fnev, :jel, :teljesnev, :varos, :utca, :iranyitoszam)";
                command = new OracleCommand(sqlKod, connection);
                command.Parameters.Add(new OracleParameter(":fnev", f.FelhasznaloNev));
                command.Parameters.Add(new OracleParameter(":jel", jelszo));
                command.Parameters.Add(new OracleParameter(":teljesnev", f.Teljesnev));
                command.Parameters.Add(new OracleParameter(":varos", f.Varos));
                command.Parameters.Add(new OracleParameter(":utca", f.Utca));
                command.Parameters.Add(new OracleParameter(":iranyitoszam", f.Iranyitoszam));
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }

        }

        /// <summary>
        /// Felhasznalonev alapjan visszaadja a felhasznalot az adatbazisbol
        /// </summary>
        /// <param name="felhasznaloNev">Keresett felhasznalo</param>
        /// <returns>Felhasznalo, vagy exception</returns>
        public static Felhasznalo GetFelhasznalo(string felhasznaloNev)
        {
            try
            {
                connection.Open();
                sqlKod = "select * from " + felhasznalo_tab +
                    " where felhasznev like :felhasznev";
                command = new OracleCommand(sqlKod, connection);
                command.Parameters.Add(new OracleParameter(":felhasznev", felhasznaloNev));
                OracleDataReader odr = command.ExecuteReader();
                string s = command.CommandText;
                Felhasznalo ki = new Felhasznalo();

                if (odr.Read())
                {
                    ki.Id = Convert.ToInt64(odr["id"]);
                    ki.FelhasznaloNev = Convert.ToString(odr["felhasznev"]);
                    ki.Teljesnev = Convert.ToString(odr["teljesnev"]);
                    ki.Varos = Convert.ToString(odr["varos"]);
                    ki.Utca = Convert.ToString(odr["utca"]);
                    ki.Iranyitoszam = Convert.ToString(odr["iranyitoszam"]);
                }
                else
                {
                    throw new NincsIlyenFelhasznaloException();
                }
                return ki;
            }
            finally
            {
                connection.Close();
            }


        }


        public static string GetFelhasznaloNev()
        {
            try
            {
                connection.Open();
                sqlKod = "select * from felhasznalo_tab;";
                command = new OracleCommand(sqlKod, connection);
                OracleDataReader dr = command.ExecuteReader();
                dr.Read();
                OracleString os = dr.GetOracleString(1);

                return "semmi";
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Letezik-e az adott felhasznalonev
        /// </summary>
        /// <param name="felhasznev"></param>
        /// <returns></returns>
        public static bool FelhasznaloLetezik(string felhasznev)
        {
            try
            {
                connection.Open();
                sqlKod = "select felhasznev from " + felhasznalo_tab +
                    " where felhasznev like :felhasznev";
                command = new OracleCommand(sqlKod, connection);
                command.Parameters.Add(new OracleParameter(":felhasznev", felhasznev));
                OracleDataReader odr = command.ExecuteReader();
                return odr.HasRows;

            }
            finally
            {
                connection.Close();
            }

        }


        #endregion

        #region Kegeszito tablak

        private static void kiegeszitoFrissites()
        {
            gyartok = GetGyartok();
            cpu_foglalatok = GetProcesszorFoglalatok();
            video_foglalaok = GetVideokartyaFoglalatok();
        }

        private static void AddKiegeszitoTablaba(string nev, string tabla)
        {
            try
            {
                connection.Open();
                sqlKod = "insert into " + tabla + "(nev) values(:nev)";
                command = new OracleCommand(sqlKod, connection);
                command.Parameters.Add(new OracleParameter(":nev", nev));
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        private static Hashtable GetKiegeszitoTabla(string tabla)
        {
            try
            {
                connection.Open();
                sqlKod = "select * from " + tabla;
                command = new OracleCommand(sqlKod, connection);
                OracleDataReader odr = command.ExecuteReader();
                Hashtable ki = new Hashtable();
                while (odr.Read())
                {
                    long id = Convert.ToInt64(odr["id"]);
                    string nev = Convert.ToString(odr["nev"]).Trim();
                    ki.Add(id, nev);
                }
                return ki;
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion

        #region Gyarto

        public static Hashtable GetGyartok()
        {
            return GetKiegeszitoTabla(gyarto_tab);
        }

        /// <summary>
        /// Gyarto felvetele az adatbazisba
        /// </summary>
        /// <param name="nev"></param>
        public static void AddGyarto(string nev)
        {
            AddKiegeszitoTablaba(nev, gyarto_tab);

        }

        #endregion



        #region CPU

        /// <summary>
        /// Uj processor foglalatot ad hozza az adatbazishoz
        /// </summary>
        /// <param name="nev">Foglalat neve</param>
        public static void AddProcesszorFoglala(string nev)
        {
            AddKiegeszitoTablaba(nev, cpu_foglalat_tab);
        }

        /// <summary>
        /// Proci foglalatok listáját adja vissza.A kulcs long típus, az értékkek stringek
        /// </summary>
        /// <returns></returns>
        public static Hashtable GetProcesszorFoglalatok()
        {
            return GetKiegeszitoTabla(cpu_foglalat_tab);
        }

        private static void AddProcesszor(Processzor proci)
        {
            try
            {
                connection.Open();
                sqlKod = "INSERT INTO " + raktar_tab +
                    " values(cpu_typ(:nev, :gyarto, :ar, :darab_szam, " +
                    " :sebesseg, :foglalat, :magok_szama, :dobozos))";
                command = new OracleCommand(sqlKod, connection);
                command.Parameters.Add(new OracleParameter(":nev", proci.Nev));
                command.Parameters.Add(new OracleParameter(":gyarto", proci.GyartoId));
                command.Parameters.Add(new OracleParameter(":ar", proci.Ar));
                command.Parameters.Add(new OracleParameter(":darab_szam", proci.RaktaronDarab));
                command.Parameters.Add(new OracleParameter(":sebesseg", proci.Sebesseg));
                command.Parameters.Add(new OracleParameter(":foglalat", proci.FoglalatID));
                command.Parameters.Add(new OracleParameter(":magok_szama", proci.MagokSzama));
                command.Parameters.Add(new OracleParameter(":dobozos", Convert.ToInt32(proci.Dobozos)));
                string s = command.CommandText;
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Listaba vissza adja az osszes processzort
        /// </summary>
        /// <returns></returns>
        public static List<Processzor> GetProcesszorok()
        {
            try
            {
                connection.Open();
                sqlKod = "select treat(value(p) as cpu_typ).id, " +         //0
                " treat(value(p) as cpu_typ).nev, " +                       //1
                " treat(value(p) as cpu_typ).gyarto, " +                    //2
                " treat(value(p) as cpu_typ).ar, " +                        //3
                " treat(value(p) as cpu_typ).darabszam, " +                 //4
                " treat(value(p) as cpu_typ).akcio, " +                     //5
                " treat(value(p) as cpu_typ).atlag, " +                     //6
                " treat(value(p) as cpu_typ).ertekeles_szam, " +            //7
                " treat(value(p) as cpu_typ).leiras, " +                    //8
                " treat(value(p) as cpu_typ).sebesseg, " +                  //9
                " treat(value(p) as cpu_typ).foglalat, " +                  //10
                " treat(value(p) as cpu_typ).magok_szama, " +               //11
                " treat(value(p) as cpu_typ).dobozos " +                    //12

                " from " + raktar_tab + " p";
                command = new OracleCommand(sqlKod, connection);
                OracleDataReader odr = command.ExecuteReader();
                List<Processzor> ki = new List<Processzor>();

                while (odr.Read())
                {
                    if (!odr.IsDBNull(0))
                    {
                        Processzor cpu = new Processzor();
                        cpu.Id = Convert.ToInt64(odr[0]);
                        cpu.Nev = Convert.ToString(odr[1]);
                        cpu.GyartoId = Convert.ToInt64(odr[2]);
                        cpu.Ar = Convert.ToInt32(odr[3]);
                        cpu.RaktaronDarab = Convert.ToInt32(odr[4]);
                        cpu.AkcioSzazalek = Convert.ToDouble(odr[5]);
                        cpu.Atlag = Convert.ToDouble(odr[6]);
                        cpu.ErtekelesekSzama = Convert.ToInt32(odr[7]);
                        cpu.Leiras = Convert.ToString(odr[8]);
                        cpu.Sebesseg = Convert.ToInt32(odr[9]);
                        cpu.FoglalatID = Convert.ToInt64(odr[10]);
                        cpu.MagokSzama = Convert.ToInt32(odr[11]);
                        cpu.Dobozos = Convert.ToBoolean(odr[12]);

                        ki.Add(cpu);
                    }
                }

                return ki;
            }
            finally
            {
                connection.Close();
            }

        }

        /// <summary>
        /// Egy darab processzort ad vissza, az adott id-vel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Processzor GetProcesszor(long id)
        {
            try
            {
                connection.Open();
                sqlKod = "select treat(value(p) as cpu_typ).id, " +         //0
                " treat(value(p) as cpu_typ).nev, " +                       //1
                " treat(value(p) as cpu_typ).gyarto, " +                    //2
                " treat(value(p) as cpu_typ).ar, " +                        //3
                " treat(value(p) as cpu_typ).darabszam, " +                 //4
                " treat(value(p) as cpu_typ).akcio, " +                     //5
                " treat(value(p) as cpu_typ).atlag, " +                     //6
                " treat(value(p) as cpu_typ).ertekeles_szam, " +            //7
                " treat(value(p) as cpu_typ).leiras, " +                    //8
                " treat(value(p) as cpu_typ).sebesseg, " +                  //9
                " treat(value(p) as cpu_typ).foglalat, " +                  //10
                " treat(value(p) as cpu_typ).magok_szama, " +               //11
                " treat(value(p) as cpu_typ).dobozos " +                    //12

                " from " + raktar_tab + " p " +
                " where id = :id";
                ;
                command = new OracleCommand(sqlKod, connection);
                command.Parameters.Add(new OracleParameter(":id", id));
                OracleDataReader odr = command.ExecuteReader();
                Processzor cpu = new Processzor();
                odr.Read();

                if (!(odr[0] is DBNull))
                {
                    cpu.Id = Convert.ToInt64(odr[0]);
                    cpu.Nev = Convert.ToString(odr[1]);
                    cpu.GyartoId = Convert.ToInt64(odr[2]);
                    cpu.Ar = Convert.ToInt32(odr[3]);
                    cpu.RaktaronDarab = Convert.ToInt32(odr[4]);
                    cpu.AkcioSzazalek = Convert.ToDouble(odr[5]);
                    cpu.Atlag = Convert.ToDouble(odr[6]);
                    cpu.ErtekelesekSzama = Convert.ToInt32(odr[7]);
                    cpu.Leiras = Convert.ToString(odr[8]);
                    cpu.Sebesseg = Convert.ToInt32(odr[9]);
                    cpu.FoglalatID = Convert.ToInt64(odr[10]);
                    cpu.MagokSzama = Convert.ToInt32(odr[11]);
                    cpu.Dobozos = Convert.ToBoolean(odr[12]);

                }
                return cpu;
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion

        #region Videokartya

        public static void AddVideokartyaFoglalat(string nev)
        {
            AddKiegeszitoTablaba(nev, video_foglalat_tab);
        }

        private static Hashtable GetVideokartyaFoglalatok()
        {
            return GetKiegeszitoTabla(video_foglalat_tab);
        }

        /// <summary>
        /// Videokartya hozza adasa az adatbazishoz
        /// </summary>
        /// <param name="video"></param>
        private static void AddVideoKartya(Videokartya video)
        {
            try
            {
                connection.Open();

                sqlKod = "insert into raktar_tab values(video_typ( " +
                    ":nev, :gyarto, :ar, :darabszam, :foglalat, :memoria))";
                command = new OracleCommand(sqlKod, connection);
                command.Parameters.Add(new OracleParameter(":nev", video.Nev));
                command.Parameters.Add(new OracleParameter(":gyarto", video.GyartoId));
                command.Parameters.Add(new OracleParameter(":ar", video.Ar));
                command.Parameters.Add(new OracleParameter(":darabszam", video.RaktaronDarab));
                command.Parameters.Add(new OracleParameter(":foglalat", video.FoglalatId));
                command.Parameters.Add(new OracleParameter(":memoria", video.MemoriaMeret));

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Osszes videokartya lekerese
        /// </summary>
        /// <returns></returns>
        public static List<Videokartya> GetVideokartyak()
        {
            try
            {
                connection.Open();
                sqlKod = "select " +
                " treat(value(p) as video_typ).id, " +         //0
                " treat(value(p) as video_typ).nev, " +                       //1
                " treat(value(p) as video_typ).gyarto, " +                    //2
                " treat(value(p) as video_typ).ar, " +                        //3
                " treat(value(p) as video_typ).darabszam, " +                 //4
                " treat(value(p) as video_typ).akcio, " +                     //5
                " treat(value(p) as video_typ).atlag, " +                     //6
                " treat(value(p) as video_typ).ertekeles_szam, " +            //7
                " treat(value(p) as video_typ).leiras, " +                    //8
                " treat(value(p) as video_typ).foglalat, " +                  //9
                " treat(value(p) as video_typ).memoria " +                   //10

                " from " + raktar_tab + " p ";

                command = new OracleCommand(sqlKod, connection);

                OracleDataReader odr = command.ExecuteReader();
                List<Videokartya> ki = new List<Videokartya>();
                while (odr.Read())
                {
                    Videokartya vk = new Videokartya();
                    if (!odr.IsDBNull(0))
                    {
                        object o = odr[0];
                        vk.Id = Convert.ToInt64(odr[0]);
                        vk.Nev = odr.GetString(1);
                        vk.GyartoId = Convert.ToInt64(odr[2]);
                        vk.Ar = Convert.ToInt32(odr[3]);
                        vk.RaktaronDarab = Convert.ToInt32(odr[4]);
                        vk.AkcioSzazalek = Convert.ToDouble(odr[5]);
                        vk.Atlag = Convert.ToDouble(odr[6]);
                        vk.ErtekelesekSzama = Convert.ToInt32(odr[7]);
                        vk.Leiras = odr.GetString(8);
                        vk.FoglalatId = Convert.ToInt64(odr[9]);
                        vk.MemoriaMeret = Convert.ToInt32(odr[10]);
                        ki.Add(vk);
                    }
                }

                return ki;
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// ID alapja visszaadja a videokartyat
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Videokartya GetVideokartya(long id)
        {
            try
            {
                connection.Open();
                sqlKod = "select " +
                " treat(value(p) as video_typ).id, " +                        //0
                " treat(value(p) as video_typ).nev, " +                       //1
                " treat(value(p) as video_typ).gyarto, " +                    //2
                " treat(value(p) as video_typ).ar, " +                        //3
                " treat(value(p) as video_typ).darabszam, " +                 //4
                " treat(value(p) as video_typ).akcio, " +                     //5
                " treat(value(p) as video_typ).atlag, " +                     //6
                " treat(value(p) as video_typ).ertekeles_szam, " +            //7
                " treat(value(p) as video_typ).leiras, " +                    //8
                " treat(value(p) as video_typ).foglalat, " +                  //9
                " treat(value(p) as video_typ).memoria " +                    //10

                " from " + raktar_tab + " p " +
                " where id = :id";

                command = new OracleCommand(sqlKod, connection);
                command.Parameters.Add(new OracleParameter(":id", id));

                OracleDataReader odr = command.ExecuteReader();
                List<Videokartya> ki = new List<Videokartya>();
                odr.Read();

                Videokartya vk = new Videokartya();
                if (!odr.IsDBNull(0))
                {
                    object o = odr[0];
                    vk.Id = Convert.ToInt64(odr[0]);
                    vk.Nev = odr.GetString(1);
                    vk.GyartoId = Convert.ToInt64(odr[2]);
                    vk.Ar = Convert.ToInt32(odr[3]);
                    vk.RaktaronDarab = Convert.ToInt32(odr[4]);
                    vk.AkcioSzazalek = Convert.ToDouble(odr[5]);
                    vk.Atlag = Convert.ToDouble(odr[6]);
                    vk.ErtekelesekSzama = Convert.ToInt32(odr[7]);
                    vk.Leiras = odr.GetString(8);
                    vk.FoglalatId = Convert.ToInt64(odr[9]);
                    vk.MemoriaMeret = Convert.ToInt32(odr[10]);
                }
                return vk;
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion

        #region Memoria

        public static void AddMemoriaTipus(string nev)
        {
            AddKiegeszitoTablaba(nev, memoria_tipus_tab);
        }

        public static Hashtable GetMemoriaTipusok()
        {
            GetKiegeszitoTabla(memoria_tipus_tab);
        }

        private void AddMemoria(Memoria memo)
        {
            
        }

        public List<Memoria> GetMemoriak()
        {
            try
            {
                connection.Open();
                sqlKod = "select treat(value(p) as memoria_typ).id, " +         //0
                " treat(value(p) as memoria_typ).nev, " +                       //1
                " treat(value(p) as memoria_typ).gyarto, " +                    //2
                " treat(value(p) as memoria_typ).ar, " +                        //3
                " treat(value(p) as memoria_typ).darabszam, " +                 //4
                " treat(value(p) as memoria_typ).akcio, " +                     //5
                " treat(value(p) as memoria_typ).atlag, " +                     //6
                " treat(value(p) as memoria_typ).ertekeles_szam, " +            //7
                " treat(value(p) as memoria_typ).leiras, " +                    //8
                " treat(value(p) as memoria_typ).tipus, " +                     //9
                " treat(value(p) as memoria_typ).meret, " +                     //10
                " treat(value(p) as memoria_typ).sebesseg, " +                  //11
                " from " + raktar_tab + " p ";

                command = new OracleCommand(sqlKod, connection);
                OracleDataReader odr = command.ExecuteReader();

                while (odr.Read())
                {
                    if (!odr.IsDBNull())
                    {


                    }

                }
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion
    }
}
