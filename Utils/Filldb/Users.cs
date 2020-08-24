using System;
using System.Collections.Generic;
using System.Text;

namespace Filldb
{
    public class Users
    {
        public int UserId { get; set; }
        public bool Enabled { get; set; }
        public DateTime LastOrderDate { get; set; }
        public int LegalId { get; set; }
    }
}
