using Bussiness.Dto;
using Bussiness.Service.Order;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Test.Order
{
    public class OrderServiceTests
    {
        private readonly IOrderService orderService;
        public OrderServiceTests()
        {
            this.orderService = new OrderService();
        }

        [Fact]
        public void OrderService_Should_Create()
        {
            var product = new ProductDto("P1", 100, 1000);

            orderService.CreateOrder(product, 5, new TimeSpan(0, 0, 0));

            var order = orderService.GetOrders().FirstOrDefault(x => x.Product.Id == product.Id);

            order.Product.Should().Be(product);
        }
        [Fact]
        public void OrderService_Should_Calculate_Total_Sales_By_Campaign()
        {
            var product = new ProductDto("P1", 100, 1000);

            product.SetCampaign(new CampaignDto("C1", product, 10, 20, 10));

            orderService.CreateOrder(product, 5, new TimeSpan(0, 0, 0));

            int totalSales = orderService.GetTotalSalesByCampaign("C1");

            totalSales.Should().Be(5);
        }
        [Fact]
        public void OrderService_Should_Calculate_Avarage_Item_Price_By_Campaign()
        {
            var product = new ProductDto("P1", 100, 1000);

            product.SetCampaign(new CampaignDto("C1", product, 10, 20, 10));

            orderService.CreateOrder(product, 5, new TimeSpan(0, 0, 0));

            double avarageItemPrice = orderService.GetAvarageItemPriceByCampaign("C1");

            avarageItemPrice.Should().Be(product.Price);
        }

    }
}
