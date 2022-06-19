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
            if (ex.EmployeeIsAdmin())
            {
                adminHandler();
            }
        }

        public void adminHandler()
        {
            lbEmp.DisplayMemberPath = "Surname";
            containerAdmin.Children.Add(lbEmp);
            Button buttonEmployee = createButton("Работа с сотрудниками", buttonOpenWindowEmpolyeeEditor);
            Button buttonDishEditor = createButton("Добавление блюда", button_DishAdder);
            containerAdmin.Children.Add(buttonEmployee);
            containerAdmin.Children.Add(buttonDishEditor);
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


        public void buttonOpenWindowEmpolyeeEditor(Object sender, EventArgs e)
        {
            new employeeEditor.EmployeeEditor().Show();
          
        }
    }
}
