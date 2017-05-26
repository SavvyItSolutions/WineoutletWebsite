using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;

namespace UpdatingWinesData
{
    class ExtractData
    {
        public bool ReadHTML(string uri)
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
            StripHTMLTags(data, uri);

            return true;
        }
        string path;
        public void StripHTMLTags(string str, string SKU)
        {
            StringBuilder pureText = new StringBuilder();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(str);
            WineDetails _wdobj = new WineDetails();
            List<WineDetails> lstWine = new List<WineDetails>();
            path = @"C:\Users\SAVVYIT\Desktop\wineoutlet\WorkingSku.csv";
            if (str.Contains("Thank you for shopping at Wine Outlet, unfortunately we are unable to locate the product you are purchasing") == true)
            {
                var csv = new StringBuilder();
                string skuno = SKU.Replace("http://www.wineoutlet.com/", "");
                skuno = skuno.Replace(".html", "");
                skuno = skuno.Replace("sku", "");
                var newLine = string.Format("{0},{1},{2}","Not Exist in Website", skuno,SKU);
                csv.AppendLine(newLine);
                File.AppendAllText(path, csv.ToString());
                Console.WriteLine(skuno);
                //using (StreamWriter writer = new StreamWriter(path, true))
                //{

                //    writer.WriteLine(SKU + "---" + "Doesn't Exist" + "\r\n");
                //    writer.Close();
                //    Console.WriteLine(SKU + "---" + "Doesn't Exist" + "\r\n");
                //}
                //Console.WriteLine(SKU + "Doesn't Exist");
            }
            else
            {
                //using (StreamWriter writer = new StreamWriter(path, true))
                //{
                //    writer.WriteLine(SKU + "---" + "Exists" + " ");
                //    Console.WriteLine(SKU + "---" + "Exists" + " ");
                //    writer.Close();
                //}
                string skuid = "";
                try
                {
                    HtmlNodeCollection sku111 = doc.DocumentNode.SelectNodes("//*[@id='bmg_itemdetail_sku']");
                    skuid = sku111[0].InnerText;
                    skuid = skuid.Replace("Sku: ", "");

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
                string Vintag = "";
                try
                {
                    HtmlNodeCollection vin = doc.DocumentNode.SelectNodes("//*[@id='bmg_itemdetail_vintage']");
                    Vintag = vin[0].InnerText;
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
                string nameURL = "";
                try
                {
                    HtmlNodeCollection mainName = doc.DocumentNode.SelectNodes("//*[@id='bmg_itemdetail_prod_title']"); ////*[@id="bmg_itemdetail_prod_title"]
                    nameURL = mainName[0].InnerText;
                    nameURL = nameURL.Replace(",", "");
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }



                /*   HtmlNodeCollection productRating = doc.DocumentNode.SelectNodes("//*[@id='bmg_section1']/div"); ////*[@id="bmg_section1"]/div  

                   string ratingURL = "";
                   foreach (HtmlAttribute htmlAttr in productRating.FirstOrDefault().FirstChild.Attributes)
                   {
                       if (htmlAttr.Name == "bmg_itemdetail_product_rating")
                           ratingURL = htmlAttr.Value;
                      ratingURL = ratingURL.Replace(",", "");
                   }


                  */
                string Information = "";
                try
                {
                    //HtmlNodeCollection Prodinformation = doc.DocumentNode.SelectNodes("//*[@id='bmg_itemdetail_product_info']"); ////*[@id="bmg_itemdetail_prod_title"]

                    string priceHTML = doc.DocumentNode.SelectNodes("//*[@id='bmg_itemdetail_description']")[0].InnerText;//*[@id="bmg_itemdetail_description"]

                    Information = priceHTML.Replace("\t", "");
                    Information = Information.Replace("\r\n", "");
                    Information = Information.Replace(",", "");
                    Information = Information.Replace("<!--This content belongs to bevnetwork.com and WINEKING-->", "");
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);

                }
                string desURL = "";
                try
                {
                    HtmlNodeCollection productDescription = doc.DocumentNode.SelectNodes(" //*[@id='bmg_itemdetail_product_info']"); //*[@id="bmg_itemdetail_description"]

                    string des = productDescription[0].InnerText;//*[@id="bmg_itemdetail_product_info"]/table/tbody/tr/td[1]/div[2]/span[1]
                    des = des.Replace(",", "");
                    des = des.Replace("\t", "");
                    des = des.Replace("\r", ";");
                    des = des.Replace("\n", "");

                    string inf = des.Substring(0, des.IndexOf("<!--"));
                    desURL = inf;


                    string[] str1 = desURL.Split(';');
                    string[] str2 = null;
                    for (int i = 0; i < str1.Length; i++)
                    {
                        str2 = str1[i].Split(':');
                        if (str2[0].ToLower() == "country")
                        {
                            _wdobj.Country = str2[1].TrimStart();
                        }
                        else if (str2[0].ToLower() == "region")
                        {
                            _wdobj.Region = str2[1].TrimStart();
                        }
                        else if (str2[0].ToLower() == "sub-region")
                        {
                            _wdobj.SubRegion = str2[1].TrimStart();
                        }
                        else if (str2[0].ToLower() == "grape varietal")
                        {
                            _wdobj.GrapeType = str2[1].TrimStart();
                        }
                        else if (str2[0].ToLower() == "type")
                        {
                            _wdobj.WineType = str2[1].TrimStart();
                        }
                        //_wdobj.SubRegion = "";

                    }
                    _wdobj.WineName = nameURL;
                    _wdobj.Vintage = Vintag;
                    string sku_no= SKU.Replace("http://www.wineoutlet.com/", "");
                    sku_no = sku_no.Replace(".html", "");
                    sku_no = sku_no.Replace("sku", "");
                    _wdobj.Sku = sku_no;
                    lstWine.Add(_wdobj);

                    XmlDocument xmlData = ConvertListDataToXml(lstWine);

                    string constr = ConfigurationManager.ConnectionStrings["LocalCon"].ConnectionString;
                    Console.WriteLine(sku_no);
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        //using (SqlCommand cmd = new SqlCommand("ImportWineDetails", con))//TO INSERT
                        using (SqlCommand cmd = new SqlCommand("UpdateWineDetails", con))//TO UPDATE
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@xmlDoc", SqlDbType.NVarChar).Value = xmlData.InnerXml;
                            cmd.Connection = con;
                            cmd.CommandTimeout = 1000;
                            con.Open();
                            int i=cmd.ExecuteNonQuery();
                            var csv = new StringBuilder();
                            var newLine = string.Format("{0},{1},{2},{3}", "Working", _wdobj.Sku, SKU,"Updated");
                            csv.AppendLine(newLine);
                            File.AppendAllText(path, csv.ToString());
                            con.Close();
                        }

                    }


                }
                catch (Exception exe)
                {

                    Console.WriteLine(exe.Message);
                }
            }

