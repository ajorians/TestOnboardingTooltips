using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOnboardingTooltips.ViewModels
{
   public class OnboardingTipViewModel : INotifyPropertyChanged
   {
      public OnboardingTipViewModel( string onboardingText )
      {
         OnboardingText = onboardingText;
      }

      private string _onboardingText;
      public string OnboardingText
      {
         get => _onboardingText;
         set
         {
            if (_onboardingText == value)
               return;

            _onboardingText = value;
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof( OnboardingText ) ) );
         }
      }

      public event PropertyChangedEventHandler PropertyChanged;
   }
}
