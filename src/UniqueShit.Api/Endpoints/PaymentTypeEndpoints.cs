using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniqueShit.Application.Core.Responses;
using UniqueShit.Domain.Offers.Enumerations;

namespace UniqueShit.Api.Endpoints
{
    internal sealed class PaymentTypeEndpoints : IMinimalApiEndpointDefinition
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("payment-types");

            group.MapGet("", GetPaymentTypes)
                .WithName(nameof(GetPaymentTypes))
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "Get payment types"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            return builder;
        }

        public static Task<Ok<List<EnumerationResponse>>> GetPaymentTypes()
        {
            var paymentTypes = PaymentType.List
              .Select(x => new EnumerationResponse(x.Id, x.Name))
              .ToList();

            return Task.FromResult(TypedResults.Ok(paymentTypes));
        }
    }
}
