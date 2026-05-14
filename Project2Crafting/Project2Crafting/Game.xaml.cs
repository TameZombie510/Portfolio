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
using System.IO;
using System.Runtime.Intrinsics.Arm;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using static Project2Crafting.CraftingInterface;
using static Project2Crafting.Shops;


namespace Project2Crafting
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    /// 

    public enum InterfaceMode
    {
        Setup,
        Buy,
        Craft
    }

    

    public partial class Game : Page
    {
        private string filename = "../../../data/Names.txt";
        
        private Dictionary<string, string> fullNames = new Dictionary<string, string>();
     
        public static Player player = new Player("Player");
        string PlayerLastName;
        public Person Leader = new Person("Aesop");
        public Person BullCultist = new Person("Taurus");
        public Person HorseCultist = new Person("Dullahan");
        public Person GoatCultist = new Person("Gruff");

        int index;

        string[] Locations = { 
            "Meeting Room",
            "Hardware Store",
            "Arts and Crafts Store",
            "Thrift Store"
        };

        string Location = "";

        bool Start;
        bool HardwareFirstTime = true;
        bool ArtsAndCraftsFirstTime = true;
        bool ThriftFirstTime = true;    

        string[] dialogue;

        CraftingInterface workshop = new CraftingInterface();
        InterfaceMode interfaceMode = InterfaceMode.Setup;
        Shop shop = Shop.None;

        public Item Knife = new Item();
        public Item RedChalk = new Item();
        public Item SmallBottle = new Item();
        public Item BlackWax = new Item();
        

        public Game()
        {          
            InitializeComponent();
            Movement.Visibility = Visibility.Visible;
            Movement.Navigate(new MeetingRoom());
            Aesop.Visibility = Visibility.Visible;
            Taurus.Visibility = Visibility.Hidden; 
            Dullahan.Visibility = Visibility.Hidden;    
            Gruff.Visibility = Visibility.Hidden;
            NameBox.Visibility = Visibility.Visible;    
            NameBox.Foreground = Brushes.Red;
            NameBox.Text = "???";
            SpeechBox.Visibility = Visibility.Visible;  
            SpeechBox.Foreground = Brushes.Red;
            AdvanceButton.Visibility = Visibility.Hidden;
            MeetingRoomButton.Visibility = Visibility.Hidden;   
            HardwareStoreButton.Visibility = Visibility.Hidden;
            ArtsAndCraftsStoreButton.Visibility = Visibility.Hidden;    
            ThriftStoreButton.Visibility = Visibility.Hidden;   
            BackButton.Visibility = Visibility.Hidden;
            
            CraftButton.Visibility = Visibility.Hidden;
            BuyButton.Visibility = Visibility.Hidden;
            Workshop.Navigate(new CraftingInterface());
            Workshop.Visibility = Visibility.Hidden;
            

            fullNames = new Dictionary<string, string>();
            string[] lines = File.ReadAllLines(filename);
            Console.WriteLine(lines);
            for (int i = 0; i < lines.Length; i += 2) 
            {             
                string fullname = lines[i]; 
                string[] nameParts = fullname.Split(',');   
                Console.WriteLine(nameParts);
                string FirstName = nameParts[0];    
                string LastName = nameParts[1];
                
                fullNames.Add(FirstName, LastName);
            }

            Start = true;
            Location = Locations[0];
            index = 0;
            SpeechBox.Text = "Greetings, friend. We've been expecting you, and we are pleased that you decided to join us. What might your name be?";
            PlayerName.Visibility = Visibility.Visible;
            InputButton.Visibility = Visibility.Visible;
            
            
        }

        private string AdvanceText(string[] text) 
        {                   
            int currentIndex = index;
           
            SpeechBox.Text = text[currentIndex];
            index += 1;            
            
            return text[currentIndex];
        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {           
            PlayerName.Visibility = Visibility.Hidden;
            InputButton.Visibility = Visibility.Hidden;
            AdvanceButton.Visibility = Visibility.Visible;

            player.Name = PlayerName.Text.ToUpper()[0] + PlayerName.Text.Substring(1).ToLower();
            
            if (player.Name == "Syd")
            {
                SpeechBox.Text = $"{player.Name}...that is a good name...or should I call you...Isabella...?";
            }
            if (player.Name == "Tony") 
            {
                player.Name = "Anthony";
                SpeechBox.Text = $"{player.Name}...that is a good name.";
            }
            else 
            {
                SpeechBox.Text = $"{player.Name}...that is a good name.";
            }

            if (fullNames.ContainsKey(player.Name))
            {
                PlayerLastName = fullNames[player.Name];    
                SpeechBox.Text += $" And your last name...{fullNames[player.Name]}, isn't it...?";
            }
            else if (player.Name == "Oliver") 
            {
                SpeechBox.Text += $" And your last name...would that be Hustis or Woody?";
            }
            
        }      
      
        private void AdvanceButton_Click(object sender, RoutedEventArgs e)
        {
            if (Location == Locations[0]) 
            {
                if (Start == true) 
                {             
                    Begin();
                    if (index != dialogue.Length) 
                    {
                        AdvanceText(dialogue);
                    }                
                }               
            }

            if (Location == Locations[1])
            {
                if (HardwareFirstTime == true)
                {
                    HardwareStoreFirstTime();
                    if (index != dialogue.Length)
                    {
                        AdvanceText(dialogue);
                    }
                }
            }

            if (Location == Locations[2])
            {
                if (ArtsAndCraftsFirstTime == true)
                {
                    ArtsAndCraftsStoreFirstTime();
                    if (index != dialogue.Length)
                    {
                        AdvanceText(dialogue);
                    }
                }
            }

            if (Location == Locations[3])
            {
                if (ThriftFirstTime == true)
                {
                    ThriftStoreFirstTime();
                    if (index != dialogue.Length)
                    {
                        AdvanceText(dialogue);
                    }
                }
            }
        }

        private void HardwareStoreButton_Click(object sender, RoutedEventArgs e)
        {
            Aesop.Visibility = Visibility.Hidden;
            HardwareStoreButton.Visibility = Visibility.Hidden;         
            ArtsAndCraftsStoreButton.Visibility = Visibility.Hidden;
            ThriftStoreButton.Visibility = Visibility.Hidden;
            MeetingRoomButton.Visibility = Visibility.Visible;
            CraftButton.Visibility = Visibility.Hidden;
            BuyButton.Visibility = Visibility.Visible;
            Taurus.Visibility = Visibility.Visible;
            Movement.Navigate(new HardwareStore());
            Location = Locations[1];
            shop = Shop.HardwareStore;

            if (HardwareFirstTime == true) 
            {             
                index = 0;
                NameBox.Visibility = Visibility.Visible;
                SpeechBox.Visibility = Visibility.Visible;
                AdvanceButton.Visibility = Visibility.Visible;
                MeetingRoomButton.Visibility = Visibility.Hidden;
                BuyButton.Visibility = Visibility.Hidden;
                NameBox.Foreground = Brushes.Blue;
                NameBox.Text = "???";
                SpeechBox.Foreground = Brushes.Blue;
                HardwareStoreFirstTime();
                SpeechBox.Text = "Why hello, my friend! You must be the new member.";
            }
            
        
        }
        private void Begin() 
        {
            
            string[] StartingDialogue = {
             $"You may call me {Leader.Name}. Welcome to the Rebirth. The ritual must begin soon, however, we will need a few things. {player.Name}, if you would be so kind, please go out and fetch what we need.",
             "The others own shops in town. They will have what we need, and you will be able to recognize them by the masks they wear. It may be after closing hours, but you will still have to pay them. When you have what you need, you can come back here to put everything together.",
             $"For now, here are the first things that we will need to start the ritual, and some things that might come in handy. Buy what we need, craft what we need, but most importantly...",
             $"...do NOT disappoint me. Run along now, {player.Name}. Our rebirth must begin!"
            };

            dialogue = StartingDialogue;    

            int currentIndex = index;
            
            if (currentIndex == 0)
            {
                NameBox.Text = Leader.Name;
            }
            
            if (currentIndex == StartingDialogue.Length)
            {
                Start = false;
                AdvanceButton.Visibility = Visibility.Hidden;
                NameBox.Visibility = Visibility.Hidden;
                SpeechBox.Visibility = Visibility.Hidden;
                HardwareStoreButton.Visibility = Visibility.Visible;
                ArtsAndCraftsStoreButton.Visibility = Visibility.Visible;
                ThriftStoreButton.Visibility = Visibility.Visible;
                CraftButton.Visibility = Visibility.Visible;

                Knife.Name = "Knife";
                SmallBottle.Name = "Small Bottle";
                RedChalk.Name = "Red Chalk";
                BlackWax.Name = "Black Wax";
                
                player.Currency = 0;
                player.Inventory.Add(Knife);
                player.Inventory.Add(SmallBottle);  
                player.Inventory.Add(RedChalk); 
                player.Inventory.Add(BlackWax); 
            }

            Debug.WriteLine(currentIndex);
        }

        private void HardwareStoreFirstTime() 
        {
            string[] TaurusDialogue = { 
                $"{Leader.Name} told the others and I about you. {player.Name} {PlayerLastName}, is it not? Welcome to my hardware store. I apologize for the lighting, I have been meaning to fix that broken light.", 
                $"My name is {BullCultist.Name}. I am aware that {Leader.Name} has instructed you to help us prepare for the ritual tonight. Feel free to look around for anything you might need."
            };

            dialogue = TaurusDialogue;
            int currentIndex = index;
            if(currentIndex == 1) 
            {
                NameBox.Text = BullCultist.Name;
            }
            

            if (currentIndex == TaurusDialogue.Length) 
            {

                HardwareFirstTime = false;
                NameBox.Visibility = Visibility.Hidden;
                AdvanceButton.Visibility = Visibility.Hidden;
                SpeechBox.Visibility = Visibility.Hidden;
                BuyButton.Visibility = Visibility.Visible;
                MeetingRoomButton.Visibility = Visibility.Visible;            
            }

        }

        private void ArtsAndCraftsStoreFirstTime() 
        {
            string[] DullahanDialogue = {
                $"The name's {HorseCultist.Name}. {Leader.Name} tells me you're helping us prepare for the ritual. I might have some things that we will need, but it will cost you.",
                $"Well, I won't keep you waiting. You really don't want to get on {Leader.Name}'s bad side. Look around and see what you can find. Good luck, lad."
            };

            dialogue = DullahanDialogue;
            int currentIndex = index;
            if (currentIndex == 0)
            {
                NameBox.Text = HorseCultist.Name;
            }


            if (currentIndex == DullahanDialogue.Length)
            {

                ArtsAndCraftsFirstTime = false;
                NameBox.Visibility = Visibility.Hidden;
                AdvanceButton.Visibility = Visibility.Hidden;
                SpeechBox.Visibility = Visibility.Hidden;
                BuyButton.Visibility = Visibility.Visible;
                MeetingRoomButton.Visibility = Visibility.Visible;
            }
        }

        private void ThriftStoreFirstTime() 
        {
            string[] GruffDialogue = { 
                $"Welcome, {player.Name}. Helping with the ritual are we...? Yes...yes...I know...Aesop has told me quite a bit about you. You have met the others, correct?",
                $"My name is {GoatCultist.Name}...now come, look around...don't be afraid. Find what you need for the ritual. I will be right here if you need me."
            };

            dialogue = GruffDialogue;
            int currentIndex = index;
            if (currentIndex == 1)
            {
                NameBox.Text = GoatCultist.Name;
            }


            if (currentIndex == GruffDialogue.Length)
            {

                ThriftFirstTime = false;
                NameBox.Visibility = Visibility.Hidden;
                AdvanceButton.Visibility = Visibility.Hidden;
                SpeechBox.Visibility = Visibility.Hidden;
                BuyButton.Visibility = Visibility.Visible;
                MeetingRoomButton.Visibility = Visibility.Visible;
            }
        }

        private void MeetingRoomButton_Click(object sender, RoutedEventArgs e)
        {
            Aesop.Visibility = Visibility.Visible;
            Taurus.Visibility = Visibility.Hidden;
            Dullahan.Visibility = Visibility.Hidden;
            Gruff.Visibility = Visibility.Hidden;
            HardwareStoreButton.Visibility = Visibility.Visible;          
            ArtsAndCraftsStoreButton.Visibility = Visibility.Visible;
            ThriftStoreButton.Visibility = Visibility.Visible;
            MeetingRoomButton.Visibility = Visibility.Hidden;          
            BuyButton.Visibility = Visibility.Hidden;
            CraftButton.Visibility = Visibility.Visible;
            MeetingRoomButton.Visibility = Visibility.Hidden;
            CraftButton.Visibility = Visibility.Visible;
            Location = Locations[1];
            shop = Shop.None;
            Movement.Navigate(new MeetingRoom());
        }

        private void CraftButton_Click(object sender, RoutedEventArgs e)
        {
            workshop.Store(shop);
            Workshop.Navigate(workshop);
            Workshop.Visibility = Visibility.Visible;
            interfaceMode = InterfaceMode.Craft;
            workshop.Mode(interfaceMode);
            HardwareStoreButton.Visibility = Visibility.Hidden;
            ArtsAndCraftsStoreButton.Visibility = Visibility.Hidden;
            ThriftStoreButton.Visibility = Visibility.Hidden;
            CraftButton.Visibility = Visibility.Hidden;
            Aesop.Visibility = Visibility.Hidden;
            BackButton.Visibility = Visibility.Visible;
            Console.WriteLine(shop.ToString());
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            workshop.Store(shop);
            Workshop.Navigate(workshop);          
            Workshop.Visibility = Visibility.Visible;
            interfaceMode = InterfaceMode.Buy;
            workshop.Mode(interfaceMode);
            MeetingRoomButton.Visibility = Visibility.Hidden;
            BuyButton.Visibility = Visibility.Hidden;
            BackButton.Visibility = Visibility.Visible;
            Taurus.Visibility = Visibility.Hidden;
            Console.WriteLine(shop.ToString());
        }

        private void ArtsAndCraftsStoreButton_Click(object sender, RoutedEventArgs e)
        {
            Dullahan.Visibility = Visibility.Visible;  
            Aesop.Visibility = Visibility.Hidden;
            HardwareStoreButton.Visibility = Visibility.Hidden; 
            ArtsAndCraftsStoreButton.Visibility = Visibility.Hidden;
            ThriftStoreButton.Visibility = Visibility.Hidden;   
            MeetingRoomButton.Visibility = Visibility.Visible;
            CraftButton.Visibility = Visibility.Hidden;
            BuyButton.Visibility = Visibility.Visible;
            Movement.Navigate(new ArtsAndCraftsStore());
            Location = Locations[2];
            shop = Shop.ArtsAndCraftsStore;

            if (ArtsAndCraftsFirstTime == true) 
            {
                index = 0;
                NameBox.Visibility = Visibility.Visible;
                SpeechBox.Visibility = Visibility.Visible;
                AdvanceButton.Visibility = Visibility.Visible;
                MeetingRoomButton.Visibility = Visibility.Hidden;
                BuyButton.Visibility = Visibility.Hidden;
                NameBox.Foreground = Brushes.Green;
                NameBox.Text = "???";
                SpeechBox.Foreground = Brushes.Green;
                ArtsAndCraftsStoreFirstTime();
                SpeechBox.Text = $"Ah, hello lad! You must be {player.Name}.";
            }
        }

        private void ThriftStoreButton_Click(object sender, RoutedEventArgs e)
        {
            Gruff.Visibility = Visibility.Visible;
            Aesop.Visibility = Visibility.Hidden;
            HardwareStoreButton.Visibility = Visibility.Hidden;
            ArtsAndCraftsStoreButton.Visibility = Visibility.Hidden;
            ThriftStoreButton.Visibility = Visibility.Hidden;         
            MeetingRoomButton.Visibility = Visibility.Visible;
            CraftButton.Visibility = Visibility.Hidden;
            BuyButton.Visibility = Visibility.Visible;
            Movement.Navigate(new ThriftStore());
            Location = Locations[3];
            shop = Shop.ThriftStore;

            if (ThriftFirstTime == true) 
            {
                index = 0;
                NameBox.Visibility = Visibility.Visible;
                SpeechBox.Visibility = Visibility.Visible;
                AdvanceButton.Visibility = Visibility.Visible;
                MeetingRoomButton.Visibility = Visibility.Hidden;
                BuyButton.Visibility = Visibility.Hidden;
                NameBox.Foreground = Brushes.Orange;
                NameBox.Text = "???";
                SpeechBox.Foreground = Brushes.Orange;
                ThriftStoreFirstTime();
                SpeechBox.Text = "Hello...friend. Come in...don't be shy...";
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            BackButton.Visibility = Visibility.Hidden;
            Workshop.Visibility = Visibility.Hidden;
           
            
            if (Location == Locations[0]) 
            {
                HardwareStoreButton.Visibility = Visibility.Visible;
                ArtsAndCraftsStoreButton.Visibility = Visibility.Visible;
                ThriftStoreButton.Visibility = Visibility.Visible;
                CraftButton.Visibility = Visibility.Visible;
                Aesop.Visibility = Visibility.Visible;
            }
            else if (Location == Locations[1]) 
            {
                MeetingRoomButton.Visibility = Visibility.Visible;
                BuyButton.Visibility = Visibility.Visible;
                Taurus.Visibility = Visibility.Visible;
            }
            else if(Location == Locations[2]) 
            {
                MeetingRoomButton.Visibility = Visibility.Visible;
                BuyButton.Visibility = Visibility.Visible;
                Dullahan.Visibility = Visibility.Visible;
            }
            else if (Location == Locations[3])
            {
                MeetingRoomButton.Visibility = Visibility.Visible;
                BuyButton.Visibility = Visibility.Visible;
                Gruff.Visibility = Visibility.Visible;
            }

        }
    }   
}
