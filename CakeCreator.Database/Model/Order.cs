using CakeCreator.Model.Interfaces;
namespace CakeCreator.Model
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
