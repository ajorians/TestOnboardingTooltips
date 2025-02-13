using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestOnboardingTooltips.UI
{
   /// <summary>
   /// Interaction logic for TooltipPopup.xaml
   /// </summary>
   public partial class TooltipPopup : Popup
   {
      static TooltipPopup()
      {
         PlacementTargetProperty.OverrideMetadata( typeof( TooltipPopup ), new FrameworkPropertyMetadata( OnDelayOrTargetChanged ) );
      }

      public TooltipPopup()
      {
         InitializeComponent();
      }

      public static readonly DependencyProperty ShowOriginalToolTipProperty =
         DependencyProperty.Register( nameof( ShowOriginalToolTip ),
                                      typeof( bool ),
                                      typeof( TooltipPopup ),
                                      new PropertyMetadata( false ) );

      public bool ShowOriginalToolTip
      {
         get => (bool)GetValue( ShowOriginalToolTipProperty );
         set => SetValue( ShowOriginalToolTipProperty, value );
      }

      public static readonly DependencyProperty InitialShowDelayMSProperty =
         DependencyProperty.Register( nameof( InitialShowDelayMS ),
                                 typeof( int ),
                                 typeof( TooltipPopup ),
                                 new PropertyMetadata( 1000, OnDelayOrTargetChanged ) );

      public int InitialShowDelayMS
      {
         get => (int)GetValue( InitialShowDelayMSProperty );
         set => SetValue( InitialShowDelayMSProperty, value );
      }

      private static void OnDelayOrTargetChanged( DependencyObject obj, DependencyPropertyChangedEventArgs e )
      {
         var toolTipPopup = (TooltipPopup)obj;
         toolTipPopup.UpdateDelay();
      }

      public void UpdateDelay()
      {
         if (PlacementTarget == null)
         {
            return;
         }

         ToolTipService.SetInitialShowDelay( PlacementTarget, InitialShowDelayMS );
      }
   }
}
