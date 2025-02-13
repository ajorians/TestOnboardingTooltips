using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOnboardingTooltips.ViewModels
{
   public class ToolTipViewModel : INotifyPropertyChanged
   {
      public ToolTipViewModel( /*string extendedTipText,*/ string tooltipText )
      {
         //ExtendedTipText = extendedTipText;
         TooltipText = tooltipText;
      }

      //private string _extendedTipText;
      //public string ExtendedTipText
      //{
      //   get => _extendedTipText;
      //   set
      //   {
      //      if (_extendedTipText == value)
      //         return;

      //      _extendedTipText = value;
      //      PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof( ExtendedTipText ) ) );
      //   }
      //}

      private string _tooltipText;
      public string TooltipText
      {
         get => _tooltipText;
         set
         {
            if (_tooltipText == value)
               return;

            _tooltipText = value;
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof( TooltipText ) ) );
         }
      }

      public event PropertyChangedEventHandler PropertyChanged;
   }
}
