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

namespace FinalProjectFA21
{
    /* PROG 201 Final Project
     * Duncan McDonald
     * 12/12/2021
     * Credit to Janell Baxter for in clss demos: event handlers, delegates, weather API.
     * 
     * Dispatcher information:
     * https://docs.microsoft.com/en-us/dotnet/api/system.windows.threading.dispatcher?view=net-5.0
     * DispatchTimer example by kmatyaszek (https://stackoverflow.com/users/1410998/kmatyaszek)
     * https://stackoverflow.com/questions/16748371/how-to-make-a-wpf-countdown-timer
     */

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigationFrame.Navigate(new StartPage());
        }

        
    }
}
