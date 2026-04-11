using NathanielJulieApp.Core;
using NathanielJulieApp.Models;
using NathanielJulieApp.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

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

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get => _imageUrl;
            set => SetProperty(ref _imageUrl, value);
        }

        public ICommand PickImageCommand { get; }
        public ICommand AddItemCommand { get; }

        public Onglet2ViewModel(SharedDataService sharedDataService)
        {
            _sharedDataService = sharedDataService;
            AddItemCommand = new Command(OnAddItem);
            PickImageCommand = new Command(async () => await OnPickImageAsync());
        }

        private async Task OnPickImageAsync()
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Sélectionnez une image",
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                    ImageUrl = result.FullPath;
                }
            }
            catch (Exception ex)
            {
                // Utilisateur a annulé ou erreur de permission
                System.Diagnostics.Debug.WriteLine($"Erreur au choix de l'image : {ex.Message}");
            }
        }

        private void OnAddItem()
        {
            if (!string.IsNullOrWhiteSpace(Titre))
            {
                var newItem = new Airport
                {
                    Name = Titre,
                    IcaoCode = "PERSO",
                    Coordinates = Description,
                    Elevation = "N/A",
                    MapImage = ImageSource.FromFile(string.IsNullOrWhiteSpace(ImageUrl) ? "dotnet_bot.png" : ImageUrl)
                };

                _sharedDataService.AddItem(newItem);

                Titre = string.Empty;
                Description = string.Empty;
                ImageUrl = string.Empty;
            }
        }
    }
}
