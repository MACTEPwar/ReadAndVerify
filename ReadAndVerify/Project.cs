using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ReadAndVerify
{
    [Serializable]
    public class Project : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [XmlIgnore]
        private static string pathToFile = @"..\..\Xml\Project.xml";

        [XmlIgnore]
        private string title;
        [XmlIgnore]
        private DateTime startDateForProject;
        [XmlIgnore]
        private DateTime finishDateForProject;
        [XmlIgnore]
        private double dayForProject;

        public string Title { get { return title; } set { title = value; RaisePropertyChanged("Title"); } }
        [XmlElement("StartDate")]
        public DateTime StartDateForProject
        {
            get
            {
                return startDateForProject;
            }
            set
            {
                startDateForProject = value;
                // Сколько всего дней для проекта (100%)
                int maxDate = FinishDateForProject.Subtract(StartDateForProject).Days;
                // Сколько прошо дней (кол-во)
                int curentDate = DateTime.Now.Subtract(StartDateForProject).Days;
                // Текущий процен пройденых дней
                DayForProject = curentDate * 100 / maxDate;
                RaisePropertyChanged("StartDateForProject");
            }
        }
        [XmlElement("FinishDate")]
        public DateTime FinishDateForProject
        {
            get
            {
                return finishDateForProject;
            }
            set
            {
                finishDateForProject = value;
                // Сколько всего дней для проекта (100%)
                int maxDate = FinishDateForProject.Subtract(StartDateForProject).Days;
                // Сколько прошо дней (кол-во)
                int curentDate = DateTime.Now.Subtract(StartDateForProject).Days;
                // Текущий процен пройденых дней
                DayForProject = curentDate * 100 / maxDate;
                RaisePropertyChanged("FinishDateForProject");
            }
        }
        [XmlIgnore]
        public double DayForProject { get { return dayForProject; } set { dayForProject = value; RaisePropertyChanged("DayForProject"); } }

        public Project() { }

        public Project(string title, DateTime startDate, DateTime finishDate)
        {
            Title = title;
            StartDateForProject = startDate;
            FinishDateForProject = finishDate;
            // Сколько всего дней для проекта (100%)
            int maxDate = FinishDateForProject.Subtract(StartDateForProject).Days;
            // Сколько прошо дней (кол-во)
            int curentDate = DateTime.Now.Subtract(StartDateForProject).Days;
            // Текущий процен пройденых дней
            DayForProject = curentDate * 100 / maxDate;
        }

        /// <summary>
        /// Получает все проекты с xml файла (десериализация)
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Project> GetProjects()
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "ArrayOfProject";
            xRoot.IsNullable = true;

            XmlSerializer xSeriz = new XmlSerializer(typeof(Project[]), xRoot);
            ObservableCollection<Project> projects;
            using (FileStream fs = new FileStream(pathToFile, FileMode.OpenOrCreate))
            {
                Project[] newprojects = (Project[])xSeriz.Deserialize(fs);
                projects = new ObservableCollection<Project>(newprojects.Cast<Project>().ToList());
            }
            foreach (Project project in projects)
            {
                // Сколько всего дней для проекта (100%)
                int maxDate = project.FinishDateForProject.Subtract(project.StartDateForProject).Days;
                // Сколько прошо дней (кол-во)
                int curentDate = DateTime.Now.Subtract(project.StartDateForProject).Days;
                // Текущий процен пройденых дней
                project.DayForProject = curentDate * 100 / maxDate;
            }
            return projects;
        }

        public static void Create(List<Project> projects)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(pathToFile);
            foreach (Project project in projects)
            {
                XmlNode xnode = doc.CreateNode(XmlNodeType.Element, "Project", null);
                XmlSerializer xSeriz = new XmlSerializer(typeof(Project));
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                XmlWriterSettings writtersetting = new XmlWriterSettings();
                writtersetting.OmitXmlDeclaration = true;
                StringWriter sw = new StringWriter();
                using (XmlWriter xmlwriter = System.Xml.XmlWriter.Create(sw, writtersetting))
                {
                    xSeriz.Serialize(xmlwriter, project, ns);
                }
                byte[] symbol = Encoding.GetEncoding(1251).GetBytes(sw.ToString()); //Получаем байт символа в кодировке 1251
                Console.WriteLine(symbol[0]);
                string binary = Convert.ToString(symbol[0], 2);
                symbol[0] = Convert.ToByte(binary, 2); //перевели обратно в десятичную
                string r = Encoding.GetEncoding(1251).GetString(symbol); //Преобразовали обратно в строку
                xnode.InnerXml = r;
                XmlNode bindxnode = xnode.SelectSingleNode("Project");
                foreach (XmlNode ch in xnode)
                {
                    doc.DocumentElement.AppendChild(ch);
                }
            }
            doc.Save(pathToFile);
        }

        /// <summary>
        /// Заносит данные в xml файл (сериализация)
        /// </summary>
        /// <param name="projects"></param>
        public static void Update(ObservableCollection<Project> projects)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<Project>));
            using (FileStream fs = new FileStream(pathToFile, FileMode.Create))
            {
                formatter.Serialize(fs, projects);
            }
        }

        protected virtual void RaisePropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }
        protected void RaisePropertyChanged(string propertyName)
        {

            RaisePropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}
