using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOnboardingTooltips.ViewModels
{
   public class MainViewModel : INotifyPropertyChanged
   {
      public MainViewModel()
      {
         ButtonVM = new ButtonViewModel( "Tooltip only" );
      }

      public ButtonViewModel ButtonVM { get; private set; }

      public event PropertyChangedEventHandler PropertyChanged;
   }
}
