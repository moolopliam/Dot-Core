using System;
using System.Collections.Generic;

namespace TEST.Models
{
    public partial class TTitel
    {
        public TTitel()
        {
            TCustomer = new HashSet<TCustomer>();
        }

        public string InitialCode { get; set; }
        public string InitialName { get; set; }

        public ICollection<TCustomer> TCustomer { get; set; }
    }
}
