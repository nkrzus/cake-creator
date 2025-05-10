using System.Collections.ObjectModel;

namespace CakeCreator.UI.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public string Greeting { get; } = "Welcome to CakeCreator! ";

        public ObservableCollection<string> Items { get; set; } = new ObservableCollection<string>();

        public string SelectedItem { get; set; }

        public MainWindowViewModel()
        {
            for (int i = 1; i <= 50; i++)
            {
                Items.Add($"Element {i}");
            }
        }
    }
}
