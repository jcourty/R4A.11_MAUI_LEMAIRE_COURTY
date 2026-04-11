using NathanielJulieApp.Core;
using NathanielJulieApp.Models;
using NathanielJulieApp.Services;
using System;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace NathanielJulieApp.ViewModels
{
    public class Onglet2ViewModel : ViewModelBase
    {
        private readonly SharedDataService _sharedDataService;

        private string _titre;
        public string Titre
        {
            get => _titre;
            set => SetProperty(ref _titre, value);
        }

        private string _icaoCode;
        public string IcaoCode
        {
            get => _icaoCode;
            set => SetProperty(ref _icaoCode, value);
        }

        private string _longitude;
        public string Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }

        private string _latitude;
        public string Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }

        private string _elevation;
        public string Elevation
        {
            get => _elevation;
            set => SetProperty(ref _elevation, value);
        }

        public ICommand AddItemCommand { get; }

        public Onglet2ViewModel(SharedDataService sharedDataService)
        {
            _sharedDataService = sharedDataService;
            AddItemCommand = new Command(OnAddItem);
        }

        private void OnAddItem()
        {
            if (!string.IsNullOrWhiteSpace(Titre))
            {
                // Valider les coordonnées
                if (!double.TryParse(Longitude, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double lon) ||
                    !double.TryParse(Latitude, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double lat))
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await Application.Current?.MainPage?.DisplayAlert("Erreur", "Veuillez entrer des coordonnées valides", "OK");
                    });
                    return;
                }

                var newItem = new Airport
                {
                    Name = Titre,
                    IcaoCode = string.IsNullOrWhiteSpace(IcaoCode) ? "PERSO" : IcaoCode,
                    Coordinates = $"Longitude {lon}, Latitude {lat}",
                    Elevation = string.IsNullOrWhiteSpace(Elevation) ? "N/A" : Elevation,
                    MapImage = ImageSource.FromFile("dotnet_bot.png")
                };

                _sharedDataService.AddItem(newItem);

                Titre = string.Empty;
                IcaoCode = string.Empty;
                Longitude = string.Empty;
                Latitude = string.Empty;
                Elevation = string.Empty;

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current?.MainPage?.DisplayAlert("Succès", "Élément ajouté avec succès!", "OK");
                });
            }
            else
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current?.MainPage?.DisplayAlert("Erreur", "Veuillez entrer un titre", "OK");
                });
            }
        }
    }
}
