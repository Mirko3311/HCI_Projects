using ASystem;
using System.Windows;

namespace PrviProjektniZadatakHCI.Util
{
    public class EntityTypeDisplay
    {
        public EntityType Type { get; }
        public string DisplayText => (string)Application.Current.Resources[$"{Type}Text"];

        public EntityTypeDisplay(EntityType type)
        {
            Type = type;
        }
    }


}
