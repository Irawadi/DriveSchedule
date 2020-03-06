using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using DSInterfaces;
using System.IO;

namespace AppointmentK1.Models
{
    internal class DSConfigurator
    {
        #region Properties
        public XmlDocument XmlSettings
        {
            get
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("D:\\ABC.xml");
                return xDoc;
            }

        }
        public string Address { get { return address; } }
        public string Binding { get { return binding; } }
        private string address { get; set; }
        private string binding { get; set; }
        private string logo { get; set; }
        public string Logo { get { return @"/Content/"+logo.Trim(); } }
        #endregion
        #region Constructor
        public DSConfigurator()
        {
            ReadSettings();
        }
        private void ReadSettings()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(new StringReader(GetSettingsText()));
            // получим корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;
            // обход всех узлов в корневом элементе
            foreach (XmlNode xnode in xRoot)
            {
                switch (xnode.Name)
                {
                    case "Address":
                        address = xnode.InnerText;
                        break;
                    case "Binding":
                        binding = xnode.InnerText;
                        break;
                    case "Logo":
                        logo = xnode.InnerText;
                        break;
                }
            }
        }
        private string GetSettingsText()
        {
            string text = "";
            using (FileStream fstream = File.OpenRead(@"D:\ABC.xml"))
            {
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                text = System.Text.Encoding.Default.GetString(array);
                text = Crypt.Decrypt(text, "df89ygy");
            }
            return text;
        }
        #endregion
        #region Methods
        public string FData(string spName, string ParamName, string Param)
        {
            ChannelFactory<IContract> factory = GetFactory();
            IContract channel = factory.CreateChannel();
            string Result = channel.FData(spName, ParamName, Param);
            factory.Close();
            return Result;
        }
        public string Subscribe(string Param)
        {
            ChannelFactory<IContract> factory = GetFactory();
            IContract channel = factory.CreateChannel();
            string Result = channel.TrySubscribe(Param);
            factory.Close();
            return Result;
        }
        public string MySchedule(int IdStudent)
        {
            ChannelFactory<IContract> factory = GetFactory();
            IContract channel = factory.CreateChannel();
            string Result = channel.MySchedule(IdStudent);
            factory.Close();
            return Result;
        }
        public string UpdateStudent(int IdStudent)
        {
            ChannelFactory<IContract> factory = GetFactory();
            IContract channel = factory.CreateChannel();
            string Result = channel.UpdateStudent(IdStudent);
            factory.Close();
            return Result;
        }
        public string Schedule(int IdStudent, DateTime Day)
        {
            ChannelFactory<IContract> factory = GetFactory();
            IContract channel = factory.CreateChannel();
            string Result = channel.Schedule(IdStudent, DSHelper.RussianDate(Day));
            factory.Close();
            return Result;
        }
        public string StudentInfo(int idstudent, string PWD)
        {
            ChannelFactory<IContract> factory = GetFactory();
            IContract channel = factory.CreateChannel();
            string Result = channel.Auth(idstudent, PWD);
            factory.Close();
            return Result;
        }
        public string StudentInfo(string Login, string Password)
        {
            ChannelFactory<IContract> factory = GetFactory();
            IContract channel = factory.CreateChannel();
            string Result = channel.Authorize(Login, Password);
            factory.Close();
            return Result;
        }
        public string GetHelp(string NameWebDocument)
        {
            ChannelFactory<IContract> factory = GetFactory();
            IContract channel = factory.CreateChannel();
            string Result = channel.GetHelp(NameWebDocument);
            factory.Close();
            return Result;
        }
        private ChannelFactory<IContract> GetFactory()
        {
            Uri uAddress = new Uri(address);
            BasicHttpBinding bBinding = new BasicHttpBinding();
            EndpointAddress endpoint = new EndpointAddress(uAddress);
            ChannelFactory<IContract> factory = new ChannelFactory<IContract>(bBinding, endpoint);
            return factory;
        }
        #endregion
    }
}