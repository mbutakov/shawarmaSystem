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
using  OrderingFood.pages;


namespace OrderingFood
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ItemPage itemPage = new ItemPage();
            MainFrame.Navigate(itemPage);
            mainWindow.Visibility = Visibility.Collapsed;
            mainWindow.ResizeMode = ResizeMode.NoResize;
            mainWindow.WindowState = WindowState.Maximized;
            //mainWindow.Topmost = true;
            mainWindow.Visibility = Visibility.Visible;

        }
    }
}
