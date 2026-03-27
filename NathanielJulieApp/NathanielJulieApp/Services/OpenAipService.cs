using System;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace NathanielJulieApp.Services
{
    public class OpenAipService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string ApiKey = "a1b6b50f9b92a3f8bd70f28c14b3ebb1";

        public async Task<(string Nom, string Icao, string Altitude, string Coordonnees)?> GetAirportInfoAsync(string search)
        {
            try
            {
                string infosUrl = $"https://api.core.openaip.net/api/airports?search={search}&apiKey={ApiKey}";
                System.Diagnostics.Debug.WriteLine($"Appel API: {infosUrl}");

                var jsonResponse = await _httpClient.GetStringAsync(infosUrl);
                System.Diagnostics.Debug.WriteLine($"Réponse API brute: {jsonResponse}");

                var jsonNode = JsonNode.Parse(jsonResponse);
                var items = jsonNode?["items"]?.AsArray();

                if (items != null && items.Count > 0)
                {
                    var airport = items[0];
                    string nom = airport["name"]?.ToString();
                    string icao = airport["icaoCode"]?.ToString();
                    string altitude = airport["elevation"]?["value"]?.ToString();
                    var coords = airport["geometry"]?["coordinates"]?.AsArray();

                    string coordonneesStr = null;
                    if (coords != null && coords.Count >= 2)
                        coordonneesStr = $"Longitude {coords[0]}, Latitude {coords[1]}";

                    System.Diagnostics.Debug.WriteLine($"Données parsées - Nom: {nom}, ICAO: {icao}, Alt: {altitude}");
                    return (nom, icao, altitude, coordonneesStr);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Aucun item trouvé dans la réponse");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de l'appel à l'API Core : {ex.Message}\n{ex.StackTrace}");
            }
            return null;
        }

        public async Task<byte[]> GetMapTileAsync(int z, int x, int y)
        {
            try
            {
                string openAipUrl = $"https://a.api.tiles.openaip.net/api/data/openaip/{z}/{x}/{y}.png?apiKey={ApiKey}";
                return await _httpClient.GetByteArrayAsync(openAipUrl);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors du téléchargement de la tuile : {ex.Message}");
                return null;
            }
        }
    }
}