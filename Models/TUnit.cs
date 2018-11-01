using System;
using System.Collections.Generic;

namespace TEST.Models
{
    public partial class TUnit
    {
        public TUnit()
        {
            TProduct = new HashSet<TProduct>();
        }

        public string UnitCode { get; set; }
        public string NameUnit { get; set; }

        public ICollection<TProduct> TProduct { get; set; }
    }
}
