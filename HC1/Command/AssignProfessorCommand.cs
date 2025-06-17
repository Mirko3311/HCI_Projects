using ASystem;
using PrviProjektniZadatakHCI.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrviProjektniZadatakHCI.Command
{
    public class AssignProfessorCommand
    {
        private readonly Profesor _profesor;
        private readonly Predmet _predmet;

        public AssignProfessorCommand(Profesor profesor, Predmet predmet)
        {
            _profesor = profesor;
            _predmet = predmet;
        }

        public bool Execute()
        {


            if (PredmetDataAccess.profesorPredmet(_profesor, _predmet))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
