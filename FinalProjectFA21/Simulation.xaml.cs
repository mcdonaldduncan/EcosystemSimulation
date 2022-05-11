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
using System.Windows.Threading;
using static FinalProjectFA21.Utility;
using static FinalProjectFA21.DataLoad;

namespace FinalProjectFA21
{
    /// <summary>
    /// Interaction logic for Simulation.xaml
    /// </summary>
    public partial class Simulation : Page
    {
        private DispatcherTimer timer;
        private TimeSpan timeSpan;
        private int currentDay = 0;
        private int shopIndex = 0;
        private int inventoryIndex = 0;

        Ecosystem ecosystem = new Ecosystem();
        Player player = new Player();
        Shop shop = new Shop();
        

        
        public void ShowData()
        {
            CottonWormCount.Text = FindEntity(ecosystem.entities, "Cotton Bollworm").Count.ToString();
            CornWormCount.Text = FindEntity(ecosystem.entities, "Corn earworm").Count.ToString();
            CornCount.Text = FindEntity(ecosystem.entities, "Corn").Count.ToString();
            CottonCount.Text = FindEntity(ecosystem.entities, "Cotton").Count.ToString();
            BatCount.Text = FindEntity(ecosystem.entities, "Brazilian free-tailed bat").Count.ToString();
            HawkCount.Text = FindEntity(ecosystem.entities, "Red-tailed hawk").Count.ToString();
            SoilCount.Text = FindEntity(ecosystem.entities, "Soil").Count.ToString();
            BioticCount.Text = (FindEntity(ecosystem.entities, "Guano").Count + FindEntity(ecosystem.entities, "Biotic").Count).ToString();
            BeetleCount.Text = (FindEntity(ecosystem.entities, "Dermestid beetle").Count + FindEntity(ecosystem.entities, "Guano Beetle").Count).ToString();
            CurrentDay.Text = $"Day: {currentDay}";
            testblock.Text = ecosystem.HandlerWrite();
            Status.Text = ecosystem.status.ToString();
            CurrentWeather.Text = $"Temp: {ecosystem.ReturnTemp()}";

        }

        public Simulation()
        {
            InitializeComponent();
            testblock.Text = ecosystem.WriteList();
            
        }

        //CREDITS
        //Dispatcher information:
        //https://docs.microsoft.com/en-us/dotnet/api/system.windows.threading.dispatcher?view=net-5.0
        //DispatchTimer example by kmatyaszek (https://stackoverflow.com/users/1410998/kmatyaszek)
        //https://stackoverflow.com/questions/16748371/how-to-make-a-wpf-countdown-timer

        private void GameLoop()
        {
            currentDay++;
            ecosystem.IterateEcosystem();
            RenewShop();
            ShowData();
            Counter();
        }

        private void Counter()
        {
            //DispatchTimer example by kmatyaszek (https://stackoverflow.com/users/1410998/kmatyaszek)
            timeSpan = TimeSpan.FromSeconds(5);

            timer = new DispatcherTimer(
                new TimeSpan(0, 0, 1),
                DispatcherPriority.Normal,
                delegate
                {
                    //CurrentDay.Text = timeSpan.ToString("c");
                    ShowData();
                    
                    
                    

                    if (timeSpan == TimeSpan.Zero)
                    {
                        timer.Stop();
                        GameLoop();
                        //Show the "next day" button
                        //btnNextDay.Visibility = Visibility.Visible;
                        //call game loop
                    }
                    timeSpan = timeSpan.Add(TimeSpan.FromSeconds(-1));
                },
                Application.Current.Dispatcher);

            timer.Start();
        }

        private void InitializeShop() => shop.powerups = new List<Powerup>
        {
            new Powerup("Rejuvenate","Replenishes Ecosystem", "Biotic", 10000),
            new Powerup("Eviscerate","Reduce the population of all creatures by 1000", "Red-tailed hawk", 100),
            new Powerup("Renew Predators","Replenish the predators of the ecosystem", "Dermestid beetle", 1000),
            new Powerup("Winter Frost","Make the weather colder", "Corn earworm", 1000),
            new Powerup("Summmer Heat","Make the weather warmer", "Cotton Bollworm", 1000),
            new Powerup("","","", 0)
        };

