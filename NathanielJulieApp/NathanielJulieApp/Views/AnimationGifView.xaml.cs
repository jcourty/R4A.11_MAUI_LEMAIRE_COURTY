using NathanielJulieApp.ViewModels;

namespace NathanielJulieApp.Views
{
    public partial class AnimationGifView : ContentPage
    {
        public AnimationGifView()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //pour attendre la fin de snoopy
            await Task.Delay(3000);

            // le bouton qui s'active
            returnButton.IsEnabled = true;
        }

        private async void OnReturnClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
