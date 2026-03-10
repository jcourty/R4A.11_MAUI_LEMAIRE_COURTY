using NathanielJulieApp.ViewModels;

namespace NathanielJulieApp.Views
{
    public partial class Onglet3View : ContentPage
    {
        public Onglet3View()
        {
            InitializeComponent();
            BindingContext = new Onglet3ViewModel();
        }
    }
}
