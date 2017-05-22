using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wineoutlet.Models;
using Wineoutlet.DataAccess;

namespace Wineoutlet.Business
{
    public class ItemService : IItemService
    {
     
           
      
        public ItemDetailsResponse GetItemList(int sku)
        {
            ItemDetailsResponse idr = new ItemDetailsResponse();
            List<ItemDetails> itemList = new List<ItemDetails>();
            IDBManager idbm = new DBManager();
            IList<GetItemDetailsResult> productresult = idbm.GetLists(sku).ToList();
            foreach(GetItemDetailsResult result in productresult)
            {
                itemList.Add(new ItemDetails
                {
                    Sku = result.Sku,
                    WineName = result.Name,
                    Vintage=result.Vintage.ToString(),
                    GrapeType=result.GrapeTypeName,
                    WineType=result.WineTypeName,
                    Region=result.RegionName,
                    SubRegion=result.SubRegionName,
                    Country=result.CountryName
                });
            }
            idr.itemDetails = itemList;
            //idr.itemDetails = itemList;
            return idr;
        }
    }
}
