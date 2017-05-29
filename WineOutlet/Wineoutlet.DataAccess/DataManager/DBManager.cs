using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wineoutlet.Models;
using System.Data.Linq;

namespace Wineoutlet.DataAccess
{
    public class DBManager : IDBManager
    {
        public WineOutletDataContext WoDBContext;
        public DBManager()
        {
            string connection = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            WoDBContext = new DataAccess.WineOutletDataContext(connection);
        }
        public IList<GetItemDetailsResult> GetItemDetails(int sku)
        {
            try
            {
                ISingleResult<GetItemDetailsResult> result =
                WoDBContext.GetItemDetails(sku);
                return result.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public IList<GetItemsByCountryIdResult> GetItemListByCountry(int Id)
        {
            try
            {
                ISingleResult<GetItemsByCountryIdResult> result =
                WoDBContext.GetItemsByCountryId(Id);
                return result.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public IList<GetItemsByGrapeTypeIdResult> GetItemListByGrape(int Id)
        {
            try
            {
                ISingleResult<GetItemsByGrapeTypeIdResult> result =
                WoDBContext.GetItemsByGrapeTypeId(Id);
                return result.ToList();
            }
            catch(Exception ex)
            {
                return null;
            }

        }
        public IList<GetItemsByWineTypeIdResult> GetItemListByWine(int Id)
        {
            try
            {
                ISingleResult<GetItemsByWineTypeIdResult> result =
                WoDBContext.GetItemsByWineTypeId(Id);
                return result.ToList();
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public IList<GetItemsByRegionIdResult> GetItemListByRegion(int Id)
        {
            try
            {
                ISingleResult<GetItemsByRegionIdResult> result =
                WoDBContext.GetItemsByRegionId(Id);
                return result.ToList();
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public IList<GetItemsBySubRegionIdResult> GetItemListBySubRegion(int Id)
        {
            try
            {
                ISingleResult<GetItemsBySubRegionIdResult> result =
                    WoDBContext.GetItemsBySubRegionId(Id);
                return result.ToList();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

    }
}
