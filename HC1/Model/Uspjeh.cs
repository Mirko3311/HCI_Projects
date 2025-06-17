
using System.Configuration;

namespace PrviProjektniZadatakHCI
{
    public class Uspjeh
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["MySql_hci"].ConnectionString;
        public string Predmet { get; set; }
        public decimal BodoviIspit { get; set; }
        public int OcjenaIspit { get; set; }
        public decimal BodoviDomaci { get; set; }

     
    }
}
