
using DataMamagerClassLibrary.DataAccess;
using DataMamagerClassLibrary.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DataManager.Controllers
{

    [Authorize]
   
    //    // GET api/Product
  
  
    public class SaleController : ApiController
    {
        public void Post(SaleModel saleModel)
        {
            SaleData data = new SaleData();
            string userId = RequestContext.Principal.Identity.GetUserId();
            data.SaveSale(saleModel, userId);
        }
    }
}
