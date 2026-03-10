using NathanielJulieApp.ViewModels;

namespace NathanielJulieApp.Views
{
    public partial class Onglet2View : ContentPage
    {
        public Onglet2View()
        {
            InitializeComponent();
            BindingContext = new onglet2ViewModel();
        }
    }
}
