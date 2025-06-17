using ASystem;
using PrviProjektniZadatakHCI.DataAccess;
using PrviProjektniZadatakHCI.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
namespace PrviProjektniZadatakHCI.ViewModel
{
    public class StudentViewModel : BaseViewModel
    {
        public Student Student { get; }

        private Predmet _selectedPredmetZadaci;
        public Predmet SelectedPredmetZadaci
        {
            get => _selectedPredmetZadaci;
            set
            {
                _selectedPredmetZadaci = value;
                OnPropertyChanged();
                LoadZadaci();
            }
        }

        private ObservableCollection<DomaciZadatak> _zadaciZaPredmet;
        public ObservableCollection<DomaciZadatak> ZadaciZaPredmet
        {
            get => _zadaciZaPredmet;
            set
            {
                _zadaciZaPredmet = value;
                OnPropertyChanged();
            }
        }
        private Predmet _selectedPredmetIspiti;
        public Predmet SelectedPredmetIspiti
        {
            get => _selectedPredmetIspiti;
            set
            {
                _selectedPredmetIspiti = value;
                OnPropertyChanged();
                LoadIspiti();
            }
        }
        public ObservableCollection<Predmet> PredmetiZaStudenta { get; set; }
        public ObservableCollection<Prisustvo> Prisustva { get; set; }

        private ObservableCollection<Prisustvo> _prisustvaZaStudenta;
        public ObservableCollection<Prisustvo> PrisustvaZaStudenta
        {
            get => _prisustvaZaStudenta;
            set
            {
                _prisustvaZaStudenta = value;
                OnPropertyChanged();
            }
        }

        private Predmet _selectedPredmet;
        public Predmet SelectedPredmet
        {
            get => _selectedPredmet;
            set
            {
                _selectedPredmet = value;
                OnPropertyChanged();
                LoadPrisustva();
                LoadZadaci();
                LoadIspiti();
            }
        }

        private Predmet _selectedPredmetPrisustvo;
        public Predmet SelectedPredmetPrisustvo
        {
            get => _selectedPredmetPrisustvo;
            set
            {
                _selectedPredmetPrisustvo = value;
                OnPropertyChanged();
                UcitajPrisustva();
            }
        }

        private DomaciZadatak _selectedDomaci;
        public DomaciZadatak SelectedDomaci
        {
            get => _selectedDomaci;
            set
            {
                _selectedDomaci = value;
                OnPropertyChanged();
                if (_selectedDomaci != null)
                {
                    new ZadatakWindow(_selectedDomaci).ShowDialog();
                }
            }
        }
        
        private ObservableCollection<Ispit> _ispitiZaPredmet;
        public ObservableCollection<Ispit> IspitiZaPredmet
        {
            get => _ispitiZaPredmet;
            set
            {
                _ispitiZaPredmet = value;
                OnPropertyChanged();
            }
        }
        public ICommand PasswordCommand { get; }
        public StudentViewModel(Student student) : base()
        {
            Student = student;
            _korisnik = student;
            Prisustva = new ObservableCollection<Prisustvo>();

            PredmetiZaStudenta = new ObservableCollection<Predmet>(PredmetDataAccess.studentiSlusajuPredmet(Student));
            IspitiZaPredmet = new ObservableCollection<Ispit>();
    
            if (PredmetiZaStudenta.Count > 0)
            {
                SelectedPredmetZadaci = PredmetiZaStudenta[0];
                SelectedPredmetIspiti = PredmetiZaStudenta[0];
                SelectedPredmetPrisustvo = PredmetiZaStudenta[0];
                SelectedPredmet = PredmetiZaStudenta[0];
            }
        }
        private void LoadZadaci()
        {
            if (SelectedPredmetZadaci == null)
            {
                ZadaciZaPredmet = new ObservableCollection<DomaciZadatak>();
                new WarningWindow((string)Application.Current.Resources["NoAssignmentsForCourse"])?.ShowDialog();
                return;
            }

            var zadaci = DomaciZadatakDataAccess.pregledDomacegZadatkaPoPredmetu(SelectedPredmetZadaci);
            ZadaciZaPredmet = new ObservableCollection<DomaciZadatak>(zadaci);

            if (zadaci.Count == 0)
            {
                new WarningWindow((string)Application.Current.Resources["NoAssignmentsForCourse"])?.ShowDialog();
            }
        }

        private void LoadIspiti()
        {
            if (SelectedPredmetIspiti == null)
            {
                IspitiZaPredmet = new ObservableCollection<Ispit>();
                new WarningWindow((string)Application.Current.Resources["NoExamsForCourse"])?.ShowDialog();
                return;
            }

            var ispiti = IspitDataAccess.pregledIspita(SelectedPredmetIspiti, Student);
            IspitiZaPredmet = new ObservableCollection<Ispit>(ispiti);

            if (ispiti.Count == 0)
            {
                new WarningWindow((string)Application.Current.Resources["NoExamsForCourse"])?.ShowDialog();
            }
        }

        private void LoadPrisustva()
        {
            if (SelectedPredmet == null)
            {
                Prisustva = new ObservableCollection<Prisustvo>();
                new WarningWindow((string)Application.Current.Resources["NoAttendanceForCourse"])?.ShowDialog();
                return;
            }

            var prisustva = PrisustvoDataAccess.pregledPrisustvaList(Student.idKorisnika, SelectedPredmet.IdPredmeta);
            Prisustva = new ObservableCollection<Prisustvo>(prisustva);

            if (prisustva.Count == 0)
            {
                new WarningWindow((string)Application.Current.Resources["NoAttendanceForCourse"])?.ShowDialog();
            }
        }
       public void UcitajPrisustva()
        {
            if (SelectedPredmetPrisustvo == null)
            {
                PrisustvaZaStudenta = new ObservableCollection<Prisustvo>();
                new WarningWindow((string)Application.Current.Resources["NoAttendanceForCourse"])?.ShowDialog();
                return;
            }

            var prisustva = PrisustvoDataAccess.pregledPrisustvaList(Student.idKorisnika, SelectedPredmetPrisustvo.IdPredmeta);
            PrisustvaZaStudenta = new ObservableCollection<Prisustvo>(prisustva);

            if (prisustva.Count == 0)
            {
                new WarningWindow((string)Application.Current.Resources["NoAttendanceForCourse"])?.ShowDialog();
            }
        }
    }
}