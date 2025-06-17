using PrviProjektniZadatakHCI.ViewModel;
using System.Windows;
namespace ASystem
{
    public partial class AdminWindow 
    {
        public AdminWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            var vm = new AdminViewModel();
            this.DataContext = vm;
        }

    }
}
   



