using System;
using System.Collections.Generic;

namespace TEST.Models
{
    public partial class TProduct
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public double? UnitPerPrice { get; set; }
        public string Qty { get; set; }
        public string StatusId { get; set; }
        public string UnitCode { get; set; }
        public string CatId { get; set; }

        public TCatategory Cat { get; set; }
        public TSatus Status { get; set; }
        public TUnit UnitCodeNavigation { get; set; }
    }
}
