using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.Core.Primitives.Results;
using UniqueShit.Domain.Enitities;
using UniqueShit.Domain.Repositories;

namespace UniqueShit.Application.Features.AppUsers.Commands.CreateAppUser
{
    public sealed class CreateAppUserCommandHandler : ICommandHandler<CreateAppUserCommand, Result>
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAppUserCommandHandler(IAppUserRepository appUserRepository, IUnitOfWork unitOfWork)
        {
            _appUserRepository = appUserRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateAppUserCommand request, CancellationToken cancellationToken)
        {
            var existingAppUser = await _appUserRepository.GetByEmailAsync(request.Email);
            if (existingAppUser is not null)
            {
                return Result.Success();
            }

            var appUser = new AppUser(request.DisplayName, request.Email, request.ObjectId);
            _appUserRepository.Add(appUser);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
