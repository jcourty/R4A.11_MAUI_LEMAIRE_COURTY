using NathanielJulieApp.Core;
using NathanielJulieApp.Models;
using NathanielJulieApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace NathanielJulieApp.ViewModels
{
    public class Onglet1ViewModel : ViewModelBase
    {
        private ObservableCollection<Airport> _airports;
        public ObservableCollection<Airport> Airports
        {
            get => _airports;
            set => SetProperty(ref _airports, value);
        }

        private Airport _selectedAirport;
        public Airport SelectedAirport
        {
            get => _selectedAirport;
            set
            {
                if (SetProperty(ref _selectedAirport, value))
                {
                    LoadAirportDetailsAsync();
                }
            }
        }

        private string _titre = "Sélectionner un aéroport";
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

        private string _description = "Cliquez sur un aéroport pour voir les détails";
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public ICommand SelectAirportCommand { get; }

        public Onglet1ViewModel()
        {
            Airports = new ObservableCollection<Airport>();
            SelectAirportCommand = new Command<Airport>(async (airport) => await SelectAirportAsync(airport));
            _ = LoadAirportsAsync();
        }

        private async Task SelectAirportAsync(Airport airport)
        {
            if (airport == null)
                return;

            System.Diagnostics.Debug.WriteLine($"Sélection de l'aéroport: {airport.Name}");
            SelectedAirport = airport;
        }

        private async Task LoadAirportsAsync()
        {
            try
            {
                var service = new OpenAipService();

                // Liste de quelques aéroports majeurs français
                var airportCodes = new[] { "LFPG", "LFBO", "LFLL", "LFML", "LFPB" };

                int successCount = 0;
                foreach (var code in airportCodes)
                {
                    var airportInfo = await service.GetAirportInfoAsync(code);
                    System.Diagnostics.Debug.WriteLine($"API - Résultat pour {code}: {(airportInfo.HasValue ? "TROUVÉ" : "NON TROUVÉ")}");

                    if (airportInfo.HasValue)
                    {
                        var info = airportInfo.Value;
                        var airport = new Airport
                        {
                            Name = !string.IsNullOrEmpty(info.Nom) ? info.Nom : code,
                            IcaoCode = info.Icao ?? code,
                            Elevation = !string.IsNullOrEmpty(info.Altitude) ? $"{info.Altitude} m" : "N/A",
                            Coordinates = info.Coordonnees ?? "N/A",
                            MapImage = ImageSource.FromFile("dotnet_bot.png")
                        };

                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            Airports.Add(airport);
                            System.Diagnostics.Debug.WriteLine($"✓ Aéroport AJOUTÉ depuis l'API: {airport.Name}");
                        });
                        successCount++;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"✗ Aucune donnée trouvée pour {code} (API)");
                    }
                }

                System.Diagnostics.Debug.WriteLine($"=== API: {successCount} aéroports trouvés ===");

                // Si aucun aéroport n'a été chargé, charger les données de test
                if (successCount == 0)
                {
                    System.Diagnostics.Debug.WriteLine("=== FALLBACK: Chargement des DONNÉES DE TEST ===");
                    await LoadTestDataAsync();
                }

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    System.Diagnostics.Debug.WriteLine($"Total final: {Airports.Count} aéroports");
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Exception dans LoadAirportsAsync: {ex.Message}\n{ex.StackTrace}");
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current?.MainPage?.DisplayAlert("Erreur", $"Erreur lors du chargement : {ex.Message}", "OK");
                });
            }
        }

        private async Task LoadTestDataAsync()
        {
            // Données de test en cas d'indisponibilité de l'API
            var testAirports = new[]
            {
                new Airport
                {
                    Name = "Aéroport de Paris-Charles de Gaulle",
                    IcaoCode = "LFPG",
                    Elevation = "119 m",
                    Coordinates = "Longitude 2.5469, Latitude 49.0127",
                    MapImage = ImageSource.FromFile("dotnet_bot.png")
                },
                new Airport
                {
                    Name = "Aéroport de Bordeaux-Mérignac",
                    IcaoCode = "LFBO",
                    Elevation = "48 m",
                    Coordinates = "Longitude -0.6836, Latitude 44.8283",
                    MapImage = ImageSource.FromFile("dotnet_bot.png")
                },
                new Airport
                {
                    Name = "Aéroport de Lyon-Saint Exupéry",
                    IcaoCode = "LFLL",
                    Elevation = "249 m",
                    Coordinates = "Longitude 5.0919, Latitude 45.7254",
                    MapImage = ImageSource.FromFile("dotnet_bot.png")
                },
                new Airport
                {
                    Name = "Aéroport de Marseille Provence",
                    IcaoCode = "LFML",
                    Elevation = "75 m",
                    Coordinates = "Longitude 5.2163, Latitude 43.4426",
                    MapImage = ImageSource.FromFile("dotnet_bot.png")
                },
                new Airport
                {
                    Name = "Aéroport de Paris Le Bourget",
                    IcaoCode = "LFPB",
                    Elevation = "59 m",
                    Coordinates = "Longitude 2.4472, Latitude 48.9697",
                    MapImage = ImageSource.FromFile("dotnet_bot.png")
                }
            };

            foreach (var airport in testAirports)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Airports.Add(airport);
                });
                await Task.Delay(100);
            }
        }

        private async Task LoadAirportDetailsAsync()
        {
            if (SelectedAirport == null)
            {
                System.Diagnostics.Debug.WriteLine("LoadAirportDetailsAsync: SelectedAirport est null");
                return;
            }

            System.Diagnostics.Debug.WriteLine($"LoadAirportDetailsAsync: Affichage des détails pour {SelectedAirport.Name}");

            try
            {
                Titre = SelectedAirport.Name;
                Description = $"Code ICAO : {SelectedAirport.IcaoCode}\n" +
                             $"Altitude : {SelectedAirport.Elevation}\n" +
                             $"Coordonnées : {SelectedAirport.Coordinates}";
                Image = SelectedAirport.MapImage;

                System.Diagnostics.Debug.WriteLine($"Détails affichés: {SelectedAirport.Name}");

                // Charger l'image de carte
                var service = new OpenAipService();
                var mapTile = await service.GetMapTileAsync(12, 2077, 1406);
                if (mapTile != null)
                {
                    System.Diagnostics.Debug.WriteLine("Image de carte chargée depuis l'API");
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Image = ImageSource.FromStream(() => new System.IO.MemoryStream(mapTile));
                    });
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Aucune image de carte disponible");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur dans LoadAirportDetailsAsync: {ex.Message}");
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current?.MainPage?.DisplayAlert("Erreur", $"Erreur : {ex.Message}", "OK");
                });
            }
        }
    }
}
