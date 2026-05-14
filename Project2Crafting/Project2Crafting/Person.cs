using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2Crafting
{
    public class Person
    {
        public string Name;
        public List<Item> Inventory = new List<Item>();
        public double Currency = 10.00;

        public Person(string name) { Name = name;}

        public void AddItem(Item item) 
        {
            Inventory.Add(item);
        }
        public string ShowInventory() 
        {
            string output = "";
            foreach (Item item in Inventory) 
            {
                output += $"{item.Name} {item.Amount} Cost: ${item.Value} each";
            }
            return output;
        }
    }
}
