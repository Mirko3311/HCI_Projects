using ASystem;
using PrviProjektniZadatakHCI.DataAccess;
using System.Collections.ObjectModel;


namespace PrviProjektniZadatakHCI.Command
{
    public class UpdateSubjectCommand
    {
        private readonly Predmet _updated;
        private readonly Predmet _backup;
        private readonly ObservableCollection<Predmet> _collection;

        public UpdateSubjectCommand(Predmet updated, ObservableCollection<Predmet> collection)
        {
            _updated = updated;
            _collection = collection;

            var original = collection.FirstOrDefault(p => p.IdPredmeta == updated.IdPredmeta);
            if (original != null)
            {
                _backup = new Predmet
                {
                    IdPredmeta = original.IdPredmeta,
                    Naziv = original.Naziv,
                    Opis = original.Opis,
                    ECTS = original.ECTS
                };
            }
        }

        public bool Execute()
        {
            if (PredmetDataAccess.AzurirajPredmet(_updated))
            {
                var subject = _collection.FirstOrDefault(p => p.IdPredmeta == _updated.IdPredmeta);
                if (subject != null)
                {
                    subject.Naziv = _updated.Naziv;
                    subject.Opis = _updated.Opis;
                    subject.ECTS = _updated.ECTS;
                }
                return true;
            }
            return false;
        }
    }
}
