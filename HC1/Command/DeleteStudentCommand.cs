using ASystem;
using PrviProjektniZadatakHCI.DataAccess;
using System.Collections.ObjectModel;


namespace PrviProjektniZadatakHCI.Command
{
    public class DeleteStudentCommand 
    {
        private Student _student;
        private ObservableCollection<Student> _students;
        public DeleteStudentCommand(Student student, ObservableCollection<Student> students)
        {
            _student = student;
            _students = students;
        }
        public bool Execute()
        {
            if (StudentDataAccess.obrisiStudenta(_student.idKorisnika))
            {
                _students.Remove(_student);
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
