using System;
using System.Data;
using System.Configuration;
using PcBolt.Beans;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using PcBolt.Exceptions;
using PcBolt.Beans.Aruk;
using PcBolt.Beans.SzamlaBeans;
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
        static string hdd_csatolo_tav = "hdd_csatolok_tab";
        static string memoria_tipus_tab = "memoria_tip_tab";
        static string gyarto_tab = " gyarto_tab ";
        static string raktar_tab = " raktar_tab ";
        static string hozzaszolas_tab = " hozzaszolas_tab ";
        static string szamla_tab = " szamla_tab ";
        static string szamla_seq = " szamla_seq ";
        static string tetel_tab = " tetel_tab ";
        static OracleConnection connection = new OracleConnection(oradb);
        static string sqlKod;
        static OracleCommand command = new OracleCommand("", connection);
        static OracleDataReader dr;
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

        static private Hashtable hdd_csatolok = new Hashtable();

        public static Hashtable Hdd_csatolok
        {
            get { return Adatbazis.hdd_csatolok; }
            set { Adatbazis.hdd_csatolok = value; }
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
                AddMemoria((Memoria)aru);
            if (aru is Hdd)
                AddHdd((Hdd)aru);
            if (aru is Alaplap)
                AddAlaplap((Alaplap)aru);
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

        public static Felhasznalo GetFelhasznalo(long id)
        {
            try
            {
                connection.Open();
                sqlKod = "select * from " + felhasznalo_tab +
                    " where id = :id";
                command = new OracleCommand(sqlKod, connection);
                command.Parameters.Add(new OracleParameter(":id", id));
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

        #region Arucikk kezeles
        public static AruCikk GetArucikk(long id)
        {
            try
            {
                connection.Open();
                sqlKod = "select * from " + raktar_tab + "where id = :id";
                command = new OracleCommand(sqlKod, connection);
                command.Parameters.Add(new OracleParameter(":id", id));
                OracleDataReader dr = command.ExecuteReader();
                AruCikk aru = new AruCikk();
                if (dr.Read())
                {
                    aru.Id = id;
                    aru.Nev = Convert.ToString(dr["nev"]);
                    aru.GyartoId = Convert.ToInt64(dr["gyarto"]);
                    aru.Ar = Convert.ToInt32(dr["ar"]);
                    aru.RaktaronDarab = Convert.ToInt32(dr["darabszam"]);
                    aru.AkcioSzazalek = Convert.ToDouble(dr["akcio"]);
                    aru.Atlag = Convert.ToDouble(dr["atlag"]);
                    aru.ErtekelesekSzama = Convert.ToInt32(dr["ertekeles_szam"]);
                    aru.Leiras = Convert.ToString(dr["leiras"]);
                }
                return aru;
            }
            finally
            {
                if (connection != null)
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
            return GetKiegeszitoTabla(memoria_tipus_tab);
        }

        private static void AddMemoria(Memoria memo)
        {
            try
            {
                connection.Open();
                sqlKod = "insert into raktar_tab values(memoria_typ( " +
                    ":nev, :gyarto, :ar, :darabszam, :tipus, :meret, :sebesseg))";
                command = new OracleCommand(sqlKod, connection);
                command.Parameters.Add(new OracleParameter(":nev", memo.Nev));
                command.Parameters.Add(new OracleParameter(":gyarto", memo.GyartoId));
                command.Parameters.Add(new OracleParameter(":ar", memo.Ar));
                command.Parameters.Add(new OracleParameter(":darabszam", memo.RaktaronDarab));
                command.Parameters.Add(new OracleParameter(":tipus", memo.TipusId));
                command.Parameters.Add(new OracleParameter(":meret", memo.Meret));
                command.Parameters.Add(new OracleParameter(":sebesseg", memo.Sebesseg));

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }

        }

        public static List<Memoria> GetMemoriak()
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
                List<Memoria> ki = new List<Memoria>();
                while (odr.Read())
                {
                    if (!odr.IsDBNull(0))
                    {
                        Memoria memo = new Memoria();
                        memo.Id = Convert.ToInt64(odr[0]);
                        memo.Nev = Convert.ToString(odr[1]);
                        memo.GyartoId = Convert.ToInt64(odr[2]);
                        memo.Ar = Convert.ToInt32(odr[3]);
                        memo.RaktaronDarab = Convert.ToInt32(odr[4]);
                        memo.AkcioSzazalek = Convert.ToInt32(odr[5]);
                        memo.Atlag = Convert.ToInt32(odr[6]);
                        memo.ErtekelesekSzama = Convert.ToInt32(odr[7]);
                        memo.Leiras = Convert.ToString(odr[8]);
                        memo.TipusId = Convert.ToInt64(odr[9]);
                        memo.Meret = Convert.ToInt32(odr[10]);
                        memo.Sebesseg = Convert.ToInt32(odr[11]);

                        ki.Add(memo);
                    }

                }
                return ki;
            }
            finally
            {
                connection.Close();
            }
        }

        public static Memoria GetMemoria(long id)
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
                " from " + raktar_tab + " p " +
                " where id:=id";

                command = new OracleCommand(sqlKod, connection);

                OracleDataReader odr = command.ExecuteReader();
                Memoria memo = new Memoria();
                if (odr.Read() || !odr.IsDBNull(0))
                {
                    memo.Id = Convert.ToInt64(odr[0]);
                    memo.Nev = Convert.ToString(odr[1]);
                    memo.GyartoId = Convert.ToInt64(odr[2]);
                    memo.Ar = Convert.ToInt32(odr[3]);
                    memo.RaktaronDarab = Convert.ToInt32(odr[4]);
                    memo.AkcioSzazalek = Convert.ToInt32(odr[5]);
                    memo.Atlag = Convert.ToInt32(odr[6]);
                    memo.ErtekelesekSzama = Convert.ToInt32(odr[7]);
                    memo.Leiras = Convert.ToString(odr[8]);
                    memo.TipusId = Convert.ToInt64(odr[9]);
                    memo.Meret = Convert.ToInt32(odr[10]);
                    memo.Sebesseg = Convert.ToInt32(odr[11]);
                }

                return memo;
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion

        #region HDD

        public static void AddHddCsatolo(string nev)
        {
            AddKiegeszitoTablaba(nev, hdd_csatolo_tav);
        }

        public static Hashtable GetHddCsatolok()
        {
            return GetKiegeszitoTabla(hdd_csatolo_tav);
        }

        public static List<Hdd> GetHddk()
        {
            try
            {
                connection.Open();
                sqlKod = "select treat(value(p) as hdd_typ).id, " +         //0
                " treat(value(p) as hdd_typ).nev, " +                       //1
                " treat(value(p) as hdd_typ).gyarto, " +                    //2
                " treat(value(p) as hdd_typ).ar, " +                        //3
                " treat(value(p) as hdd_typ).darabszam, " +                 //4
                " treat(value(p) as hdd_typ).akcio, " +                     //5
                " treat(value(p) as hdd_typ).atlag, " +                     //6
                " treat(value(p) as hdd_typ).ertekeles_szam, " +            //7
                " treat(value(p) as hdd_typ).leiras, " +                    //8
                " treat(value(p) as hdd_typ).csatolo, " +                   //9
                " treat(value(p) as hdd_typ).meret " +                      //10

                " from " + raktar_tab + " p ";

                command = new OracleCommand(sqlKod, connection);
                OracleDataReader odr = command.ExecuteReader();

                List<Hdd> ki = new List<Hdd>();
                while (odr.Read())
                {
                    if (!(odr[0] is DBNull))
                    {
                        Hdd hdd = new Hdd();
                        hdd.Id = Convert.ToInt64(odr[0]);
                        hdd.Nev = Convert.ToString(odr[1]);
                        hdd.GyartoId = Convert.ToInt64(odr[2]);
                        hdd.Ar = Convert.ToInt32(odr[3]);
                        hdd.RaktaronDarab = Convert.ToInt32(odr[4]);
                        hdd.AkcioSzazalek = Convert.ToDouble(odr[5]);
                        hdd.Atlag = Convert.ToDouble(odr[6]);
                        hdd.ErtekelesekSzama = Convert.ToInt32(odr[7]);
                        hdd.Leiras = Convert.ToString(odr[8]);
                        hdd.CsatoloId = Convert.ToInt64(odr[9]);
                        hdd.Meret = Convert.ToInt32(odr[10]);

                        ki.Add(hdd);
                    }
                }
                return ki;
            }
            finally
            {
                connection.Close();
            }
        }

        public static Hdd GetHddk(int id)
        {
            try
            {
                connection.Open();
                sqlKod = "select treat(value(p) as hdd_typ).id, " +         //0
                " treat(value(p) as hdd_typ).nev, " +                       //1
                " treat(value(p) as hdd_typ).gyarto, " +                    //2
                " treat(value(p) as hdd_typ).ar, " +                        //3
                " treat(value(p) as hdd_typ).darabszam, " +                 //4
                " treat(value(p) as hdd_typ).akcio, " +                     //5
                " treat(value(p) as hdd_typ).atlag, " +                     //6
                " treat(value(p) as hdd_typ).ertekeles_szam, " +            //7
                " treat(value(p) as hdd_typ).leiras, " +                    //8
                " treat(value(p) as hdd_typ).csatolo, " +                   //9
                " treat(value(p) as hdd_typ).meret " +                      //10

                " from " + raktar_tab + " p " +
                " where id = :id";

                command = new OracleCommand(sqlKod, connection);
                command.Parameters.Add(new OracleParameter(":id", id));
                OracleDataReader odr = command.ExecuteReader();
                odr.Read();
                Hdd hdd = new Hdd();
                if (!(odr[0] is DBNull))
                {
                    hdd.Id = Convert.ToInt64(odr[0]);
                    hdd.Nev = Convert.ToString(odr[1]);
                    hdd.GyartoId = Convert.ToInt64(odr[2]);
                    hdd.Ar = Convert.ToInt32(odr[3]);
                    hdd.RaktaronDarab = Convert.ToInt32(odr[4]);
                    hdd.AkcioSzazalek = Convert.ToDouble(odr[5]);
                    hdd.Atlag = Convert.ToDouble(odr[6]);
                    hdd.ErtekelesekSzama = Convert.ToInt32(odr[7]);
                    hdd.Leiras = Convert.ToString(odr[8]);
                    hdd.CsatoloId = Convert.ToInt64(odr[9]);
                    hdd.Meret = Convert.ToInt32(odr[10]);
                }

                return hdd;
            }
            finally
            {
                connection.Close();
            }
        }

        public static void AddHdd(Hdd h)
        {
            try
            {
                connection.Open();
                sqlKod = "INSERT INTO " + raktar_tab +
                    " values(hdd_typ(:nev, :gyarto, :ar, :darab_szam, :darab_szam" +
                    " :csatolo, :meret )";
                command = new OracleCommand(sqlKod, connection);
                command.Parameters.Add(new OracleParameter(":nev", h.Nev));
                command.Parameters.Add(new OracleParameter(":gyarto", h.GyartoId));
                command.Parameters.Add(new OracleParameter(":ar", h.Ar));
                command.Parameters.Add(new OracleParameter(":darab_szam", h.RaktaronDarab));
                command.Parameters.Add(new OracleParameter(":csatolo", h.CsatoloId));
                command.Parameters.Add(new OracleParameter(":meret", h.Meret));
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion

        #region Alaplap
        private static void AddAlaplap(Alaplap alap)
        {
            try
            {
                connection.Open();
                sqlKod = "INSERT INTO " + raktar_tab +
                    " values(alaplap_typ(:nev, :gyarto, :ar, :darab_szam, " +
                    " :foglalat, :mem_foglalat, :mem_szama, :video_foglalat, :sata, :ide))";
                command = new OracleCommand(sqlKod, connection);
                command.Parameters.Add(new OracleParameter(":nev", alap.Nev));
                command.Parameters.Add(new OracleParameter(":gyarto", alap.GyartoId));
                command.Parameters.Add(new OracleParameter(":ar", alap.Ar));
                command.Parameters.Add(new OracleParameter(":darab_szam", alap.RaktaronDarab));
                command.Parameters.Add(new OracleParameter(":foglalat", alap.CpuFoglalatId));
                command.Parameters.Add(new OracleParameter(":mem_foglalat", alap.MemoriaFoglalatId));
                command.Parameters.Add(new OracleParameter(":mem_szama", alap.MemoriaSzama));
                command.Parameters.Add(new OracleParameter(":video_foglalat", alap.VideoFoglalatId));
                command.Parameters.Add(new OracleParameter(":sata", alap.SataSzama));
                command.Parameters.Add(new OracleParameter(":ide", alap.IdeSzama));
                string s = command.CommandText;
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }

        }

        public static List<Alaplap> GetAlaplapok()
        {
            try
            {
                connection.Open();
                sqlKod = "select treat(value(p) as alap_typ).id, " +         //0
                " treat(value(p) as alap_typ).nev, " +                       //1
                " treat(value(p) as alap_typ).gyarto, " +                    //2
                " treat(value(p) as alap_typ).ar, " +                        //3
                " treat(value(p) as alap_typ).darabszam, " +                 //4
                " treat(value(p) as alap_typ).akcio, " +                     //5
                " treat(value(p) as alap_typ).atlag, " +                     //6
                " treat(value(p) as alap_typ).ertekeles_szam, " +            //7
                " treat(value(p) as alap_typ).leiras, " +                    //8
                " treat(value(p) as alap_typ).foglalat, " +                  //9
                " treat(value(p) as alap_typ).mem_foglalat, " +                  //10
                " treat(value(p) as alap_typ).mem_fog_szam, " +               //11
                " treat(value(p) as alap_typ).video_foglalat " +                    //12
                " treat(value(p) as alap_typ).sata " +                    //13
                " treat(value(p) as alap_typ).ide " +                    //14

                " from " + raktar_tab + " p";
                command = new OracleCommand(sqlKod, connection);
                OracleDataReader odr = command.ExecuteReader();
                List<Alaplap> ki = new List<Alaplap>();

                while (odr.Read())
                {
                    if (!odr.IsDBNull(0))
                    {
                        Alaplap alap = new Alaplap();
                        alap.Id = Convert.ToInt64(odr[0]);
                        alap.Nev = Convert.ToString(odr[1]);
                        alap.GyartoId = Convert.ToInt64(odr[2]);
                        alap.Ar = Convert.ToInt32(odr[3]);
                        alap.RaktaronDarab = Convert.ToInt32(odr[4]);
                        alap.AkcioSzazalek = Convert.ToDouble(odr[5]);
                        alap.Atlag = Convert.ToDouble(odr[6]);
                        alap.ErtekelesekSzama = Convert.ToInt32(odr[7]);
                        alap.Leiras = Convert.ToString(odr[8]);
                        alap.CpuFoglalatId = Convert.ToInt64(odr[9]);
                        alap.MemoriaFoglalatId = Convert.ToInt64(odr[10]);
                        alap.MemoriaSzama = Convert.ToInt32(odr[11]);
                        alap.VideoFoglalatId = Convert.ToInt64(odr[12]);
                        alap.SataSzama = Convert.ToInt32(odr[13]);
                        alap.IdeSzama = Convert.ToInt32(odr[14]);

                        ki.Add(alap);
                    }
                }

                return ki;
            }
            finally
            {
                connection.Close();
            }

        }

        public static Alaplap GetAlaplap(long id)
        {
            try
            {
                connection.Open();
                sqlKod = "select treat(value(p) as alap_typ).id, " +         //0
                " treat(value(p) as alap_typ).nev, " +                       //1
                " treat(value(p) as alap_typ).gyarto, " +                    //2
                " treat(value(p) as alap_typ).ar, " +                        //3
                " treat(value(p) as alap_typ).darabszam, " +                 //4
                " treat(value(p) as alap_typ).akcio, " +                     //5
                " treat(value(p) as alap_typ).atlag, " +                     //6
                " treat(value(p) as alap_typ).ertekeles_szam, " +            //7
                " treat(value(p) as alap_typ).leiras, " +                    //8
                " treat(value(p) as alap_typ).foglalat, " +                  //9
                " treat(value(p) as alap_typ).mem_foglalat, " +              //10
                " treat(value(p) as alap_typ).mem_fog_szam, " +              //11
                " treat(value(p) as alap_typ).video_foglalat " +             //12
                " treat(value(p) as alap_typ).sata " +                       //13
                " treat(value(p) as alap_typ).ide " +                        //14

                " from " + raktar_tab + " p " +
                " where id = id";
                command = new OracleCommand(sqlKod, connection);
                OracleDataReader odr = command.ExecuteReader();

                Alaplap alap = new Alaplap();
                odr.Read();
                if (!odr.IsDBNull(0))
                {
                    alap.Id = Convert.ToInt64(odr[0]);
                    alap.Nev = Convert.ToString(odr[1]);
                    alap.GyartoId = Convert.ToInt64(odr[2]);
                    alap.Ar = Convert.ToInt32(odr[3]);
                    alap.RaktaronDarab = Convert.ToInt32(odr[4]);
                    alap.AkcioSzazalek = Convert.ToDouble(odr[5]);
                    alap.Atlag = Convert.ToDouble(odr[6]);
                    alap.ErtekelesekSzama = Convert.ToInt32(odr[7]);
                    alap.Leiras = Convert.ToString(odr[8]);
                    alap.CpuFoglalatId = Convert.ToInt32(odr[9]);
                    alap.MemoriaFoglalatId = Convert.ToInt64(odr[10]);
                    alap.MemoriaSzama = Convert.ToInt32(odr[11]);
                    alap.VideoFoglalatId = Convert.ToInt64(odr[12]);
                    alap.SataSzama = Convert.ToInt32(odr[13]);
                    alap.IdeSzama = Convert.ToInt32(odr[14]);

                }


                return alap;
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion

        #region HozzaszolasKezeles
        public static void AddUjHozzaszolas(Hozzaszolas h)
        {
            try
            {
                connection.Open();
                sqlKod = "insert into + " + hozzaszolas_tab + " values("
                    + ":id, :aru, :felhasz, :datum, :szoveg, :ellenorzott)";
                command = new OracleCommand(sqlKod, connection);
                command.Parameters.Add(new OracleParameter(":id", h.Id));
                command.Parameters.Add(new OracleParameter(":aru", h.AruCikkId));
                command.Parameters.Add(new OracleParameter(":felhasz", h.FelhasznaloId));
                command.Parameters.Add(new OracleParameter(":datum", h.Datum));
                command.Parameters.Add(new OracleParameter(":szoveg", h.Szoveg));
                command.Parameters.Add(new OracleParameter(":ellenorzott", h.Ellenorzott));
                command.ExecuteNonQuery();
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        public static List<Hozzaszolas> GetHozzaszolasok()
        {
            try
            {
                connection.Open();
                sqlKod = "select * from " + hozzaszolas_tab + ";";
                command = new OracleCommand(sqlKod, connection);
                OracleDataReader dr = command.ExecuteReader();
                List<Hozzaszolas> ki = new List<Hozzaszolas>();
                while (dr.Read())
                {
                    Hozzaszolas h = new Hozzaszolas();
                    h.Id = Convert.ToInt64(dr["id"]);
                    h.AruCikkId = Convert.ToInt64(dr["aru"]);
                    h.FelhasznaloId = Convert.ToInt64(dr["felhasz"]);
                    h.Datum = (DateTime)dr["datum"];
                    h.Szoveg = Convert.ToString(dr["szoveg"]);
                    h.Ellenorzott = Convert.ToBoolean(dr["ellenorzott"]);
                    ki.Add(h);
                }
                return ki;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        public static void HozzaszolasEllenorzes(Hozzaszolas h)
        {
            HozzaszolasEllenorzes(h.Id);
        }

        public static void HozzaszolasEllenorzes(long id)
        {
            try
            {
                connection.Open();
                sqlKod = "update " + hozzaszolas_tab + " set ellenorzott = 1 where " +
                    " id = :id";
                command = new OracleCommand(sqlKod, connection);
                command.Parameters.Add(new OracleParameter(":id", id));
                command.ExecuteNonQuery();

            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        #endregion HozzaszolasKezeles

        #region SzamlaKezelest

        public static void AddSzamla(Szamla szamla)
        {
            try
            {
                connection.Open();
                sqlKod = "insert into " + szamla_tab +
                    "(felhasz) values(:felhasz)";
                command = new OracleCommand(sqlKod, connection);
                command.Parameters.Add(new OracleParameter(":felhasz", szamla.FelhasznaloId));
                command.ExecuteNonQuery();

                long aktualisIndex = -1;
                sqlKod = "select " + szamla_seq + ".CURRVAL from dual";
                command = new OracleCommand(sqlKod, connection);
                OracleDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    aktualisIndex = Convert.ToInt64(dr[0]);
                }
                foreach (Tetel tetel in szamla.Tetelek)
                {
                    sqlKod = "insert into " + tetel_tab +
                        "(aru, szamla, darab, ar) values (:aru, :szamla, :darab, :ar)";
                    command = new OracleCommand(sqlKod, connection);
                    command.Parameters.Add(new OracleParameter(":aru", tetel.AruId));
                    command.Parameters.Add(new OracleParameter(":szamla", aktualisIndex));
                    command.Parameters.Add(new OracleParameter(":darab", tetel.Darab));
                    command.Parameters.Add(new OracleParameter(":ar", tetel.Ar));
                    command.ExecuteNonQuery();
                }

            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        public static Szamla GetSzamla(long id)
        {
            try
            {
                connection.Open();
                sqlKod = "select * from " + szamla_tab + "where id = :id";
                command = new OracleCommand(sqlKod, connection);
                command.Parameters.Add(new OracleParameter(":id", id));
                dr = command.ExecuteReader();
                Szamla ki = new Szamla();
                ki.Id = id;
                if (dr.Read())
                {
                    ki.FelhasznaloId = Convert.ToInt64(dr["felhasz"]);
                    ki.Datum = (DateTime)dr["datum"];
                }
                sqlKod = "select * from " + tetel_tab + "where szamla = :id";
                commandBeallitas();
                command.Parameters.Add(new OracleParameter(":id", id));
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    Tetel t = new Tetel();
                    t.SzamlaId = id;
                    t.AruId = Convert.ToInt64(dr["aru"]);
                    t.Ar = Convert.ToInt32(dr["ar"]);
                    t.Darab = Convert.ToInt32(dr["darab"]);
                    ki.Tetelek.Add(t);
                }

                return ki;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }


        #endregion

        #region Akcio kezeles
        public static List<AruCikk> GetAkciosAruk()
        {
            try
            {
                connection.Open();
                sqlKod = "select * from" + raktar_tab + "where akcio <> 0";
                commandBeallitas();
                dr = command.ExecuteReader();
                List<AruCikk> ki = new List<AruCikk>();
                while (dr.Read())
                {
                    ki.Add(getArucikkRaktarSorbol(dr));
                }
                return ki;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }
        #endregion

        private static void commandBeallitas()
        {
            command = new OracleCommand(sqlKod, connection);
        }

        private static AruCikk getArucikkRaktarSorbol(OracleDataReader dr)
        {
            AruCikk aru = new AruCikk();
            aru.Id = Convert.ToInt64(dr["id"]); ;
            aru.Nev = Convert.ToString(dr["nev"]);
            aru.GyartoId = Convert.ToInt64(dr["gyarto"]);
            aru.Ar = Convert.ToInt32(dr["ar"]);
            aru.RaktaronDarab = Convert.ToInt32(dr["darabszam"]);
            aru.AkcioSzazalek = Convert.ToDouble(dr["akcio"]);
            aru.Atlag = Convert.ToDouble(dr["atlag"]);
            aru.ErtekelesekSzama = Convert.ToInt32(dr["ertekeles_szam"]);
            aru.Leiras = Convert.ToString(dr["leiras"]);
            return aru;
        }

    }
}
