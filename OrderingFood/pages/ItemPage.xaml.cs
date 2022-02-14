using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using OrderingFood.utils;

namespace OrderingFood.pages
{
    /// <summary>
    /// Логика взаимодействия для ItemPage.xaml
    /// </summary>
    public partial class ItemPage : Page
    {
        public static List<Dish> dishList = new List<Dish>();
        public static List<Dish> prevDishList = new List<Dish>();
        public static List<Grid> gridList = new List<Grid>();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public static List<Tuple<Dish, int>> dishListToOrder = new List<Tuple<Dish, int>>();

        public ItemPage()
        {
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            setItemList();
            prevDishList = dishList;
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            
            if (prevDishList != dishList)
            {
                prevDishList = dishList;
                refreshList();
            }
        }
        static double RoundToSignificantDigits(double d, int digits)
        {
            if (d == 0)
                return 0;

            double scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(d))) + 1);
            return scale * Math.Round(d / scale, digits);
        }
        private void refreshList()
        {
           // gridDishes.Children.Clear();
            gridList = new List<Grid>();
            double countGrids = 0;
            double operac = (float) prevDishList.Count / (float) 9;
            double number = RoundToSignificantDigits(operac, 3);
            if (Convert.ToInt32(operac) == Convert.ToDouble(operac))
            {
                //если число целое и не имеет цифр после точек
                countGrids = operac;
            }
            else
            {  
                if (number > Convert.ToInt32(number))
                {
                    countGrids = RoundToSignificantDigits(number, 1) + 1;
                }
                else if(number < Convert.ToInt32(number))
                {
                    countGrids = Convert.ToInt32(number);
                }
            }

            int r = 0;
            for (int i = 0; i < countGrids; i++)
            {
                Grid grid = new Grid();
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                

                for (int k = 0; k < grid.ColumnDefinitions.Count; k++)
                {
                    grid.ColumnDefinitions[k].Width = new GridLength(1,GridUnitType.Star); ;
                }

                for (int k = 0; k < grid.RowDefinitions.Count; k++)
                {
                    grid.RowDefinitions[k].Height = new GridLength(200); ;
                }

                gridList.Add(grid);
            }


            int index = 0;
            for (int i = 0; i < gridList.Count; i++)
            {
                for (int j = 0; j < 3; j++){
                    for (int k = 0; k < 3; k++)
                    {
                        if (index < dishList.Count)
                        {
                            Button button = new Button();
                            button.Content = prevDishList[index].Name + " Цена: " + prevDishList[index].Price;
                            button.Height = Double.NaN;
                            button.SetValue(Grid.ColumnProperty, j);
                            button.SetValue(Grid.RowProperty, k);
                            button.Tag = prevDishList[index];
                            button.Click += addToListDishHandler;
                            Grid.SetRow(button, j);
                            Grid.SetColumn(button, k);
                            gridList[i].Children.Add(button);
                        }
                        index++;
                    }
                    
                }


            }

            index = 0;



            for (int i = 0; i < gridList.Count; i++)
            {
                StackPanelAllGrid.Children.Add(gridList[i]);
            }
        }

        private void refreshListOrder()
        {
            DishListAdded.Items.Clear();
            foreach (var dish in dishListToOrder)
            {
                DishListAdded.Items.Add(dish);
            }

            
        }

        public void addToListDishHandler(object sender, EventArgs e)
        {
            Dish dish = (Dish)((Control)sender).Tag;
            var item = new Tuple<Dish, int>(dish, 1);
            int index = 0;
            bool find = false;
            foreach (var di in dishListToOrder)
            {
                if (di.Item1.Name == item.Item1.Name)
                {
                    var newTuple = new Tuple<Dish, int>(item.Item1, di.Item2 + 1);
                    newTuple.Item1.Price += 100;
                    dishListToOrder.RemoveAt(index);
                    dishListToOrder.Insert(index, newTuple);
                    find = true;
                    break;
                }
                index++;
            }

            if (find)
            {
          
            }
            else
            {
                dishListToOrder.Add(item);
            }

            refreshListOrder();

        }
        
        static bool Divisibility(int n)
        {
            if (n % 3 == 0)
            {
                return true;
            }
            return false;
        }

        public async void setItemList()
        {
            await Task.WhenAll(ApiFunc.getItemDish());
            
        }

        private async void ButtonOrder_Click(object sender, RoutedEventArgs e)
        {
            await apiOrderSendPosTask();
            refreshListOrder();
            dishListToOrder.Clear();
            DishListAdded.Items.Clear();

        }


        private async static Task apiOrderSendPosTask()
        {
            List<DishEx> dishListToOrderJsonOptimization = new List<DishEx>();
            foreach (var dish in dishListToOrder)
            {
                DishEx de = new DishEx();
                de.Count = dish.Item2;
                de.Name = dish.Item1.Name;
                de.Id = dish.Item1.Id;
                de.Price = dish.Item1.Price;
                dishListToOrderJsonOptimization.Add(de);
            }
             var stringPayload = JsonConvert.SerializeObject(dishListToOrderJsonOptimization);
            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();
            // Do the actual request and await the response
            var httpResponse = await httpClient.PostAsync("http://localhost:8080/acceptOrder", httpContent);
            // If the response contains content we want to read it!
            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                MessageBox.Show("Заказ успешно оформлен");
            }
        }
    }
}
