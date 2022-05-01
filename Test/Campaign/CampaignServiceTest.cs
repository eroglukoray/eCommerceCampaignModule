using Bussiness.Dto;
using Bussiness.ExceptionMessage;
using Bussiness.Service.Campaign;
using Bussiness.Service.Order;
using Bussiness.Service.Product;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Test.Campaign
{
    public class CampaignServiceTests
    {
        private readonly ICampaignService _campaignService;
        private readonly IProductService _productService;
        ProductDto _product;

        public CampaignServiceTests()
        {
            _productService = new ProductService();
            _campaignService = new CampaignService(new OrderService());
            CreateProduct();
            _product = GetProduct("P1");
        }

        [Fact]
        public void CampaignService_Should_Add_New_Campaign()
        {
            _campaignService.CreateCampaign("C1", _product, 10, 20, 100);

            var campaign = _campaignService.GetCampaignInfo("C1");

            campaign.Count.Should().Be(100);

            campaign.Limit.Should().Be(20);

            campaign.Duration.Should().Be(10);

            campaign.Name.Should().Be("C1");

            campaign.Product.ProductCode.Should().Be("P1");
        }
        [Fact]
        public void CampaignService_Should_ThrowLogicException_CampaignAlreadyExists()
        {

            _campaignService.CreateCampaign("C1", _product, 10, 20, 100);

            Assert.Throws<LogicException>(() =>
            {
                _campaignService.CreateCampaign("C1", _product, 10, 20, 100);
            });
        }
        [Fact]
        public void CampaignService_Should_GetProductCode()
        {
       
            _campaignService.CreateCampaign("C1", _product, 10, 20, 100);

            var campaign = _campaignService.GetCampaignByProductCode("P1");

            campaign.Count.Should().Be(100);

            campaign.Limit.Should().Be(20);

            campaign.Duration.Should().Be(10);

            campaign.Name.Should().Be("C1");

            campaign.Product.ProductCode.Should().Be("P1");
        }

        private void CreateProduct()
        {
            _productService.CreateProduct("P1", 100, 1000);
        }
        private ProductDto GetProduct(string productCode)
        {
           return _productService.GetProduct("P1");
        }
    }
}
