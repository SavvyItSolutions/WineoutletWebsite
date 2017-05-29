using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wineoutlet.Models;

namespace Wineoutlet.DataAccess
{
    public interface IDBManager
    {
        IList<GetItemDetailsResult> GetItemDetails(int sku);
        IList<GetItemsByCountryIdResult> GetItemListByCountry(int Id);
        IList<GetItemsByGrapeTypeIdResult> GetItemListByGrape(int Id);
        IList<GetItemsByWineTypeIdResult> GetItemListByWine(int Id);
        IList<GetItemsByRegionIdResult> GetItemListByRegion(int Id);
        IList<GetItemsBySubRegionIdResult> GetItemListBySubRegion(int Id);
    }
}
