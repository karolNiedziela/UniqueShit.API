namespace UniqueShit.Api.Endpoints
{
    internal interface IMinimalApiEndpointDefinition
    {
        IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder);
    }
}
