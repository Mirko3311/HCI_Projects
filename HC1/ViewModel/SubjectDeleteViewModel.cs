
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
    public class SubjectDeleteViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Predmet> Subjects { get; }

        private Predmet _selectedSubject;
        public Predmet SelectedSubject
        {
            get => _selectedSubject;
            set
            {
                _selectedSubject = value;
                OnPropertyChanged();
                ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand DeleteCommand { get; }
        public SubjectDeleteViewModel(ObservableCollection<Predmet> sharedSubjects)
        {
            Subjects = sharedSubjects;
            DeleteCommand = new RelayCommand(ExecuteDelete, CanDelete);
        }

        private bool CanDelete(object obj) => SelectedSubject != null;

        private void ExecuteDelete(object obj)
        {
            if (SelectedSubject == null)
                return;

            var confirmDialog = new ConfirmWindow(
                (string)Application.Current.Resources["DeleteConfirmation"]
            );

            bool? result = confirmDialog.ShowDialog();

            if (result != true)
                return;

            var cmd = new DeleteSubjectCommand(SelectedSubject, Subjects);
            bool success = cmd.Execute();

            string messageKey = success ? "SuccessfullyDelete" : "UnsuccessfullyDelete";
            var message = (string)Application.Current.Resources[messageKey];

            if (success)
            {
                SelectedSubject = null;
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
