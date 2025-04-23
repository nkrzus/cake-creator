using CakeCreator.Model.Enums;
using CakeCreator.Model.Interfaces;

namespace CakeCreator.Model
{
    public class CakeIngredient : ICakeEntity
    {
        public CakeIngredient()
        {
            
        }
        public int Id { get; set; }
        public required string Name { get; set; }
        public required Category Category { get; set; }
        public int Quantity { get; set; }
        public required string Recipe { get; set; }
        public required IList<Ingredient> Ingredients { get; set; } = [];
        public int Diameter {  get; set; }
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
        public bool IsBase { get; set; }

    }
}