            //try
            //{
                
            //    string connectionString = ConfigurationManager.ConnectionStrings["LocalCon"].ConnectionString;
            //   // SqlConnection conv = new SqlConnection(connectionString);


            //    DataSet ds = new DataSet("TimeRanges");
            //    using (SqlConnection conv = new SqlConnection(connectionString))
            //    {
            //        SKU = SKU.Replace("http://www.wineoutlet.com/sku", "");
            //        SKU = SKU.Replace(".html", "");
            //        SqlCommand sqlComm = new SqlCommand("GetItemDetails", conv);
            //        sqlComm.Parameters.AddWithValue("@Sku", SKU);
            //        sqlComm.CommandType = CommandType.StoredProcedure;
            //        SqlDataAdapter da = new SqlDataAdapter()
            //        {
            //            SelectCommand = sqlComm
            //        };
            //        da.Fill(ds);

            //        DataTable dt = new DataTable();
            //        dt = ds.Tables[0];

            //        if (dt.Rows.Count > 0)
            //        {
            //            DataRow row = dt.Rows[0];

            //            string s = row["Sku"].ToString();
            //            string name = row["Name"].ToString();
            //            //lbl_contact = row["contactno"].ToString();
            //        }


            //    }






            //    //List<WineDetails> TestList = new List<WineDetails>();
            //    //WineDetails test;
            //    //SqlCommand cmd = new SqlCommand("getdetails", conv);
            //    //{
            //    //    cmd.CommandType = CommandType.StoredProcedure;
            //    //    SKU = SKU.Replace("http://www.wineoutlet.com/sku", "");
            //    //    SKU = SKU.Replace(".html", "");
            //    //    //cmd.Parameters.AddWithValue("@Sku", SKU);
            //    //    DataSet ds = new DataSet();
            //    //    conv.Open();
            //    //    SqlDataReader reader = cmd.ExecuteReader();
            //    //    while (reader.Read())
            //    //    {
            //    //        test = new WineDetails();
            //    //        test.Sku = reader["Sku"].ToString();
            //    //        test.WineName = reader["Name"].ToString();
            //    //        TestList.Add(test);
            //    //    }
            //    //    conv.Close();
            //    //    Console.WriteLine("Inserted into DB");
            //    //}
            //}
            //catch (Exception e)
            //{
            //    string exe = e.Message.ToString();
            //}

            /*
            System.IO.File.WriteAllText(@"C:\Users\Savvy IT\Documents\sumanth\image.txt", imageURL);
            System.IO.File.WriteAllText(@"C:\Users\Savvy IT\Documents\sumanth\rating.txt", ratingURL);
            System.IO.File.WriteAllText(@"C:\Users\Savvy IT\Documents\sumanth\Information.txt", Information);
            System.IO.File.WriteAllText(@"C:\Users\Savvy IT\Documents\sumanth\name.txt", nameURL);
            System.IO.File.WriteAllText(@"C:\Users\Savvy IT\Documents\sumanth\desc.txt",desURL);
            System.IO.File.WriteAllText(@"C:\Users\Savvy IT\Documents\sumanth\reg.txt", priceURL);
            System.IO.File.WriteAllText(@"C:\Users\Savvy IT\Documents\sumanth\sale.txt",saleURL);
            System.IO.File.WriteAllText(@"C:\Users\Savvy IT\Documents\sumanth\TastingNotes.txt", TastingNotes);
            System.IO.File.WriteAllText(@"C:\Users\Savvy IT\Documents\sumanth\MoreInfo.txt", MoreInfo);
            System.IO.File.WriteAllText(@"C:\Users\Savvy IT\Documents\sumanth\Review.txt", Review);
            System.IO.File.WriteAllText(@"C:\Users\Savvy IT\Documents\sumanth\Rating.txt", Rating);
            */




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
    }

}

