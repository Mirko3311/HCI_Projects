
using System.Windows;

namespace PrviProjektniZadatakHCI.View
{
    
    public partial class ConfirmWindow : Window
    {
        public ConfirmWindow(string message)
        {
            InitializeComponent();
            MessageText.Text = message;
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true; 
            this.Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;  
            this.Close();
        }
    }
}
