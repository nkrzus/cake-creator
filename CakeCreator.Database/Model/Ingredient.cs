using CakeCreator.Model.Interfaces;
namespace CakeCreator.Model
{
    public class Ingredient : ICakeEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public double Quantity { get; set; }
        public required string Unit { get; set; }
        public int CakeIngredientId { get; set; }
        public CakeIngredient CakeIngredient { get; set; }
    }
}
