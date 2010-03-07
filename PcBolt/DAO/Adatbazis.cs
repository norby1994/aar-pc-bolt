using System;
using System.Data;
using System.Configuration;
using PcBolt.Beans;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using PcBolt.Exceptions;

namespace PcBolt.DAO
{
    public static class Adatbazis
    {
        static string oradb = "Data Source=XE;User Id=peti; Password=peti;"; 
        static string felhasznalo_tab = "felhasznalo_tab";    
            /*"Data Source=(DESCRIPTION="
             + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=8080)))"
             + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));"
             + "User Id=peti;Password=78ikl9;";
             * 
             */


        static OracleConnection connection = new OracleConnection(oradb);
        static string sqlKod;
        static OracleCommand command = new OracleCommand("", connection);



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

        public static void AddUjFelhasznalo(Felhasznalo f, string jelszo)
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

        public static Felhasznalo GetFelhasznalo(string felhasznaloNev)
        {
            try
            {
                connection.Open();
                sqlKod = "select * from " + felhasznalo_tab +
                    " where felhasznev like :felhasznev";
                command = new OracleCommand(sqlKod, connection);
                command.Parameters.Add(new OracleParameter(":felhasznev",felhasznaloNev));
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



    }
}
