using CakeCreator.Database.Model.Interfaces;
namespace CakeCreator.Database.Model
{
    public class Order : ICakeEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Diameter { get; set; }
        public required IList<CakeIngredient> Ingredients { get; set; } = [];
        public int QuantityOfLayers { get; set; }
    }
}
