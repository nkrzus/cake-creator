using CakeCreator.Database.Model;
using CakeCreator.Services.Builders.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCreator.Services.Builders
{
    internal class IngredientBuilder : IIngredientBuilder
    {
        private string name;
        private double quantity;
        private string unit;

        public Ingredient Build()
        {
            return new Ingredient()
            {
                Name = name,
                Quantity = quantity,
                Unit = unit
            };
        }

        public IIngredientBuilder SetName()
        {
            Console.Write("Podaj nazwe składnika: ");
            this.name = Console.ReadLine();
            return this;
        }

        public IIngredientBuilder SetQuantity()
        {
            Console.Write("Podaj ilość: ");
            this.quantity = Convert.ToDouble(Console.ReadLine());
            return this;
        }

        public IIngredientBuilder SetUnit()
        {
            Console.Write("Podaj jednostkę: ");
            this.unit = Console.ReadLine();
            return this;
        }
    }
}
