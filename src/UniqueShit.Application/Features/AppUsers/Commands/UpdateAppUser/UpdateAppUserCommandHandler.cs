using UniqueShit.Application.Core.Authentication;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.Core.Errors;
using UniqueShit.Domain.Core.Primitives.Results;
using UniqueShit.Domain.Repositories;
using UniqueShit.Domain.ValueObjects;

namespace UniqueShit.Application.Features.AppUsers.Commands.UpdateAppUser
{
    public sealed class UpdateAppUserCommandHandler : ICommandHandler<UpdateAppUserCommand, Result>
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAppUserCommandHandler(IAppUserRepository appUserRepository, IUserIdentifierProvider userIdentifierProvider, IUnitOfWork unitOfWork)
        {
            _appUserRepository = appUserRepository;
            _userIdentifierProvider = userIdentifierProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateAppUserCommand request, CancellationToken cancellationToken)
        {
            var appUser = await _appUserRepository.GetById(_userIdentifierProvider.UserId);
            if (appUser is null)
            {
                return DomainErrors.AppUser.AppUserNotFound;
            }

            var phoneNumber = PhoneNumber.Create(request.PhoneNumber);
            if (phoneNumber.IsFailure)
            {
                return phoneNumber.Errors;
            }

            appUser.Update(phoneNumber.Value, request.AboutMe, request.City);

            _appUserRepository.Update(appUser);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
