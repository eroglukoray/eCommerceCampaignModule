using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Dto
{
    public class Order : BaseEntity
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
    }
}
