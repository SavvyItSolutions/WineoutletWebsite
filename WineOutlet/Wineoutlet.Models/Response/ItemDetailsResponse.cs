using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wineoutlet.Models;

namespace Wineoutlet.Models
{
    public class ItemDetailsResponse : BaseServiceResponse
    {
       
        public IList<ItemDetails> itemDetails { get; set; }
    }
}
