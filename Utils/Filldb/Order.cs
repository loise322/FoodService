using System;
using System.Collections.Generic;
using System.Text;

namespace Filldb
{
    public class Order
    {
        public DateTime OrderDate { get; set; }

        public int UserId { get; set; }

        public int LegalId { get; set; }

        public int TransferReason { get; set; }
    }
}
