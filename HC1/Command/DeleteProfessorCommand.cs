using ASystem;
using PrviProjektniZadatakHCI.DataAccess;
using System.Collections.ObjectModel;

namespace PrviProjektniZadatakHCI.Command
{
    public class DeleteProfessorCommand
    {
        private readonly Profesor _professor;
        private readonly ObservableCollection<Profesor> _professorCollection;

        public DeleteProfessorCommand(Profesor professor, ObservableCollection<Profesor> professors)
        {
            _professor = professor;
            _professorCollection = professors;
        }

        public bool Execute()
        {
            if (ProfessorDataAccess.obrisiProfesora(_professor))
            {
                _professorCollection.Remove(_professor);
                return true;
            }
            else { return false; }
        }
    }
}
