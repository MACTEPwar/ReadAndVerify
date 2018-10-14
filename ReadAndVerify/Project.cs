using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;
using System.Xml;

namespace ReadAndVerify
{
    [Serializable]
    public class Project
    {
        [XmlIgnore]
        private static string pathToFile = @"..\..\Xml\Project.xml";

        public string Title { get; set; }
        [XmlElement("StartDate")]
        public DateTime StartDateForProject { get; set; }
        [XmlElement("FinishDate")]
        public DateTime FinishDateForProject { get; set; }

        public Project() { }

        public Project(string title,DateTime startDate, DateTime finishDate)
        {
            Title = title;
            StartDateForProject = startDate;
            FinishDateForProject = finishDate;
        }

        public static List<Project> GetProjects()
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Projects";
            xRoot.IsNullable = true;

            XmlSerializer xSeriz = new XmlSerializer(typeof(Project[]),xRoot);
            List<Project> projects = new List<Project>();
            using (FileStream fs = new FileStream(pathToFile, FileMode.OpenOrCreate))
            {
                Project[] newprojects = (Project[])xSeriz.Deserialize(fs);
                return newprojects.Cast<Project>().ToList();
            }
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

        public override string ToString()
        {
            return "20";
        }

    }
}
