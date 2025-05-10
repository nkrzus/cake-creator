using CakeCreator.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCreator.Services.Builders.Interfaces
{
    internal interface IOrderBuilder
    {
        IOrderBuilder SetName();
        IOrderBuilder SetDiameter();
        IOrderBuilder SetQuantityOfLayers();
        IOrderBuilder SetIngredients();
        Order Build();
    }
}
