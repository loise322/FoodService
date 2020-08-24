using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Quartz;
using SchedulerService.Services.Import;
using TravelLine.Food.Core.Import.Models;
using TravelLine.Food.Domain.Legals;
using TravelLine.Food.Domain.Users;
using TravelLine.Food.Domain.WorkTimes;
using TravelLine.Food.Infrastructure;

namespace SchedulerService.Quartz
{
    public class FileChecker : IJob
    {
        private readonly FoodContext _context;
        private readonly IImportService _importService;

        public FileChecker( FoodContext context, IImportService importService )
        {
            _context = context;
            _importService = importService;
        }
        public async Task Execute( IJobExecutionContext context )
        {
            string pathDirectory = @"E:\checkScheduler";
            string[] fileEntries = Directory.GetFiles( pathDirectory );

            var _xmlSerializer = new XmlSerializer( typeof( ImportModel ) );
            foreach ( string item in fileEntries )
            {
                using ( var fs = new FileStream( item, FileMode.Open ) )
                {
                    _importService.ImportFrom1c(fs);
                }
            }
        }

        private List<User> ImportUsers( UserModel[] models )
        {
            List<User> users = _context.Users.AsNoTracking().ToList();
            foreach ( UserModel model in models )
            {
                if ( !users.Any( u => u.ExternalId == model.Id ) )
                {
                    User user = users.FirstOrDefault( u => u.Name.Replace( " ", "" ).ToLower() == model.FullName.Replace( " ", "" ).ToLower() );
                    if ( user != null )
                    {
                        user.Code = model.Code;
                        user.ExternalId = model.Id;

                        _context.Users.AddOrUpdate( user );
                        _context.SaveChanges();
                    }
                }
            }
            return users;
        }
    }
}
