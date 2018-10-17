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

        public MainWindow()
        {

            InitializeComponent();
            //Project.Create(new List<Project> {
            //    new Project("Мой проект 2", DateTime.Parse("01.10.2018"), DateTime.Parse("10.10.2018")),
            //    new Project("Мой проект 3", DateTime.Parse("30.10.2018"), DateTime.Parse("20.12.2018"))
            //});
            //_projects = Project.GetProjects();
            //this.DataContext = _projects;
            //this.Resources.Add("Projects", _projects);
            progressBarList.ItemsSource = _projects;
            CreateDynamicGridView();
        }

        private void CreateDynamicGridView()
        {
            // Create a GridView  
            //GridView grdView = new GridView();
            //grdView.AllowsColumnReorder = true;
            //grdView.ColumnHeaderToolTip = "Project";
            //GridViewColumn nameColumn = new GridViewColumn();
            //nameColumn.DisplayMemberBinding = new Binding("Title");
            //nameColumn.Header = "Project Title";
            //nameColumn.Width = 120;
            //grdView.Columns.Add(nameColumn);
            //ListView1.View = grdView;
        }
    }
}
