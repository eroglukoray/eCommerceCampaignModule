using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Dto
{
    public class Campaign : BaseEntity
    {

        public string Name { get; set; }
        public string ProductCode { get; set; }
        public int Duration { get; set; }
        public int PriceManipulationLimit { get; set; }
        public int TargetSalesCount  { get; set; }

    }
}
