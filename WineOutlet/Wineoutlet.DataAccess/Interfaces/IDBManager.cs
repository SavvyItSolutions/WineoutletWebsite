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
        IList<GetItemDetailsResult> GetLists(int sku);
    }
}
