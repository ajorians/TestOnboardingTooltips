using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOnboardingTooltips.ViewModels
{
   public class OnboardingViewModel : INotifyPropertyChanged
   {
      private bool _onboardingEnabled;
      public bool OnboardingEnabled
      {
         get => _onboardingEnabled;
         set
         {
            if (_onboardingEnabled == value)
               return;

            _onboardingEnabled = value;
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof( OnboardingEnabled ) ) );
         }
      }

      public event PropertyChangedEventHandler PropertyChanged;
   }
}
