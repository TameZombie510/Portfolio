using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Project2Crafting.Loader;
using static Project2Crafting.Recipe;
using static Project2Crafting.Shops;
using static Project2Crafting.Game;

namespace Project2Crafting
{
    /// <summary>
    /// Interaction logic for CraftingInterface.xaml
    /// </summary>
    public partial class CraftingInterface : Page
    {    

        MainWindow window = (MainWindow) Application.Current.MainWindow;
        
        public List<Recipe> Recipes = FileData();
        public Recipe selectedRecipe = new();
        public List<Item> HardwareStoreItems = new List<Item>();  
        public List<Item> ArtsAndCraftsStoreItems = new List<Item>();
        public List<Item> ThriftStoreItems = new List<Item>();
      
    

        public Shop CurrentShop;
        

        public CraftingInterface()
        {
            InitializeComponent();
            Recipes = FileData();
            CurrencyLabel.Content = $"${player.Currency}";
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            
            foreach (Recipe recipe in Recipes)
            {
                foreach (Item item in recipe.Ingredients)
                {
                    if (item.Name == "Medium Wooden Plank")
                    {
                        HardwareStoreItems.Add(item);
                    }
                    if (item.Name == "Large Wooden Plank")
                    {
                        HardwareStoreItems.Add(item);
                    }
                    if (item.Name == "Black Wax") 
                    {
                        ArtsAndCraftsStoreItems.Add(item);
                    }
                    if (item.Name == "Red Chalk")
                    {
                        ArtsAndCraftsStoreItems.Add(item);
                    }
                    if (item.Name == "Small Bottle") 
                    {
                        ThriftStoreItems.Add(item);
                    }
                    if (item.Name == "Knife")
                    {
                        ThriftStoreItems.Add(item);
                    }
                    if (item.Name == "Easter Bunny Costume")
                    {
                        ThriftStoreItems.Add(item);
                    }
                }
            }
            InventoryText.Text = PlayerInventory(player.Inventory);

        }

        public string PlayerInventory(List<Item> playerInventory) 
        {
            string inventoryItems = "";
           
            foreach (Item item in playerInventory) 
            {
                inventoryItems += $"  *{item.Name}\n";
            }
            return inventoryItems;
        }


        public Shop Store(Shop currentShop) 
        {
            CurrentShop = currentShop;  
            int number;
            switch (currentShop) 
            {
                case Shop.None:
                    ItemBoxText.Text = "";
                    Recipes = FileData();
                    number = 0;
                    foreach (Recipe recipe in Recipes) 
                    {
                        ItemBoxText.Text += @$" 
  {number+=1}. {recipe.Name} 
  {recipe.GetIngredientInfo()}
                         ";
                    }
                    break;
                case Shop.HardwareStore:
                    ItemBoxText.Text = "";
                    number = 0;
                    foreach (Item item in HardwareStoreItems)
                    {
                        ItemBoxText.Text += $"    {number += 1}. {item.Name} Amount: {item.Amount} - ${item.Value} each\n";
                    }
                    break;
                case Shop.ArtsAndCraftsStore:
                    ItemBoxText.Text = "";
                    number = 0;
                    foreach (Item item in ArtsAndCraftsStoreItems)
                    {
                        ItemBoxText.Text += $"    {number += 1}. {item.Name} Amount: {item.Amount} - ${item.Value} each\n";
                    }
                    break;
                case Shop.ThriftStore:
                    ItemBoxText.Text = "";
                    number = 0;
                    foreach (Item item in ThriftStoreItems)
                    {
                        ItemBoxText.Text += $"     {number += 1} . {item.Name} Amount: {item.Amount} - ${item.Value} each\n";
                    }
                    break;
            }
            Console.WriteLine(currentShop);

            return currentShop;
        }

        public InterfaceMode Mode(InterfaceMode mode) 
        {
            switch (mode) 
            {
                case InterfaceMode.Craft:
                    CraftRecipeButton.Visibility = Visibility.Visible;
                    BuyItemButton.Visibility = Visibility.Hidden;
                    break;
                case InterfaceMode.Buy:
                    BuyItemButton.Visibility = Visibility.Visible;
                    CraftRecipeButton.Visibility = Visibility.Hidden;
                    break;
            }
            return mode;
        }
     

        public bool HasItem(List<Item> list, string ItemName) 
        {
            foreach (Item item in list) 
            {
                if (item.Name.ToLower() == ItemName.ToLower()) 
                {
                    return true;
                }
            }
            return false;
        }

        

        private void CraftRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            string input = Input.Text;
            
