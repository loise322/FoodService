using System;
using System.Collections.Generic;
using System.Text;

namespace Filldb
{
    public class TransferInfo
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int UserId { get; set; }
        public int LegalId { get; set; }
    }
}
