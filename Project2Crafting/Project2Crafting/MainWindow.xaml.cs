using System.Numerics;
using System;
using System.Text;
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

namespace Project2Crafting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /* PROG 201 Craft Project: The Ritual
         * Max Polancic
         * 5/5/24
         * Credits:
         * Images: Google
         * Characters made in Scratch
         * Code by Max Polancic(me) with influences from Janell Baxter*/

        public static List<Recipe> Recipes = FileData();
        
        public MainWindow()
        {
            InitializeComponent();
            Recipes = FileData();
            BeginButton.Visibility = Visibility.Visible;
            TitleFrame.Visibility = Visibility.Hidden;
        }

        private void BeginButton_Click(object sender, RoutedEventArgs e)
        {
            BeginButton.Visibility = Visibility.Hidden;
            TitleFrame.Visibility = Visibility.Visible;
            TitleFrame.Navigate(new Game());
        }

        

    }


}