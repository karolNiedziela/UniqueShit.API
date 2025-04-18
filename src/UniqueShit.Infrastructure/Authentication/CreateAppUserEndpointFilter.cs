using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using UniqueShit.Infrastructure.Authentication.Options;

namespace UniqueShit.Infrastructure.Authentication
{
    public sealed class CreateAppUserEndpointFilter : IEndpointFilter
    {
        private const string FunctionsKeyHeader = "x-functions-key";

        private readonly CreateAppUserEndpointFilterOptions _options;

        public CreateAppUserEndpointFilter(IOptions<CreateAppUserEndpointFilterOptions> options)
        {
            _options = options.Value;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var apiKey = context.HttpContext.Request.Headers[FunctionsKeyHeader].FirstOrDefault();
            if (string.IsNullOrEmpty(apiKey))
            {
                return Results.Unauthorized();
            }

            var expectedApi = _options.Key;
            if (expectedApi != apiKey)
            {
                return Results.Unauthorized();
            }

            return await next(context);
        }
    }
}
