using ASystem;
using PrviProjektniZadatakHCI.Command;
using PrviProjektniZadatakHCI.DataAccess;
using PrviProjektniZadatakHCI.Util;
using PrviProjektniZadatakHCI.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace PrviProjektniZadatakHCI.ViewModel
{
    public class UpdateProfessorViewModel : BaseViewModel
    {
        public ObservableCollection<Profesor> Professors { get; }

        private Profesor _selectedProfessor;
        public Profesor SelectedProfessor
        {
            get => _selectedProfessor;
            set
            {
                _selectedProfessor = value;
                OnPropertyChanged();
                ((RelayCommand)UpdateProfessorCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand UpdateProfessorCommand { get; }
        public UpdateProfessorViewModel(ObservableCollection<Profesor> sharedProfessors)
        {
            Professors = sharedProfessors;
            UpdateProfessorCommand = new RelayCommand(ExecuteUpdate, CanUpdate);
        }
        public UpdateProfessorViewModel()
            : this(new ObservableCollection<Profesor>(ProfessorDataAccess.GetProfessors()))
        {
        }

        private bool CanUpdate(object obj) => SelectedProfessor != null;

        private void ExecuteUpdate(object obj)
        {
            if (SelectedProfessor == null)
                return;
            if (string.IsNullOrWhiteSpace(SelectedProfessor.ime) ||
                string.IsNullOrWhiteSpace(SelectedProfessor.prezime) ||
                string.IsNullOrWhiteSpace(SelectedProfessor.username) ||
                string.IsNullOrWhiteSpace(SelectedProfessor.password) ||
                string.IsNullOrWhiteSpace(SelectedProfessor.email) ||
                string.IsNullOrWhiteSpace(SelectedProfessor.Zvanje))
            {
                string message = (string)Application.Current.Resources["FillField"];
                new WarningWindow(message).ShowDialog();
                return;
            }
            if (!ValidationHelper.IsValidEmail(SelectedProfessor.email))
            {
                string message = (string)Application.Current.Resources["InvalidEmailFormat"];
                new WarningWindow(message).ShowDialog();
                return;
            }
            var cmd = new UpdateProfessorCommand(SelectedProfessor, Professors);
            bool success = cmd.Execute();

            if (success)
            {
                string message = (string)Application.Current.Resources["ProfessorSuccessfullyUpdated"];
                new SuccessWindow(message).ShowDialog();
            }
            else
            {
                string message = (string)Application.Current.Resources["UnsuccessfullyUpdated"];
                new WarningWindow(message).ShowDialog();
            }
        }

    }
}
