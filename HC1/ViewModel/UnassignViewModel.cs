using ASystem;
using PrviProjektniZadatakHCI.Command;
using PrviProjektniZadatakHCI.DataAccess;
using PrviProjektniZadatakHCI.Util;
using PrviProjektniZadatakHCI.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace PrviProjektniZadatakHCI.ViewModel
{
    public class UnassignViewModel : BaseViewModel
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
                if (_selectedProfessor != null)
                {
                    var predmetiProfesora = Subjects
                        .Where(p => PredmetDataAccess.jeZaduzen(_selectedProfessor, p))
                        .ToList();

                    FilteredSubjects = new ObservableCollection<Predmet>(predmetiProfesora);
                    if (FilteredSubjects.Count == 0)
                    {
                        string poruka = (string)Application.Current.Resources["NoAssignedSubjects"];
                        new WarningWindow(poruka).ShowDialog();
                    }
                }
                else
                {
                    FilteredSubjects.Clear();
                }

                ((RelayCommand)UnassignCommand).RaiseCanExecuteChanged();
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
                ((RelayCommand)UnassignCommand).RaiseCanExecuteChanged();
            }
        }
        public bool HasSubjects => FilteredSubjects != null && FilteredSubjects.Count > 0;
        private ObservableCollection<Predmet> _filteredSubjects = new();
        public ObservableCollection<Predmet> FilteredSubjects
        {
            get => _filteredSubjects;
            set
            {
                _filteredSubjects = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasSubjects));
            }
        }
        public ICommand UnassignCommand { get; }
  
        public UnassignViewModel(ObservableCollection<Profesor> sharedProfessors, ObservableCollection<Predmet> sharedSubjects)
        {
            Professors = sharedProfessors;
            Subjects = sharedSubjects;
            UnassignCommand = new RelayCommand(ExecuteUnassign, CanUnassign);
        }

        private bool CanUnassign(object obj) => SelectedProfessor != null && SelectedSubject != null;

        private void ExecuteUnassign(object obj)
        {
            var cmd = new UnassignProfessorCommand(SelectedProfessor, SelectedSubject);
            bool success = cmd.Execute();
            string messageKey = success ? "UnassignMessage" : "UnsuccessfullyAssignment";
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
