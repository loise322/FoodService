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
using SchedulerService.Models;
using TravelLine.Food.Core.Import.Models;

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
            ImportFrom1cRequest file = new ImportFrom1cRequest();

            foreach ( string item in fileEntries )
            {
                file.Name = item;
                file.CreationDate = File.GetCreationTime( item ); 
                if (file.CreationDate.Date == DateTime.Now.Date)
                {
                    file.Content = File.ReadAllText( item );
                    PostRequest( file );
                } 
            }
        }

        public async Task PostRequest(ImportFrom1cRequest file)
        {
            HttpContent httpContent = new StringContent( JsonConvert.SerializeObject(file), Encoding.UTF8, "application/json" );
            _httpClient.PostAsync( _configuration.ApiUrl, httpContent);
        }
    }
}
