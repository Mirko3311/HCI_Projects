using ASystem;
using PrviProjektniZadatakHCI.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrviProjektniZadatakHCI.Command
{
    public class UpdateStudentCommand 
    {
        private readonly Student _updated;
        private readonly Student _backup;
        private readonly ObservableCollection<Student> _collection;

        public UpdateStudentCommand(Student updated, ObservableCollection<Student> collection)
        {
            _updated = updated;
            _collection = collection;

            var original = collection.FirstOrDefault(s => s.idKorisnika == updated.idKorisnika);
            if (original != null)
            {
                _backup = new Student
                {
                    idKorisnika = original.idKorisnika,
                    ime = original.ime,
                    prezime = original.prezime,
                    email = original.email,
                    BrojIndeksa = original.BrojIndeksa,
                    GodinaStudija = original.GodinaStudija,
                    username = original.username
                };
            }
        }

        public bool Execute()
        {
            if (StudentDataAccess.AzurirajStudenta(_updated))
            {
                var studentInList = _collection.FirstOrDefault(s => s.idKorisnika == _updated.idKorisnika);
                if (studentInList != null)
                {
                    studentInList.ime = _updated.ime;
                    studentInList.prezime = _updated.prezime;
                    studentInList.email = _updated.email;
                    studentInList.BrojIndeksa = _updated.BrojIndeksa;
                    studentInList.GodinaStudija = _updated.GodinaStudija;
                    studentInList.username = _updated.username;
                }
                return true;
            }
            return false;
        }

    }

}