        private void InitializeInventory() => player.powerups = new List<Powerup>
        {
            new Powerup("Reset","Reset Ecosystem", "Guano", 1000),
            new Powerup("","","", 0),
            new Powerup("Harvest", "Harvest 1000 corn and cotton", "", 0)

        };

        private void RenewShop()
        {
            if (currentDay%10 == 0)
            {
                InitializeShop();
            }
            
        }

        private void WriteInventory()
        {
            Inventory_ItemName.Text = player.powerups[inventoryIndex].Name;
            Inventory_ItemEffect.Text = player.powerups[inventoryIndex].Effect;
        }

        //Event Handlers
        private void EcoPage_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeShop();
            InitializeInventory();
            DataContext = shop.powerups[shopIndex];
            WriteInventory();
            Counter();
        }

        private void ShopNext_Click(object sender, RoutedEventArgs e)
        {
            shopIndex++;
            if (shopIndex >= shop.powerups.Count)
            {
                shopIndex = 0;
                DataContext = shop.powerups[shopIndex];
            }
            else
            {

                DataContext = shop.powerups[shopIndex];
            }

        }

        private void InventoryNext_Click(object sender, RoutedEventArgs e)
        {
            inventoryIndex++;
            if (inventoryIndex >= player.powerups.Count)
            {
                inventoryIndex = 0;
                WriteInventory();
            }
            else
            {

                WriteInventory();
            }
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            if (shop.powerups[shopIndex].Name == "")
            {

            }
            else if (FindEntity(ecosystem.entities, shop.powerups[shopIndex].CostSpecies).Count > shop.powerups[shopIndex].CostAmount)
            {
                player.powerups.Add(shop.powerups[shopIndex]);
                FindEntity(ecosystem.entities, shop.powerups[shopIndex].CostSpecies).Count -= shop.powerups[shopIndex].CostAmount;
                shop.powerups.Remove(shop.powerups[shopIndex]);
                shopIndex = 0;
                DataContext = shop.powerups[shopIndex];
            }
            
        }

        private void UseButton_Click(object sender, RoutedEventArgs e)
        {
            if (player.powerups[inventoryIndex].Name == "")
            {

            }
            else
            {
                if (player.powerups[inventoryIndex].Name == "Reset")
                {
                    ecosystem.entities = LoadEntities("../../data/entities.xml");
                }
                else if (player.powerups[inventoryIndex].Name == "Rejuvenate")
                {
                    for (int i = 0; i < ecosystem.entities.Count; i++)
                    {
                        ecosystem.entities[i].Count += 1000;
                    }
                }
                else if (player.powerups[inventoryIndex].Name == "Renew Predators")
                {
                    FindEntity(ecosystem.entities, "Red-tailed hawk").Count += 100;
                    FindEntity(ecosystem.entities, "Brazilian free-tailed bat").Count += 1000;
                }
                else if (player.powerups[inventoryIndex].Name == "Winter Frost")
                {
                    ecosystem.currentTemperature.Temperature -= 15;
                }
                else if (player.powerups[inventoryIndex].Name == "Summer Heat")
                {
                    ecosystem.currentTemperature.Temperature += 15;
                }
                else if (player.powerups[inventoryIndex].Name == "Eviscerate")
                {
                    foreach (Entity i in ecosystem.entities)
                    {
                        i.Count -= 1000;
                    }
                }
                else if (player.powerups[inventoryIndex].Name == "Harvest")
                {
                    FindEntity(ecosystem.entities, "Corn").Count -= 1000;
                    FindEntity(ecosystem.entities, "Cotton").Count -= 1000;
                }
                else
                {

                }
                player.powerups.Remove(player.powerups[inventoryIndex]);
                inventoryIndex = 0;
                WriteInventory();
                //Change remove to call only for non reusables.
                //Make this whole thing dynamic, blegh

            }

            
        }

    }
}
