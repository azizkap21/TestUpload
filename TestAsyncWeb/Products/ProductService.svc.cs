using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestAsyncWeb.Products
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceContract]
    public class ProductService
    {

        [OperationContract()]
        [WebGet(UriTemplate = "GetProductsAsync")]
        public async Task<string> GetProductsAsync()
        {

            Task<string> task = Task.Run(() => GetProducts());

            return await task;

        }

        private string GetProducts()
        {

            List<Product> products = new List<Product>();

            Thread.Sleep(10000);

            products.Add(new Product()
            {
                BrandName = "Samsung",
                CategoryName = "TV",
                Price = 560,
                ProdutName = "Samsung 42 inch Full HD LED TV",

            });

            products.Add(new Product()
            {
                BrandName = "Samsung",
                CategoryName = "TV",
                Price = 720,
                ProdutName = "Samsung 47 inch Full HD LED TV",

            });

            products.Add(new Product()
            {
                BrandName = "Samsung",
                CategoryName = "TV",
                Price = 380,
                ProdutName = "Samsung 32 inch Full HD LED TV",

            });

            products.Add(new Product()
            {
                BrandName = "Samsung",
                CategoryName = "TV",
                Price = 1020,
                ProdutName = "Samsung 60 inch Full HD LED TV",

            });

            string retStr = Newtonsoft.Json.JsonConvert.SerializeObject(products);

            return retStr;

        }


        [OperationContract]
        [WebGet(UriTemplate="GetCurrentTime")]
        public async Task<string> GetCurrentTime()
        {
            return await Task.Run<string>(() => { return DateTime.Now.ToString(); });
        }



    }
}
