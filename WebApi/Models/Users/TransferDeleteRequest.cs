﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelLine.Food.WebApi.Models.Users
{
    public class TransferDeleteRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }
}
