using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Dto
{
    public class ProductDto :BaseEntity
    {
        public ProductDto(string productCode, double price, int stock)
        {
            ProductCode = productCode;
            RealPrice = price;
            SetPrice(price);
            Stock =  stock;
            Id = new Guid();
        }

        public string ProductCode { get;  set; }
        public double RealPrice { get;  set; }
        public double Price { get;  set; }
        public int Stock { get;  set; }
        private CampaignDto Campaign { get; set; }
        public void SetCampaign(CampaignDto campaign) => Campaign = campaign;
        private void SetPrice(double price) => Price = price;
        public void MakeDiscount(double price)
        {
            if (HasCampaign())
            {
                Price = price;
                if (Campaign.IsPriceManipulationLimitExceed())
                {
                    Price = RealPrice;
                    Campaign.CampaignClose();
                }
            }
        }

        internal bool HasCampaign() => Campaign != null && Campaign.Status;
        internal CampaignDto GetCampaign() => Campaign;
    }
}
