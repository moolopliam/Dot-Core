using System;
using System.Collections.Generic;

namespace TEST.Models
{
    public partial class TType
    {
        public TType()
        {
            TCustomer = new HashSet<TCustomer>();
        }

        public string CustType { get; set; }
        public string NameType { get; set; }

        public ICollection<TCustomer> TCustomer { get; set; }
    }
}
