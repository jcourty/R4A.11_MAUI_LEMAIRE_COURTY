using NathanielJulieApp.ViewModels;

namespace NathanielJulieApp.Views
{
    public partial class Onglet1View : ContentPage
    {
        public Onglet1View()
        {
            InitializeComponent();
            BindingContext = new Onglet1ViewModel();
        }
    }
}
