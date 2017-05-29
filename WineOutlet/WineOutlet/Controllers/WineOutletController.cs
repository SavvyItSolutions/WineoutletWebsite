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
        public ItemDetailsResponse GetItemDetails(int objectId)
        {
            ItemDetailsResponse resp = new ItemDetailsResponse();
            IItemService itemService = new ItemService();
            resp = itemService.GetItemDetails(objectId);
            return resp;
        }
        public ItemDetailsResponse GetItemListByCountry(int objectId)
        {
            ItemDetailsResponse resp = new ItemDetailsResponse();
            IItemService itemService = new ItemService();
            resp = itemService.GetItemListByCountry(objectId);
            return resp;
        }
        public ItemDetailsResponse GetItemListByGrape(int objectId)
        {
            ItemDetailsResponse resp = new ItemDetailsResponse();
            IItemService itemService = new ItemService();
            resp = itemService.GetItemListByGrape(objectId);
            return resp;
        }
        public ItemDetailsResponse GetItemListByWine(int objectId)
        {
            ItemDetailsResponse resp = new ItemDetailsResponse();
            ItemService itemService = new ItemService();
            resp = itemService.GetItemListByWine(objectId);
            return resp;
        }
        public ItemDetailsResponse GetItemListByRegion(int objectId)
        {
            ItemDetailsResponse resp = new ItemDetailsResponse();
            ItemService itemService = new ItemService();
            resp = itemService.GetItemListByRegion(objectId);
            return resp;
        }
        public ItemDetailsResponse GetItemListBySubRegion(int objectId)
        {
            ItemDetailsResponse resp = new ItemDetailsResponse();
            ItemService itemService = new ItemService();
            resp = itemService.GetItemListBySubRegion(objectId);
            return resp;
        }
    }
}
