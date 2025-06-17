using ASystem;
using PrviProjektniZadatakHCI.DataAccess;
using PrviProjektniZadatakHCI.Util;
using PrviProjektniZadatakHCI.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace PrviProjektniZadatakHCI.ViewModel
{
    public class ProfessorViewModel : BaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Stack<Action> _undoStack = new();

        private Profesor _currentProfessor;
        public Profesor Profesor { get; }
        public ObservableCollection<Predmet> Predmeti { get; set; }
        public ObservableCollection<Student> Studenti { get; set; }
        public ObservableCollection<DomaciZadatak> Zadaci { get; set; }
        public ObservableCollection<Ispit> IspitZaStudenta { get; set; }
        public ObservableCollection<Prisustvo> PrisustvaZaStudenta { get; set; } = new();
        public ICommand AddTaskCommand { get; }
        public ICommand PregledStudenataCommand { get; }
        public ICommand OpenZadatakCommand { get; }
        public ICommand SaveAttendanceCommand { get; }
        public ICommand SaveGradeCommand { get; }

        public ProfessorViewModel(Profesor profesor)
        {
            _currentProfessor = profesor;
            _korisnik = profesor;
            Profesor = profesor ?? throw new ArgumentNullException(nameof(profesor));

            Predmeti = new(PredmetDataAccess.izlistajPredmete(profesor));
            if (Predmeti != null && Predmeti.Count > 0)
            {
                SelectedPredmetZaPregled = Predmeti[0];
                SelectedPredmetPrisustvo = Predmeti.First();

            }
            Zadaci = new(DomaciZadatakDataAccess.pregledDomacegPoProfesoru(profesor));
            SelectedDate = DateTime.Today;

            AddTaskCommand = new RelayCommand(AddTask);
            OpenZadatakCommand = new RelayCommand(OpenSelectedZadatak);
            SaveAttendanceCommand = new RelayCommand(SaveAttendance);
            PregledStudenataCommand = new RelayCommand(PregledStudenata);
            SaveGradeCommand = new RelayCommand(SaveGrade);
        }

        private string _taskName;
        public string TaskName
        {
            get => _taskName;
            set
            {
                _taskName = value;
                OnPropertyChanged(nameof(TaskName));
            }
        }

        private string _taskDescription;
        public string TaskDescription
        {
            get => _taskDescription;
            set
            {
                _taskDescription = value;
                OnPropertyChanged(nameof(TaskDescription));
            }
        }

        private string _taskCode;
        public string TaskCode
        {
            get => _taskCode;
            set
            {
                _taskCode = value;
                OnPropertyChanged(nameof(TaskCode));
            }
        }

        private DateTime? _taskDeadline;
        public DateTime? TaskDeadline
        {
            get => _taskDeadline;
            set
            {
                _taskDeadline = value;
                OnPropertyChanged(nameof(TaskDeadline));
            }
        }
        public DateTime? SelectedDate { get; set; }

        private string _points;
        public string Points
        {
            get => _points;
            set
            {
                _points = value;
                OnPropertyChanged(nameof(Points));
            }
        }

        private string _grade;
        public string Grade
        {
            get => _grade;
            set
            {
                _grade = value;
                OnPropertyChanged(nameof(Grade));
            }
        }

        private DateTime? _examDate;
        public DateTime? ExamDate
        {
            get => _examDate;
            set
            {
                _examDate = value;
                OnPropertyChanged(nameof(ExamDate));
            }
        }

        public Predmet SelectedPredmet { get; set; }
        private Predmet _selectedPredmetZaPregled;
        public Predmet SelectedPredmetZaPregled
        {
            get => _selectedPredmetZaPregled;
            set
            {
                _selectedPredmetZaPregled = value;
                //  OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedPredmet));

                if (_selectedPredmetZaPregled != null)
                {

                    Studenti = new ObservableCollection<Student>(PredmetDataAccess.studentiSlusaju(_selectedPredmetZaPregled));
                    OnPropertyChanged(nameof(Studenti));
                    if (Studenti != null && Studenti.Any())
                    {
                        SelectedStudentZaPrisustvo = Studenti.First();
                        SelectedStudentZaPregled = Studenti.First();
                    }
                    else
                    {
                        SelectedStudentZaPrisustvo = null;
                    }
                }
            }
        }

        public Predmet SelectedPredmetIspit { get; set; }

        private Predmet _selectedPredmetOcjena;
        public Predmet SelectedPredmetOcjena
        {
            get => _selectedPredmetOcjena;
            set
            {
                _selectedPredmetOcjena = value;
                OnPropertyChanged();

                if (_selectedPredmetOcjena != null)
                {
                    Studenti = new ObservableCollection<Student>(PredmetDataAccess.studentiSlusaju(_selectedPredmetOcjena));
                    OnPropertyChanged(nameof(Studenti));
                }
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

                if (_selectedPredmetPrisustvo != null)
                {
                    UcitajStudenteZaPrisustvo();

                }
            }
        }
        private Student _selectedStudentZaPrisustvo;
        public Student SelectedStudentZaPrisustvo
        {
            get => _selectedStudentZaPrisustvo;
            set
            {
                _selectedStudentZaPrisustvo = value;
                OnPropertyChanged();

                if (_selectedStudentZaPrisustvo != null && SelectedPredmetZaPregled != null)
                {
                    UcitajPrisustvaZaStudentaNaPredmetu();
                }

            }
        }

        private ObservableCollection<Student> _filtriraniStudentiOcjena;
        public ObservableCollection<Student> FiltriraniStudentiOcjena
        {
            get => _filtriraniStudentiOcjena;
            set
            {
                _filtriraniStudentiOcjena = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PrisustvoViewModel> StudentiZaPrisustvo { get; set; } = new();

        public Student SelectedStudent { get; set; }
        private Student _selectedStudentZaPregled;
        public Student SelectedStudentZaPregled
        {
            get => _selectedStudentZaPregled;
            set
            {
                _selectedStudentZaPregled = value;
                OnPropertyChanged();



                UcitajIspiteZaStudenta();

            }
        }

        public Student SelectedStudentOcjena { get; set; }

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
                    OpenSelectedZadatak();
                }
            }
        }


        private void UcitajPrisustvaZaStudentaNaPredmetu()
        {
            PrisustvaZaStudenta.Clear();

            var prisustva = PrisustvoDataAccess.pregledPrisustvaList(
                SelectedStudentZaPrisustvo.idKorisnika,
                SelectedPredmetZaPregled.IdPredmeta);

            foreach (var p in prisustva)
            {
                PrisustvaZaStudenta.Add(p);
            }
        }

        private void AddTask()
        {
            if (string.IsNullOrWhiteSpace(TaskName) || string.IsNullOrWhiteSpace(TaskCode) || string.IsNullOrWhiteSpace(TaskDescription) || !TaskDeadline.HasValue || SelectedPredmet == null)
            {
                string message = (string)Application.Current.Resources["FillField"];
                new WarningWindow(message).ShowDialog();
                return;
            }

            var zadatak = new DomaciZadatak
            {
                naziv = TaskName,
                rok = TaskDeadline.Value.Date,
                predmet = SelectedPredmet,
                opis = TaskDescription,
                idProfesora = Profesor.idKorisnika,
                idDomaciZadatak = TaskCode,
                Profesor = _currentProfessor
            };

            bool success = DomaciZadatakDataAccess.dodajDZadatak(zadatak.naziv, zadatak.opis, zadatak.rok, zadatak.idDomaciZadatak, zadatak.predmet, Profesor);

            if (success)
            {
                Zadaci.Add(zadatak);
                string message = (string)Application.Current.Resources["TaskAdd"];
                new SuccessWindow(message).ShowDialog();
                ResetTaskFields();
            }
            else
            {
                string message = (string)Application.Current.Resources["TaskErrorMessage"];
                new WarningWindow(message).ShowDialog();
                return;
            }
        }


        private void UcitajStudenteZaPrisustvo()
        {
            StudentiZaPrisustvo.Clear();

            var studenti = PredmetDataAccess.studentiSlusaju(SelectedPredmetPrisustvo);
            if (studenti == null || studenti.Count == 0)
            {
                string message = (string)Application.Current.Resources["NoStudentsForCourse"];
                new WarningWindow(message).ShowDialog();
                return;
            }

            foreach (var s in studenti)
            {
                StudentiZaPrisustvo.Add(new PrisustvoViewModel
                {
                    Id = s.idKorisnika,
                    Ime = s.ime,
                    Prezime = s.prezime,
                    IsPresent = false,
                    Student = s
                });
            }
        }

        private void SaveAttendance()
        {
            if (SelectedPredmetPrisustvo == null || !SelectedDate.HasValue)
            {
                string message = (string)Application.Current.Resources["FillField"];
                new WarningWindow(message).ShowDialog();
                return;
            }

            bool prisustvoPostoji = PrisustvoDataAccess.daLiPostojiPrisustvo(
                SelectedDate.Value, SelectedPredmetPrisustvo, SelectedStudentZaPrisustvo);

            if (prisustvoPostoji)
            {
                string message = (string)Application.Current.Resources["AttendanceAlreadyExists"];
                new WarningWindow(message).ShowDialog();
                return;
            }

            bool save = false;

            foreach (var student in StudentiZaPrisustvo)
            {
                if (student.IsPresent)
                {
                    bool success = PrisustvoDataAccess.UnesiPrisustvo(
                        SelectedDate.Value,
                        SelectedPredmetPrisustvo,
                        student.Student);

                    if (!success)
                    {
                        string message = (string)Application.Current.Resources["ErrorAdding"];
                        new WrongWindow(message).ShowDialog();
                        return;
                    }

                    save = true;
                }
            }

            if (save)
            {
                string message = (string)Application.Current.Resources["SuccessfullyAdded"];
                new SuccessWindow(message).ShowDialog();
                UcitajPrisustvaZaStudentaNaPredmetu();
            }
            else
            {
                string message = (string)Application.Current.Resources["ErrorAdding"];
                new WrongWindow(message).ShowDialog();
            }
        }


        private void SaveGrade()
        {
            if (SelectedPredmetOcjena == null || SelectedStudentZaPregled == null || !ExamDate.HasValue)
            {
                string message = (string)Application.Current.Resources["FillField"];
                new WarningWindow(message).ShowDialog();
                return;
            }



            if (!double.TryParse(Points, out double bodovi) || bodovi < 0 || bodovi > 100)
            {
                string message = (string)Application.Current.Resources["PointsValid"];
                new WarningWindow(message).ShowDialog();
                return;
            }

            if (!int.TryParse(Grade, out int ocjena) || ocjena < 5 || ocjena > 10)
            {
                string message = (string)Application.Current.Resources["ValidGrade"];
                new WarningWindow(message).ShowDialog();
                return;
            }

            bool postojiOcjena = IspitDataAccess.daLiPostojiOcjena(ExamDate.Value, SelectedPredmetOcjena, SelectedStudentZaPregled);
            if (postojiOcjena)
            {
                string message = (string)Application.Current.Resources["GradeAlreadyExistsMessage"];
                new WarningWindow(message).ShowDialog();
                return;
            }
            try
            {
                bool uspjeh = IspitDataAccess.sacuvajIspit(
                    bodovi,
                    ocjena,
                    ExamDate.Value,
                    SelectedPredmetOcjena,
                    SelectedStudentZaPregled
                );

                if (uspjeh)
                {
                    string message = (string)Application.Current.Resources["SuccessfullyAdded"];
                    new SuccessWindow(message).ShowDialog();
                    UcitajIspiteZaStudenta();
                    ResetGradeForm();
                }
                else
                {
                    string message = (string)Application.Current.Resources["ErrorAdding"];
                    new WrongWindow(message).ShowDialog();
                }
            }
            catch (Exception ex)
            {
                new WarningWindow($"Greška: {ex.Message}").ShowDialog();
            }
        }

        private void ResetTaskFields()
        {
            TaskName = string.Empty;
            TaskDescription = string.Empty;
            TaskDeadline = null;
            SelectedPredmet = null;
            TaskCode = string.Empty;
        }

        private void ResetGradeForm()
        {
            Points = string.Empty;
            Grade = string.Empty;
            ExamDate = null;
            SelectedPredmetOcjena = null;
            SelectedStudentZaPregled = null;
        }

        private void PregledStudenata()
        {
            if (SelectedPredmet != null)
            {
                var studenti = PredmetDataAccess.studentiSlusaju(SelectedPredmet);
                new StudentView(new ObservableCollection<Student>(studenti)).ShowDialog();
            }
            else
            {
                string message = (string)Application.Current.Resources["NoSubjectSelected"];
                new WarningWindow(message).ShowDialog();
            }
        }

        private void OpenSelectedZadatak()
        {
            if (SelectedDomaci == null) return;
            new ZadatakWindow(SelectedDomaci, _currentProfessor, _undoStack, Zadaci).ShowDialog();
        }
        protected override void ChangeTheme(string theme)
        {
            ChangerThemes.ChangeTheme(theme, Profesor.username);
        }

        private void UcitajIspiteZaStudenta()
        {

            if (SelectedStudentZaPregled != null && SelectedPredmetZaPregled != null)
            {
                IspitZaStudenta = new ObservableCollection<Ispit>(IspitDataAccess.pregledIspita(SelectedPredmetZaPregled, SelectedStudentZaPregled));
                OnPropertyChanged(nameof(IspitZaStudenta));
            }
        }
    }
}
