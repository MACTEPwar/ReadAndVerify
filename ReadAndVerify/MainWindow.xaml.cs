using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace ReadAndVerify
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Project> _projects = Project.GetProjects();
        ObservableCollection<MyDictionary> data;

        public MainWindow()
        {

            InitializeComponent();
            programMenu.Visibility = Visibility.Hidden;
            DataContext = this;
            //Project.Create(new List<Project> {
            //    new Project("Мой проект 2", DateTime.Parse("01.10.2018"), DateTime.Parse("10.10.2018")),
            //    new Project("Мой проект 3", DateTime.Parse("30.10.2018"), DateTime.Parse("20.12.2018"))
            //});
            //Dictionary<string, double> data = Project.getTheurrentPercentageOfTheDayOfProjectExecution(_projects);
            data = Project.getTheurrentPercentageOfTheDayOfProjectExecution(_projects);
            progressBarList.ItemsSource = data;
            CreateDynamicGridView();
        }

        private void CreateDynamicGridView()
        {

        }

        private void MainWindow_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            PasswordWindow pw = new PasswordWindow();
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.R)
            {
                if (pw.ShowDialog() == true)
                {
                    if (pw.Password == "12")
                    {
                        programMenu.Visibility = Visibility.Visible;
                    }
                    else MessageBox.Show("Пароль не верный, попробуйте еще раз.","Ошибка ввода пароля", MessageBoxButton.OK, MessageBoxImage.Error );
                }
            }
        }

        /// <summary>
        /// Закрывает панель админки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            programMenu.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Изменить проект
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            UpdateData ud = new UpdateData(_projects);
            if (ud.ShowDialog() == true)
            {
                data = Project.getTheurrentPercentageOfTheDayOfProjectExecution(_projects);
                foreach(var p in data)
                {
                    MessageBox.Show(p.Key);
                }
            }
        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
            //data[2].Key = _projects[0].Title;
            data = Project.getTheurrentPercentageOfTheDayOfProjectExecution(_projects);
            //MessageBox.Show(_projects[0].Title);
        }
    }
}
