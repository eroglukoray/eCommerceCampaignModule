
using Bussiness.Service.Campaign;
using Bussiness.Service.Product;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Test.Product
{
    public class ProductServiceTest
    {
        private readonly IProductService productService;
        private readonly ICampaignService campaignService;
        public ProductServiceTest()
        {
            this.productService = new ProductService();
        }

        [Fact]
        public void Product_Should_Add()
        {
            productService.CreateProduct("P1", 100, 1000);

            var product = productService.GetProduct("P1");

            product.ProductCode.Should().Be("P1");

            product.Price.Should().Be(100);

            product.Stock.Should().Be(1000);
        }

        [Fact]
        public void Product_Should_MakeDiscount()
        {
            productService.CreateProduct("P1", 100, 1000);

            var product = productService.GetProduct("P1");

            product.SetCampaign(new Bussiness.Dto.CampaignDto("C1", product, 10, 70, 200));

            productService.IncraseTime(3);

            product.Price.Should().NotBe(100);

            product.RealPrice.Should().Be(100);

        }
    }
}
