using ASystem;
using PrviProjektniZadatakHCI.DataAccess;
using System.Collections.ObjectModel;
using System.Linq;

namespace PrviProjektniZadatakHCI.Command
{
    public class AddProfessorCommand 
    {
        private readonly Profesor _profesor;
        private readonly ObservableCollection<Profesor> _professorsCollection;

        public AddProfessorCommand(Profesor profesor, ObservableCollection<Profesor> professors)
        {
            _profesor = profesor;
            _professorsCollection = professors;
        }
        public bool Execute()
        {
            if (ProfessorDataAccess.dodajProfesora(_profesor))
            {
                _professorsCollection.Add(_profesor);
                return true;
            }

            return false;
        }
    }
}
