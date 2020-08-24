using System;
using System.Linq;
using TravelLine.Food.Core.Users;
using TravelLine.Food.Domain.Legals;
using TravelLine.Food.Domain.Users;

namespace TravelLine.Food.Core.Transfers
{
    public class TransferService : ITransferService
    {
        private readonly IUserService _userService;
        private readonly IUserLegalRepository _userLegalRepository;

        public TransferService( IUserService userService, IUserLegalRepository userLegalRepository )
        {
            _userService = userService;
            _userLegalRepository = userLegalRepository;
        }

        public void DeleteTransfer( int userId, int transferId )
        {
            UserModel user = _userService.GetUser( userId );
            UserLegal userLegal = user?.UserLegals?.FirstOrDefault( u => u.Id == transferId );
            UserLegal previousLegal = user.UserLegals.LastOrDefault( u => u.StartDate < userLegal.StartDate );
            UserLegal previousPreviousLegal = user.UserLegals.LastOrDefault( u => u.StartDate < previousLegal.StartDate );
            UserLegal nextUserLegal = user?.UserLegals?.FirstOrDefault( u => u.StartDate > userLegal.StartDate );

            if ( userLegal != null )
            {
                if ( previousLegal.TransferReason != TransferReasonsType.NoReason
                    && previousLegal.TransferReason != TransferReasonsType.Application
                    && previousLegal.TransferReason != TransferReasonsType.Dismissal )
                {
                    UserLegal newUserLegal = new UserLegal()
                    {
                        LegalId = user.UserLegals.Last( ul => ul.TransferReason == TransferReasonsType.Application ).LegalId,
                        StartDate = userLegal.StartDate,
                        UserId = user.Id,
                        TransferReason = TransferReasonsType.NoReason
                    };

                    _userLegalRepository.Save( newUserLegal );
                }

                _userLegalRepository.Remove( userLegal.Id );

                if ( previousPreviousLegal != null )
                {
                    if ( ( previousPreviousLegal.TransferReason == TransferReasonsType.Dismissal
                        || previousPreviousLegal.TransferReason == TransferReasonsType.Application )
                        && previousLegal.TransferReason == TransferReasonsType.NoReason )
                    {
                        _userLegalRepository.Remove( previousLegal.Id );
                    }
                }

                if ( nextUserLegal != null )
                {
                    if ( nextUserLegal.TransferReason == TransferReasonsType.NoReason )
                    {
                        _userLegalRepository.Remove( nextUserLegal.Id );
                    }
                }
            };
        }

        public TransferDto GetUserLegal( int userId, int transferRequestId )
        {
            UserModel user = _userService.GetUser( userId );
            UserLegal userLegal = user?.UserLegals?.FirstOrDefault( u => u.Id == transferRequestId );

            UserLegal nextLegal = user.UserLegals.FirstOrDefault( u => u.StartDate > userLegal.StartDate && u.TransferReason != TransferReasonsType.NoReason );
            UserLegal nextLegalWithNoReason = user.UserLegals.FirstOrDefault( u => u.StartDate > userLegal.StartDate );
            UserLegal previousLegal = user.UserLegals.LastOrDefault( u => u.StartDate < userLegal.StartDate );
            TransferDto transferDto = new TransferDto()
            {
                TransferReason = userLegal.TransferReason,
                StartDate = userLegal.StartDate,
                EndDate = nextLegalWithNoReason != null ? nextLegalWithNoReason.StartDate.AddDays( -1 ) : ( DateTime? )null,
                LegalId = userLegal.LegalId,
                UserId = userLegal.UserId,
                UserLegalId = userLegal.Id,
                MaxDate = nextLegal != null ? nextLegal.StartDate.AddDays( -1 ) : ( DateTime? )null,
            };

            if ( previousLegal != null )
            {
                if ( previousLegal.TransferReason == TransferReasonsType.NoReason )
                {
                    if ( DateTime.Today.AddDays( 2 ) > previousLegal.StartDate )
                    {
                        transferDto.MinDate = DateTime.Today.AddDays( 2 );
                    }
                    else
                    {
                        transferDto.MinDate = previousLegal.StartDate;
                    }
                }
                else
                {
                    transferDto.MinDate = transferDto.StartDate;
                }
            }
            else
            {
                transferDto.MinDate = DateTime.Today;
            }

            return transferDto;
        }

