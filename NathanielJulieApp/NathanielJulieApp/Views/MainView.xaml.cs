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

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            if (BindingContext is MainViewModel viewModel)
            {
                viewModel.OnCounterClicked();
            }
        }
    }
}
