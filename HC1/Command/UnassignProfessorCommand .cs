using ASystem;
using PrviProjektniZadatakHCI.DataAccess;


namespace PrviProjektniZadatakHCI.Command
{
    public class UnassignProfessorCommand
    {
        private readonly Profesor _profesor;
        private readonly Predmet _predmet;

        public UnassignProfessorCommand(Profesor profesor, Predmet predmet)
        {
            _profesor = profesor;
            _predmet = predmet;
        }

        public bool Execute()
        {
            return PredmetDataAccess.razduzi(_profesor, _predmet);
        }
    }
}
