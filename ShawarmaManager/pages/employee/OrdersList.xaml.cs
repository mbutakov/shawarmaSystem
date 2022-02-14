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
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
                    StatusOrder = c.Status
                });



            List<Tuple<List<Tuple<Dish, int, int>>, string>> newList =
                new List<Tuple<List<Tuple<Dish, int, int>>, string>>();
            var onlyOrderTable = MainWindow.connection.Order.Where(o => o.Status != "Готов к выдаче");
            var onlyOrderTableToList = onlyOrderTable.ToList();
            for (int i = 0; i < onlyOrderTableToList.Count; i++)
            {
                Order o = new Order();
                o.ID = onlyOrderTableToList[i].ID;
                o.Status = onlyOrderTableToList[i].Status;
                var onlyOneOrderInCompound = GlobalOrder.Where(u => u.OrderId == o.ID);
                List<Tuple<Dish, int, int>> listDishes = new List<Tuple<Dish, int, int>>();

                foreach (var dish in onlyOneOrderInCompound)
                {
                    var dishCurrent = MainWindow.connection.Dish.Where(id => id.ID == dish.DishId).FirstOrDefault();
                    var item = new Tuple<Dish, int, int>(dishCurrent, dish.Count, dish.OrderId);
                    listDishes.Add(item);
                }

                newList.Add(new Tuple<List<Tuple<Dish, int, int>>, string>(listDishes, o.Status));
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
                            StackPanel sp = new StackPanel();
                            var dishList = newList[index].Item1;
                            var orderStatus = newList[index].Item2;
                            foreach (var dish in dishList)
                            {
                                Label button = new Label();
                                button.FontSize = 20;
                                button.Content = dish.Item1.Name + " " + dish.Item2;
                                button.Height = Double.NaN;
                                button.SetValue(Grid.ColumnProperty, j);
                                button.SetValue(Grid.RowProperty, k);
                                sp.Children.Add(button);
                                sp.Tag = dish.Item3;
                            }

                            sp.MouseDown += acceptToWork;
                            if (orderStatus.Equals("В ожиданий"))
                            {
                                sp.Background = new SolidColorBrush(Color.FromArgb(100, 255, 255, 255));
                            }
                            else if (orderStatus.Equals("В работе"))
                            {
                                sp.Background = new SolidColorBrush(Color.FromArgb(150, 255, 255, 0));
                            }
                            else if (orderStatus.Equals("Приготовлен"))
                            {
                                sp.Background = new SolidColorBrush(Color.FromArgb(100, 111, 255, 0));
                            }
                            else
                            {
                                sp.Background = new SolidColorBrush(Color.FromArgb(100, 255, 0, 255));
                            }

                            Grid.SetRow(sp, j);
                            Grid.SetColumn(sp, k);
                            gridListOrders.Children.Add(sp);
                        }

                        index++;
                    }
                }

            }
        }


        public void acceptToWork(object sender, EventArgs e)
        {
            StackPanel sp = (StackPanel) sender;
            int order = Convert.ToInt32(sp.Tag);

            Order orderF = MainWindow.connection.Order.Where(id => id.ID == order).FirstOrDefault();
            var orderC = MainWindow.connection.OrderCompound.Where(id => id.Order == order);
            foreach (var v in orderC)
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
            if (orderF.Status == "В ожиданий")
            {
                orderF.Status = "В работе";
            }
            if (orderF.Status == "В работе")
            {
                orderF.Status = "Приготовлен";
            }
            else if (orderF.Status == "Приготовлен")
            {
                orderF.Status = "Готов к выдаче";
            }


            MainWindow.connection.SaveChanges();

        }

        public void timerTick10sec()
        {
            updateList();
        }
    }
}
