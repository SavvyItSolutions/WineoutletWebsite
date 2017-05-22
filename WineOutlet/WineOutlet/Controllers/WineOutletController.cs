using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wineoutlet.Business;
using Wineoutlet.DataAccess;
using Wineoutlet.Models;

namespace WineOutlet.Controllers
{
    public class WineOutletController : ApiController
    {

    
        [HttpGet]
        public string TestService()
        {
            return "Lokesh";
        }
        [HttpGet]
        public ItemDetailsResponse GetItemList(int objectId)
        {
            ItemDetailsResponse resp = new ItemDetailsResponse();
            IItemService itemService = new ItemService();
            resp = itemService.GetItemList(objectId);
            return resp;
        }
    }
}
