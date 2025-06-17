using ASystem;
using PrviProjektniZadatakHCI.DataAccess;
using System.Collections.ObjectModel;


namespace PrviProjektniZadatakHCI.Command
{
    public class AddSubjectCommand 
    {
        private readonly Predmet _predmet;
        private readonly ObservableCollection<Predmet> _subjectsCollection;

        public AddSubjectCommand(Predmet predmet, ObservableCollection<Predmet> subjects)
        {
            _predmet = predmet;
            _subjectsCollection = subjects;
        }
        public bool Execute()
        {
            if (PredmetDataAccess.dodajPredmet(_predmet))
            {
                _subjectsCollection.Add(_predmet);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
