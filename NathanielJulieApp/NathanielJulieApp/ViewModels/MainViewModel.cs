using NathanielJulieApp.Core;

namespace NathanielJulieApp.ViewModels
{
    /// <summary>
    /// ViewModel pour la page d'accueil
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private int clickCount = 0;
        private string counterButtonText = "Click me";

        public string CounterButtonText
        {
            get => counterButtonText;
            set => SetProperty(ref counterButtonText, value);
        }

        public MainViewModel()
        {
        }

        public void OnCounterClicked()
        {
            clickCount++;

            if (clickCount == 1)
                CounterButtonText = $"Clicked {clickCount} time";
            else
                CounterButtonText = $"Clicked {clickCount} times";

            SemanticScreenReader.Announce(CounterButtonText);
        }
    }
}
