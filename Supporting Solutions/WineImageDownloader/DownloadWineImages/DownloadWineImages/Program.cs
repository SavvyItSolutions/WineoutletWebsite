using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Data.OleDb;

namespace DownloadWineImages
{
    public class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.FilterList();
            //string winename = "TestWine";
            //string html = p.GetHtmlCode(winename);
            //List<string> urls = p.GetUrls(html, 1);
            //for (int i = 0; i < urls.Count; i++)
            //{
            //    string luckyUrl = urls[i];
            //    p.Downloadimages(luckyUrl,winename);
            //}
        }
        public void FilterList()
        {
            //string FileName = ConfigurationManager.AppSettings["FileLocation"];
            string FileName = @"C:\Users\SAVVYIT\Desktop\Winenames.xlsx";
            Console.WriteLine("Reading from Excel sheet");
            string con = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties='Excel 12.0 xml;HDR=YES;'";



            //List<Item> ret = new List<Item>();
            List<string> ExcelString = new List<string>();
            using (OleDbConnection connection = new OleDbConnection(con))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from [Winenames$]", connection);
                using (OleDbDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string Winename = dr[0].ToString();
                        ExcelString.Add(Winename);
                    }
                }
                command = null;
            }
            List<string> final = new List<string>();
            for (int i = 0; i < ExcelString.Count; i++)
            {
                Program p = new Program();
                string winename =ExcelString[i] ;
                string html = p.GetHtmlCode(winename);
                List<string> urls = p.GetUrls(html, 1);
                for (int j = 0; j < urls.Count; j++)
                {
                    string luckyUrl = urls[j];
                    p.Downloadimages(luckyUrl, winename);
                }
                ////Console.WriteLine(ret[i].SKU + ':' + ret[i].Quantity);
                //if (SKUExist(ExcelString[i]))
                //{
                //    final.Add(ExcelString[i]);
                //}
            }

            for (int i = 0; i < final.Count; i++)
            {
                Console.WriteLine(final[i]);
            }

            Console.ReadLine();
        }
        private void Downloadimages(string url,string winename)
        {
            WebClient client = new WebClient();
            //int i = 0;
            try
            {
                Program p = new Program();
                int i = 5;
                string str4 = p.RandomString(i);
                Console.WriteLine(url);
                string filename = "urls";
                string path = @"E:\WineOutletImages\" + filename + ".txt";
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(url + "---" + str4 + "\r\n");
                    writer.Close();
                }
                string dwn = @"E:\WineOutletImages\" + winename+"_"+str4+ ".jpg";
                client.DownloadFile(new Uri(url), dwn);
                Console.WriteLine("success");
                //using (WebClient client1 = new WebClient())
                //{
                //    string id = "test";
                //    string pwd = "test";
                //    client.Credentials = new NetworkCredential(id,pwd);
                //    client.UploadFile(@"ftp.mylogicpvtltd.com\\httpdocs\\images\\s.jpg", path);
                //}
                //p.uploadImages();
            }
            catch (Exception w)
            {
                
            }

        }
        //For searching wine images with name
        private string GetHtmlCode(string wineName)
        {
            string searchText = "image for " + wineName;

            string url = "https://www.google.com/search?q=" + searchText + "&tbm=isch";
            string data = "";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";

            var response = (HttpWebResponse)request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                if (dataStream == null)
                    return "";
                using (var sr = new StreamReader(dataStream))
                {
                    data = sr.ReadToEnd();
                }
            }
            return data;
        }
        //for creating random string
        private Random random = new Random();
        public string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        //for getting urls
        private List<string> GetUrls(string html, int number)
        {
            var urls = new List<string>();

            int ndx = html.IndexOf("\"ou\"", StringComparison.Ordinal);

            int count = 0;
            while (ndx >= 0 && count < number)
            {
                ndx = html.IndexOf("\"", ndx + 4, StringComparison.Ordinal);
                ndx++;
                int ndx2 = html.IndexOf("\"", ndx, StringComparison.Ordinal);
                string url = html.Substring(ndx, ndx2 - ndx);
                urls.Add(url);
                ndx = html.IndexOf("\"ou\"", ndx2, StringComparison.Ordinal);
                count++;
            }
            return urls;
        }
    }
}
