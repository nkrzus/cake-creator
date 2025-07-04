using CakeCreator.Database;
using CakeCreator.Services.Services.Interfaces;
using CakeCreator.Database.Model;
using CakeCreator.Database.Model.Enums;

namespace CakeCreator.Services.Services
{
    public class RecipeService : IRecipeService
    {
        public RecipeService()
        {

        }

        public bool CheckRecipeExist(int id)
        {
            using (var db = new CakeContext())
            {
                return db.CakeIngredients.Any(x => x.Id == id);
            }
        }

        public bool CheckRecipeNameExist(string name)
        {
            using (var db = new CakeContext())
            {
                return db.CakeIngredients.Any(x => x.Name.ToLower().Equals(name.ToLower()));
            }
        }
        public CakeIngredient GetRecipe(int id)
        {
            using (var db = new CakeContext())
            {
                return db.CakeIngredients.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<CakeIngredient> GetBaseCakeIngredients()
        {
            using (var db = new CakeContext())
                return db.CakeIngredients.Where(x => x.IsBase == true).ToList();
        }


        public async Task AddNewRecipe(string name, Category category, IList<Ingredient> ingredients, string recipe, int diameter)
        {
            using (var db = new CakeContext())
            {
                var newCakeIngredient = new CakeIngredient
                {
                    Name = name,
                    Category = category,
                    Ingredients = ingredients,
                    Recipe = recipe,
                    Diameter = diameter,
                    IsBase = true
                };

                await db.CakeIngredients.AddAsync(newCakeIngredient);

                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteRecipe(CakeIngredient recipe)
        {
            using (var db = new CakeContext())
            {
                db.Remove(recipe);

                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateRecipe(CakeIngredient selected)
        {
            using (var db = new CakeContext())
            {


                db.Update(selected);

                await db.SaveChangesAsync();
            }
        }
    }
}
