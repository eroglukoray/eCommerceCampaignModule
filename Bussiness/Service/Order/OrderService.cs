using Bussiness.Dto;
using Bussiness.Log;
using Bussiness.Service.Campaign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bussiness.Service.Order
{
    public class OrderService : IOrderService
    {
        private readonly ICampaignService _campaignService;
        private readonly IOrderService _orderService;
        private List<OrderDto> OrderList { get; set; }
        public OrderService()
        {
            if (OrderList == null)
                OrderList = new List<OrderDto>();

            _campaignService = new CampaignService(_orderService);
        }
        public void CreateOrder(ProductDto product, int quantity, TimeSpan systemTime)
        {
            if (product.Stock - quantity >= 0 )
            {
                product.Stock -= quantity;
                var order = new OrderDto(product, quantity);

                if (product.HasCampaign())
                {
                    var existCampaign = product.GetCampaign();

                    if (existCampaign.HasDuration(systemTime) && !existCampaign.HasTargetSalesCountExceed(quantity))
                    {
                        existCampaign.IncraseTotalSalesCount(quantity);

                        order.SetCampaign(existCampaign);

                        order.SetSalesPrice(product.Price);

                        OrderList.Add(order);
                        Logger.Log($"Order created; product {product.ProductCode}, quantity {quantity}");
                    }
                }
                else
                {
                    order.SetSalesPrice(product.Price);
                    OrderList.Add(order);
                    Logger.Log($"Order created; product {product.ProductCode}, quantity {quantity}");
                }
            }
            
        }

        public List<OrderDto> GetOrdersByCampaignName(string campaignName)
        {
            foreach (var order in OrderList)
            {
                if (order.Campaign != null)
                    OrderList =  OrderList.Where(x => x.Campaign.Name == campaignName).ToList();
            }
            return OrderList;
        }
        public List<OrderDto> GetOrders() => OrderList;

        public int GetTotalSalesByCampaign(string campaignName)
        {
            int totalSales = 0;
            foreach (var order in OrderList)
            {
                if (order.Campaign != null)
                    totalSales = GetOrdersByCampaignName(campaignName)?.Sum(x => x.Quantity) ?? 0;
                
            }
                return GetOrdersByCampaignName(campaignName)?.Sum(x => x.Quantity) ?? 0;
           

        }
        public double GetAvarageItemPriceByCampaign(string campaignName)
        {
            int totalSales = GetTotalSalesByCampaign(campaignName);
            double salesPrice = GetOrdersByCampaignName(campaignName)?.Sum(x => x.SalesPrice) * totalSales ?? 0;
            return salesPrice / totalSales;
        }
    }
}
