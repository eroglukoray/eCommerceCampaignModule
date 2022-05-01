using Bussiness.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Service.Campaign
{
    public interface ICampaignService
    {
        void CreateCampaign(string campaignName, ProductDto product, int duration, int priceManipulationLimit, int targetSalesCount);

        CampaignDto GetCampaignInfo(string name);

        CampaignDto GetCampaignByProductCode(string productCode);
    }
}
