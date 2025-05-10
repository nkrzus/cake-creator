using CakeCreator.Database.Model.Enums;
using CakeCreator.Database.Model;
using CakeCreator.Services.Builders.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCreator.Services.Builders
{
    internal class CakeBuilder : ICakeBuilder
    {
        private string? recipeName;
        private Category category;
        private int quantity;
        private string? recipe;
        private int diameter;
        private IList<Ingredient> ingredients = [];
        private readonly IIngredientBuilder igBuilder;

        public CakeBuilder(IIngredientBuilder igBuilder)
        {
            this.igBuilder = igBuilder;
        }

        public ICakeBuilder SetCategory()
        {
            Console.WriteLine("Wybierz kategorie: ");

            ShowCategories();

            Category selectedCategory;

            if (int.TryParse(Console.ReadLine(), out int selectedValue) && Enum.IsDefined(typeof(Category), selectedValue))
            {
                selectedCategory = (Category)selectedValue;
                Console.WriteLine($"Wybrano kategorię: {selectedCategory}");
            }
            else
            {
                Console.WriteLine("Nieprawidłowy wybór!");
                throw new Exception("Niepoprawna kategoria");
            }

            this.category = selectedCategory;

            return this;
        }

        public ICakeBuilder SetRecipe()
        {
            Console.Write("Podaj przepis: ");
            this.recipe = Console.ReadLine();
            return this;
        }

        public ICakeBuilder SetDiameter()
        {
            bool isValid = false;
            while (isValid == false)
            {
                Console.Write("Podaj na jaka srednice jest ten przepis, w cm: ");
                string value = Console.ReadLine();

                isValid = int.TryParse(value, out int tmpDiameter);

                if (isValid)
                    this.diameter = tmpDiameter;
            }
            return this;
        }

        public ICakeBuilder SetRecipeName()
        {
            Console.Write("Podaj nazwe przepisu: ");
            this.recipeName = Console.ReadLine();
            return this;
        }

        public void ShowCategories()
        {
            foreach (Category category in Enum.GetValues(typeof(Category)))
            {
                Console.WriteLine($"{(int)category}-{category}");
            }
        }

        public CakeIngredient Build()
        {

            return new CakeIngredient()
            {
                Category = category,
                Diameter = diameter,
                Ingredients = ingredients,
                Quantity = quantity,
                Name = recipeName,
                Recipe = recipe,
                IsBase = true
            };
        }

        public ICakeBuilder SetIngredients()
        {
            for (int i = 0; i < this.quantity; i++)
            {
                var ig = this
                    .igBuilder
                    .SetName()
                    .SetQuantity()
                    .SetUnit()
                    .Build();

                this.ingredients.Add(ig);
            }

            return this;
        }

        public ICakeBuilder SetQuantity()
        {
            Console.Write("Podaj ilosc składnikow: ");
            this.quantity = Convert.ToInt32(Console.ReadLine());
            return this;
        }
    }
}

