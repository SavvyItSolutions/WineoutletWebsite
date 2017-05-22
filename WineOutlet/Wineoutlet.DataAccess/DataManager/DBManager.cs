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
        public IList<GetItemDetailsResult> GetLists(int sku)
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
    }
}
