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

namespace BicycleRentalWPF
{
    /// <summary>
    /// These methods are for the MainWindow.xaml (login screen) button listens
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Will close the GUI once EXIT is clicked
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        //Will switch out screens once SUBMIT is clicked
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            mainMenu mainMenu = new mainMenu();

            this.Hide();
            mainMenu.Show();

        }
    }
}
