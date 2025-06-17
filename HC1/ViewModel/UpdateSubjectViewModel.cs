using ASystem;
using PrviProjektniZadatakHCI.Command;
using PrviProjektniZadatakHCI.Util;
using PrviProjektniZadatakHCI.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace PrviProjektniZadatakHCI.ViewModel
{
    public class UpdateSubjectViewModel : BaseViewModel
    {
        public ObservableCollection<Predmet> Subjects { get; set; }

        private Predmet _selectedSubject;
        public Predmet SelectedSubject
        {
            get => _selectedSubject;
            set
            {
                _selectedSubject = value;
                ECTSInput = _selectedSubject?.ECTS.ToString() ?? string.Empty;
                OnPropertyChanged();
            }
        }
        private string _ectsInput;
        public string ECTSInput
        {
            get => _ectsInput;
            set
            {
                _ectsInput = value;
                OnPropertyChanged();
            }
        }
        public ICommand UpdateSubjectCommand { get; }

        public UpdateSubjectViewModel(ObservableCollection<Predmet> sharedSubjects)
        {
            Subjects = sharedSubjects;
            UpdateSubjectCommand = new RelayCommand(ExecuteUpdate, CanUpdate);
        }

        private bool CanUpdate(object obj) => SelectedSubject != null;

        private void ExecuteUpdate(object obj)
        {
            if (string.IsNullOrWhiteSpace(SelectedSubject.Naziv) ||
                string.IsNullOrWhiteSpace(SelectedSubject.Opis) ||
                string.IsNullOrWhiteSpace(ECTSInput))
            {
               
                ShowWarning("FillField");
                return;
            }
            if (!ValidationHelper.IsInteger(ECTSInput))
            {
                ShowWarning("ECTSIntegerValue");
                return;
            }
            int parsedECTS = int.Parse(ECTSInput);
            if (!ValidationHelper.IsValidECTS(parsedECTS))
            {
                ShowWarning("ECTSRange");
                return;
            }
            SelectedSubject.ECTS = parsedECTS;
            var cmd = new UpdateSubjectCommand(SelectedSubject, Subjects);
            bool success = cmd.Execute();
            if (success)
            {
                string message = (string)Application.Current.Resources["SuccessfullyUpdated"];
                new SuccessWindow(message).ShowDialog();
            }
            else
            {
                string message = (string)Application.Current.Resources["UnsuccessfullyUpdated"];
                new WarningWindow(message).ShowDialog();
            }
        }

        private void ShowWarning(string resourceKey)
        {
            string message = (string)Application.Current.Resources[resourceKey];
            new WarningWindow(message).ShowDialog();
        }
    }
}
