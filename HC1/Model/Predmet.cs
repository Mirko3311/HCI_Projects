using System.ComponentModel;



namespace ASystem
{
        public class Predmet : INotifyPropertyChanged
        {
            private int idPredmeta;
            private string naziv;
            private string opis;
            private int eCTS;

            public event PropertyChangedEventHandler PropertyChanged;

            public int IdPredmeta
            {
                get => idPredmeta;
                set
                {
                    if (idPredmeta != value)
                    {
                        idPredmeta = value;
                        OnPropertyChanged(nameof(IdPredmeta));
                    }
                }
            }

            public string Naziv
            {
                get => naziv;
                set
                {
                    if (naziv != value)
                    {
                        naziv = value;
                        OnPropertyChanged(nameof(Naziv));
                    }
                }
            }

            public string Opis
            {
                get => opis;
                set
                {
                    if (opis != value)
                    {
                        opis = value;
                        OnPropertyChanged(nameof(Opis));
                    }
                }
            }

            public int ECTS
            {
                get => eCTS;
                set
                {
                    if (eCTS != value)
                    {
                        eCTS = value;
                        OnPropertyChanged(nameof(ECTS));
                    }
                }
            }

            public string DisplayText => Naziv;
            protected void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }


        public Predmet DeepCopy()
        {
            return (Predmet)this.MemberwiseClone();
        }
    }
}



