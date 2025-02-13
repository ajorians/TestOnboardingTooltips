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

      private bool _shortTooltips = false;
      public bool ShortTooltips
      {
         get => _shortTooltips;
         set
         {
            if (_shortTooltips == value)
               return;

            _shortTooltips = value;
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof( ShortTooltips ) ) );
         }
      }

      public event PropertyChangedEventHandler PropertyChanged;
   }
}
