using NathanielJulieApp.Core;
using NathanielJulieApp.Models;
using NathanielJulieApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using Microsoft.Maui.Controls; // Remplacé Xamarin.Forms par Microsoft.Maui.Controls

namespace NathanielJulieApp.ViewModels
{
    public class Onglet1ViewModel : ViewModelBase
    {
        private string _titre;
        public string Titre
        {
            get => _titre;
            set => SetProperty(ref _titre, value);
        }

        private ImageSource _image;
        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public Onglet1ViewModel()
        {
            Titre = "Chargement...";
            Image = ImageSource.FromFile("dotnet_bot.png");
            Description = "Récupération des données OpenAIP...";

            _ = LoadOpenAIPDataAsync();
        }

        private async Task LoadOpenAIPDataAsync()
        {
            ImageSource newImageSource = null;
            string nouveauTitre = "Aéroport de Paris-Charles-de-Gaulle (LFPG)";
            string nouvelleDescription = "Le premier aéroport français par son importance...\nPistes : 4\nAltitude : 119m\nCoordonnées : 49° 00' 35'' N, 2° 32' 52'' E";

            var openAipService = new OpenAipService();

            var airportInfo = await openAipService.GetAirportInfoAsync("LFPG");
            if (airportInfo.HasValue)
            {
                var info = airportInfo.Value;
                if (!string.IsNullOrEmpty(info.Nom))
                    nouveauTitre = $"{info.Nom} ({info.Icao})";

                nouvelleDescription = "Données récupérées via l'API OpenAIP.\n";
                if (!string.IsNullOrEmpty(info.Altitude)) 
                    nouvelleDescription += $"Altitude : {info.Altitude} m\n";
                if (!string.IsNullOrEmpty(info.Coordonnees))
                    nouvelleDescription += $"Coordonnées : {info.Coordonnees}";
            }

            var imageBytes = await openAipService.GetMapTileAsync(12, 2077, 1406);
            if (imageBytes != null)
            {
                newImageSource = ImageSource.FromStream(() => new System.IO.MemoryStream(imageBytes));
            }
            else
            {
                newImageSource = ImageSource.FromFile("dotnet_bot.png");
            }

            Microsoft.Maui.ApplicationModel.MainThread.BeginInvokeOnMainThread(() =>
            {
                Titre = nouveauTitre;
                Image = newImageSource;
                Description = nouvelleDescription;
            });
        }
    }
}
