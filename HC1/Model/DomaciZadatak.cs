
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    namespace ASystem
    {
        public class DomaciZadatak : INotifyPropertyChanged
        {
            private string _idDomaciZadatak;
            private string _naziv;
            private string _opis;
            private DateTime _rok;
            private int _idPredmeta;
            private int _idProfesora;
            private Predmet _predmet;

            public string idDomaciZadatak
            {
                get => _idDomaciZadatak;
                set
                {
                    if (_idDomaciZadatak != value)
                    {
                        _idDomaciZadatak = value;
                        OnPropertyChanged();
                    }
                }
            }

            public string naziv
            {
                get => _naziv;
                set
                {
                    if (_naziv != value)
                    {
                        _naziv = value;
                        OnPropertyChanged();
                    }
                }
            }

            public string opis
            {
                get => _opis;
                set
                {
                    if (_opis != value)
                    {
                        _opis = value;
                        OnPropertyChanged();
                    }
                }
            }

            public DateTime rok
            {
                get => _rok;
                set
                {
                    if (_rok != value)
                    {
                        _rok = value;
                        OnPropertyChanged();
                    }
                }
            }

            public int idPredmeta
            {
                get => _idPredmeta;
                set
                {
                    if (_idPredmeta != value)
                    {
                        _idPredmeta = value;
                        OnPropertyChanged();
                    }
                }
            }

            public int idProfesora
            {
                get => _idProfesora;
                set
                {
                    if (_idProfesora != value)
                    {
                        _idProfesora = value;
                        OnPropertyChanged();
                    }
                }
            }

            public Predmet predmet
            {
                get => _predmet;
                set
                {
                    if (_predmet != value)
                    {
                        _predmet = value;
                        OnPropertyChanged();
                    }
                }
            }

        private Profesor _profesor;
        public Profesor Profesor
        {
            get => _profesor;
            set
            {
                if (_profesor != value)
                {
                    _profesor = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ProfesorImePrezime));
                }
            }
        }

     
        public string ProfesorImePrezime => Profesor == null ? "" : $"{Profesor.ime} {Profesor.prezime}";


        public DomaciZadatak DeepCopy()
            {
                return (DomaciZadatak)this.MemberwiseClone();
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }



