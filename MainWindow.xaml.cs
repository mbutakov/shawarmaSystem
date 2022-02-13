using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
using WpfApp2.pages.employee;

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
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        public MainWindow()
        {
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Start();
            mw = this;
            InitializeComponent();
            FrameMain.Navigate(lp);
            Closing += this.OnWindowClosing;
        }
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
        }
        private void timerTick(object sender, EventArgs e)
        {
            if(OrdersList.Instance != null)
            {
                OrdersList.Instance.timerTick10sec();
            }
        }

        private void zzz_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
