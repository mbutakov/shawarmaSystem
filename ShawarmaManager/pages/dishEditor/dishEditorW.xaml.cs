using System;
using System.Collections.Generic;
using System.IO;
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
using Image = System.Windows.Controls.Image;

namespace ShawarmaManager.pages.dishEditor
{
    /// <summary>
    /// Логика взаимодействия для dishEditorW.xaml
    /// </summary>
    /// 
    public partial class dishEditorW : Window
    {
        public static dishEditorW Instance;
        public static List<Ingredient> listIngridientLoaded = new List<Ingredient>();
        public static List<Tuple<Ingredient, int>> createIngridientList = new List<Tuple<Ingredient, int>>();

        public dishEditorW()
        {
            Instance = this;
            InitializeComponent();
            dishEditorWindow.Visibility = Visibility.Collapsed;
            dishEditorWindow.ResizeMode = ResizeMode.NoResize;
            dishEditorWindow.WindowState = WindowState.Maximized;
            dishEditorWindow.Visibility = Visibility.Visible;
            loadListIngridient();
        }

        private void loadListIngridient()
        {
            gridIngridient.Children.Clear();
            listIngridientLoaded.Clear();
            using (fastfoodEntitiesContext db = new fastfoodEntitiesContext())
            {
                foreach (Ingredient u in db.Ingredient)
                    listIngridientLoaded.Add(u);
            }
            int column = 0;
            int row = 0;
            for (int i = 0; i < listIngridientLoaded.Count; i++)
            {
                if (i % 3 == 0)
                {
                    column = 0;
                }
                else
                {
                    if(i != 0)
                    {
                        column++;
                    }
                }
                if (Divisibility(i) && i != 0)
                {
                    row++;
                }
                StackPanel stackPanel = new StackPanel();
                Button btn = new Button();
                Image img = new Image();
                img.Height = 250;
                img.Source = new BitmapImage(new Uri("/pages/dishEditor/" +listIngridientLoaded[i].Image + ".png", UriKind.Relative));
                btn.Height = Double.NaN;
                btn.Click += addToListIngridient;
                btn.Tag = listIngridientLoaded[i];
                btn.Content = listIngridientLoaded[i].Name;
                btn.Style = (Style)FindResource("MaterialDesignFlatDarkBgButton");
                stackPanel.Children.Add(img);
                stackPanel.Children.Add(btn);
                stackPanel.VerticalAlignment = VerticalAlignment.Bottom;
                btn.VerticalAlignment = VerticalAlignment.Bottom;
                
                Grid.SetColumn(stackPanel, column);
                Grid.SetRow(stackPanel, row);
                gridIngridient.Children.Add(stackPanel);
               
            }
            
        }

        public void addToListIngridient(object sender, EventArgs e)
        {
            try
            {
                Ingredient ing = (Ingredient)((Control)sender).Tag;
                Tuple<Ingredient, int> comboTuple = null;
                int index = 0;
                foreach (Tuple<Ingredient, int> t in createIngridientList)
                {
                    if(t.Item1.Name == ing.Name)
                    {
                        comboTuple = new Tuple<Ingredient, int>(ing, t.Item2 + 10);
                        createIngridientList.RemoveAt(index);
                        createIngridientList.Insert(index, comboTuple);
                        break;
                    }
                    index++;

                }
                if(comboTuple == null)
                {
                    comboTuple = new Tuple<Ingredient, int>(ing, 10);
                    createIngridientList.Add(comboTuple);
                }
               
                refreshListAddedIngredient();
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
 
        }

        public void refreshListAddedIngredient()
        {
            ingridientListAdded.Items.Clear();
            foreach(Tuple<Ingredient,int>  t in createIngridientList)
            {
                ingridientListAdded.Items.Add(t);
            }
            
        }

            static bool Divisibility(int n)
        {
            if (n % 3 == 0)
            {
                return true;
            }
            return false;
        }

        private void closeB_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Dish newDish = new Dish();
                newDish.Name = nameDish.Text;
                newDish.Price = Convert.ToInt32(priceDish.Text);
                MainWindow.connection.Dish.Add(newDish);
                MainWindow.connection.SaveChanges();
                int id = newDish.ID;
                foreach (Tuple<Ingredient, int> t in createIngridientList)
                {
                    DishCompound dc = new DishCompound();
                    dc.Dish = id;
                    dc.Ingredient = t.Item1.Articule;
                    dc.Volume = t.Item2;
                    MainWindow.connection.DishCompound.Add(dc);
                }
                int z = MainWindow.connection.SaveChanges();
                if (z > 0)
                {
                    MessageBox.Show("Успешно добавлено");
                }
                else
                {
                    MessageBox.Show("Не успешно");
                }
            }
            catch (Exception)
            {

            }
        }

        private void ingridientListAdded_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void deleteIng_Click(object sender, RoutedEventArgs e)
        {
            if(ingridientListAdded.SelectedItem != null)
            {
                createIngridientList.Remove((Tuple<Ingredient, int>)ingridientListAdded.SelectedItem);
                ingridientListAdded.Items.Remove(ingridientListAdded.SelectedItem);
            }
        }

        private void uploadButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция не реализованна");
        }
    }
}
