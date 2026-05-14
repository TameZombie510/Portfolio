using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2Crafting
{
    public class Recipe
    {
        public string Name;
        public string Description;
        public float YieldAmount;
        public string YieldType;
        public float Value;

        public List<Item> Ingredients = new List<Item>();
        
        public string GetIngredientInfo() 
        {

            string output = "Ingredients needed:\n";
            foreach (Item item in Ingredients) 
            {
                output += $"    *{item.Amount} {item.Name}\n";
            }

            return output;
        }
    }
}
