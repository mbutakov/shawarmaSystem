using SharpVectors.Converters;
using ShawarmaManager.database;
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

namespace ShawarmaManager.pages.employeeEditor
{
    /// <summary>
    /// Логика взаимодействия для EmployeeEditor.xaml
    /// </summary>
    public partial class EmployeeEditor : Window
    {
        public EmployeeEditor()
        {
            InitializeComponent();
        }

        private void Button_Click_add(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.RemoveRange(2, MainGrid.Children.Count);
            EmployeeToEdit = null;
            openAddEmployee();
        }

            private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.RemoveRange(2, MainGrid.Children.Count);
            openEdit();
            
        }


        public void openAddEmployee()
        {
            Grid grid = new Grid();
            Grid.SetColumn(grid, 1);
            MainGrid.Children.Add(grid);
            StackPanel sp = new StackPanel();
            grid.Children.Add(sp);
            TextBox tbPhone = new TextBox() { Name = "TBPHONEADD", Text = "Телефон", Background = Brushes.White, Width = 250, Padding = new Thickness(15), VerticalAlignment = VerticalAlignment.Stretch, TextAlignment = TextAlignment.Center, Foreground = Brushes.Black };
            TextBox tbPassword = new TextBox() { Name = "TBPASSWORDADD", Text = "Пароль", Background = Brushes.White, Width = 250, Padding = new Thickness(15), VerticalAlignment = VerticalAlignment.Stretch, TextAlignment = TextAlignment.Center, Foreground = Brushes.Black };
            TextBox tbSurname = new TextBox() { Name = "TBSURNAMEADD", Text = "Фамилия", Background = Brushes.White, Width = 250, Padding = new Thickness(15), VerticalAlignment = VerticalAlignment.Stretch, TextAlignment = TextAlignment.Center, Foreground = Brushes.Black };
            TextBox tbName = new TextBox() { Name = "TBNAMEADD", Text = "Имя", Background = Brushes.White, Width = 250, Padding = new Thickness(15), VerticalAlignment = VerticalAlignment.Stretch, TextAlignment = TextAlignment.Center, Foreground = Brushes.Black };
            TextBox tbPatronymic = new TextBox() { Name = "TBPATRONYMICADD", Text = "Отчество", Background = Brushes.White, Width = 250, Padding = new Thickness(15), VerticalAlignment = VerticalAlignment.Stretch, TextAlignment = TextAlignment.Center, Foreground = Brushes.Black };
            tbPhone.Margin = new Thickness(0, 50, 10, 0);
            tbPassword.Margin = new Thickness(0, 0, 10, 0);
            tbSurname.Margin = new Thickness(0, 0, 10, 0);
            tbName.Margin = new Thickness(0, 0, 10, 0);
            tbPatronymic.Margin = new Thickness(0, 0, 10, 0);
            sp.Children.Add(tbPhone);
            sp.Children.Add(tbPassword);
            sp.Children.Add(tbSurname);
            sp.Children.Add(tbName);
            sp.Children.Add(tbPatronymic);
            Button btnAdd = new Button();
            btnAdd.Click += onClickButtonAdd;
            btnAdd.Content = "Добавить";
            btnAdd.Width = 150;
            sp.Children.Add(btnAdd);
        }


        public void onClickButtonAdd(object sender, EventArgs ea)
        {
            Employee e = new Employee();
            e.Phone = FindChild<TextBox>(this, "TBPHONEADD").Text;
            e.Password = FindChild<TextBox>(this, "TBPASSWORDADD").Text;
            e.Surname = FindChild<TextBox>(this, "TBSURNAMEADD").Text;
            e.Name = FindChild<TextBox>(this, "TBNAMEADD").Text;
            e.Patronymic = FindChild<TextBox>(this, "TBPATRONYMICADD").Text;

            if(e.Phone.Length > 10)
            {
                MessageBox.Show("Номер телефона должен содержать не больше 10 цифр");
                return;
            }

            if(MainWindow.connection.Employee.Find(e.Phone) != null)
            {
                MessageBox.Show("Номер телефона уже занят");
            }
            else
            {

                MainWindow.connection.Employee.Add(e);
                MainWindow.connection.SaveChanges();
                MessageBox.Show("Успешно");
                MainGrid.Children.RemoveRange(2, MainGrid.Children.Count);
            }

        }




        public void openEdit()
        {
            DataGrid dataGrid = new DataGrid() { IsReadOnly=true,Name="DataGridEmployeeListToEdit"};
            MainGrid.Children.Add(dataGrid);
            Grid.SetColumn(dataGrid, 1);
            dataGrid.Items.Clear();
            dataGrid.MouseUp += onMouseUpDataGrid;
            List<EmployeeListElement> result = new List<EmployeeListElement>(3);
            foreach (Employee ee in MainWindow.connection.Employee)
            {
                result.Add(new EmployeeListElement(ee));

            }
            dataGrid.ItemsSource = result;

        }

