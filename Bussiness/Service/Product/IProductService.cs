using Bussiness.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Service.Product
{
    public interface IProductService
    {
        void CreateProduct(string productCode, double price, int stock);
        ProductDto GetProduct(string productCode);
        void IncraseTime(int totalIncrase);
    }
}
