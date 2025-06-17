using ASystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using PrviProjektniZadatakHCI.ViewModel;

namespace PrviProjektniZadatakHCI.Util
{
    /*  public class UpdateTemplateSelector : DataTemplateSelector
      {
          public DataTemplate StudentTemplate { get; set; }
          public DataTemplate ProfessorTemplate { get; set; }
          public DataTemplate SubjectTemplate { get; set; }

          public override DataTemplate SelectTemplate(object item, DependencyObject container)
          {
              if (item is Student)
                  return StudentTemplate;

              if (item is Profesor)
                  return ProfessorTemplate;

              if (item is Predmet)
                  return SubjectTemplate;

              return base.SelectTemplate(item, container);
          }
      } */

    public class UpdateTemplateSelector : DataTemplateSelector
    {
        public DataTemplate StudentTemplate { get; set; }
        public DataTemplate ProfessorTemplate { get; set; }
        public DataTemplate SubjectTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is UpdateStudentViewModel)
                return StudentTemplate;
            if (item is UpdateProfessorViewModel)
                return ProfessorTemplate;
            if (item is UpdateSubjectViewModel)
                return SubjectTemplate;

            return base.SelectTemplate(item, container);
        }
    }

}