        class EmployeeListElement
        {
            public EmployeeListElement(Employee e)
            {
                this.Телефон = e.Phone;
                this.Фио = e.Surname + " " + e.Name + " " + e.Patronymic;
            }
            public string Телефон { get; set; }
            public string Фио { get; set; }

        }

        public void onMouseUpDataGrid(object sender, EventArgs e)
        {
            EmployeeListElement path = ((DataGrid)sender).SelectedItem as EmployeeListElement;
            if(path != null)
            {
                var dialog = new WindowPrompt("Начатать Редактирование?") { WindowStartupLocation = WindowStartupLocation.CenterScreen};
                if (dialog.ShowDialog() == true)
                {
                    EmployeeToEdit = MainWindow.connection.Employee.Find(path.Телефон);
                    openEditFunc(((DataGrid)sender));
                }
                else
                {
                    return;
                 
                }
            }
        }

        public void openEditFunc(DataGrid dg)
        {
            MainGrid.Children.Remove(dg);
            TextBox tbSurname = new TextBox() { Name = "TBSURNAME", Text = EmployeeToEdit.Surname, Background = Brushes.White, Width = 120, Padding = new Thickness(15), VerticalAlignment = VerticalAlignment.Stretch, TextAlignment = TextAlignment.Center, Foreground = Brushes.Black };
            TextBox tbName = new TextBox() { Name = "TBNAME", Text = EmployeeToEdit.Name, Background = Brushes.White, Width = 120, Padding = new Thickness(15), VerticalAlignment = VerticalAlignment.Stretch, TextAlignment = TextAlignment.Center, Foreground = Brushes.Black };
            TextBox tbPatronymic = new TextBox() { Name = "TBPATRONYMIC", Text = EmployeeToEdit.Patronymic, Background = Brushes.White, Width = 120, Padding = new Thickness(15), VerticalAlignment = VerticalAlignment.Stretch,TextAlignment=TextAlignment.Center,Foreground = Brushes.Black };
            Grid grid = new Grid();
            Grid.SetColumn(grid, 1);
            MainGrid.Children.Add(grid);
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.10, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.5, GridUnitType.Star) });
            StackPanel SPfio = new StackPanel();
            SPfio.Orientation = Orientation.Horizontal;
            SPfio.Margin = new Thickness(3);
            grid.Children.Add(SPfio);
            Grid.SetRow(SPfio, 0);
            SPfio.Children.Add(tbSurname);
            SPfio.Children.Add(tbName);
            SPfio.Children.Add(tbPatronymic);
            tbSurname.Margin = new Thickness(0,0,10, 0);
            tbName.Margin = new Thickness(0, 0, 10, 0);
            tbPatronymic.Margin = new Thickness(0, 0, 10, 0);
            StackPanel SPbutton = new StackPanel();
            Button btnSave = new Button();
            Button btnRemove = new Button();
            SPbutton.Children.Add(btnSave);
            SPbutton.Children.Add(btnRemove);

            btnRemove.Content = "Удалить сотрудника";
            btnSave.Click += onClickButtonSave;
            btnSave.Content = "Сохранить";
            btnRemove.Click += removeEmployee;
            grid.Children.Add(SPbutton);
            Grid.SetRow(SPbutton, 1);
        }

        public void removeEmployee(object sender, EventArgs e)
        {
            MainWindow.connection.Employee.Remove(EmployeeToEdit);
            MainWindow.connection.SaveChanges();
            MessageBox.Show("Успешно удалено");
            MainGrid.Children.RemoveRange(2, MainGrid.Children.Count);
        }

        public void onClickButtonSave(object sender, EventArgs e)
        {
            var surname = FindChild<TextBox>(this, "TBSURNAME").Text;
            var name = FindChild<TextBox>(this, "TBNAME").Text;
            var patronymic = FindChild<TextBox>(this, "TBPATRONYMIC").Text;
            EmployeeToEdit.Surname = surname;
            EmployeeToEdit.Name = name;
            EmployeeToEdit.Patronymic = patronymic;
            MainWindow.connection.SaveChanges();
            MessageBox.Show("Успешно сохранено");
            MainGrid.Children.RemoveRange(2, MainGrid.Children.Count);

        }



        private static Employee EmployeeToEdit;



        /// <summary>
        /// Finds a Child of a given item in the visual tree. 
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, 
        /// a null parent is being returned.</returns>
        public static T FindChild<T>(DependencyObject parent, string childName)
           where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

    }



}
