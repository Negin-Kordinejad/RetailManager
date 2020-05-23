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
    public class SaleController : ApiController
    {
        [Authorize(Roles = "Cashier")]
        public void Post(SaleModel saleModel)
        {
            SaleData data = new SaleData();
            string userId = RequestContext.Principal.Identity.GetUserId();
            data.SaveSale(saleModel, userId);
        }
        [Route("GetSalesReport")]
        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public List<SaleReportModel> SalesGetReport()
        {
            if(RequestContext.Principal.IsInRole("Admin"))
            {
                //To Admin Stuff
            }
            else if(RequestContext.Principal.IsInRole("Manager  "))
            {
                //Do Manager stuff
            }
            SaleData data = new SaleData();
            return data.GetSaleReport();
        }
    }
}
