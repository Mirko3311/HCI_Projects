

using PrviProjektniZadatakHCI.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace ASystem
{
    public partial class StudentWindow : Window
    {
    
        private Student student;
       public StudentWindow(Student student)
        {
            this.student = student;
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            this.DataContext = new StudentViewModel(student);

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
        /*  public void RefreshAllDataGrids()
          {
              var allDataGrids = FindAllDataGrids(this); 
              foreach (var dataGrid in allDataGrids)
              {
                  dataGrid.InvalidateVisual();
                  dataGrid.Items.Refresh();
              }
          }*/
        public void RefreshDataGridHeaders()
        {
            var allDataGrids = FindAllDataGrids(this);
            foreach (var dataGrid in allDataGrids)
            {
                foreach (var column in dataGrid.Columns)
                {
                    if (column is DataGridTextColumn textColumn)
                    {
                        var dynamicKey = (string)(textColumn.Header as string);
                        if (!string.IsNullOrEmpty(dynamicKey))
                        {

                            textColumn.Header = Application.Current.TryFindResource(dynamicKey);
                        }
                    }
                    else if (column is DataGridTemplateColumn templateColumn)
                    {
                        var dynamicKey = (string)(templateColumn.Header as string);
                        if (!string.IsNullOrEmpty(dynamicKey))
                        {
                            templateColumn.Header = Application.Current.TryFindResource(dynamicKey);
                        }
                    }
                }
            }
        }
   
    
    }
}

