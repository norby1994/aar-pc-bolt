using System;
using System.Data;
using System.Configuration;
namespace PcBolt.Beans
{
    /// <summary>
    /// Absztrakt class, aminek ket leszarmazottja van, a felhasznalo es az admin
    /// </summary>
    public abstract class Szemely
    {
        int id = -1;
        string felhasznaloNev = null;


        /// <summary>
        /// A szemely ID-je az adatbazisban
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// A szemely felhasznaloneve az adatbazisban
        /// </summary>
        public string FelhasznaloNev
        {
            get { return felhasznaloNev; }
            set { felhasznaloNev = value; }
        }
    }
}
