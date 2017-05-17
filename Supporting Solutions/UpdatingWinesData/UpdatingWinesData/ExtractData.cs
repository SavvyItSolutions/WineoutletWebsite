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

        public void StripHTMLTags(string str, string SKU)
        {
            StringBuilder pureText = new StringBuilder();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(str);
            string Country = null;
            string Region = null;
            string SubRegion = null;
            string GrapeType = null;
            string Type = null;
            HtmlNodeCollection mainImg = doc.DocumentNode.SelectNodes("//*[@id='loadarea']/img"); ////*[@id="""loadarea"""]/img

            //string imageURL = "";
            //foreach (HtmlAttribute htmlAttr in mainImg.FirstOrDefault().Attributes)
            //{
            //    if (htmlAttr.Name == "src")
            //        imageURL = htmlAttr.Value;
            //}
            string skuid = "";
            try
            {
                HtmlNodeCollection sku111 = doc.DocumentNode.SelectNodes("//*[@id='bmg_itemdetail_sku']");
                skuid = sku111[0].InnerText;
                skuid = skuid.Replace("Sku: ","");

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            string Vintag="";
            try
            {
                HtmlNodeCollection vin = doc.DocumentNode.SelectNodes("//*[@id='bmg_itemdetail_vintage']");
                Vintag = vin[0].InnerText;
            }
            catch(Exception e)
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
                str2 = str1[5].Split(':');
                Country=str2[1];
                str2 = str1[6].Split(':');
                Region = str2[1];
                str2 = str1[7].Split(':');
                SubRegion = str2[1];
                str2 = str1[8].Split(':');
                GrapeType = str2[1];
                str2 = str1[9].Split(':');
                Type = str2[1];
                //string country = str2[1].Replace(" ", "");
                Console.WriteLine(Country +"\n"+ Region + "\n"+ SubRegion + "\n"+ GrapeType+"\n"+Type);
                
            }
            catch (Exception exe)
            {

                Console.WriteLine(exe.Message);
            }

            string priceURL = "";
            try
            {
                //HtmlNodeCollection regPrice = doc.DocumentNode.SelectNodes("//*[@id='bmg_itemdetail_item_price']/table/tbody/tr/td[1]/span[1]"); 
                string priceHTML = doc.DocumentNode.SelectNodes("//*[@id='bmg_itemdetail_item_price']/table")[0].InnerHtml;

                //priceHTML = ""\r\n\t\t\t\t\t < tr >\r\n\t\t\t\t\t\t < td align =\"right\" width=\"100%\" valign=\"middle\">\r\n\t\t\t\t\t\t\t<span class=\"RegularPrice\">Reg.&nbsp;$49.99</span><br><span class=\"SalePrice\">On Sale $34.65</span>\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t<td align=\"right\" valign=\"middle\">\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t<a href=\"javascript:buySku('22596','',1)\"><img alt=\"Buy Alto Moncayo\" src=\"images/buy.gif\" name=\"buy\" border=\"0\">\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t</a></td>\r\n\t\t\t\t\t</tr>\r\n\t\t\t\t""
                string startString = priceHTML.Substring(priceHTML.IndexOf("RegularPrice"));
                string regPrice = startString.Substring(startString.IndexOf("$"), startString.IndexOf("</") - startString.IndexOf("$"));

                priceURL = regPrice.Replace("$", "");

            }
            catch (Exception)
            {

                Console.WriteLine();
            }

            string saleURL = "";
            try
            {
                //  HtmlNodeCollection salePrice = doc.DocumentNode.SelectNodes("//*[@id='bmg_itemdetail_item_price']/table/tbody/tr/td[1]/span[2]"); ////*[@id="bmg_itemdetail_item_price"]/table/tbody/tr/td[1]/span[2]
                string priceHTML = doc.DocumentNode.SelectNodes("//*[@id='bmg_itemdetail_item_price']/table")[0].InnerHtml;

                //priceHTML = ""\r\n\t\t\t\t\t < tr >\r\n\t\t\t\t\t\t < td align =\"right\" width=\"100%\" valign=\"middle\">\r\n\t\t\t\t\t\t\t<span class=\"RegularPrice\">Reg.&nbsp;$49.99</span><br><span class=\"SalePrice\">On Sale $34.65</span>\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t<td align=\"right\" valign=\"middle\">\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t<a href=\"javascript:buySku('22596','',1)\"><img alt=\"Buy Alto Moncayo\" src=\"images/buy.gif\" name=\"buy\" border=\"0\">\r\n\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t</a></td>\r\n\t\t\t\t\t</tr>\r\n\t\t\t\t""
                string startString = priceHTML.Substring(priceHTML.IndexOf("SalePrice"));
                string salePrice = startString.Substring(startString.IndexOf("$"), startString.IndexOf("</") - startString.IndexOf("$"));

                saleURL = salePrice.Replace("$", "");


            }
            catch (Exception)
            {

                Console.WriteLine();

            }

            String TastingNotes = "";
            try
            {
                // HtmlNodeCollection TasteDescription = doc.DocumentNode.SelectNodes("//*[@id='tastingnotes']"); //*//*[@id="bmg_itemdetail_tabs"]/div
                String priceHTML = doc.DocumentNode.SelectNodes("//*[@id='tastingnotes']")[0].InnerText;

                var rgx = new Regex(" < !--This content belongs to bevnetwork.com and WINEKING-- >");
                TastingNotes = priceHTML.Replace("\t", "");
                TastingNotes = TastingNotes.Replace("\r\n", "");
                TastingNotes = TastingNotes.Replace("\n", "");
                TastingNotes = TastingNotes.Replace(",", "");
                TastingNotes = TastingNotes.Replace("<!--This content belongs to bevnetwork.com and WINEKING-->", "");
                /*  var input = TastingNotes;
                  var pattern = " <!--This content belongs to bevnetwork.com and WINEKING-->";
              var Result = rgx.Replace(input, "", 1);*/



            }
            catch (Exception)
            {

                Console.WriteLine("message");
            }


            string Rating = "";
            try
            {

                string priceHTML = doc.DocumentNode.SelectNodes("//*[@id='ratings']")[0].InnerText;

                Rating = priceHTML.Replace("\t", "");
                Rating = Rating.Replace("\r\n", "");
                Rating = Rating.Replace("\n", "");
                Rating = Rating.Replace(",", "");

                Rating = Rating.Replace("<!--CUSTOMER-->", "");
                Rating = Rating.Replace("<!--This content belongs to bevnetwork.com and WINEKING-->", "");
            }
            catch (Exception)
            {

                Console.WriteLine("message");
            }





            string MoreInfo = "";
            try
            {
                //  HtmlNodeCollection MoreInfoDes = doc.DocumentNode.SelectNodes("//*[@id='moreinfo']")[0].; //*//*[@id="bmg_itemdetail_tabs"]/div
                string priceHTML = doc.DocumentNode.SelectNodes("//*[@id='moreinfo']")[0].InnerText;


                // MoreInfo = MoreInfoDes[0].InnerText;

                //priceHTML.Replace( "/r/n,/t", String.Empty);
                MoreInfo = priceHTML.Replace("\t", "");
                MoreInfo = MoreInfo.Replace("\r\n", "");
                MoreInfo = MoreInfo.Replace(",", "");
                MoreInfo = MoreInfo.Replace("\n", "");
                MoreInfo = MoreInfo.Replace("<!--This content belongs to bevnetwork.com and WINEKING-->", "");


            }
            catch (Exception)
            {

                Console.WriteLine("message");
            }

            WineDetails wine = new WineDetails()
            {
                sku = skuid,
                //  wine.imageURL = imageURL;
                // wine.ratingURL = ratingURL;
                Information = Information,
                nameURL = nameURL,
                desURL = desURL,
                priceURL = priceURL,
                saleURL = saleURL,
                TastingNotes = TastingNotes,
                MoreInfo = MoreInfo
            };
            //  wine.Review = Review;
            // wine.Rating = Rating;





            string Rating1 = Rating;
            SKU = SKU.Replace("http://www.wineoutlet.com/", "");
            SKU = SKU.Replace(".html", "");
            Country = Country.Replace(" ", "");
            Region = Region.Replace(" ", "");
            Type = Type.Replace(" ", "");
            GrapeType = GrapeType.Replace(" ", "");
            nameURL = nameURL.Replace(" ", "");
            SubRegion = SubRegion.Replace(" ", "");
            //string result = SKU +" , " + nameURL +","+Vintag+"," +Country+ " , " + Region + " , " + SubRegion + " , " + GrapeType + ","+Type + "\n";
            //File.AppendAllText(@"E:\Wines.csv", result);



            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["LocalCon"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("InsertSampleData", con);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Sku", SKU);
                    //cmd.Parameters.AddWithValue("@desURL", desURL);
                    cmd.Parameters.AddWithValue("@WineName", nameURL);
                    cmd.Parameters.AddWithValue("@Vintage", Vintag);
                    cmd.Parameters.AddWithValue("@Country", Country);
                    //  cmd.Parameters.AddWithValue("@TastingNotes", TastingNotes);
                    // cmd.Parameters.AddWithValue("@MoreInfo", MoreInfo);
                    cmd.Parameters.AddWithValue("@Region", Region);
                    cmd.Parameters.AddWithValue("@SubRegion", SubRegion);
                    cmd.Parameters.AddWithValue("@GrapeType", GrapeType);
                    cmd.Parameters.AddWithValue("@WineType", Type);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception e)
            {
                string exe = e.Message.ToString();
            }

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

    }

}