        public void SaveTransfer( UserModel user, TransferDto transferDto )
        {
            int userLegalId = transferDto.UserLegalId;
            DateTime startDate = transferDto.StartDate;
            DateTime? endDate = transferDto.EndDate;
            int legalId = transferDto.LegalId;
            TransferReasonsType transferReason = transferDto.TransferReason;

            if ( userLegalId != 0 )
            {
                UserLegal userLegal = _userLegalRepository.GetUserLegal( userLegalId );
                // Если поменяли дату начала
                if ( userLegal.StartDate != startDate )
                {
                    UserLegal previousUserLegal = user.UserLegals?.LastOrDefault( u => u.StartDate < userLegal.StartDate );
                    if ( previousUserLegal != null )
                    {
                        if ( startDate < userLegal.StartDate && previousUserLegal.TransferReason == TransferReasonsType.NoReason )
                        {
                            if ( startDate == previousUserLegal.StartDate )
                            {
                                _userLegalRepository.Remove( previousUserLegal.Id );
                                userLegal.StartDate = startDate;
                                _userLegalRepository.Save( userLegal );
                            }
                            else
                            {
                                userLegal.StartDate = startDate;
                                _userLegalRepository.Save( userLegal );
                            }
                        }
                        else if ( startDate > userLegal.StartDate && previousUserLegal.TransferReason != TransferReasonsType.NoReason )
                        {
                            if ( startDate == endDate )
                            {
                                userLegal.StartDate = startDate;
                                _userLegalRepository.Save( userLegal );
                            }
                            else
                            {
                                UserLegal newUserLegal = new UserLegal()
                                {
                                    LegalId = user.UserLegals.Last( ul => ul.TransferReason == TransferReasonsType.Application ).LegalId,
                                    StartDate = userLegal.StartDate,
                                    UserId = user.Id,
                                    TransferReason = TransferReasonsType.NoReason
                                };

                                _userLegalRepository.Save( newUserLegal );

                                userLegal.StartDate = startDate;

                                _userLegalRepository.Save( userLegal );
                            }
                        }
                        else if ( startDate > userLegal.StartDate && previousUserLegal.TransferReason == TransferReasonsType.NoReason )
                        {
                            userLegal.StartDate = startDate;
                            _userLegalRepository.Save( userLegal );
                        }
                        else if ( previousUserLegal.TransferReason == TransferReasonsType.Application )
                        {
                            userLegal.StartDate = startDate;
                            _userLegalRepository.Save( userLegal );
                        }
                    }
                    else if ( transferDto.TransferReason == TransferReasonsType.Application )
                    {
                        userLegal.StartDate = transferDto.StartDate;
                        userLegal.LegalId = transferDto.LegalId;
                        _userLegalRepository.Save( userLegal );
                    }
                }
                // Если поменяли дату окончания
                if ( endDate != null )
                {
                    UserLegal nextUserLegal = user.UserLegals?.FirstOrDefault( u => u.StartDate >= userLegal.StartDate && u.Id != userLegalId );
                    if ( nextUserLegal != null )
                    {
                        if ( nextUserLegal.TransferReason == TransferReasonsType.NoReason )
                        {
                            if ( endDate >= nextUserLegal.StartDate )
                            {
                                UserLegal nextNextUserlegal = user.UserLegals?.FirstOrDefault( u => u.StartDate > nextUserLegal.StartDate );
                                if ( nextNextUserlegal != null && nextNextUserlegal.StartDate == endDate.Value.AddDays( 1 ) )
                                {
                                    _userLegalRepository.Remove( nextUserLegal.Id );
                                    nextNextUserlegal.StartDate = endDate.Value.AddDays( 1 );
                                    _userLegalRepository.Save( nextNextUserlegal );
                                }
                                else
                                {
                                    nextUserLegal.StartDate = endDate.Value.AddDays( 1 );
                                    _userLegalRepository.Save( nextUserLegal );
                                }
                            }
                            else
                            {
                                nextUserLegal.StartDate = endDate.Value.AddDays( 1 );
                                _userLegalRepository.Save( nextUserLegal );
                            }
                        }
                        else
                        {
                            if ( nextUserLegal.StartDate != endDate.Value.AddDays( 1 ) )
                            {
                                UserLegal newUserLegal = new UserLegal()
                                {
                                    LegalId = user.UserLegals.Last( ul => ul.TransferReason == TransferReasonsType.Application ).LegalId,
                                    StartDate = endDate.Value.AddDays( 1 ),
                                    UserId = user.Id,
                                    TransferReason = TransferReasonsType.NoReason
                                };

                                _userLegalRepository.Save( newUserLegal );
                            }
                        }
                    }
                }

                if ( userLegal.LegalId != transferDto.LegalId )
                {
                    userLegal.LegalId = transferDto.LegalId;
                    _userLegalRepository.Save( userLegal );
                }
            }
            else
            {
                UserLegal previousLegalWithNoReason = user?.UserLegals?.LastOrDefault( u => u.StartDate <= startDate );
                UserLegal userLegal = new UserLegal()
                {
                    LegalId = legalId,
                    StartDate = startDate,
                    UserId = user.Id,
                    TransferReason = transferReason
                };

                if ( previousLegalWithNoReason != null )
                {
                    if ( previousLegalWithNoReason.StartDate >= startDate )
                    {
                        userLegal.Id = previousLegalWithNoReason.Id;
                    }
                }

                _userLegalRepository.Save( userLegal );

                switch ( transferReason )
                {
                    // Для больничных, отпусков и командировок возвращает пользователя обратно в прошлое юр лицо
                    case TransferReasonsType.Holiday:
                    case TransferReasonsType.Sick:
                    case TransferReasonsType.BusinessTrip:

                        var nextUserLegal = new UserLegal()
                        {
                            LegalId = user.GetUserLegal( startDate ).LegalId,
                            UserId = user.Id,
                            TransferReason = TransferReasonsType.NoReason,
                            StartDate = endDate.Value.AddDays( 1 ),
                        };

                        _userLegalRepository.Save( nextUserLegal );

                        break;
                }
            }
        }
    }
}
