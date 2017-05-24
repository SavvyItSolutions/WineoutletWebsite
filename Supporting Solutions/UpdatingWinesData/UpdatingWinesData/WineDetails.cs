using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdatingWinesData
{
    public class WineDetails
    {
        public string Sku { get; set; }
        public string WineName { get; set; }
        public string Vintage { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string SubRegion { get; set; }
        public string GrapeType { get; set; }
        public string WineType { get; set; }
    }
}
