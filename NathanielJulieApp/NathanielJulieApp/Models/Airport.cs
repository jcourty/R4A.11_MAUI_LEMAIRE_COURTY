namespace NathanielJulieApp.Models
{
    /// <summary>
    /// Modèle représentant un aéroport
    /// </summary>
    public class Airport
    {
        public string Name { get; set; } = string.Empty;
        public string IcaoCode { get; set; } = string.Empty;
        public string Elevation { get; set; } = string.Empty;
        public string Coordinates { get; set; } = string.Empty;
        public ImageSource MapImage { get; set; }
    }
}
