using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using System.IO;

namespace ReadAndVerify
{
    [Serializable]
    public class News : INotifyPropertyChanged
    {
        private static string pathToFile = @"..\..\Xml\News.xml";

        public event PropertyChangedEventHandler PropertyChanged;
        [XmlIgnore]
        public static ObservableCollection<News> news = new ObservableCollection<News>();

        [XmlIgnore]
        private string title, discription;
        [XmlIgnore]
        private DateTime startDateTime;

        [XmlElement]
        public string Title { get { return title; } set { title = value; RaisePropertyChanged("Title"); } }
        [XmlElement]
        public string Discription { get { return discription; } set { discription = value; RaisePropertyChanged("Discription"); } }
        [XmlElement]
        public DateTime StartDateTime { get { return startDateTime; } set { startDateTime = value; RaisePropertyChanged("StartDateTime"); } }

        public News() { }

        public News(string title,string discription,DateTime startDateTime)
        {
            this.Title = title;
            this.Discription = discription;
            this.StartDateTime = startDateTime;
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

        public static ObservableCollection<News> GetNews()
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "ArrayOfNews";
            xRoot.IsNullable = true;

            XmlSerializer xSeriz = new XmlSerializer(typeof(News[]), xRoot);
            ObservableCollection<News> news;
            using (FileStream fs = new FileStream(pathToFile, FileMode.OpenOrCreate))
            {
                News[] newprojects = (News[])xSeriz.Deserialize(fs);
                news = new ObservableCollection<News>(newprojects.Cast<News>().ToList());
            }
            return news;
        }

        public static void Update(ObservableCollection<News> news)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<News>));
            using (FileStream fs = new FileStream(pathToFile, FileMode.Create))
            {
                formatter.Serialize(fs, news);
            }
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
