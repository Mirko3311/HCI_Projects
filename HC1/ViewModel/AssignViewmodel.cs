
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
    public class AssignViewModel : BaseViewModel
    {
        public ObservableCollection<Profesor> Professors { get; }
        public ObservableCollection<Predmet> Subjects { get; }

        private Profesor _selectedProfessor;
        public Profesor SelectedProfessor
        {
            get => _selectedProfessor;
            set
            {
                _selectedProfessor = value;
                OnPropertyChanged();
                ((RelayCommand)AssignCommand).RaiseCanExecuteChanged();
            }
        }

        private Predmet _selectedSubject;
        public Predmet SelectedSubject
        {
            get => _selectedSubject;
            set
            {
                _selectedSubject = value;
                OnPropertyChanged();
                ((RelayCommand)AssignCommand).RaiseCanExecuteChanged();
            }
        }
        public ICommand AssignCommand { get; }

        public AssignViewModel(ObservableCollection<Profesor> sharedProfessors, ObservableCollection<Predmet> sharedSubjects)
        {
            Professors = sharedProfessors;
            Subjects = sharedSubjects;
            AssignCommand = new RelayCommand(ExecuteAssign, CanAssign);
        }

        private bool CanAssign(object obj) => SelectedProfessor != null && SelectedSubject != null;

        private void ExecuteAssign(object obj)
        {

            if (PredmetDataAccess.jeZaduzen(SelectedProfessor, SelectedSubject))
            {
                string msg = (string)Application.Current.Resources["AlreadyAssigned"];
                new WarningWindow(msg).Show();
                return;
            }
            else
            {
                var cmd = new AssignProfessorCommand(SelectedProfessor, SelectedSubject);
                bool success = cmd.Execute();
                string messageKey = success ? "SuccessProfSubject" : "UnsuccessfullyAssignment";
                string finalMessage = (string)Application.Current.Resources[messageKey];
                if (success)
                {
                    new SuccessWindow(finalMessage).Show();
                    SelectedProfessor = null;
                    SelectedSubject = null;
                }
                else
                {
                    new WarningWindow(finalMessage).Show();
                }
            }
        }
    }
}
