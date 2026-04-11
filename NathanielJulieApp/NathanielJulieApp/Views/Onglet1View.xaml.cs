using NathanielJulieApp.ViewModels;

namespace NathanielJulieApp.Views
{
    public partial class Onglet1View : ContentPage
    {
        public Onglet1View(Onglet1ViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
