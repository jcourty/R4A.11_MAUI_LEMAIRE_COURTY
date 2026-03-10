namespace NathanielJulieApp.Models
{
    /// <summary>
    /// Modèle représentant un résultat de recherche
    /// </summary>
    public class SearchResult
    {
        public string Query { get; set; } = string.Empty;
        public string ResultText { get; set; } = string.Empty;
        public DateTime SearchDateTime { get; set; } = DateTime.Now;
    }
}
