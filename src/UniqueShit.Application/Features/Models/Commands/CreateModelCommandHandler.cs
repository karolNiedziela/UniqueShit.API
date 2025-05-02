using UniqueShit.Application.Core.Authentication;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Application.Features.Offers.Contracts.Responses;
using UniqueShit.Domain.Core.Errors;
using UniqueShit.Domain.Core.Primitives.Results;
using UniqueShit.Domain.Enitities;
using UniqueShit.Domain.Enumerations;
using UniqueShit.Domain.Repositories;

namespace UniqueShit.Application.Features.Models.Commands
{
    public sealed class CreateModelCommandHandler : ICommandHandler<CreateModelCommand, Result<CreateModelResponse>>
    {
        private readonly IModelRepository _modelRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IUnitOfWork _unitOfWork;

        public CreateModelCommandHandler(IModelRepository modelRepository, IBrandRepository brandRepository, IUnitOfWork unitOfWork, IUserIdentifierProvider userIdentifierProvider)
        {
            _modelRepository = modelRepository;
            _brandRepository = brandRepository;
            _unitOfWork = unitOfWork;
            _userIdentifierProvider = userIdentifierProvider;
        }

        public async Task<Result<CreateModelResponse>> Handle(CreateModelCommand request, CancellationToken cancellationToken)
        {
            var productCategoryIdResult = ProductCategory.FromValue(request.ProductCategoryId);
            if (productCategoryIdResult.IsFailure)
            {
                return productCategoryIdResult.Errors;
            }

            var brand = await _brandRepository.GetAsync(request.BrandId);
            if (brand is null)
            {
                return DomainErrors.Model.BrandNotFound;
            }

            var model = new Model(request.Name, request.ProductCategoryId, request.BrandId, _userIdentifierProvider.UserId);
            _modelRepository.Add(model);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var modelResponse = new CreateModelResponse(model.Id, model.Name, model.ProductCategoryId, model.BrandId);
            return modelResponse;
        }
    }
}
