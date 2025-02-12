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
         OnboardingVM = new OnboardingViewModel();

         Button1VM = new ButtonViewModel( "Tooltip only" );
         Button2VM = new ButtonViewModel( "Tooltip only" );
         Button3VM = new ButtonViewModel( "Tooltip only" );
      }

      public ButtonViewModel Button1VM { get; private set; }
      public ButtonViewModel Button2VM { get; private set; }
      public ButtonViewModel Button3VM { get; private set; }

      public OnboardingViewModel OnboardingVM { get; }

      public event PropertyChangedEventHandler PropertyChanged;
   }
}
