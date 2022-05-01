using Bussiness.Dto;
using Bussiness.ExceptionMessage;
using Bussiness.Log;
using Bussiness.Service.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bussiness.Service.Product
{
    public class ProductService : IProductService
    {
        private List<ProductDto> ProductList { get; set; }

        public ProductService()
        {
            if (ProductList == null)
                ProductList = new List<ProductDto>();
        }
        public void CreateProduct(string productCode, double price, int stock)
        {
            if (ProductList.Any(x => x.ProductCode == productCode))
            {
                Logger.Log("This product already exists");
            }
            else
            {
                ProductList.Add(new ProductDto(productCode, price, stock));
                Logger.Log($"Product created; code {productCode}, price {price}, stock {stock}");
            }

        }
        public ProductDto GetProduct(string productCode)
        {
            var product = ProductList.FirstOrDefault(x => x.ProductCode == productCode);
            if (product != null)
            {
                return product;
            }
            else
            {
                Logger.Log("Product not exits");
                return null;
            }
        }
        private void MakeDiscount()
        {
            foreach (var product in ProductList)
            {
                CampaignDto campaign = product.GetCampaign();

                product.MakeDiscount(product.Price - (100 / campaign.Limit));
            }
        }


        public void IncraseTime(int totalIncrase)
        {
            for (int i = 0; i < totalIncrase; i++)
            {
                MakeDiscount();
            }
        }

    }
}
