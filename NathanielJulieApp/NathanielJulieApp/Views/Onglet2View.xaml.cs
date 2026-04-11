using NathanielJulieApp.ViewModels;

namespace NathanielJulieApp.Views
{
    public partial class Onglet2View : ContentPage
    {
        public Onglet2View(Onglet2ViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
