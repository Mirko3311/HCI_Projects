using ASystem;


namespace PrviProjektniZadatakHCI.Model
{
    public class StudentAttendance
    {
        public Student Student { get; set; }
        public bool IsPresent { get; set; }
        public DateTime Date { get; set; }
    }
}
