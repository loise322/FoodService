using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TravelLine.Food.Core.Import.Models;
using TravelLine.Food.Domain.Legals;
using TravelLine.Food.Domain.Users;
using TravelLine.Food.Domain.WorkTimes;
using TravelLine.Food.Infrastructure;

namespace SchedulerService.Services.Import
{
    public class ImportService : IImportService
    {
        private readonly FoodContext _context;

        public ImportService( FoodContext context )
        {
            _context = context;
        }

        public void ImportFrom1c( Stream input )
        {
            var _xmlSerializer = new XmlSerializer( typeof( ImportModel ) );

            var model = ( ImportModel )_xmlSerializer.Deserialize( input );

            List<User> users = ImportUsers( model.Operation.Users );

            ImportWorkTimes( model.Operation.WorkDays, _context.Legals.AsNoTracking().ToList() , users );
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

        private void ImportWorkTimes( WorkDaysModel[] models, List<Legal> legals, List<User> users )
        {
            var month = DateTime.Parse( models[ 0 ].Period );

            RemoveWorkTimes(month);

            var workTimes = new List<WorkTime>();
            foreach ( WorkDaysModel model in models )
            {
                Legal legal = legals.FirstOrDefault( l => l.ExternalId == model.LegalId );
                User user = users.FirstOrDefault( u => u.ExternalId == model.UserId );

                if ( legal != null && user != null )
                {
                    workTimes.Add( new WorkTime
                    {
                        Month = month,
                        Days = model.Days,
                        LegalId = legal.Id,
                        UserId = user.Id
                    } );
                }
            }

            if ( workTimes.Count > 0 )
            {
                foreach ( var workTime in workTimes )
                {
                    _context.WorkTimes.AddOrUpdate( workTime );
                }
                _context.SaveChanges();
            }
        }

        public void RemoveWorkTimes( DateTime month )
        {
            var workTimes = _context.WorkTimes.AsQueryable().Where( wt => wt.Month == month ).ToList();
            if ( workTimes.Count > 0 )
            {
                _context.WorkTimes.RemoveRange( workTimes );
                _context.SaveChanges();
            }
        }
    }
}
