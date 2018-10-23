using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace ReadAndVerify
{
    /// <summary>
    /// Логика взаимодействия для UpdateData.xaml
    /// </summary>
    public partial class UpdateData : Window
    {
        public UpdateData()
        {
            InitializeComponent();
        }

        List<Project> projects = new List<Project>();

        public UpdateData(List<Project> projects)
        {
            InitializeComponent();
            DataContext = this;
            this.projects = projects;
            dGrid.ItemsSource = projects;

            Type myType = Type.GetType("ReadAndVerify.Project", false, true);

            foreach (PropertyInfo mi in myType.GetProperties())
            {
                if (mi.PropertyType.ToString() == "System.DateTime")
                {
                    // Режим отображения
                    FrameworkElementFactory textF = new FrameworkElementFactory(typeof(TextBlock));
                    Binding bind = new Binding(mi.Name)
                    {
                        UpdateSourceTrigger = UpdateSourceTrigger.Default,
                        StringFormat = "d",
                        Mode = BindingMode.TwoWay
                    };
                    textF.SetBinding(TextBlock.TextProperty, bind);
                    DataTemplate dt = new DataTemplate();
                    dt.VisualTree = textF;

                    //Режим редактирования
                    FrameworkElementFactory dateF = new FrameworkElementFactory(typeof(DatePicker));
                    Binding bind2 = new Binding(mi.Name)
                    {
                        UpdateSourceTrigger = UpdateSourceTrigger.Default,
                        Mode = BindingMode.TwoWay
                    };
                    dateF.SetBinding(DatePicker.SelectedDateProperty, bind2);
                    DataTemplate dt2 = new DataTemplate();
                    dt2.VisualTree = dateF;

                    //Добавляю шаблоны
                    DataGridTemplateColumn dgc1 = new DataGridTemplateColumn();
                    dgc1.CellTemplate = dt;
                    dgc1.CellEditingTemplate = dt2;
                    dgc1.Header = mi.Name;
                    dGrid.Columns.Add(dgc1);

                    continue;
                }
                DataGridTextColumn dgc = new DataGridTextColumn();
                dgc.Header = mi.Name;
                dgc.Binding = new Binding(mi.Name)
                {
                    UpdateSourceTrigger = UpdateSourceTrigger.Default,
                    Mode = BindingMode.TwoWay
                };
                dGrid.Columns.Add(dgc);
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dGrid.CanUserAddRows = false;
            this.DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            dGrid.CanUserAddRows = true;
            //AddDataForProject adfp = new AddDataForProject();
            //adfp.Show();
            //adfp.Ok_b.Click += (o, v) =>
            //{
            //    dGrid.Items.Add();
            //};
        }

        private void Delete(object sender, RoutedEventArgs e)
        {

        }
    }
}
