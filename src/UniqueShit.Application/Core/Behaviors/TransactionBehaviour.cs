using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Application.Core.Persistence;

namespace UniqueShit.Application.Core.Behaviors
{
    internal sealed class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
      where TRequest : class, IRequest<TResponse>
      where TResponse : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionBehaviour(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IQuery<TResponse>)
            {
                return await next();
            }

            await using IDbContextTransaction transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                TResponse response = await next();

                await transaction.CommitAsync(cancellationToken);

                return response;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }
        }
    }
}
