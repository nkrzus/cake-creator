using CakeCreator.Database.Model.Enums;
using CakeCreator.Database.Model;
using CakeCreator.Database;
using CakeCreator.Services.Builders.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCreator.Services.Builders
{
    internal class OrderBuilder : IOrderBuilder
    {
        private string name;
        private int diameter;
        private int qtyOfLayers;
        private string shoppinglist;
        private IList<CakeIngredient> cakeIngredients = [];
        private readonly IngredientBuilder igBuilder;
        private readonly CakeBuilder cakeBuilder;

        public OrderBuilder()
        {
            this.igBuilder = new IngredientBuilder();
            this.cakeBuilder = new CakeBuilder(igBuilder);
        }
        public Order Build()
        {
            return new Order()
            {
                Name = name,
                Diameter = diameter,
                QuantityOfLayers = qtyOfLayers,
                Ingredients = cakeIngredients,
            };
        }

        public IOrderBuilder SetName()
        {
            Console.Write("Podaj nazwe zamowienia: ");
            this.name = Console.ReadLine();
            return this;
        }
        public IOrderBuilder SetDiameter()
        {
            Console.Write("Podaj srednice tortu: ");
            this.diameter = Convert.ToInt32(Console.ReadLine());
            return this;
        }
        public IOrderBuilder SetQuantityOfLayers()
        {
            Console.Write("Podaj ilość skladnikow tortu: ");
            this.qtyOfLayers = Convert.ToInt32(Console.ReadLine());
            return this;
        }
        public IOrderBuilder SetIngredients()
        {
            using (var db = new CakeContext())
            {
                string IsJellyHere = "N";
                Console.WriteLine("");
                Console.WriteLine("Dostepne przepisy: ");
                var baseRecipes = db.CakeIngredients.Where(x => x.IsBase == true).ToList();
                foreach (var baseRecipe in baseRecipes)
                {
                    Console.WriteLine($"{baseRecipe.Id} - {baseRecipe.Category} - {baseRecipe.Name}");
                }

                Console.WriteLine();
                Console.WriteLine("Wybierz skladniki warstw: ");
                for (int i = 0; i < this.qtyOfLayers; i++)
                {
                    Console.Write($"Podaj id {i + 1} warstwy: ");
                    int id = Convert.ToInt32(Console.ReadLine());

                    var newRecipe = baseRecipes.FirstOrDefault(x => x.Id == id);
                    if (newRecipe != null)
                    {

                        int oldDiameter = newRecipe.Diameter;
                        int newDiameter = 0;
                        if (newRecipe.Category == Category.Żelka)
                        {
                            newDiameter = this.diameter - 3;
                        }
                        else
                        {
                            newDiameter = this.diameter;
                        }
                        newRecipe.IsBase = false;
                        var recipeIngredients = db.Ingredients.Where(x => x.CakeIngredientId == newRecipe.Id).ToList();
                        IList<Ingredient> updatedIngredientsList = new List<Ingredient>();


                        if (newRecipe.Category != Category.Żelka && newRecipe.Category != Category.Biszkopt)
                        {
                            Console.Write("Czy bedzie w niej zelka? Y/N: ");
                            IsJellyHere = Console.ReadLine();



                        }
                        foreach (var item in recipeIngredients)
                        {
                            double calculatedQuantity = 0;
                            if (IsJellyHere == "Y")
                            {
                                calculatedQuantity = CalculatorForQuantities(oldDiameter, newDiameter, item.Quantity) / 1.7;
                            }
                            else if (IsJellyHere == "N")
                            {
                                calculatedQuantity = CalculatorForQuantities(oldDiameter, newDiameter, item.Quantity);
                            }

                            updatedIngredientsList.Add(new Ingredient
                            {
                                Name = item.Name,
                                Quantity = calculatedQuantity,
                                Unit = item.Unit
                            }
                            );
                        }

                        cakeIngredients.Add(new CakeIngredient
                        {
                            Category = newRecipe.Category,
                            Diameter = newDiameter,
                            Name = newRecipe.Name,
                            Quantity = newRecipe.Quantity,
                            Recipe = newRecipe.Recipe,
                            Ingredients = updatedIngredientsList
                        });
                    }
                }
            }
            return this;
        }
        public double CalculatorForQuantities(int oldDiameter, int newDiameter, double oldQuantity)
        {
            double oldArea = Math.PI * Math.Pow((oldDiameter / 2), 2);
            double newArea = Math.PI * Math.Pow((newDiameter / 2), 2);
            double newQuantity = (newArea * oldQuantity) / oldArea;
            return Math.Round(newQuantity, 2);
        }
    }
}
