using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using PrviProjektniZadatakHCI.DataAccess;
namespace ASystem
{
    public class Prisustvo
    {
       
        public DateTime Datum { get; set; }

        public string Status { get; set; }
        public int Predmet_idPredmeta { get; set; }
        public int Student_Korisnik_idKorisnik { get; set; }
        public Student Student { get; set; }
        public Predmet Predmet { get; set; }
        public bool Prisutan { get; set; }
        public string ImeStudenta => Student?.ime ?? "Nepoznato";
        public string PrezimeStudenta => Student?.prezime ?? "Nepoznato";

      

        public Prisustvo(DateTime datum, string status, int predmetId, int studentId)
        {
            Datum = datum;
            Status = status;
            Predmet_idPredmeta = predmetId;
            Student_Korisnik_idKorisnik = studentId;
        }

        public Prisustvo(DateTime datum, Predmet predmet, Student student, bool prisutan)
        {
     
            Datum = datum;
            Predmet = predmet;
            Student = student;
            Prisutan = prisutan;
        }

        public Prisustvo(Student student, DateTime datum, string status, Predmet predmet)
        {
            Student = student;
            Datum = datum;
            Status = status;
            Predmet_idPredmeta = predmet.IdPredmeta;

        }

    }

}
