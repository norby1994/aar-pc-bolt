using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using PcBolt.Beans;
using Oracle.DataAccess.Client;

namespace PcBolt.DAO
{
    public static class Adatbazis
    {
        static string oradb = "Data Source=XE;User Id=peti; Password=peti;"; 
            
            /*"Data Source=(DESCRIPTION="
             + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=8080)))"
             + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));"
             + "User Id=peti;Password=78ikl9;";
             * 
             */


        static OracleConnection connection = new OracleConnection(oradb);
        static string sqlKod;
        static OracleCommand lekerdezes = new OracleCommand("", connection);



        public static string GetFelhasznaloNev()
        {
            try
            {
                connection.Open();
                sqlKod = "select * from felhasznalo_tab;";
                lekerdezes.CommandText = sqlKod;
                OracleDataReader dr = lekerdezes.ExecuteReader();
                dr.Read();
                return dr["felhasznev"].ToString() + "</br>" + dr["teljesnev"].ToString() ;
            }
            finally
            {
                connection.Close();
            }
        }


    }
}
