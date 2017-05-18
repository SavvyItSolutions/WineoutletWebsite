using System;
using System.Collections.Generic;
using System.Linq;
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
            ExtractData Ed = new ExtractData();
            Ed.ReadHTML("http://www.wineoutlet.com/sku01983.html");

        }
    }
}
