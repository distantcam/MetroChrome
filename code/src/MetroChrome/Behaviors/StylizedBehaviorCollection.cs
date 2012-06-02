using System.Windows;
using System.Windows.Interactivity;

namespace MetroChrome.Behaviors
{
    public class StylizedBehaviorCollection : FreezableCollection<Behavior>
    {
        protected override Freezable CreateInstanceCore()
        {
            return new StylizedBehaviorCollection();
        }
    }
}