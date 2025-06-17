using ASystem;
using System.Collections.ObjectModel;
using System.Windows;


namespace PrviProjektniZadatakHCI
{
   
    public partial class StudentView : Window
    {
        public StudentView(ObservableCollection<Student> studenti)
        {
            InitializeComponent();
            lvStudenti.ItemsSource = studenti; 
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
       // public StudentView() { }
    }
}
