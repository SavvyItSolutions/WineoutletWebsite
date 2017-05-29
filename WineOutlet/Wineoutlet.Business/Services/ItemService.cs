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
        public ItemDetailsResponse GetItemDetails(int sku)
        {
            ItemDetailsResponse idr = new ItemDetailsResponse();
            List<ItemDetails> itemList = new List<ItemDetails>();
            IDBManager idbm = new DBManager();
            IList<GetItemDetailsResult> productresult = idbm.GetItemDetails(sku).ToList();
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
        public ItemDetailsResponse GetItemListByCountry(int Id)
        {
            ItemDetailsResponse idr = new ItemDetailsResponse();
            List<ItemDetails> itemList = new List<ItemDetails>();
            IDBManager idbm = new DBManager();
            IList<GetItemsByCountryIdResult> productresult = idbm.GetItemListByCountry(Id).ToList();
            foreach (GetItemsByCountryIdResult result in productresult)
            {
                itemList.Add(new ItemDetails
                {
                    Sku = result.Sku,
                    WineName = result.Name,
                    Vintage = result.Vintage.ToString(),
                    GrapeType = result.GrapeTypeName,
                    WineType = result.WineTypeName,
                    Region = result.RegionName,
                    SubRegion = result.SubRegionName,
                    Country = result.CountryName
                });
            }
            idr.itemDetails = itemList;
            //idr.itemDetails = itemList;
            return idr;
        }
        public ItemDetailsResponse GetItemListByGrape(int Id)
        {
            ItemDetailsResponse idr = new ItemDetailsResponse();
            List<ItemDetails> itemList = new List<ItemDetails>();
            IDBManager idbm = new DBManager();
            IList<GetItemsByGrapeTypeIdResult> productresult = idbm.GetItemListByGrape(Id).ToList();
            foreach (GetItemsByGrapeTypeIdResult result in productresult)
            {
                itemList.Add(new ItemDetails
                {
                    Sku = result.Sku,
                    WineName = result.Name,
                    Vintage = result.Vintage.ToString(),
                    GrapeType = result.GrapeTypeName,
                    WineType = result.WineTypeName,
                    Region = result.RegionName,
                    SubRegion = result.SubRegionName,
                    Country = result.CountryName
                });
            }
            idr.itemDetails = itemList;
            //idr.itemDetails = itemList;
            return idr;
        }
        public ItemDetailsResponse GetItemListByWine(int Id)
        {
            ItemDetailsResponse idr = new ItemDetailsResponse();
            List<ItemDetails> itemList = new List<ItemDetails>();
            IDBManager idbm = new DBManager();
            IList<GetItemsByWineTypeIdResult> productresult = idbm.GetItemListByWine(Id).ToList();
            foreach (GetItemsByWineTypeIdResult result in productresult)
            {
                itemList.Add(new ItemDetails
                {
                    Sku = result.Sku,
                    WineName = result.Name,
                    Vintage = result.Vintage.ToString(),
                    GrapeType = result.GrapeTypeName,
                    WineType = result.WineTypeName,
                    Region = result.RegionName,
                    SubRegion = result.SubRegionName,
                    Country = result.CountryName
                });
            }
            idr.itemDetails = itemList;
            //idr.itemDetails = itemList;
            return idr;
        }
        public ItemDetailsResponse GetItemListByRegion(int Id)
        {
            ItemDetailsResponse idr = new ItemDetailsResponse();
            List<ItemDetails> itemList = new List<ItemDetails>();
            IDBManager idbm = new DBManager();
            IList<GetItemsByRegionIdResult> productresult = idbm.GetItemListByRegion(Id).ToList();
            foreach (GetItemsByRegionIdResult result in productresult)
            {
                itemList.Add(new ItemDetails
                {
                    Sku = result.Sku,
                    WineName = result.Name,
                    Vintage = result.Vintage.ToString(),
                    GrapeType = result.GrapeTypeName,
                    WineType = result.WineTypeName,
                    Region = result.RegionName,
                    SubRegion = result.SubRegionName,
                    Country = result.CountryName
                });
            }
            idr.itemDetails = itemList;
            //idr.itemDetails = itemList;
            return idr;
        }
        public ItemDetailsResponse GetItemListBySubRegion(int Id)
        {
            ItemDetailsResponse idr = new ItemDetailsResponse();
            List<ItemDetails> itemList = new List<ItemDetails>();
            IDBManager idbm = new DBManager();
            IList<GetItemsBySubRegionIdResult> productresult = idbm.GetItemListBySubRegion(Id).ToList();
            foreach (GetItemsBySubRegionIdResult result in productresult)
            {
                itemList.Add(new ItemDetails
                {
                    Sku = result.Sku,
                    WineName = result.Name,
                    Vintage = result.Vintage.ToString(),
                    GrapeType = result.GrapeTypeName,
                    WineType = result.WineTypeName,
                    Region = result.RegionName,
                    SubRegion = result.SubRegionName,
                    Country = result.CountryName
                });
            }
            idr.itemDetails = itemList;
            //idr.itemDetails = itemList;
            return idr;
        }
    }
}
