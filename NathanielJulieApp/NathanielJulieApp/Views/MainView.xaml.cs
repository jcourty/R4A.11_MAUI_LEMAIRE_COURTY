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
        private async void OnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AnimationGifView());
        }


    }
}
