
using ASystem;
using PrviProjektniZadatakHCI.Command;

using PrviProjektniZadatakHCI.Util;
using PrviProjektniZadatakHCI.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace PrviProjektniZadatakHCI.ViewModel
{
    public class StudentDeleteViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Student> Students { get; }

        private Student _selectedStudent;
        public Student SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
                ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand DeleteCommand { get; }

        public StudentDeleteViewModel(ObservableCollection<Student> sharedStudents)
        {
            Students = sharedStudents;
            DeleteCommand = new RelayCommand(ExecuteDelete, CanDelete);
        }

        private bool CanDelete(object parameter) => SelectedStudent != null;

        private void ExecuteDelete(object parameter)
        {
            if (SelectedStudent == null)
                return;

            var confirmDialog = new ConfirmWindow((string)Application.Current.Resources["DeleteConfirmation"]);
            bool? result = confirmDialog.ShowDialog();

            if (result != true)
                return;

            var cmd = new DeleteStudentCommand(SelectedStudent, Students);
            bool success = cmd.Execute();

            string messageKey = success ? "SuccessfullyDelete" : "UnsuccessfullyDelete";
            string message = (string)Application.Current.Resources[messageKey];

            if (success)
            {
              
                SelectedStudent = null;
                new SuccessWindow(message).ShowDialog();
            }
            else
            {
                new WarningWindow(message).ShowDialog();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
