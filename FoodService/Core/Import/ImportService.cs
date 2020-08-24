using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using TravelLine.Food.Core.Import.Models;
using TravelLine.Food.Domain.Legals;
using TravelLine.Food.Domain.Users;
using TravelLine.Food.Domain.WorkTimes;

namespace TravelLine.Food.Core.Import
{
    public class ImportService : IImportService
    {
        private readonly ILegalRepository _legalRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWorkTimeRepository _workTimeRepository;

        public ImportService( ILegalRepository legalRepository, IUserRepository userRepository, IWorkTimeRepository workTimeRepository )
        {
            _legalRepository = legalRepository;
            _userRepository = userRepository;
            _workTimeRepository = workTimeRepository;
        }

        public void ImportFrom1c( Stream input )
        {
            var _xmlSerializer = new XmlSerializer( typeof( ImportModel ) );

            var model = ( ImportModel )_xmlSerializer.Deserialize( input );

            List<User> users = ImportUsers( model.Operation.Users );

            ImportWorkTimes( model.Operation.WorkDays, _legalRepository.GetAll(), users );
        }

        private List<User> ImportUsers( UserModel[] models )
        {
            List<User> users = _userRepository.GetAll();

            foreach ( UserModel model in models )
            {
                if ( !users.Any( u => u.ExternalId == model.Id ) )
                {
                    User user = users.FirstOrDefault( u => u.Name.Replace(" ", "").ToLower() == model.FullName.Replace( " ", "" ).ToLower() );
                    if ( user != null )
                    {
                        user.Code = model.Code;
                        user.ExternalId = model.Id;

                        _userRepository.Save( user );
                    }
                }
            }

            return users;
        }

        private void ImportWorkTimes( WorkDaysModel[] models, List<Legal> legals, List<User> users )
        {
            var month = DateTime.Parse( models[ 0 ].Period );

            _workTimeRepository.RemoveWorkTimes( month );

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
                _workTimeRepository.Save( workTimes );
            }
        }
    }
}
