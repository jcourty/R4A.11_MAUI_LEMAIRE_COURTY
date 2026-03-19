using NathanielJulieApp.ViewModels;

namespace NathanielJulieApp.Views
{
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}
