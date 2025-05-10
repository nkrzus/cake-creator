using CakeCreator.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCreator.Services.Builders.Interfaces
{
    internal interface ICakeBuilder
    {
        ICakeBuilder SetRecipeName();
        ICakeBuilder SetCategory();
        ICakeBuilder SetQuantity();
        ICakeBuilder SetRecipe();
        ICakeBuilder SetDiameter();
        ICakeBuilder SetIngredients();
        CakeIngredient Build();
    }
}
