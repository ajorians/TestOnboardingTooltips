using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestOnboardingTooltips.ViewModels
{
   public class ButtonViewModel : INotifyPropertyChanged
   {
      public ButtonViewModel( OnboardingViewModel onboardingViewModel, string buttonText )
      {
         _onboardingViewModel = onboardingViewModel;
         _buttonText = buttonText;

         //Might use EventAggregator or a state/model class, the M in MVVM.
         _onboardingViewModel.PropertyChanged += OnboardingViewModelPropertyChanged;
      }

      private void OnboardingViewModelPropertyChanged( object sender, PropertyChangedEventArgs e )
      {
         PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof( ActiveToolTipVM ) ) );
      }

      private OnboardingViewModel _onboardingViewModel;
      private string _buttonText;
      public string ButtonText => _buttonText;

      private ToolTipViewModel _tooltipVM;
      public ToolTipViewModel ToolTipVM
      {
         get => _tooltipVM;
         set
         {
            if (_tooltipVM == value)
               return;

            _tooltipVM = value;
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof( ToolTipVM ) ) );
         }
      }

      private OnboardingTipViewModel _onboadingtipVM;
      public OnboardingTipViewModel OnboardingTipVM
      {
         get => _onboadingtipVM;
         set
         {
            if (_onboadingtipVM == value)
               return;

            _onboadingtipVM = value;
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof( OnboardingTipVM ) ) );
         }
      }

      public INotifyPropertyChanged ActiveToolTipVM
      {
         get
         {
            if( _onboardingViewModel.OnboardingEnabled && OnboardingTipVM != null)
            {
               return OnboardingTipVM;
            }
            else
            {
               return ToolTipVM;
            }
         }
      }

      private bool _showOriginalTooltip = false;
      public bool ShowOriginalTooltip
      {
         get => _showOriginalTooltip;
         set
         {
            if (_showOriginalTooltip == value)
               return;

            _showOriginalTooltip= value;
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof( ShowOriginalTooltip ) ) );
         }
      }

      public bool HasOnboardingTooltip => _onboadingtipVM != null;

      public event PropertyChangedEventHandler PropertyChanged;
   }
}
