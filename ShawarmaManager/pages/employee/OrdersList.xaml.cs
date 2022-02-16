using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using ShawarmaManager.database;

namespace ShawarmaManager.pages.employee
{
    /// <summary>
    /// Логика взаимодействия для OrdersList.xaml
    /// </summary>
    public partial class OrdersList : Window
    {

        public static OrdersList Instance;

        public OrdersList()
        {
            InitializeComponent();
            Instance = this;
        }


        public void updateList()
        {
            gridListOrders.Children.Clear();
            var GlobalOrder = MainWindow.connection.OrderCompound.Join(MainWindow.connection.Order, u => u.Order,
                c => c.ID, (u, c) => new
                {
                    DishName = u.Dish1.Name,
                    DishId = u.Dish,
                    Status = u.Status,
                    OrderId = u.Order,
                    Count = u.Count,
                    StatusOrder = c.Status,
                    DateOrder = c.Date
                });



            List<Tuple<List<Tuple<Dish, int, int>>, string,DateTime>> newList = new List<Tuple<List<Tuple<Dish, int, int>>, string, DateTime>>();
            var onlyOrderTable = MainWindow.connection.Order.Where(o => o.Status != "Готов к выдаче");
            var onlyOrderTableToList = onlyOrderTable.ToList();
            for (int i = 0; i < onlyOrderTableToList.Count; i++)
            {
                Order o = new Order();
                o.ID = onlyOrderTableToList[i].ID;
                o.Status = onlyOrderTableToList[i].Status;
                o.Date = onlyOrderTableToList[i].Date;
                var onlyOneOrderInCompound = GlobalOrder.Where(u => u.OrderId == o.ID);
                List<Tuple<Dish, int, int>> listDishes = new List<Tuple<Dish, int, int>>();

                foreach (var dish in onlyOneOrderInCompound)
                {
                    var dishCurrent = MainWindow.connection.Dish.Where(id => id.ID == dish.DishId).FirstOrDefault();
                    var item = new Tuple<Dish, int, int>(dishCurrent, dish.Count, dish.OrderId);
                    listDishes.Add(item);
                }

                newList.Add(new Tuple<List<Tuple<Dish, int, int>>, string,DateTime>(listDishes, o.Status,o.Date));
            }

            int index = 0;
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        if (index < newList.Count)
                        {
                           
                            var dishList = newList[index].Item1;
                            var orderStatus = newList[index].Item2;
                            var orderDateTime = newList[index].Item3;
                            Border borderOrder = new Border();
                            Grid gridInStack = new Grid();
                            gridInStack.RowDefinitions.Add(new RowDefinition());
                            gridInStack.RowDefinitions.Add(new RowDefinition());
                            gridInStack.RowDefinitions.Add(new RowDefinition());
                            gridInStack.RowDefinitions[0].Height = new GridLength(0.25, GridUnitType.Star);
                            gridInStack.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);
                            gridInStack.RowDefinitions[2].Height = new GridLength(0.25, GridUnitType.Star);
                            Viewbox viewboxStackPanel = new Viewbox();
                            StackPanel sp = new StackPanel();
                            foreach (var dish in dishList)
                            {
                                int count = dish.Item2;
                                TextBlock tbDishItem = new TextBlock();
                                tbDishItem.FontSize = 13;
                                if (count < 2)
                                {
                                    tbDishItem.Text = dish.Item1.Name;
                                }
                                else
                                {
                                    tbDishItem.Text = dish.Item1.Name + "  " + dish.Item2;
                                }
                                tbDishItem.Height = Double.NaN;
                                tbDishItem.TextAlignment = TextAlignment.Left;
                                tbDishItem.VerticalAlignment = VerticalAlignment.Top;
                                tbDishItem.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                                tbDishItem.SetValue(Typography.CapitalsProperty, FontCapitals.AllSmallCaps);
                                tbDishItem.SetValue(FontFamilyProperty, FindResource("listLabelStyleFF"));
                                sp.Children.Add(tbDishItem);
                                borderOrder.Tag = dish.Item3;
                            }
                            viewboxStackPanel.SetValue(Grid.RowProperty,1);
                            viewboxStackPanel.Stretch = Stretch.Uniform;
                            viewboxStackPanel.HorizontalAlignment = HorizontalAlignment.Left;
                            borderOrder.MouseDown += acceptToWork;
                            borderOrder.BorderBrush = Brushes.Black;
                            borderOrder.BorderThickness = new Thickness(1, 1, 1, 1);
                            Viewbox viewboxTitle = new Viewbox();
                            TextBlock labelTitle = new TextBlock();
                            if (dishList == null || dishList.Count < 1)
                            {
                                labelTitle.Text = " Заказ: пустой" + " " + orderStatus + " ";
                            }
                            else
                            {
                                labelTitle.Text = " Заказ: " + dishList[0].Item3 + " " + orderStatus + " ";
                            }
                          
                            labelTitle.FontSize = 15;
                            labelTitle.SetValue(FontWeightProperty, FindResource("titleLabelStyleFW"));
                            labelTitle.SetValue(FontFamilyProperty, FindResource("listLabelStyleFF"));
                            labelTitle.VerticalAlignment = VerticalAlignment.Center;
                            labelTitle.TextAlignment = TextAlignment.Center;
                            viewboxTitle.SetValue(Grid.RowProperty, 0);
                            viewboxTitle.Child = labelTitle;
                            viewboxTitle.Stretch = Stretch.Uniform;
                            viewboxStackPanel.Margin = new Thickness(10, 1, 0, 1);
                            if (orderStatus.Equals("В ожиданий"))
                            {
                                gridInStack.Background = new SolidColorBrush(Color.FromArgb(100, 255, 255, 255));
                            }
                            else if (orderStatus.Equals("В работе"))
                            {
                                gridInStack.Background = new SolidColorBrush(Color.FromArgb(100, 255, 255, 0));
                            }
                            else if (orderStatus.Equals("Приготовлен"))
                            {
                                gridInStack.Background = new SolidColorBrush(Color.FromArgb(100, 111, 255, 0));
                            }
                            else
                            {
                                gridInStack.Background = new SolidColorBrush(Color.FromArgb(100, 255, 0, 255));
                            }



                            Viewbox ViewBoxToGridBottom = new Viewbox();
                            Grid gridBottom = new Grid();
                            gridBottom.ColumnDefinitions.Add(new ColumnDefinition());
                            gridBottom.ColumnDefinitions.Add(new ColumnDefinition());
                            gridBottom.ColumnDefinitions.Add(new ColumnDefinition());
                            gridBottom.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                            gridBottom.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                            gridBottom.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);
                            TextBlock TextBlockOrderTime = new TextBlock();
                            TextBlock TextBlockTimePassed = new TextBlock();
                            TextBlockTimePassed.FontSize = 3;
                            TextBlockOrderTime.FontSize = 3;
                            ViewBoxToGridBottom.Margin = new Thickness(1, 10, 1, 0);
                            TextBlockOrderTime.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                            ViewBoxToGridBottom.Child = gridBottom;
                            gridBottom.Children.Add(TextBlockOrderTime);
                            TextBlockOrderTime.Text = orderDateTime.Hour + ":" + orderDateTime.Minute + ":" + orderDateTime.Second + " ";
                            TextBlockOrderTime.SetValue(Grid.ColumnProperty,0);
                            gridBottom.Children.Add(TextBlockTimePassed);
                            var timePassed = (DateTime.Now - orderDateTime).Duration();
                            colorTimeHandler(TextBlockTimePassed, timePassed);
                            TextBlockTimePassed.Text = timePassed.Minutes + ":" + timePassed.Seconds + "";
                            TextBlockTimePassed.SetValue(Grid.ColumnProperty, 1);
                            ViewBoxToGridBottom.SetValue(Grid.RowProperty,2);
                            Grid.SetRow(borderOrder, j);
                            Grid.SetColumn(borderOrder, k);
                            viewboxStackPanel.Child = sp;
                            gridInStack.Children.Add(viewboxStackPanel);
                            gridInStack.Children.Add(viewboxTitle);
                            gridInStack.Children.Add(ViewBoxToGridBottom);
                            borderOrder.Child = gridInStack;
                            borderOrder.Margin = new Thickness(2);
                            gridListOrders.Children.Add(borderOrder);
                        }

