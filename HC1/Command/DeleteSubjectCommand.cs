using ASystem;
using PrviProjektniZadatakHCI.DataAccess;
using PrviProjektniZadatakHCI.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrviProjektniZadatakHCI.Command
{
    public class DeleteSubjectCommand
    {
        private readonly Predmet _subject;
        private readonly ObservableCollection<Predmet> _subjectCollection;

        public DeleteSubjectCommand(Predmet subject, ObservableCollection<Predmet> subjects)
        {
            _subject = subject;
            _subjectCollection = subjects;
        }
        public bool Execute()
        {
            if (_subject == null) return false;

            if (PredmetDataAccess.ObrisiPredmet(_subject.IdPredmeta))
            {
                _subjectCollection.Remove(_subject);
                return true;
            }
            return false;
        }
    }
}
