using System.Collections.ObjectModel;
using NathanielJulieApp.Models;

namespace NathanielJulieApp.Services
{
    public class SharedDataService
    {
        public ObservableCollection<Airport> Items { get; } = new ObservableCollection<Airport>();

        public void AddItem(Airport item)
        {
            if (item != null)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Items.Add(item);
                });
            }
        }
    }
}