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
        public void Post(SaleModel saleModel)
        {
            SaleData data = new SaleData();
            string userId = RequestContext.Principal.Identity.GetUserId();
            data.SaveSale(saleModel, userId);
        }
        [Route("GetSalesReport")]
        [HttpGet]
        public List<SaleReportModel> SalesGetReport()
        {
            SaleData data = new SaleData();
            return data.GetSaleReport();
        }
    }
}
