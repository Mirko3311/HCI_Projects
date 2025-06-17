
using ASystem;
using PrviProjektniZadatakHCI.DataAccess;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using PrviProjektniZadatakHCI.Util;

namespace PrviProjektniZadatakHCI.ViewModel
{
    public class AdminViewModel : BaseViewModel
    {
        public ObservableCollection<Student> Students { get; }
        public ObservableCollection<Profesor> Professors { get; }
        public ObservableCollection<Predmet> Subjects { get; }
        public ObservableCollection<Korisnik> Korisnici{ get; }

        public AddStudentViewModel AddStudentVM { get; }
        public AddProfessorViewModel AddProfessorVM { get; }
        public AddSubjectViewModel AddSubjectVM { get; }
        public StudentDeleteViewModel StudentDeleteVM { get; }
        public ProfessorDeleteViewModel ProfessorDeleteVM { get; }
        public SubjectDeleteViewModel SubjectDeleteVM { get; }
        public UpdateStudentViewModel UpdateStudentVM { get; }
        public UpdateProfessorViewModel UpdateProfessorVM { get; }
        public UpdateSubjectViewModel UpdateSubjectVM { get; }
        public AssignViewModel AssignVM { get; }
        public UnassignViewModel UnassignVM { get; }

        public ICommand CancelCommand { get; }

        public ICommand DeleteFromNavBarCommand { get; }

        private string _selectedUpdateEntityType;
        public string SelectedUpdateEntityType
        {
            get => _selectedUpdateEntityType;
            set
            {
                if (_selectedUpdateEntityType != value)
                {      
                    _selectedUpdateEntityType = value;
                    OnPropertyChanged();
                   LoadUpdateItems();
                   OnPropertyChanged(nameof(SelectedUpdateViewModel));
                }
            }
        }
       

        private ObservableCollection<object> _updateItems;
        public ObservableCollection<object> UpdateItems
        {
            get => _updateItems;
            set
            {
                _updateItems = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsUpdateComboEnabled));
            }
        }

        public bool IsUpdateComboEnabled => UpdateItems != null && UpdateItems.Any();

        private object _selectedUpdateItem;
        public object SelectedUpdateItem
        {

            get => _selectedUpdateItem;
            set
            {
                _selectedUpdateItem = value;

                if (value == null)
                    return;
                if (UpdateStudentVM != null)
                    UpdateStudentVM.SelectedStudent = value as Student;
                if (UpdateProfessorVM != null)
                    UpdateProfessorVM.SelectedProfessor = value as Profesor;
                if (UpdateSubjectVM != null)
                    UpdateSubjectVM.SelectedSubject = value as Predmet;

                OnPropertyChanged();
              OnPropertyChanged(nameof(SelectedUpdateViewModel));
            }
        }

   
       public object SelectedUpdateViewModel
        {
            get
            {
              
                return SelectedUpdateEntityType switch
                {
                    "Student" => UpdateStudentVM,
                    "Professor" => UpdateProfessorVM,
                    "Subject" => UpdateSubjectVM,
                    _ => null
                };
            }
        }

        private EntityType _selectedDeleteEntityType = EntityType.Student;
        public EntityType SelectedDeleteEntityType
        {
            get => _selectedDeleteEntityType;
            set
            {
                _selectedDeleteEntityType = value;
                OnPropertyChanged();
                ((RelayCommand)DeleteFromNavBarCommand).RaiseCanExecuteChanged();
            }
        }



        private EntityType _selectedEntity;
        public EntityType SelectedEntity
        {
            get => _selectedEntity;
            set
            {
                if (_selectedEntity != value)
                {
                    _selectedEntity = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsProfessorSelectedVisibility));
                    OnPropertyChanged(nameof(IsStudentSelectedVisibility));
                    OnPropertyChanged(nameof(IsSubjectSelectedVisibility));
                }
            }
        }

        private void ExecuteCancel(object obj)
        {
            AddProfessorVM?.ClearFields();
            AddStudentVM?.ClearFields();
            AddSubjectVM?.ClearFields();
            SelectedUpdateItem = null;
        }

        public Visibility IsProfessorSelectedVisibility => SelectedEntity == EntityType.Professor ? Visibility.Visible : Visibility.Collapsed;
        public Visibility IsStudentSelectedVisibility => SelectedEntity == EntityType.Student ? Visibility.Visible : Visibility.Collapsed;
        public Visibility IsSubjectSelectedVisibility => SelectedEntity == EntityType.Subject ? Visibility.Visible : Visibility.Collapsed;

        public string Password { get; set; }
        public bool IsPasswordVisible { get; set; }

        private void LoadUpdateItems()
        {
            switch (SelectedUpdateEntityType)
            {


                case "Student":
                    UpdateItems = new ObservableCollection<object>(Students);
                    break;
                case "Professor":
                    UpdateItems = new ObservableCollection<object>(Professors);
                    break;
                case "Subject":
                    UpdateItems = new ObservableCollection<object>(Subjects);
                    break;
                default:
                    UpdateItems = new ObservableCollection<object>();
                    break;
            }
            SelectedUpdateItem = null;
        }
        public AdminViewModel()
        {
            try
            {
            CancelCommand = new RelayCommand(ExecuteCancel);
            Students = StudentDataAccess.GetStudents();
            Professors = ProfessorDataAccess.GetProfessors();
            Subjects = PredmetDataAccess.pregledPredmeta();
             Korisnici = KorisnikDataAccess.GetKorisnici();

            AddStudentVM = new AddStudentViewModel(Students,Korisnici);
            AddProfessorVM = new AddProfessorViewModel(Professors, Korisnici);
            AddSubjectVM = new AddSubjectViewModel(Subjects);

            StudentDeleteVM = new StudentDeleteViewModel(Students);
            ProfessorDeleteVM = new ProfessorDeleteViewModel(Professors);
            SubjectDeleteVM = new SubjectDeleteViewModel(Subjects);

            UpdateStudentVM = new UpdateStudentViewModel(Students);
            UpdateProfessorVM = new UpdateProfessorViewModel(Professors);
            UpdateSubjectVM = new UpdateSubjectViewModel(Subjects);
            DeleteFromNavBarCommand = new RelayCommand(ExecuteDelete, CanDelete);
            SelectedUpdateEntityType = "Professor";
            AssignVM = new AssignViewModel(Professors, Subjects);
            UnassignVM = new UnassignViewModel(Professors, Subjects);
            IsPasswordVisible = false;
          


            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška u AdminViewModel: " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }


        private bool CanDelete(object obj)
        {
            return SelectedDeleteEntityType switch
            {
                EntityType.Student => StudentDeleteVM.SelectedStudent != null,
                EntityType.Professor => ProfessorDeleteVM.SelectedProfessor != null,
                EntityType.Subject => SubjectDeleteVM.SelectedSubject != null,
                _ => false
            };
        }

        private void ExecuteDelete(object obj)
        {
            switch (SelectedDeleteEntityType)
            {
                case EntityType.Student:
                    StudentDeleteVM.DeleteCommand.Execute(null);
                    break;
                case EntityType.Professor:
                    ProfessorDeleteVM.DeleteCommand.Execute(null);
                    break;
                case EntityType.Subject:
                    SubjectDeleteVM.DeleteCommand.Execute(null);
                    break;
            }
        }
    }
}
