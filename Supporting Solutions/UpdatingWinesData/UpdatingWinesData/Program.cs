using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace UpdatingWinesData
{
    public class Program
    {
        WineDetails _wdo = new WineDetails();
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
            string FileName = @"C:\Users\SAVVYIT\Desktop\Estage.xlsx";
            Console.WriteLine("Reading Excel sheet");
            string con = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties='Excel 12.0 xml;HDR=YES;'";
            ExtractData edo = new ExtractData();


            //List<Item> ret = new List<Item>();
            List<WineDetails> lstwine = new List<WineDetails>();
            
            
            XmlDocument xmlData = null;
            using (OleDbConnection connection = new OleDbConnection(con))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from [Estage$]", connection);
                using (OleDbDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string sku = dr[3].ToString();
                        string Winename = dr[4].ToString();
                        string vintage = dr[12].ToString();
                        string country = dr[14].ToString();
                        string region = dr[15].ToString();
                        string subregion = dr[16].ToString();
                        string WineType = dr[7].ToString();
                        string grapetype = dr[19].ToString();
                        
                        _wdo.Sku = sku;
                        _wdo.Country = country;
                        _wdo.GrapeType = grapetype;
                        _wdo.Region = region;
                        _wdo.SubRegion = subregion;
                        _wdo.WineName = Winename;
                        _wdo.WineType = WineType;
                        _wdo.Vintage = vintage;
                        lstwine = new List<WineDetails>();
                        lstwine.Add(_wdo);
                        //it is to insert into Sample wine
                        //InsertIntoSample(_wdo);
                        ////For inserting through xml and into products table
                       
                        xmlData = ConvertListDataToXml(lstwine);
                        InsertXmlData(xmlData);
                       // Console.Write(sku);
                    }
                    
                }
                
                command = null;
            }
            

            Console.WriteLine("Done");
        }
        public void InsertXmlData(XmlDocument xmlData)
        {
            try
            { 
            string constr = ConfigurationManager.ConnectionStrings["LocalCon"].ConnectionString;
            using (SqlConnection con1 = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("ImportWineDetails", con1))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@xmlDoc", SqlDbType.NVarChar).Value = xmlData.InnerXml;
                    cmd.Connection = con1;
                    cmd.CommandTimeout = 1000;
                    con1.Open();
                    cmd.ExecuteNonQuery();
                        //da.SelectCommand = cmd;
                        //da.Fill(ds);
                        //DataRow dr = ds.Tables[0].Rows[0];
                        //DailyStats(dr);
                       // Console.WriteLine(xmlData.InnerXml);
                    con1.Close();
                }

            }
            }
            catch(Exception exe)
            {
               //Console.WriteLine(exe.Message);
            }
        }
        
        public void InsertIntoSample(WineDetails _wdobg1)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["LocalCon"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("InsertSampleData", con);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Sku", _wdobg1.Sku);
                    //cmd.Parameters.AddWithValue("@desURL", desURL);
                    cmd.Parameters.AddWithValue("@WineName", _wdobg1.WineName);
                    cmd.Parameters.AddWithValue("@Vintage", _wdobg1.Vintage);
                    cmd.Parameters.AddWithValue("@Country", _wdobg1.Country);
                    //  cmd.Parameters.AddWithValue("@TastingNotes", TastingNotes);
                    // cmd.Parameters.AddWithValue("@MoreInfo", MoreInfo);
                    cmd.Parameters.AddWithValue("@Region", _wdobg1.Region);
                    cmd.Parameters.AddWithValue("@SubRegion", _wdobg1.SubRegion);
                    cmd.Parameters.AddWithValue("@GrapeType", _wdobg1.GrapeType);
                    cmd.Parameters.AddWithValue("@WineType", _wdobg1.WineType);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Console.WriteLine("Inserted into DB");
                }
            }
            catch (Exception e)
            {
                string exe = e.Message.ToString();
            }
        }

        public static XmlDocument ConvertListDataToXml(List<WineDetails> lwd)
        {
            XmlDocument xdoc = new XmlDocument();
            XmlElement rootNode = xdoc.CreateElement("Wine");
            for (int i = 0; i < lwd.Count; i++)
            {
                XmlElement childNode = xdoc.CreateElement("WineData");
                childNode.SetAttribute("Sku", lwd[i].Sku);
                childNode.SetAttribute("WineName", lwd[i].WineName);
                childNode.SetAttribute("Vintage", lwd[i].Vintage);
                childNode.SetAttribute("Country", lwd[i].Country);
                childNode.SetAttribute("Region", lwd[i].Region);
                childNode.SetAttribute("SubRegion", lwd[i].SubRegion);
                childNode.SetAttribute("GrapeType", lwd[i].GrapeType);
                childNode.SetAttribute("WineType", lwd[i].WineType);
                rootNode.AppendChild(childNode);
            }
            xdoc.AppendChild(rootNode);

            return xdoc;
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
