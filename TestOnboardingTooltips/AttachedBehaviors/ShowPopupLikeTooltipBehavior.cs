using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Xaml.Behaviors;
using TestOnboardingTooltips.UI;
using TestOnboardingTooltips.ViewModels;

namespace TestOnboardingTooltips.AttachedBehaviors
{
   public class ShowPopupLikeTooltipBehavior : Behavior<TooltipPopup>
   {
      private DispatcherTimer _CloseOnLeaveTimer;
      private DispatcherTimer _AutoCloseTimer;

      public static readonly DependencyProperty PopupTargetProperty =
         DependencyProperty.Register( nameof( PopupTarget ),
                                      typeof( UIElement ),
                                      typeof( ShowPopupLikeTooltipBehavior ),
                                      new FrameworkPropertyMetadata( null, OnPopupTargetChanged ) );

      public UIElement PopupTarget
      {
         get => (UIElement)GetValue( PopupTargetProperty );
         set => SetValue( PopupTargetProperty, value );
      }

      private static void OnPopupTargetChanged( DependencyObject obj, DependencyPropertyChangedEventArgs e )
      {
         var thisPopupOpenBehavior = (ShowPopupLikeTooltipBehavior)obj;
         thisPopupOpenBehavior.DetachPopupTargetHandlers( (FrameworkElement)e.OldValue );
         thisPopupOpenBehavior.AttachPopupTargetHandlers( (FrameworkElement)e.NewValue );
      }

      public void AttachPopupTargetHandlers( UIElement target )
      {
         if (target != null)
         {
            target.AddHandler( UIElement.MouseDownEvent, new MouseButtonEventHandler( OnPopupTargetMouseDown ), true );

            var targetAsFE = target as FrameworkElement;
            targetAsFE.ToolTipOpening += OnPopupTargetTooltipOpening;
            targetAsFE.ToolTipClosing += OnPopupTargetTooltipClosing;

            //_toolTipPropertyNotifier = new PropertyChangeNotifier( target, ToolTipService.ToolTipProperty );
            //_toolTipPropertyNotifier.ValueChanged += OnTargetToolTipChanged;

            AssignTooltipAsElement( target );
         }
      }

      private void OnPopupTargetMouseDown( object sender, MouseButtonEventArgs e )
      {
         _CloseOnLeaveTimer.Stop();
         _AutoCloseTimer.Stop();
         AssociatedObject.IsOpen = false;
      }

      private void AssignTooltipAsElement( UIElement target )
      {
         // If the target's ToolTip is not an actual visual element (aka just assigned to a string in the xaml)
         // Create a visual for it so that we can still receive opening/closing events but hide the control when we show our popup
         var targetAsFE = target as FrameworkElement;
         var targetTooltip = targetAsFE.ToolTip as ToolTip;
         if (targetAsFE.ToolTip != null && targetTooltip == null)
         {
            BindingExpression tooltipBinding = targetAsFE.GetBindingExpression( FrameworkElement.ToolTipProperty );
            ToolTip visualElemToolTip = new ToolTip()
            {
               Content = targetAsFE.ToolTip
            };
            if (tooltipBinding != null)
            {
               visualElemToolTip.SetBinding( ContentControl.ContentProperty, tooltipBinding.ParentBinding );
            }

            targetAsFE.ToolTip = visualElemToolTip;
         }
      }

      public void DetachPopupTargetHandlers( UIElement target )
      {
         if (target != null)
         {
            target.RemoveHandler( UIElement.MouseDownEvent, new MouseButtonEventHandler( OnPopupTargetMouseDown ) );

            //if (_toolTipPropertyNotifier != null)
            //{
            //   _toolTipPropertyNotifier.ValueChanged -= OnTargetToolTipChanged;
            //   _toolTipPropertyNotifier.Dispose();
            //}

            var targetAsFE = target as FrameworkElement;
            targetAsFE.ToolTipOpening -= OnPopupTargetTooltipOpening;
            targetAsFE.ToolTipClosing -= OnPopupTargetTooltipClosing;
         }
      }

      protected override void OnAttached()
      {
         _CloseOnLeaveTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds( 2 ) };
         _CloseOnLeaveTimer.Tick += OnCloseOnLeaveTimerTick;

         _AutoCloseTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds( 2 ) };
         _AutoCloseTimer.Tick += OnAutoCloseTimerTick;
      }

      private void OnCloseOnLeaveTimerTick( object sender, EventArgs e )
      {
         if (AssociatedObject.IsMouseOver || PopupTarget.IsMouseOver)
         {
            return;
         }

         _CloseOnLeaveTimer.Stop();
         _AutoCloseTimer.Stop();
         AssociatedObject.IsOpen = false;
      }

      private void OnAutoCloseTimerTick( object sender, EventArgs e )
      {
         if (AssociatedObject.IsMouseOver)
         {
            return;
         }

         _CloseOnLeaveTimer.Stop();
         _AutoCloseTimer.Stop();
         AssociatedObject.IsOpen = false;
      }

      private void ChangePopupTargetToolTipVisibility( Visibility newVisibility )
      {
         var popupTargetAsFE = PopupTarget as FrameworkElement;
         var targetTooltip = popupTargetAsFE.ToolTip as ToolTip;
         if (targetTooltip != null)
         {
            targetTooltip.Visibility = newVisibility;
         }
      }

      private bool ShowShortTooltips
      {
         get
         {
            var parentWindow = Window.GetWindow( PopupTarget );
            var mainViewModel = parentWindow.DataContext as MainViewModel;
            return mainViewModel.ShortTooltips;
         }
      }

      private bool ShowOnboardingTooltips
      {
         get
         {
            var parentWindow = Window.GetWindow( PopupTarget );
            var mainViewModel = parentWindow.DataContext as MainViewModel;
            return mainViewModel.OnboardingVM.OnboardingEnabled;
         }
      }

      private void OnPopupTargetTooltipOpening( object sender, ToolTipEventArgs e )
      {
         //if (ShouldIgnorePopupOpening())
         //{
         //   e.Handled = true;
         //   return;
         //}

         bool onboardingEnabledAndHasOnbordingTip = ShowOnboardingTooltips && AssociatedObject.HasOnboardingTip;

         if ( !onboardingEnabledAndHasOnbordingTip && (AssociatedObject.ShowOriginalToolTip || ShowShortTooltips ) )
         {
            // Make sure the PopupTarget's original tooltip will be visible
            ChangePopupTargetToolTipVisibility( Visibility.Visible );
            return;
         }

         // Hide the PopupTarget's tooltip since we may be showing our custom popup tooltip
         ChangePopupTargetToolTipVisibility( Visibility.Collapsed );

         //WPFCommonControls.AttachedBehaviors.PopupStaysOpenOnClickBehavior.ClosePopupsIfNecessary();
         AssociatedObject.IsOpen = true;
         _AutoCloseTimer.Start();
      }

      private void OnPopupTargetTooltipClosing( object sender, ToolTipEventArgs e )
      {
         _CloseOnLeaveTimer.Start();
         e.Handled = true;
      }

   }
}
