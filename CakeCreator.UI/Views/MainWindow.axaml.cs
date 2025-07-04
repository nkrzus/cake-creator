using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Collections.ObjectModel;
using System.Diagnostics;
namespace CakeCreator.UI.Views
{
    public partial class MainWindow : Window
    {
        public string SelectedItem { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            // Dodajemy przyk³adowe dane
           
        }
    }
}