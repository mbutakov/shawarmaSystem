using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
using ShawarmaManager.pages.dishEditor;


namespace ShawarmaManager.pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        private static Employee thisUser;
        static ListBox lbEmp = new ListBox();

        public Main(Employee e)
        {
            InitializeComponent();
            thisUser = e;
            EmployeeEx ex = new EmployeeEx(e);
            nameTitle.Content = "Ваше имя: " + e.Name + " ";
            if (ex.EmployeeIsAdmin())
            {
                adminHandler();
            }
        }

        public void adminHandler()
        {
            Label label = new Label();
            label.Content = "Выберите сотрудника:";
            lbEmp.DisplayMemberPath = "Surname";
            refreshListUser();
            containerAdmin.Children.Add(label);
            containerAdmin.Children.Add(lbEmp);
            Button buttonDelete = createButton("Удалить сотрудника",buttonRemoveUserByAdmin);
            Button buttonDishEditor = createButton("Добавление блюда", button_DishAdder);
            Button buttonDishList = createButton("Список/редакция блюда", button_DishList);
            containerAdmin.Children.Add(buttonDelete);
            containerAdmin.Children.Add(buttonDishEditor);
        }


        public static void button_DishList2(Object sender, EventArgs e)
        {
            
        }

        public void button_DishList(Object sender, EventArgs e)
        {
            new dishEditorW().Show();

        }
        public void button_DishAdder(Object sender, EventArgs e)
        {
            new dishEditorW().Show();
        }


            public static Button createButton(string text,RoutedEventHandler r)
        {
            Button btn = new Button();
            btn.Content = text;
            if(r!= null)
            {
                btn.Click += r;
            }
            return btn;
        }

            public void refreshListUser()
        {
            lbEmp.Items.Clear();
            foreach (Employee p in MainWindow.connection.Employee)
            {
                if (p != thisUser)
                {
                    lbEmp.Items.Add(p);
                }
            }
            
        }

        public void buttonRemoveUserByAdmin(Object sender, EventArgs e)
        {
            Object ob = lbEmp.SelectedItem;
            if(ob is Employee)
            {
                Employee emp = (Employee)ob;
                MainWindow.connection.Employee.Remove(emp);
                int i = MainWindow.connection.SaveChanges();
                if(i < 1)
                {
                    MessageBox.Show("Не успешное удаление");
                }
                else
                {
                    MessageBox.Show("Успешное удаление ");
                }
                refreshListUser();
            }
          
        }
    }
}
