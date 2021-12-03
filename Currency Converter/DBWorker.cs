using System;
using System.Net;
using System.Xml;
using MySql.Data.MySqlClient;

namespace Currency_Converter
{
      class DBWorker
      {
        private MySqlConnection SqlServer;
        public DBWorker(string Config_Path)
        {
            SqlServer = new MySqlConnection(ReadConfig(Config_Path));
        }
        private bool LoadDataFromServer(DateTime Date)
        {
            string xml;
            using (var client = new WebClient())
            {
                string ate = Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine(ate);
                xml = client.DownloadString($"https://www.cbr.ru/scripts/XML_daily.asp?date_req={ate}");
            }
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);

            string date = xmlDocument.DocumentElement.GetAttribute("Date");
            foreach (XmlElement element in xmlDocument.DocumentElement)
            {
                MySqlCommand command = new MySqlCommand($"INSERT INTO cur (Date, Code, Nominal, Value)" +
                    $" VALUES( \"{date.Replace(',', '-')}\"" +
                    $",\"{element.GetElementsByTagName("CharCode").Item(0).InnerText}\"" +
                    $",{element.GetElementsByTagName("Nominal").Item(0).InnerText}" +
                    $",{element.GetElementsByTagName("Value").Item(0).InnerText.ToString().Replace(',', '.')});", SqlServer);
                command.ExecuteNonQuery();
            }

            MySqlCommand com = new MySqlCommand("delete t1 FROM cur t1 INNER JOIN cur t2 WHERE t1.id > t2.id AND t1.Date = t2.Date AND t1.Code = t2.Code;", SqlServer);
            com.ExecuteNonQuery();
            return true;
        }
        public (double, int) GetDataFromDB(DateTime date, string Code)
        {
            SqlServer.Open();

            MySqlCommand command;

            // Проверить если данные в БД, и загрузить их с сервера если нет
            command = new MySqlCommand($"SELECT EXISTS(SELECT Date FROM cur WHERE Date = '{date.ToString("dd/MM/yyyy")});')", SqlServer);
            if (Convert.ToInt32(command.ExecuteScalar()) == 0)
                LoadDataFromServer(date);

            // Произвести выборку
            command = new MySqlCommand($"SELECT Value, Nominal FROM cur WHERE Date = '{date.ToString("dd/MM/yyyy")}' AND Code = '{Code}';", SqlServer);
            Console.WriteLine(command.CommandText);
            MySqlDataReader reader = command.ExecuteReader();

            double values = 0;
            int nominal = 0;

            if (reader.Read())
            {

                values = reader.GetDouble("Value");
                nominal = reader.GetInt32("Nominal");
            }

            reader.Close();
            SqlServer.Close();

            return (values, nominal);
        }
        private string ReadConfig(string Path)
        {
            using (System.IO.StreamReader file = new System.IO.StreamReader(Path))
            {
                return file.ReadLine();
            }
        }
      }
}
