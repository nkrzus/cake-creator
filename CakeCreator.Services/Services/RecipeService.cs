using CakeCreator.Database;
using CakeCreator.Services.Builders.Interfaces;
using CakeCreator.Services.Builders;
using CakeCreator.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using System.Runtime.Intrinsics.X86;
using CakeCreator.Database.Model;

namespace CakeCreator.Services.Services
{
    public class RecipeService : IRecipeService
    {
        public RecipeService() { }

        public async Task AddBaseRecipe()
        {
            using (var db = new CakeContext())
            {
                IIngredientBuilder igBuilder = new IngredientBuilder();
                ICakeBuilder cakeBuilder = new CakeBuilder(igBuilder);

                cakeBuilder
                    .SetRecipeName()
                    .SetRecipe()
                    .SetDiameter()
                    .SetCategory()
                    .SetQuantity()
                    .SetIngredients();

                var cakeIngredient = cakeBuilder.Build();

                db.CakeIngredients.Add(cakeIngredient);

                db.SaveChanges();

                Console.WriteLine($"Dodano nowy przepis: {cakeIngredient.Name}");
            }

        }
        public async Task GenerateShoppingList()
        {
            Console.WriteLine("Dostepne zamowienia: ");
            var db = new CakeContext();
            var allOrders = db.Orders.ToList();
            foreach (var ord in allOrders)
            {
                Console.WriteLine($"{ord.Id} - {ord.Name}");
            }

            Console.WriteLine("");
            Console.WriteLine("Podaj ID zamowienia dla ktorego chcesz wygenerowac liste zakupow: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Order order = db.Orders.FirstOrDefault(x => x.Id == id);
            var igList = new List<Ingredient>();
            if (order != null)
            {
                var layers = db.CakeIngredients.Where(x => x.OrderId == order.Id);

                foreach (var layer in layers)
                {
                    var ingredients = db.Ingredients.Where(x => x.CakeIngredientId == layer.Id);

                    igList.AddRange(ingredients);
                }
            }

            var grouped = igList.GroupBy(x => x.Name);
            Console.WriteLine("");
            Console.WriteLine($"Lista zakupow dla zamowienia {order.Name}: ");

            foreach (var group in grouped)
            {

                Console.WriteLine($"{group.Key} - {Math.Round(group.Sum(x => x.Quantity), 2)} {group.FirstOrDefault().Unit}");
            }
        }

        public async Task AddNewOrder()
        {
            Console.WriteLine($"Prosze podać dane do nowego zamowienia");

            using (var db = new CakeContext())
            {
                IIngredientBuilder igBuilder = new IngredientBuilder();
                ICakeBuilder cakeBuilder = new CakeBuilder(igBuilder);
                IOrderBuilder orderBuilder = new OrderBuilder();

                orderBuilder
                    .SetName()
                    .SetDiameter()
                    .SetQuantityOfLayers()
                    .SetIngredients();


                var newOrder = orderBuilder.Build();

                db.Orders.Add(newOrder);

                db.SaveChanges();

                Console.WriteLine($"Dodano nowe zamowienie: {newOrder.Name}");
            }

        }

        public async Task BrowseBaseRecipes() {
            using (var db = new CakeContext())
            {
                Console.WriteLine("Dostepne przepisy: ");
                var baseRecipes = db.CakeIngredients.Where(x => x.IsBase == true).ToList();
                foreach (var baseRecipe in baseRecipes)
                {
                    Console.WriteLine($"{baseRecipe.Id} - {baseRecipe.Category} - {baseRecipe.Name}");
                }
            }
        }

        public async Task BrowseOrderByID() {
            Console.Write("Podaj ID zamowienia ktore chcesz wyswietlic: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            using (var db = new CakeContext())
            {

                var recipe = db.CakeIngredients.FirstOrDefault(x => x.Id == id);
                if (recipe != null)
                {
                    var recipeDetails = db.Ingredients.Where(x => x.CakeIngredientId == id).ToList();

                    Console.WriteLine($"Nazwa: {recipe.Name}");
                    Console.WriteLine($"Kategoria: {recipe.Category}");
                    Console.WriteLine($"Srednica: {recipe.Diameter}");
                    Console.WriteLine("Skladniki: ");
                    foreach (var item in recipeDetails)
                    {
                        Console.WriteLine($"{item.Name} - {item.Quantity}{item.Unit}");
                    }
                    Console.WriteLine($"Przygotowanie: {recipe.Recipe}");
                }
            }
        }
    }
}
