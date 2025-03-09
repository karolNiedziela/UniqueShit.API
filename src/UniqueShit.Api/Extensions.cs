using System.Reflection;
using UniqueShit.Api.Endpoints;

namespace UniqueShit.Api
{
    internal static class Extensions
    {
        public static IEndpointRouteBuilder MapEndpoints(
                     this IEndpointRouteBuilder builder,
                     params Assembly[] scanAssemblies)
        {
            var assembly = typeof(Program).Assembly;

            builder.MapGroup("/api");

            var endpoints = assembly.GetTypes().Where(t =>
                t.IsClass && !t.IsAbstract && !t.IsGenericType && !t.IsInterface
                && t.GetConstructor(Type.EmptyTypes) != null
                && typeof(IMinimalApiEndpointDefinition).IsAssignableFrom(t)).ToList();

            foreach (var endpoint in endpoints)
            {
                var instantiatedType = (IMinimalApiEndpointDefinition)Activator.CreateInstance(endpoint)!;
                instantiatedType.MapEndpoints(builder);
            }

            return builder;
        }
    }
}
