using ASystem;
using PrviProjektniZadatakHCI.DataAccess;
using System.Collections.ObjectModel;
namespace PrviProjektniZadatakHCI.Command
{
    public class UpdateProfessorCommand 
    {
        private readonly Profesor _updated;
        private readonly Profesor _backup;
        private readonly ObservableCollection<Profesor> _collection;

        public UpdateProfessorCommand(Profesor updated, ObservableCollection<Profesor> collection)
        {
            _updated = updated;
            _collection = collection;

            var original = collection.FirstOrDefault(p => p.idKorisnika == updated.idKorisnika);
            if (original != null)
            {
                _backup = new Profesor
                {
                    idKorisnika = original.idKorisnika,
                    ime = original.ime,
                    prezime = original.prezime,
                    email = original.email,
                    username = original.username,
                    password = original.password,
                    Zvanje = original.Zvanje
                };
            }
        }
     
        public bool Execute()
        {
            bool success = ProfessorDataAccess.AzurirajProfesora(_updated);

            if (!success)
                return false;

            var prof = _collection.FirstOrDefault(p => p.idKorisnika == _updated.idKorisnika);
            if (prof != null)
            {
                prof.ime = _updated.ime;
                prof.prezime = _updated.prezime;
                prof.email = _updated.email;
                prof.username = _updated.username;
                prof.Zvanje = _updated.Zvanje;
            }

            return true;
        }

     
    }
}