            Console.WriteLine(input);
            if (input == "1")
            {
                foreach (Recipe recipe in Recipes)
                {
                    if (recipe.Name == "Bottle of Blood")
                    {
                        if (HasItem(player.Inventory, "Small Bottle") && HasItem(player.Inventory, "Knife"))
                        {
                            Item BottleOfBlood = new();
                            BottleOfBlood.Name = "Bottle of Blood";
                            player.Inventory.Add(BottleOfBlood);                         
                            InventoryText.Text = PlayerInventory(player.Inventory);
                        }
                    }
                }
            }
            else if (input == "2")
            {
                foreach (Recipe recipe in Recipes)
                {
                    if (recipe.Name == "Summoning Circle")
                    {
                        if (HasItem(player.Inventory, "Red Chalk") && HasItem(player.Inventory, "Bottle of Blood"))
                        {
                            Item SummoningCircle = new();
                            SummoningCircle.Name = "Summoning Circle";
                            player.Inventory.Add(SummoningCircle);

                        }
                    }
                }
            }
            else if (input == "3")
            {
                foreach (Recipe recipe in Recipes)
                {
                    if (recipe.Name == "Summoning Candle")
                    {
                        if (HasItem(player.Inventory, "Black Wax") && HasItem(player.Inventory, "Bottle of Blood"))
                        {
                            Item SummoningCandle = new();
                            SummoningCandle.Name = "Summoning Candle";
                            player.Inventory.Add(SummoningCandle);

                        }
                    }
                }
            }
            else if (input == "4")
            {
                foreach (Recipe recipe in Recipes)
                {
                    if (recipe.Name == "Crucifix")
                    {
                        if (HasItem(player.Inventory, "Medium Wooden Plank") && HasItem(player.Inventory, "Large Wooden Plank"))
                        {
                            Item Crucifix = new();
                            Crucifix.Name = "Crucifix";
                            player.Inventory.Add(Crucifix);

                        }
                    }
                }
            }
            else if (input == "5")
            {
                foreach (Recipe recipe in Recipes)
                {
                    if (recipe.Name == "Podium")
                    {
                        if (HasItem(player.Inventory, "Easter Bunny Costume") && HasItem(player.Inventory, "Crucifix"))
                        {
                            Item Podium = new();
                            Podium.Name = "Podium";
                            player.Inventory.Add(Podium);

                        }
                    }
                }
            }
        }

        private void BuyItemButton_Click(object sender, RoutedEventArgs e)
        {
            string input = Input.Text;

            if (CurrentShop == Shop.HardwareStore)
            {
                foreach (Item item in HardwareStoreItems)
                {
                    if (input == "1") 
                    {
                        if (item.Name == "Medium Wooden Plank") 
                        {
                            if (player.Currency >= item.Value)
                            {
                                Item MediumWoodenPlank = new();
                                MediumWoodenPlank.Name = item.Name;
                                player.Inventory.Add(MediumWoodenPlank);
                                player.Currency -= item.Value;  
                            }
                        }
                    }
                    else if (input == "2")
                    {
                        if (item.Name == "Large Wooden Plank")
                        {
                            if (player.Currency >= item.Value)
                            {
                                Item LargeWoodenPlank = new();
                                LargeWoodenPlank.Name = item.Name;
                                player.Inventory.Add(LargeWoodenPlank);
                                player.Currency -= item.Value;
                            }
                        }
                    }
                }
            }
            else if (CurrentShop == Shop.ArtsAndCraftsStore)
            {
                foreach (Item item in ArtsAndCraftsStoreItems)
                {
                    if (input == "1")
                    {
                        if (item.Name == "Red Chalk")
                        {
                            if (player.Currency >= item.Value)
                            {
                                Item RedChalk = new();
                                RedChalk.Name = item.Name;
                                player.Inventory.Add(RedChalk);
                                player.Currency -= item.Value;
                            }
                        }
                    }
                    else if (input == "2")
                    {
                        if (item.Name == "Black Wax")
                        {
                            if (player.Currency >= item.Value)
                            {
                                Item BlackWax = new();
                                BlackWax.Name = item.Name;
                                player.Inventory.Add(BlackWax);
                                player.Currency -= item.Value;
                            }
                        }
                    }
                }
            }
            else if (CurrentShop == Shop.ThriftStore)
            {
                foreach (Item item in ThriftStoreItems)
                {
                    if (input == "1")
                    {
                        if (item.Name == "Small Bottle")
                        {
                            if (player.Currency >= item.Value) 
                            {
                                Item SmallBottle = new();
                                SmallBottle.Name = item.Name;
                                player.Inventory.Add(SmallBottle);
                                player.Currency -= item.Value;
                            }
                        }
                    }
                    else if (input == "2")
                    {
                        if (item.Name == "Knife")
                        {
                            if (player.Currency >= item.Value)
                            {
                                Item Knife = new();
                                Knife.Name = item.Name;
                                player.Inventory.Add(Knife);
                                player.Currency -= item.Value;
                            }
                        }
                    }
                    else if (input == "3")
                    {
                        if (item.Name == "Easter Bunny Costume")
                        {
                            if (player.Currency >= item.Value)
                            {
                                Item EasterBunnyCostume = new();
                                EasterBunnyCostume.Name = item.Name;
                                player.Inventory.Add(EasterBunnyCostume);
                                player.Currency -= item.Value;
                            }
                        }
                    }
                }
            }
        }

       
    }

}
