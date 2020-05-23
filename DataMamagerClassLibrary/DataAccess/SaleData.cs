using DataMamagerClassLibrary.Internal.DataAccess;
using DataMamagerClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMamagerClassLibrary.DataAccess
{
    public class SaleData
    {
        public void SaveSale(SaleModel saleInfo, string CachierId)
        {

            List<SaleDetailDBModel> detaials = new List<SaleDetailDBModel>();
            ProductData products = new ProductData();
            var taxRate = ConfigHelper.GetTaxRate() / 100;

            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                var productInfo = products.GetProductById(item.ProductId);
                if (products == null)
                {
                    throw new Exception($"The product Id of {item.ProductId} could not find in the database.");
                }
                detail.PurchasPrice = productInfo.RetailPrice * detail.Quantity;
                if (productInfo.IsTaxable)
                {
                    detail.Tax = detail.PurchasPrice * taxRate;
                }

                detaials.Add(detail);
            }
            SaleDBModel sale = new SaleDBModel
            {
                SubTotal = detaials.Sum(x => x.PurchasPrice),
                Tax = detaials.Sum(x => x.Tax),
                CashierId = CachierId
            };
            sale.Total = sale.SubTotal + sale.Tax;
            //SqlDataAccess sql = new SqlDataAccess();
            //sql.SaveData<SaleDBModel>("spSale_Insert", sale, "DataManager");
            //sale.Id = sql.LoadData<int,dynamic>("spSale_LookUp", new {sale.CashierId,sale.SaleDate }, "DataManager").FirstOrDefault();
            //foreach (var item in detaials)
            //{
            //    item.SaleId = sale.Id;
            //    sql.SaveData("spSaleDetail_Insert", item, "DataManager");
            //}


            using (SqlDataAccess sql = new SqlDataAccess())
            {
                try
                {
                    sql.StartTransaction("DataManager");
                    sql.SaveDataInTransaction<SaleDBModel>("spSale_Insert", sale);
                    sale.Id = sql.LoadDataInTransaction<int, dynamic>("spSale_LookUp", new { sale.CashierId, sale.SaleDate }).FirstOrDefault();
                    foreach (var item in detaials)
                    {
                        item.SaleId = sale.Id;
                        sql.SaveDataInTransaction("spSaleDetail_Insert", item);
                    }
                    sql.CommitTransaction();
                }
                catch
                {
                    sql.RolbackTransaction();
                    throw;
                }
            }

        }
        public List<SaleReportModel> GetSaleReport()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output=sql.LoadData<SaleReportModel, dynamic>("spSale_SaleReport", new { }, "DataManager");
            return output;
        }
    }
}
