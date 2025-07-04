using CakeCreator.Database.Model.Enums;
using CakeCreator.Database.Model;
using CakeCreator.Database;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCreator.Services.Services.Interfaces
{
    public interface IRecipeService
    {
        bool CheckRecipeExist(int id);
        CakeIngredient GetRecipe(int id);
        bool CheckRecipeNameExist(string name);
        List<CakeIngredient> GetBaseCakeIngredients();
        Task AddNewRecipe(string name, Category category, IList<Ingredient> ingredients, string recipe, int diameter);
        Task DeleteRecipe(CakeIngredient recipe);
        Task UpdateRecipe(CakeIngredient selected);
    }
}
