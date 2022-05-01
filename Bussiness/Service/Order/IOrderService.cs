using Bussiness.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Service.Order
{
    public interface IOrderService
    {
        void CreateOrder(ProductDto product, int quantity, TimeSpan systemTime);

        List<OrderDto> GetOrdersByCampaignName(string campaignName);

        List<OrderDto> GetOrders();
        int GetTotalSalesByCampaign(string value);
        double GetAvarageItemPriceByCampaign(string value);
    }
}
