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

         Button1VM = new ButtonViewModel( OnboardingVM, "Tool button w/o onboarding tip" )
         {
            ToolTipVM = new ToolTipViewModel( "This is my tooltip" ),
         };
         Button2VM = new ButtonViewModel( OnboardingVM, "Tool button with onboarding tip" )
         {
            ToolTipVM = new ToolTipViewModel( "This is my tooltip" ),
            OnboardingTipVM = new OnboardingTipViewModel( "Onboarding text here")
         };
         Button3VM = new ButtonViewModel( OnboardingVM, "Button with only tooltip" )
         {
            ToolTipVM = new ToolTipViewModel( "This is my tooltip" ),
            ShowOriginalTooltip = true
         };
         Button4VM = new ButtonViewModel( OnboardingVM, "Button with no tips" )
         {
         };
      }

      public ButtonViewModel Button1VM { get; private set; }
      public ButtonViewModel Button2VM { get; private set; }
      public ButtonViewModel Button3VM { get; private set; }
      public ButtonViewModel Button4VM { get; private set; }

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
