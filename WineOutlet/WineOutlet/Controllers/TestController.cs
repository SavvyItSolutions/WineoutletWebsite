using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Wineoutlet.Business;
using Wineoutlet.DataAccess;
using Wineoutlet.Models;

namespace WineOutlet.Controllers
{
    public class TestController : ApiController
    {

        [HttpGet]
        public string Test()
        {
            return "Lokesh";
        }
    }
}