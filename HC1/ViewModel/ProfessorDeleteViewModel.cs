using ASystem;
using PrviProjektniZadatakHCI.Command;
using PrviProjektniZadatakHCI.Util;
using PrviProjektniZadatakHCI.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace PrviProjektniZadatakHCI.ViewModel
{
    public class ProfessorDeleteViewModel : INotifyPropertyChanged
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
                ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand DeleteCommand { get; }

      

        public ProfessorDeleteViewModel(ObservableCollection<Profesor> sharedProfessors)
        {
            Professors = sharedProfessors;
            DeleteCommand = new RelayCommand(ExecuteDelete, CanDelete);
        }

        private bool CanDelete(object parameter) => SelectedProfessor != null;

        private void ExecuteDelete(object parameter)
        {
            if (SelectedProfessor == null)
                return;

            var confirmDialog = new ConfirmWindow(
                (string)Application.Current.Resources["DeleteConfirmation"]
            );

            bool? result = confirmDialog.ShowDialog();

            if (result != true)
                return;

            var cmd = new DeleteProfessorCommand(SelectedProfessor, Professors);
            bool success = cmd.Execute();

            string messageKey = success ? "SuccessfullyDelete" : "UnsuccessfullyDelete";
            string message = (string)Application.Current.Resources[messageKey];

            if (success)
            {
            
                SelectedProfessor = null;
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
