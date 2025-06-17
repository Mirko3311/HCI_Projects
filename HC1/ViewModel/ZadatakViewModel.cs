
using global::ASystem;
using global::PrviProjektniZadatakHCI.DataAccess;
using PrviProjektniZadatakHCI.Util;
using PrviProjektniZadatakHCI.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace PrviProjektniZadatakHCI.ViewModel
{
    public class ZadatakViewModel : INotifyPropertyChanged
    {
        private DomaciZadatak _zadatak;
        private Profesor _profesor;
        private DomaciZadatak _originalniZadatak;
        private Stack<Action> _undoStack;
        private ObservableCollection<DomaciZadatak> _zadaci;
        public bool IsProfesor { get; }
        public event Action<DomaciZadatak> ZadatakObrisan;
        public event EventHandler WindowCloseRequest;
        public event PropertyChangedEventHandler PropertyChanged;

        public ZadatakViewModel(DomaciZadatak zadatak, Profesor profesor, Stack<Action> undoStack, ObservableCollection<DomaciZadatak> zadaci)
        {
            _zadatak = zadatak ?? throw new ArgumentNullException(nameof(zadatak));
            _profesor = profesor ?? throw new ArgumentNullException(nameof(profesor));
            _undoStack = undoStack ?? new Stack<Action>();
            _zadaci = zadaci ?? throw new ArgumentNullException(nameof(zadaci));
            _originalniZadatak = zadatak.DeepCopy();
            IsProfesor = true;

            BrisiCommand = new RelayCommand(BrisiZadatak);
            AzurirajCommand = new RelayCommand(AzurirajZadatak);
        }

        public ZadatakViewModel(DomaciZadatak zadatak)
        {
            _zadatak = zadatak ?? throw new ArgumentNullException(nameof(zadatak));
            _originalniZadatak = zadatak.DeepCopy();
            IsProfesor = false;
        }
        public string Naziv
        {
            get => _zadatak.naziv;
            set
            {
                if (_zadatak.naziv != value)
                {
                    _zadatak.naziv = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Opis
        {
            get => _zadatak.opis;
            set
            {
                if (_zadatak.opis != value)
                {
                    _zadatak.opis = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime? Rok
        {
            get => _zadatak.rok;
            set
            {
                var newValue = value ?? DateTime.Today;
                if (_zadatak.rok != newValue)
                {
                    _zadatak.rok = newValue;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand BrisiCommand { get; }
        public ICommand AzurirajCommand { get; }

        private void BrisiZadatak()
        {

            var confirmDialog = new ConfirmWindow(
             (string)Application.Current.Resources["DeleteConfirmation"]
         );

            bool? result = confirmDialog.ShowDialog();

            if (result != true)
                return;
            if (DomaciZadatakDataAccess.obrisiZadatak(_zadatak))
            {
                _zadaci.Remove(_zadatak);
                string message = (string)Application.Current.Resources["SuccessfullyDelete"];
                new SuccessWindow(message).ShowDialog();

                WindowCloseRequest?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Greška pri brisanju zadatka!", "Greška",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AzurirajZadatak()
        {
            if (DomaciZadatakDataAccess.azurirajDZadatak(_zadatak, _profesor))
            {
                string message = (string)Application.Current.Resources["SuccessfullyUpdated"];
                new SuccessWindow(message).ShowDialog();


                WindowCloseRequest?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Greška pri ažuriranju zadatka!", "Greška",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
