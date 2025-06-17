

namespace ASystem
{
    public class Ispit 
    {
       
        public double bodovi { get; set; }
        public int ocjena { get; set; }
        public DateTime datumIspita { get; set; }
        public Student Student { get; set; }
        public int studentId { get; set; }
        public int predmetId { get; set; }

        public Ispit(Student student, double bodovii, int ocjena, DateTime datum)
        {
            Student = student;
            bodovi = bodovii;
            datumIspita = datum;
        }
        public Ispit() { }


    }
}