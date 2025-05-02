using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniqueShit.Application.Core.Responses;
using UniqueShit.Domain.Offers.Enumerations;

namespace UniqueShit.Api.Endpoints
{
    internal sealed class DeliveryTypeEndpoints : IMinimalApiEndpointDefinition
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("delivery-types");

            group.MapGet("", GetDeliveryTypes)
                .WithName(nameof(GetDeliveryTypes))
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "Get delivery types"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            return builder;
        }

        public static Task<Ok<List<EnumerationResponse>>> GetDeliveryTypes()
        {
            var deliveryTypes = DeliveryType.List
              .Select(x => new EnumerationResponse(x.Id, x.Name))
              .ToList();

            return Task.FromResult(TypedResults.Ok(deliveryTypes));
        }
    }
}
