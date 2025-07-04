using CakeCreator.Database.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCreator.Services.Services.Interfaces
{
    public interface IIngredientService
    {
        void Delete(int id, string name);
        List<Ingredient> GetIngredientsByCakeIngredientId(int id);
    }
}
