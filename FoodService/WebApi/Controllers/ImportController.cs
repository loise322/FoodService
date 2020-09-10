using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using TravelLine.Food.Core.Calendar;
using TravelLine.Food.Core.Dishes;
using TravelLine.Food.Core.Import;
using TravelLine.Food.Core.Menus;
using TravelLine.Food.WebApi.Models;

namespace TravelLine.Food.WebApi.Controllers
{
    [RoutePrefix( "import" )]
    public class ImportController : ApiController
    {
        private readonly IImportService _importService;

        public ImportController( IImportService importService )
        {
            _importService = importService;
        }

        [HttpPost]
        [Route( "from1c" )]
        public IHttpActionResult Import(FileStream file)
        {
            _importService.ImportFrom1c( file );
            return Ok();
        }
    }
}
