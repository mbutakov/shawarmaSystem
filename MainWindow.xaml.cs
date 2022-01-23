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
using WpfApp2.database;
using WpfApp2.pages;
namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static fastfoodEntities connection = new fastfoodEntities();
        LoginP lp = new LoginP();
        public static MainWindow mw;
        public MainWindow()
        {
            mw = this;
            InitializeComponent();
            FrameMain.Navigate(lp);


        }

     
    }
}
