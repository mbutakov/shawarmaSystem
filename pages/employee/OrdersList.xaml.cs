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
using System.Windows.Shapes;
using WpfApp2.database;

namespace WpfApp2.pages.employee
{
    /// <summary>
    /// Логика взаимодействия для OrdersList.xaml
    /// </summary>
    public partial class OrdersList : Window
    {
        public OrdersList()
        {
            InitializeComponent();
            int orderCount = 0;

            var GlobalOrder = MainWindow.connection.OrderCompound.Join(MainWindow.connection.Order, u => u.Order, c => c.ID, (u, c) => new
            {
                DishId = u.Dish,
                Status = u.Status,
                OrderId = u.Order
            });


            foreach (var go in GlobalOrder)
            {
                var Dish = MainWindow.connection.Dish.Where(id => id.ID == go.DishId).FirstOrDefault();
                Button button = new Button();
                button.Content = Dish.Name;
                if (go.Status.Equals("В ожиданий"))
                {
                    button.Click += acceptToWork;
                    button.Tag = go.OrderId;
                    ListWaiting.Children.Add(button);
                }
               // else if (go.Status.Equals("В работе"))
                //{
                  //  ListWork.Children.Add(button);
                //}
            }

            foreach (Order o in MainWindow.connection.Order)
            {
                orderCount++;
            }
            counter.Content = orderCount + " ";
        }

        public void acceptToWork(object sender, EventArgs e)
        {
            int order = Convert.ToInt32(((Control)sender).Tag);
            ListWaiting.Children.Remove((UIElement)sender);
            ListWork.Children.Add((UIElement)sender);
            Button button = (Button)sender;
            button.Click -= acceptToWork;
            button.Click += workCancel;
            Order orderF = MainWindow.connection.Order.Where(id => id.ID == order).FirstOrDefault();
            OrderCompound orderC = MainWindow.connection.OrderCompound.Where(id => id.Order == order).FirstOrDefault();
            orderF.Status = orderC.Status = "В работе";
            MainWindow.connection.SaveChanges();

        }

        public void workCancel(object sender, EventArgs e)
        {
            int order = Convert.ToInt32(((Control)sender).Tag);
            ListWork.Children.Remove((UIElement)sender);
            Order orderF = MainWindow.connection.Order.Where(id => id.ID == order).FirstOrDefault();
            OrderCompound orderC = MainWindow.connection.OrderCompound.Where(id => id.Order == order).FirstOrDefault();
            orderF.Status = orderC.Status = "Приготовлен";
            MainWindow.connection.SaveChanges();

        }
    }
}
