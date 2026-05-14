using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace Project2Crafting
{
    public class Loader
    {

        public static string LoadData(string filename)
        {
            return File.ReadAllText(filename);
        }

        public static List<Recipe> FileData() 
        {
            List<Recipe> list = new List<Recipe>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../../../data/Recipes.xml");
            XmlNode root = xmlDoc.DocumentElement;
            XmlNodeList recipeList = root.SelectNodes("/recipes/recipe");

            foreach(XmlElement element in recipeList) 
            {
                Recipe recipe = new Recipe(); 
                recipe.Name = element.GetAttribute("Name");
                recipe.Description = element.GetAttribute("Description");
                if(float.TryParse(element.GetAttribute("Value"), out float valueFloat)) 
                {
                    recipe.Value = valueFloat;  
                }
                recipe.YieldType = element.GetAttribute("YieldType");
                if(float.TryParse(element.GetAttribute("YieldAmount"), out float yieldFloat)) 
                {
                    recipe.YieldAmount = yieldFloat;
                }

                XmlNodeList itemNodes = element.ChildNodes;
                List<Item> items = new List<Item>();
                foreach(XmlElement itemNode in itemNodes) 
                {
                    Item item = new Item();
                    item.Name = itemNode.GetAttribute("Name");
                    if(Double.TryParse(itemNode.GetAttribute("Value"), out double itemValue)) 
                    {
                        item.Value = itemValue;
                    }
                    if(Double.TryParse(itemNode.GetAttribute("Amount"), out double amountValue)) 
                    { 
                        item.Amount = amountValue;  
                    }
                    item.AmountType = itemNode.GetAttribute("AmountType");

                    items.Add(item);
                }
                recipe.Ingredients = items;
                list.Add(recipe);
            }


            return list;
        }
    }
}
