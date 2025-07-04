using CakeCreator.Database;
using CakeCreator.Database.Model;
using CakeCreator.Database.Model.Enums;
using CakeCreator.Services.Services;
using CakeCreator.Services.Services.Interfaces;

using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CakeCreator.UI.ViewModels
{
    public class CreateRecipePopupViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public CreateRecipePopupViewModel()
        {
            recipeService = new RecipeService();

            AddNewIngredientCommand = new RelayCommand(async () => await AddNewIngredient());
            ClosePopupCommand = new RelayCommand(ClosePopup);
            OpenPopupCommand = new RelayCommand(OpenPopup);
            AddNewCakeIngredientNewIngredientCommand = new RelayCommand(AddNewCakeIngredientNewIngredient);
            DeleteNewCakeIngredientNewIngredientCommand = new RelayCommand<string>(DeleteNewCakeIngredientNewIngredient);

            SetDefaultValues();
        }
        private readonly IRecipeService recipeService;
        public ICommand OpenPopupCommand { get; }
        public ICommand AddNewIngredientCommand { get; }
        public ICommand ClosePopupCommand { get; }
        public ICommand AddNewCakeIngredientNewIngredientCommand { get; }
        public ICommand DeleteNewCakeIngredientNewIngredientCommand { get; }

        private bool _isOpen;
        public bool IsOpen
        {
            get => _isOpen;
            set
            {
                if (_isOpen != value)
                {
                    _isOpen = value;
                    OnPropertyChanged(nameof(IsOpen));
                }
            }
        }
        private async Task AddNewIngredient()
        {
            if (this._newCakeIngredientName == null || this._newCakeIngredientDiameter == 0 || this._newCakeIngredientIngredients == null || this.NewCakeIngredientRecipe == null)
                return;

            if (this.recipeService.CheckRecipeNameExist(this._newCakeIngredientName))
                return;

            await this.recipeService.AddNewRecipe(this.NewCakeIngredientName, this.NewCakeIngredientCategory, this.NewCakeIngredientIngredients, this.NewCakeIngredientRecipe, this.NewCakeIngredientDiameter);

            SetDefaultValues();

            ClosePopup();
        }

        private void SetDefaultValues()
        {
            this.NewCakeIngredientName = string.Empty;
            this.NewCakeIngredientRecipe = string.Empty;
            this.NewCakeIngredientDiameter = 0;
            this.NewCakeIngredientCategory = Categories.FirstOrDefault();
            this.NewCakeIngredientIngredients = [];
            this.NewCakeIngredientNewIngredientUnit = string.Empty;
            this.NewCakeIngredientNewIngredientQuantity = 0;
            this.NewCakeIngredientNewIngredientName = string.Empty;
        }

        private void OpenPopup()
        {
            IsOpen = true;
        }

        private void ClosePopup()
        {
            SetDefaultValues();
            IsOpen = false;
            Refresh();
        }

        private Ingredient? _selectedIngredient;
        public Ingredient? SelectedIngredient
        {
            get => _selectedIngredient;
            set
            {
                if (_selectedIngredient != value)
                {
                    DeleteNewCakeIngredientNewIngredient(value.Name);
                    OnPropertyChanged(nameof(SelectedIngredient));
                }
            }
        }


        private void DeleteNewCakeIngredientNewIngredient(string name)
        {
            var temp = this.NewCakeIngredientIngredients.Where(x =>x.Name != name);

            this.NewCakeIngredientIngredients = new ObservableCollection<Ingredient>(temp);
        }

        private void AddNewCakeIngredientNewIngredient()
        {
            var newIngredient = new Ingredient
            {
                Name = NewCakeIngredientNewIngredientName,
                Quantity = NewCakeIngredientNewIngredientQuantity,
                Unit = NewCakeIngredientNewIngredientUnit
            };

            this.NewCakeIngredientIngredients.Add(newIngredient);

            this.NewCakeIngredientNewIngredientUnit = string.Empty;
            this.NewCakeIngredientNewIngredientQuantity = 0;
            this.NewCakeIngredientNewIngredientName = string.Empty;
        }


        public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>((Category[])Enum.GetValues(typeof(Category)));

        private Category _newCakeIngredientCategory;
        public Category NewCakeIngredientCategory
        {
            get => _newCakeIngredientCategory;
            set
            {
                if (_newCakeIngredientCategory != value)
                {
                    _newCakeIngredientCategory = value;
                    OnPropertyChanged(nameof(NewCakeIngredientCategory));
                }
            }
        }

        private string _newCakeIngredientName;
        public string NewCakeIngredientName
        {
            get => _newCakeIngredientName;
            set
            {
                if (_newCakeIngredientName != value)
                {
                    _newCakeIngredientName = value;
                    OnPropertyChanged(nameof(NewCakeIngredientName));
                }
            }
        }

        private int _newCakeIngredientDiameter;
        public int NewCakeIngredientDiameter
        {
            get => _newCakeIngredientDiameter;
            set
            {
                if (_newCakeIngredientDiameter != value)
                {
                    _newCakeIngredientDiameter = value;
                    OnPropertyChanged(nameof(NewCakeIngredientDiameter));
                }
            }
        }

        private string _newCakeIngredientRecipe;
        public string NewCakeIngredientRecipe
        {
            get => _newCakeIngredientRecipe;
            set
            {
                if (_newCakeIngredientRecipe != value)
                {
                    _newCakeIngredientRecipe = value;
                    OnPropertyChanged(nameof(NewCakeIngredientRecipe));
                }
            }
        }

        private ObservableCollection<Ingredient> _newCakeIngredientIngredients;
        public ObservableCollection<Ingredient> NewCakeIngredientIngredients
        {
            get => _newCakeIngredientIngredients;
            set
            {
                if (_newCakeIngredientIngredients != value)
                {
                    _newCakeIngredientIngredients = value;
                    OnPropertyChanged(nameof(NewCakeIngredientIngredients));
                }
            }
        }
        private string _newCakeIngredientNewIngredientName;
        public string NewCakeIngredientNewIngredientName
        {
            get => _newCakeIngredientNewIngredientName;
            set
            {
                if (_newCakeIngredientNewIngredientName != value)
                {
                    _newCakeIngredientNewIngredientName = value;
                    OnPropertyChanged(nameof(NewCakeIngredientNewIngredientName));
                }
            }
        }

        private double _newCakeIngredientNewIngredientQuantity;
        public double NewCakeIngredientNewIngredientQuantity
        {
            get => _newCakeIngredientNewIngredientQuantity;
            set
            {
                if (_newCakeIngredientNewIngredientQuantity != value)
                {
                    _newCakeIngredientNewIngredientQuantity = value;
                    OnPropertyChanged(nameof(NewCakeIngredientNewIngredientQuantity));
                }
            }
        }

        private string _newCakeIngredientNewIngredientUnit;
        public string NewCakeIngredientNewIngredientUnit
        {
            get => _newCakeIngredientNewIngredientUnit;
            set
            {
                if (_newCakeIngredientNewIngredientUnit != value)
                {
                    _newCakeIngredientNewIngredientUnit = value;
                    OnPropertyChanged(nameof(NewCakeIngredientNewIngredientUnit));
                }
            }
        }

        public Action Refresh { get; internal set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
