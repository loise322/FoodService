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

            foreach ( string item in fileEntries )
            {
                DateTime CreationTime = File.GetCreationTime( item ); 
                if (CreationTime.Date == DateTime.Now.Date)
                {
                    var fs = new FileStream( item, FileMode.Open );
                    PostRequestFileStream( fs );
                } 
            }
        }

        public async Task PostRequestFileStream(FileStream file)
        {
            HttpContent httpContent = new StreamContent( file );
            _httpClient.PostAsync( _configuration.ApiUrl, httpContent);
        }
    }
}
