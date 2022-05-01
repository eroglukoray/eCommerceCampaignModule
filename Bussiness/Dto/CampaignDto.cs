using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Dto
{
    public class CampaignDto : BaseEntity
    {
        public CampaignDto(string name, ProductDto product, int duration, int limit, int targetSalesCount)
        {
            Id = new Guid();
            Name = name;
            Product = product;
            Duration = duration;
            Limit = limit;
            Count = targetSalesCount;
            Status = true;
          
        }
        public string Name { get; private set; }
        public ProductDto Product { get; private set; }
        public int Duration { get; private set; }
        public int Limit { get; private set; }
        public int Count { get; private set; }
        public bool Status { get; private set; }
        public int TotalSalesCount { get; private set; }
        public void IncraseTotalSalesCount(int quantity) => TotalSalesCount += quantity;
        public void CampaignClose() => Status = false;
        public bool HasTargetSalesCountExceed(int quantity) => TotalSalesCount + quantity > Count;
        public bool IsPriceManipulationLimitExceed()
        {
            var maximumManipulationValue = Product.RealPrice * (100 - Limit) / 100;

            return Product.Price < maximumManipulationValue;
        }

        public bool HasDuration(TimeSpan localTime) => localTime.Hours < Duration;

        public string GetStatusString() => Status ? "Active" : "Ended";
    }
}
