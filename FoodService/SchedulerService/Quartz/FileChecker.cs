using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Quartz;
using SchedulerService.Configs;
using TravelLine.Food.Core.Import.Models;
using TravelLine.Food.Domain.Legals;
using TravelLine.Food.Domain.Users;
using TravelLine.Food.Domain.WorkTimes;
using TravelLine.Food.Infrastructure;

namespace SchedulerService.Quartz
{
    public class FileChecker : IJob
    {
        private readonly QuartzConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public FileChecker( QuartzConfiguration configuration, HttpClient httpClient )
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task Execute( IJobExecutionContext context )
        {
            string pathDirectory = _configuration.PathDirectory;
            string[] fileEntries = Directory.GetFiles( pathDirectory );

            var _xmlSerializer = new XmlSerializer( typeof( ImportModel ) );
            foreach ( string item in fileEntries )
            {
                using ( var fs = new FileStream( item, FileMode.Open ) )
                {
                    var model = ( ImportModel )_xmlSerializer.Deserialize( fs );
                    if (model.Operation.Date.Day == DateTime.Now.Day)
                    {
                        PostXmlPath( item );
                    }
                }
            }
        }

        public async Task PostXmlPath(string path)
        {
            HttpContent httpContent = new StringContent( JsonConvert.SerializeObject( path ), Encoding.UTF8 );
            _httpClient.PostAsync( "api/import", httpContent );
        }
    }
}
