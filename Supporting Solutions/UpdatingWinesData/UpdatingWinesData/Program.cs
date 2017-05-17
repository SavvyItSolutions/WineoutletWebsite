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
            ExtractData Ed = new ExtractData();
            Ed.ReadHTML("http://www.wineoutlet.com/sku13980.html");

        }
    }
}
