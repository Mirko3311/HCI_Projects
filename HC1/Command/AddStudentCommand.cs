using ASystem;

using PrviProjektniZadatakHCI.DataAccess;

using System.Collections.ObjectModel;


namespace PrviProjektniZadatakHCI.Command
{
    public class AddStudentCommand 
    {
        private readonly Student _studentToAdd;
        private readonly ObservableCollection<Student> _studentsCollection;

        public AddStudentCommand(Student studentToAdd, ObservableCollection<Student> studentsCollection)
        {
            _studentToAdd = studentToAdd;
            _studentsCollection = studentsCollection;
        }

        public bool Execute()
        {
            _studentToAdd.tipKorisnika = "student";
            if (StudentDataAccess.dodajStudenta(_studentToAdd))
            {
                _studentsCollection.Add(_studentToAdd);
                return true;
            }
            return false;
        }
    }

}