                        index++;
                    }
                }

            }
        }

        public void colorTimeHandler(TextBlock tb,TimeSpan timePassed)
        {
            if (timePassed.Minutes < 3)
            {
                tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 51, 153, 0));
            }
            else if (timePassed.Minutes >= 3 && timePassed.Minutes < 5)
            {
                tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 153, 204,51));
            }
            else if (timePassed.Minutes >= 5 && timePassed.Minutes < 10)
            {
                tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 204, 0));
            }
            else if (timePassed.Minutes >= 10 && timePassed.Minutes < 15)
            {
                tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 153, 102));
            }
            else if (timePassed.Minutes >= 15)
            {
                tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 204, 51, 0));
            }
        }

        public void acceptToWork(object sender, EventArgs e)
        {
            try
            {
                Border border = (Border)sender;
                int order = Convert.ToInt32(border.Tag);
                Order orderF = MainWindow.connection.Order.Where(id => id.ID == order).FirstOrDefault();
                if (orderF == null)
                {
                    MessageBox.Show("Заказ пустой или с ошибкой");
                    return;
                }
                var orderCompounds = MainWindow.connection.OrderCompound.Where(id => id.Order == order);
                if (orderCompounds != null && orderCompounds.Count() > 1)
                {
                    foreach (var v in orderCompounds)
                    {
                        if (v.Status == "В ожиданий")
                        {
                            v.Status = "В работе";
                        }
                        else
                        {
                            v.Status = "Приготовлен";
                        }
                    }
                }
                if (orderF.Status == "В ожиданий")
                {
                    orderF.Status = "В работе";
                }
                else if (orderF.Status == "В работе")
                {
                    orderF.Status = "Приготовлен";
                }
                else if (orderF.Status == "Приготовлен")
                {
                    orderF.Status = "Готов к выдаче";
                }
                MainWindow.connection.SaveChanges();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                throw;
            }
          

        }

        public void timerTick10sec()
        {
            updateList();
        }
    }
}
