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
        static string oradb = "Data Source=XE;User Id=peti; Password=peti;";
        static string felhasznalo_tab = "felhasznalo_tab";
        static string cpu_foglalat_tab = "cpu_foglalat_tab";
        static string gyarto_tab = "gyarto_tab";
        static string raktar_tab = "raktar_tab";

        static private Hashtable gyartok = new Hashtable();
        static private Hashtable cpu_foglalatok = new Hashtable();


        static OracleConnection connection = new OracleConnection(oradb);
        static string sqlKod;
        static OracleCommand command = new OracleCommand("", connection);

        public static void Init()
        {
            kiegeszitoFrissites();

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

        // FELHASZNALOKEZELES

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

        private static void kiegeszitoFrissites()
        {
            gyartok = GetGyartok();
            cpu_foglalatok = GetProcesszorFoglalatok();
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

            return null;

        }

        #endregion

        #region Kegeszito tablak

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

        public static void AddProcesszor(Processzor proci)
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
                    Processzor cpu = new Processzor();
                    cpu.Id = Convert.ToInt64(odr[0]);
                    cpu.Nev = Convert.ToString(odr[1]);
                    cpu.GyartoId = Convert.ToInt64(odr[2]);
                    cpu.Gyarto = Convert.ToString(gyartok[cpu.GyartoId]);
                    cpu.Ar = Convert.ToInt32(odr[3]);
                    cpu.RaktaronDarab = Convert.ToInt32(odr[4]);
                    cpu.AkcioSzazalek = Convert.ToDouble(odr[5]);
                    cpu.Atlag = Convert.ToDouble(odr[6]);
                    cpu.ErtekelesekSzama = Convert.ToInt32(odr[7]);
                    cpu.Leiras = Convert.ToString(odr[8]);
                    cpu.Sebesseg = Convert.ToInt32(odr[9]);
                    cpu.FoglalatID = Convert.ToInt64(odr[10]);
                    cpu.Foglalat = Convert.ToString(cpu_foglalatok[cpu.FoglalatID]);
                    cpu.MagokSzama = Convert.ToInt32(odr[11]);
                    cpu.Dobozos = Convert.ToBoolean(odr[12]);

                    ki.Add(cpu);

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
                odr.Read();
                Processzor cpu = new Processzor();
                cpu.Id = Convert.ToInt64(odr[0]);
                cpu.Nev = Convert.ToString(odr[1]);
                cpu.GyartoId = Convert.ToInt64(odr[2]);
                cpu.Gyarto = Convert.ToString(gyartok[cpu.GyartoId]);
                cpu.Ar = Convert.ToInt32(odr[3]);
                cpu.RaktaronDarab = Convert.ToInt32(odr[4]);
                cpu.AkcioSzazalek = Convert.ToDouble(odr[5]);
                cpu.Atlag = Convert.ToDouble(odr[6]);
                cpu.ErtekelesekSzama = Convert.ToInt32(odr[7]);
                cpu.Leiras = Convert.ToString(odr[8]);
                cpu.Sebesseg = Convert.ToInt32(odr[9]);
                cpu.FoglalatID = Convert.ToInt64(odr[10]);
                cpu.Foglalat = Convert.ToString(cpu_foglalatok[cpu.FoglalatID]);
                cpu.MagokSzama = Convert.ToInt32(odr[11]);
                cpu.Dobozos = Convert.ToBoolean(odr[12]);
                return cpu;
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion

    }
}
