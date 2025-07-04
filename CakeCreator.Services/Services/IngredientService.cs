using CakeCreator.Database;
using CakeCreator.Database.Model;
using CakeCreator.Services.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCreator.Services.Services
{
    public class IngredientService : IIngredientService
    {
        public void Delete(int id, string name)
        {
            using (var db = new CakeContext())
            {
                var recipeIngredient = db.Ingredients.FirstOrDefault(x=>x.Name == name && x.CakeIngredientId == id);

                if (recipeIngredient == null)
                    return;

                db.Ingredients.Remove(recipeIngredient);

                db.SaveChanges();
            }
        }

        public List<Ingredient> GetIngredientsByCakeIngredientId(int id)
        {
            using (var db = new CakeContext())
            {
                return db.Ingredients.Where(i => i.CakeIngredientId == id).ToList();
            }
        }
    }
}
