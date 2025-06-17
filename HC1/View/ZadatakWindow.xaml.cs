using ASystem;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Media;
using PrviProjektniZadatakHCI.ViewModel;
using System.Collections.ObjectModel;
using System.Diagnostics;

    namespace PrviProjektniZadatakHCI.View
    {
    public partial class ZadatakWindow : Window
    {
        public ZadatakViewModel ViewModel { get; }

        public ZadatakWindow(DomaciZadatak zadatak)
        {
            InitializeComponent();
            ViewModel = new ZadatakViewModel(zadatak);
            DataContext = ViewModel;
            ViewModel.WindowCloseRequest += (s, e) => this.Close();
        }

        public ZadatakWindow(DomaciZadatak zadatak, Profesor profesor, Stack<Action> undoStack,
                        ObservableCollection<DomaciZadatak> zadaci)
    {
        InitializeComponent(); 

      
        if (zadatak == null || profesor == null || undoStack == null || zadaci == null)
        {
            MessageBox.Show("Došlo je do greške pri inicijalizaciji prozora.", "Greška");
            Close();
            return;
        }

        ViewModel = new ZadatakViewModel(zadatak, profesor, undoStack, zadaci);
        DataContext = ViewModel;
        ViewModel.WindowCloseRequest += (s, e) => this.Close();
            Debug.WriteLine($"Inicijalizovan ViewModel za zadatak: {zadatak.naziv}");
        Debug.WriteLine($"Naziv: {zadatak.naziv}");
        Debug.WriteLine($"Opis: {zadatak.opis}");
        Debug.WriteLine($"Rok: {zadatak.rok}");
    }



    private List<DataGrid> FindAllDataGrids(DependencyObject parent)
        {
            var dataGrids = new List<DataGrid>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is DataGrid dataGrid)
                {
                    dataGrids.Add(dataGrid);
                }
                else
                {
                    dataGrids.AddRange(FindAllDataGrids(child));
                }
            }
            return dataGrids;
        }
        public void RefreshAllDataGrids()
        {
            var allDataGrids = FindAllDataGrids(this); 
            foreach (var dataGrid in allDataGrids)
            {
                dataGrid.InvalidateVisual();
                dataGrid.Items.Refresh(); 
            }
        }
    }
}
