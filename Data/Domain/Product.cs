using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Dto
{
    public class Product : BaseEntity
    {
        public string ProductCode { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
    }
}
