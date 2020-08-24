using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TravelLine.Food.Core.Transfers;
using TravelLine.Food.Core.Users;
using TravelLine.Food.Domain.Legals;
using TravelLine.Food.Domain.Users;
using TravelLine.Food.WebApi.Models;
using TravelLine.Food.WebApi.Models.Users;

namespace TravelLine.Food.WebApi.Controllers
{
    [RoutePrefix( "user" )]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IUserLegalRepository _userLegalRepository;
        private readonly ITransferService _transferService;

        public UserController( IUserService userService, IUserLegalRepository userLegalRepository, ITransferService transferService )
        {
            _userService = userService;
            _userLegalRepository = userLegalRepository;
            _transferService = transferService;
        }

        [HttpGet]
        [Route( "search" )]
        public IHttpActionResult Search( string userName )
        {
            if ( userName.Length < 4 )
            {
                return BadRequest( "UserName length less then 5 letters" );
            }

            List<UserModel> users = _userService.Search( userName );

            return Ok( users.Select( GetUserDto ) );
        }

        [HttpPost]
        [Route( "save-userlegal" )]
        public IHttpActionResult SaveUserLegal( [FromBody]SaveTransferRequest saveTransferRequest )
        {
            TransferDto transferDto = saveTransferRequest.TransferDto;
            UserModel user = _userService.GetUser( transferDto.UserId );

            if ( user == null )
            {
                return BadRequest( "Пользователь не найден." );
            }

            else if ( transferDto.TransferReason == TransferReasonsType.Application || transferDto.TransferReason == TransferReasonsType.Dismissal )
            {
                transferDto.EndDate = null;
            }

            if ( transferDto.TransferReason == TransferReasonsType.Dismissal )
            {
                transferDto.LegalId = user.UserLegals.Last().LegalId;
            }

            if ( user.UserLegals.Last().TransferReason == TransferReasonsType.Dismissal && transferDto.TransferReason != TransferReasonsType.Application )
            {
                if ( transferDto.UserLegalId == 0 )
                {
                    return BadRequest( "Этого пользователя можно только принять." );
                }
            }

            if ( user.UserLegals.Last().TransferReason != TransferReasonsType.Dismissal && transferDto.TransferReason == TransferReasonsType.Application )
            {
                if ( transferDto.UserLegalId == 0 )
                {
                    return BadRequest( "Принять пользователя можно только после увольнения." );
                }
            }

            if ( saveTransferRequest.TransferDto.UserLegalId == 0 )
            {
                UserLegal legals = user.UserLegals.FirstOrDefault( u => u.StartDate >= transferDto.StartDate && u.TransferReason != TransferReasonsType.NoReason );
                if ( legals != null )
                {
                    return BadRequest( "Такая дата уже существует." );
                }
            }

            UserLegal previousLegal = user.UserLegals.LastOrDefault( u => u.StartDate <= transferDto.StartDate && u.Id != transferDto.UserLegalId );
            if ( previousLegal != null )
            {
                if ( transferDto.LegalId == previousLegal.LegalId
                    && transferDto.TransferReason != TransferReasonsType.Dismissal
                    && transferDto.TransferReason != TransferReasonsType.Application )
                {
                    return BadRequest( "Нельзя сохранять в то же юр. лицо." );
                }
            }

            if ( transferDto.TransferReason == TransferReasonsType.Application || transferDto.TransferReason == TransferReasonsType.Dismissal )
            {
                if ( transferDto.StartDate == null )
                {
                    return BadRequest( "Дата не должна быть пустой." );
                }
            }
            else
            {
                if ( ( transferDto.StartDate == null ) || ( transferDto.EndDate == null ) )
                {
                    return BadRequest( "Даты не должны быть пустыми." );
                }
            }

            if ( transferDto.StartDate > transferDto.EndDate )
            {
                return BadRequest( "Дата начала позднее даты завершения." );
            }

            try
            {
                _transferService.SaveTransfer( user, transferDto );
                
                return Ok();
            }
            catch ( Exception ex )
            {
                return BadRequest( ex.Message );
            }
        }

        [HttpGet]
        [Route( "get-userlegal" )]
        public IHttpActionResult GetUserLegal( [FromUri]GetTransferRequest transferRequest )
        {
            TransferDto transferDto = new TransferDto();

            transferDto = _transferService.GetUserLegal( transferRequest.UserId, transferRequest.Id );

            GetTransferResponse transferResponse = new GetTransferResponse()
            {
                TransferDto = transferDto
            };

            return Ok( transferResponse );
        }

        [HttpPost]
        [Route( "delete-userlegal" )]
        public IHttpActionResult DeleteUserLegal( [FromBody]TransferDeleteRequest transferDelete )
        {
            _transferService.DeleteTransfer( transferDelete.UserId, transferDelete.Id );
            return Ok();
        }

        private UserDto GetUserDto( UserModel user )
        {
            return new UserDto()
            {
                Id = user.Id,
                UserName = user.Name
            };
        }
    }
}
