using System;
using System.Data;
using System.Configuration;
using PcBolt.Beans;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

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
                command.CommandText = sqlKod;
                OracleDataReader dr = command.ExecuteReader();
                dr.Read();
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
                    "(felhasznev, jelszo, varos) " +
                    "values( :fnev, :jel, :varos)";
                command = new OracleCommand(sqlKod, connection);
                command.Parameters.Add(new OracleParameter(":fnev", f.FelhasznaloNev));
                command.Parameters.Add(new OracleParameter(":jel", jelszo));
                
               
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }

        }




    }
}
