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
        ItemDetailsResponse GetItemDetails(int sku);
        ItemDetailsResponse GetItemListByCountry(int Id);
        ItemDetailsResponse GetItemListByGrape(int Id);
        ItemDetailsResponse GetItemListByWine(int Id);
        ItemDetailsResponse GetItemListByRegion(int Id);
        ItemDetailsResponse GetItemListBySubRegion(int Id);
    }
}
