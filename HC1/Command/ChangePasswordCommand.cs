using ASystem;
using PrviProjektniZadatakHCI.View;
using System.Windows.Input;

namespace PrviProjektniZadatakHCI.Command
{

    public class ChangePasswordCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return parameter is Korisnik; 
        }

        public void Execute(object parameter)
        {
            if (parameter is Korisnik korisnik)
            {
                var prozor = new PromjenaLozinkeView(korisnik);
                prozor.ShowDialog();
            }
        }
    }
        
    
}
