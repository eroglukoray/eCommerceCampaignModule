using Bussiness.Dto;
using Bussiness.ExceptionMessage;
using Bussiness.Log;
using Bussiness.Service.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bussiness.Service.Campaign
{
    public class CampaignService : ICampaignService
    {
        private List<CampaignDto> CampaignList { get; set; }

        private readonly IOrderService _orderService;
        public CampaignService(IOrderService orderService)
        {
            if (CampaignList == null)
                CampaignList = new List<CampaignDto>();
            _orderService = orderService;
        }

        public void CreateCampaign(string campaignName, ProductDto product, int duration, int priceManipulationLimit, int targetSalesCount)
        {
            if (GetCampaignByName(campaignName) == null)
            {
                if (!product.HasCampaign() && product.Stock-targetSalesCount>=0)
                {
                    var campaign = new CampaignDto(campaignName, product, duration, priceManipulationLimit, targetSalesCount);

                    product.SetCampaign(campaign);

                    CampaignList.Add(campaign);

                    Logger.Log($"Campaign created; name {campaign.Name}, product {product.ProductCode}, duration {campaign.Duration}, limit {campaign.Limit}, target sales count {campaign.Count}");

                }
                else
                {
                    throw new LogicException("Product stock is not enough for campaign target sales count");
                }
            }
            else
            {
                throw new LogicException("Campaign name already exists");
            }
        }

        public CampaignDto GetCampaignInfo(string name)
        {
            var campaign = GetCampaignByName(name);

            if (campaign != null)
            {
                int totalSales = _orderService.GetTotalSalesByCampaign(campaign.Name);
                double avarageItemPrice = _orderService.GetAvarageItemPriceByCampaign(campaign.Name);

                Logger.Log($"Campaign {campaign.Name} info; Status {campaign.GetStatusString()}, Target Sales {campaign.Count}, Total Sales {totalSales}, Turnover {totalSales * avarageItemPrice}, Average Item Price {avarageItemPrice}");
                return campaign;
            }
            else
            {
                Logger.Log("Campaign name is not found");

                return null;
            }
        }

        private CampaignDto GetCampaign(Func<CampaignDto, bool> predicate) => CampaignList.FirstOrDefault(predicate);

        public CampaignDto GetCampaignByProductCode(string productCode)
        {
           return GetCampaign(x => x.Product.ProductCode == productCode);
        }

        public CampaignDto GetCampaignByName(string name) => GetCampaign(x => x.Name == name);
    }
}
        
    
