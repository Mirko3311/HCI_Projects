using ASystem;

namespace PrviProjektniZadatakHCI.ViewModel
{
    public class PrisustvoViewModel
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public bool IsPresent { get; set; }

        public Student Student { get; set; }
        public string DisplayText => $"{Ime} {Prezime}";

    }


}
