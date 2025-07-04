using CakeCreator.Database;
using CakeCreator.Database.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CakeCreator.Database.Model.Enums;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using CakeCreator.Services.Services;
using CakeCreator.Services.Services.Interfaces;

namespace CakeCreator.UI.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            recipeService = new RecipeService();
            ingredientService = new IngredientService();

            LoadDataFromDatabase();
            ClosePopupCommand = new RelayCommand(ClosePopup);
            EditRecipeCommand = new RelayCommand(async () => { await EditRecipe(); });
            DeleteRecipeCommand = new RelayCommand(async () => { await DeleteRecipe(); });
            AddNewIngredientCommand = new RelayCommand(async () => { await AddNewIngredient(); });
            EditRecipeCommand = new RelayCommand(async () => { await EditRecipe(); });
            LoadFromDatabaseCommnad = new RelayCommand(() => { LoadDataFromDatabase(); });

            CreateRecipePopupVM = new CreateRecipePopupViewModel();
            CreateRecipePopupVM.Refresh = () => LoadDataFromDatabase();

        }
        private readonly IRecipeService recipeService;

        public readonly IIngredientService ingredientService;
        public CreateRecipePopupViewModel CreateRecipePopupVM { get; }
        public ICommand ClosePopupCommand { get; }
        public ICommand EditRecipeCommand { get; }
        public ICommand DeleteRecipeCommand { get; }
        public ICommand AddNewIngredientCommand { get; }
        public ICommand LoadFromDatabaseCommnad { get; }
        public ObservableCollection<CakeIngredient> CakeIngredientItems { get; set; } = new();
        public ObservableCollection<CakeIngredient> AllCakeIngredientItems { get; set; } = new();
        public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>((Category[])Enum.GetValues(typeof(Category)));

        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged(nameof(SelectedCategory));
                }
            }
        }

        private CakeIngredient? _selectedCakeIngredientItem;
        public CakeIngredient? SelectedCakeIngredientItem
        {
            get => _selectedCakeIngredientItem;
            set
            {
                if (_selectedCakeIngredientItem != value)
                {
                    _selectedCakeIngredientItem = value;
                    OnPropertyChanged(nameof(SelectedCakeIngredientItem));

                    if (SelectedCakeIngredientItem != null)
                    {
                        _selectedCakeIngredients = new(SelectedCakeIngredientItem.Ingredients);
                        OnPropertyChanged(nameof(SelectedCakeIngredients));

                        _selectedCategory = SelectedCakeIngredientItem.Category;
                        OnPropertyChanged(nameof(SelectedCategory));
                    
                        _isPopupOpen = true;
                        OnPropertyChanged(nameof(IsPopupOpen));
                    }

                }
            }
        }

        private ObservableCollection<Ingredient> _selectedCakeIngredients;
        public ObservableCollection<Ingredient> SelectedCakeIngredients
        {
            get => _selectedCakeIngredients;
            set
            {
                if (_selectedCakeIngredients != value)
                {
                    _selectedCakeIngredients = value;
                    OnPropertyChanged(nameof(SelectedCakeIngredients));
                }
            }
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
            var temp = this.SelectedCakeIngredients.Where(x => x.Name != name);

            this.SelectedCakeIngredients = new ObservableCollection<Ingredient>(temp);

            ingredientService.Delete(SelectedCakeIngredientItem.Id, name);

        }

        private bool _isPopupOpen;

        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set
            {
                if (_isPopupOpen != value)
                {
                    _isPopupOpen = value;
                    OnPropertyChanged(nameof(IsPopupOpen));
                }
            }
        }

        private bool _isCreateRecipePopupOpen;

        public bool IsCreateRecipePopupOpen
        {
            get => _isCreateRecipePopupOpen;
            set
            {
                if (_isCreateRecipePopupOpen != value)
                {
                    _isCreateRecipePopupOpen = value;
                    OnPropertyChanged(nameof(IsCreateRecipePopupOpen));
                }
            }
        }

        private void ClosePopup()
        {
            this.IsPopupOpen = false;
            LoadDataFromDatabase();
        }

        private void CloseCreateRecipePopup()
        {
            this.IsCreateRecipePopupOpen = false;
        }

        private string _newIngredientName;

        public string NewIngredientName
        {
            get => _newIngredientName;
            set
            {
                if (_newIngredientName != value)
                {
                    _newIngredientName = value;
                    OnPropertyChanged(nameof(NewIngredientName));
                }
            }
        }

        private int _newIngredientQuantity;

        public int NewIngredientQuantity
        {
            get => _newIngredientQuantity;
            set
            {
                if (_newIngredientQuantity != value)
                {
                    _newIngredientQuantity = value;
                    OnPropertyChanged(nameof(NewIngredientQuantity));
                }
            }
        }

        private string _newIngredientUnit;

        public string NewIngredientUnit
        {
            get => _newIngredientUnit;
            set
            {
                if (_newIngredientUnit != value)
                {
                    _newIngredientUnit = value;
                    OnPropertyChanged(nameof(NewIngredientUnit));
                }
            }
        }

        private string _searchPattern;
        private Category _selectedCategory;

        public string SearchPattern
        {
            get => _searchPattern;
            set
            {
                if (_searchPattern != value)
                {
                    _searchPattern = value;
                    OnPropertyChanged(nameof(SearchPattern));
                    if (value.Length >= 3)
                    {
                        CakeIngredientItems.Clear();
                        foreach (var item in AllCakeIngredientItems)
                        {
                            if (item.Name.ToLower().Contains(_searchPattern.ToLower()))
                            {
                                CakeIngredientItems.Add(item);
                            }
                        }
                        OnPropertyChanged(nameof(CakeIngredientItems));
                    }
                    else
                    {
                        CakeIngredientItems.Clear();
                        foreach (var item in AllCakeIngredientItems)
                        {
                            CakeIngredientItems.Add(item);
                        }
                        OnPropertyChanged(nameof(CakeIngredientItems));
                    }
                }
            }
        }

        private async Task EditRecipe()
        {
            var selected = this.recipeService.GetRecipe(SelectedCakeIngredientItem.Id);

            if (selected == null)
                return;

            if (selected.IsBase != SelectedCakeIngredientItem.IsBase)
                selected.IsBase = SelectedCakeIngredientItem.IsBase;

            if (selected.Category != SelectedCategory)
                selected.Category = SelectedCategory;

            if (selected.Diameter != SelectedCakeIngredientItem.Diameter)
                selected.Diameter = SelectedCakeIngredientItem.Diameter;

            if (selected.Name != SelectedCakeIngredientItem.Name)
                selected.Name = SelectedCakeIngredientItem.Name;

            if (selected.Recipe != SelectedCakeIngredientItem.Recipe)
                selected.Recipe = SelectedCakeIngredientItem.Recipe;

            selected.Ingredients = SelectedCakeIngredients;

            await this.recipeService.UpdateRecipe(selected);

            LoadDataFromDatabase();

            ClosePopup();
        }


        private async Task AddNewIngredient()
        {
            if (!!(string.IsNullOrWhiteSpace(NewIngredientName) || string.IsNullOrWhiteSpace(NewIngredientUnit) || NewIngredientQuantity == 0))
                return;

            var temp = this.SelectedCakeIngredients;

            temp.Add(new Ingredient { Name = NewIngredientName, Unit = NewIngredientUnit, Quantity = NewIngredientQuantity });

            this.SelectedCakeIngredients = temp;

            OnPropertyChanged(nameof(SelectedCakeIngredients));

            NewIngredientName = string.Empty;
            NewIngredientUnit = string.Empty;
            NewIngredientQuantity = 0;
        }

        private async Task DeleteRecipe()
        {
            if (SelectedCakeIngredientItem == null)
                return;

            var selected = this.recipeService.GetRecipe(SelectedCakeIngredientItem.Id);

            if (selected == null)
                return;

            await this.recipeService.DeleteRecipe(selected);

            LoadDataFromDatabase();

            ClosePopup();
        }

        private void LoadDataFromDatabase()
        {
            AllCakeIngredientItems.Clear();

            CakeIngredientItems.Clear();

            List<CakeIngredient> recipes = this.recipeService.GetBaseCakeIngredients();

            foreach (var baseRecipe in recipes)
            {
                var ingredients = this.ingredientService.GetIngredientsByCakeIngredientId(baseRecipe.Id);

                var cakeIngredient = new CakeIngredient
                {
                    Id = baseRecipe.Id,
                    Name = baseRecipe.Name,
                    Category = baseRecipe.Category,
                    Recipe = baseRecipe.Recipe,
                    Diameter = baseRecipe.Diameter,
                    Ingredients = ingredients,
                    IsBase = baseRecipe.IsBase,
                    Quantity = baseRecipe.Quantity,
                    OrderId = baseRecipe.OrderId,
                    Order = baseRecipe.Order
                };

                AllCakeIngredientItems.Add(cakeIngredient);

                CakeIngredientItems.Add(cakeIngredient);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


    }
}
