using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UpdatingWinesData
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Enter Sku");
            //string sku=Console.ReadLine();
            //ExtractData Ed = new ExtractData();
            //Ed.ReadHTML("http://www.wineoutlet.com/sku05093.html");
            Program p = new Program();
            p.FilterList();

        }
        public void FilterList()
        {
            //string FileName = ConfigurationManager.AppSettings["FileLocation"];
            string FileName = @"C:\Users\SAVVYIT\Desktop\sku.xlsx";
            Console.WriteLine("Hey");
            string con = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties='Excel 12.0 xml;HDR=YES;'";



            //List<Item> ret = new List<Item>();
            List<string> ExcelString = new List<string>();
            using (OleDbConnection connection = new OleDbConnection(con))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from [Sku$]", connection);
                using (OleDbDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string sku = dr[0].ToString();
                        ExcelString.Add(sku);
                    }
                }
                command = null;
            }
            List<string> final = new List<string>();
            for (int i = 0; i < ExcelString.Count; i++)
            {
                //Console.WriteLine(ret[i].SKU + ':' + ret[i].Quantity);
                if (SKUExist(ExcelString[i]))
                {
                    final.Add(ExcelString[i]);
                }
            }

            for (int i = 0; i < final.Count; i++)
            {
                Console.WriteLine(final[i]);
            }

            Console.ReadLine();
        }
        public bool SKUExist(string sku)
        {
            //Make SKU of length 5.
            if (sku.Length == 1)
                sku = "0000" + sku;
            else
                if (sku.Length == 2)
                sku = "000" + sku;
            else
             if (sku.Length == 3)
                sku = "00" + sku;
            else
                if (sku.Length == 4)
                sku = "0" + sku;
            else
                if (sku.Length == 5)
                sku = "" + sku;

            string urlAddress = "http://www.wineoutlet.com/sku" + sku + ".html";

            return WorkingSKU(urlAddress);
        }

        public bool WorkingSKU(string uri)
        {
            var client = (HttpWebRequest)WebRequest.Create(uri);
            client.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            client.CookieContainer = new CookieContainer();
            client.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36";
            var response = client.GetResponse() as HttpWebResponse;
            string data = "";
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }

            if (data == "")
                return false;
            else if (data.Contains("Wine Outlet is unable to locate the product."))
                return false;
            else
            {
                ExtractData obj = new ExtractData();
                //obj.ReadHTML(uri);
                obj.StripHTMLTags(data, uri);
                return true;

            }
        }
    }
}
