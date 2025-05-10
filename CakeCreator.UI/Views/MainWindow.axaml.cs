using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Collections.ObjectModel;
using System.Diagnostics;
namespace CakeCreator.UI.Views
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> Items { get; set; } = new ObservableCollection<string>();
        public string SelectedItem { get; set; }
        public MainWindow()
        {
            //InitializeComponent();
            // cakeIngredients.ItemsSource = new string[]
            //      {"cat", "camel", "cow", "chameleon", "mouse", "lion", "zebra" }
            //    .OrderBy(x => x);


            InitializeComponent();
            DataContext = this;

            // Dodajemy przyk³adowe dane
           
        }

        private void Button_OnClick(object? sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Click!");
        }
    }
}