using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Dto
{
    public class OrderDto :BaseEntity
    {
        public OrderDto(ProductDto product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            Id = new Guid();
        }
        public int Quantity { get; private set; }
        public double SalesPrice { get; set; }
        public ProductDto Product { get; private set; }
        public CampaignDto Campaign { get; set; }
        public void SetSalesPrice(double price) => SalesPrice = price;
        public void SetCampaign(CampaignDto campaign) => Campaign = campaign;
    }
}
