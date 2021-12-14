﻿using System;
using System.Net;
using System.Xml;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Currency_Converter
{
      class DBWorker
      {
        private MySqlConnection SqlServer;
        public DBWorker(string Config_Path)
        {
            SqlServer = new MySqlConnection(ReadConfig(Config_Path));
        }
        public void Update()
        {
            LoadDataFromServer(DateTime.Now);
        }
        private void LoadDataFromServer(DateTime Date)
        {
            Console.WriteLine("Loading...");

            string str_date = Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string xml;

            using (var client = new WebClient())
            {
                xml = client.DownloadString($"https://www.cbr.ru/scripts/XML_daily.asp?date_req={str_date}");
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);

            try
            {
                foreach (XmlElement element in xmlDocument.DocumentElement)
                {
                    MySqlCommand command = new MySqlCommand($"INSERT INTO cur (Date, Code, Nominal, Value)" +
                        $" VALUES( \"{str_date}\"" +
                        $",\"{element.GetElementsByTagName("CharCode").Item(0).InnerText}\"" +
                        $",{element.GetElementsByTagName("Nominal").Item(0).InnerText}" +
                        $",{element.GetElementsByTagName("Value").Item(0).InnerText.ToString().Replace(',', '.')});", SqlServer);
                    command.ExecuteNonQuery();
                }
            }

            catch(System.InvalidCastException)
            {
                return;
            }
        }
        public double GetDataFromDB(DateTime date, string Code)
        {
            if (Code == "RUB")
                return 1.0;

            string str_date = date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InstalledUICulture);
            SqlServer.Open();

            MySqlCommand command;
            
            command = new MySqlCommand($"SELECT EXISTS(SELECT Date FROM cur WHERE Date = '{str_date}');", SqlServer);
            if (Convert.ToInt32(command.ExecuteScalar()) == 0)
                LoadDataFromServer(date);

            command = new MySqlCommand($"SELECT Value / Nominal AS Rate FROM cur WHERE Date = '{str_date}' AND Code = '{Code}';", SqlServer);
            MySqlDataReader reader = command.ExecuteReader();

            double rate = 0.0;

            if (reader.Read())
            {
                if (!reader.IsDBNull(0))
                    rate = reader.GetDouble("Rate");
            }

            reader.Close();
            SqlServer.Close();

            return rate;
        }
        public (double, double) GetDataFromDB(DateTime date, string first_code, string second_code)
        {
            double first_rate = 0;
            double second_rate = 0;

            if (first_code == "RUB")
                first_rate = 1;
            if (second_code == "RUB")
                second_rate = 1;
            if (first_rate * second_rate == 1)
                return (1.0, 1.0);

            string str_date = date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            SqlServer.Open();

            MySqlCommand command;

            command = new MySqlCommand($"SELECT EXISTS(SELECT Date FROM cur WHERE Date = '{str_date}');", SqlServer);
            if (Convert.ToInt32(command.ExecuteScalar()) == 0)
                LoadDataFromServer(date);

            command = new MySqlCommand($"SELECT Value / Nominal AS Rate, Code FROM cur WHERE Date = '{str_date}' AND (Code = '{first_code}' OR Code = '{second_code}');", SqlServer);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            { 
                if (!reader.IsDBNull(1))
                {
                    string temp = reader.GetString("Code");
                    if (temp == first_code)
                        first_rate = reader.GetDouble("Rate");
                    else
                        second_rate = reader.GetDouble("Rate");
                }
            }

            reader.Close();
            SqlServer.Close();

            return (first_rate, second_rate);
        }
        public List<string> GetAllCodes()
        {
            SqlServer.Open();
            MySqlCommand command = new MySqlCommand("SELECT DISTINCT Code FROM cur;",SqlServer);
            MySqlDataReader reader =  command.ExecuteReader();

            List<string> codes = new List<string>();

            while(reader.Read())
            {
                if (!reader.IsDBNull(0))
                    codes.Add(reader.GetString("Code"));
            }

            reader.Close();
            SqlServer.Close();

            return codes;
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
