using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOnboardingTooltips.ViewModels
{
   public class ButtonViewModel : INotifyPropertyChanged
   {
      public ButtonViewModel(string buttonText )
      {
         _buttonText = buttonText;
      }


      private string _buttonText;
      public string ButtonText => _buttonText;

      public event PropertyChangedEventHandler PropertyChanged;
   }
}
