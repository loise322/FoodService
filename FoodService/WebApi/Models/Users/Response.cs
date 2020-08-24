using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace TravelLine.Food.WebApi.Models.Users
{
    public class Response
    {
        public IHttpActionResult Status { get; set; }
        public string Error { get; set; }
    }
}
