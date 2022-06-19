using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using ShawarmaManager.database;
using ShawarmaManager.pages.employee;

namespace ShawarmaManager.pages
{
    /// <summary>
    /// Логика взаимодействия для LoginP.xaml
    /// </summary>
    public partial class LoginP : Page
    {
        public LoginP()
        {
            InitializeComponent();
        }

        private void authSucces(Employee e)
        {
            MessageBox.Show("Успешная авторизация");
            EmployeeEx empEx = new EmployeeEx(e);
            if (empEx.EmployeeIsAdmin())
            {
                NavigationService.Navigate(new Main(e));
            }
            else
            {
                new OrdersList().Show();
                MainWindow.mw.Close();
            }
        }

        private void authFail()
        {
            MessageBox.Show("пароль не подходит");
        }

        private void authFail(string message)
        {
            MessageBox.Show(message);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string pass = passBox.Password;
            string login = loginBox.Text;
            int count = 0;
            foreach (Employee p in MainWindow.connection.Employee.Where(p => p.Phone == login))
            {
                count++;
                if (p.Password == pass)
                {
                    authSucces(p);
                }
                else
                {
                    authFail();
                }
            }
            if(count < 1)
            {
                authFail("Пользователь не найден!");
            }
        }

        private void loginBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void zzz_Click(object sender, RoutedEventArgs e)
        {

        }

        private void loginBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void loginBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }
    }
}
