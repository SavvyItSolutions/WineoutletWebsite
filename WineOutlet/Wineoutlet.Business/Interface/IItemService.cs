using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wineoutlet.Models;

namespace Wineoutlet.Business
{
    public interface IItemService
    {
        ItemDetailsResponse GetItemList(int sku); 
    }
}
