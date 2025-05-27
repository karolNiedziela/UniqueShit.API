using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Web;
using Microsoft.Net.Http.Headers;
using UniqueShit.Api;
using UniqueShit.Application;
using UniqueShit.Chatting.SignalRProviders;
using UniqueShit.Infrastructure;
using UniqueShit.Infrastructure.Authentication.Options;
using UniqueShit.Infrastructure.Realtime.Hubs;
using Z.BulkOperations;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
  .SetBasePath(builder.Environment.ContentRootPath)
  .AddJsonFile("appsettings.json")
  .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true)
  .AddUserSecrets<Program>()
  .AddEnvironmentVariables()
  .Build();

builder.Services.AddSingleton(TimeProvider.System);

builder.Services.AddSignalR();
builder.Services.AddSingleton<IUserIdProvider, UserIdProvider>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Origin",
                      policy =>
                      {
                          policy
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials()
                          .WithOrigins("http://localhost:4200")
                          .WithExposedHeaders(HeaderNames.Location);
                      });
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(options =>
            {
                configuration.Bind(AzureB2COptions.SectionName, options);

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken)
                              && path.StartsWithSegments("/chatHub", StringComparison.OrdinalIgnoreCase))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                        logger.LogError("Authentication failed: {Message}", context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                        logger.LogError("Challenge error: {ErrorDescription}", context.ErrorDescription);
                        return Task.CompletedTask;
                    }
                };
            }, options => { configuration.Bind(AzureB2COptions.SectionName, options); });

builder.Services.AddHttpContextAccessor();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("Origin");

app.UseAuthentication();

app.UseAuthorization();

var apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();

var versionedGroup = app
    .MapGroup("api/v{apiVersion:apiVersion}")
    .WithApiVersionSet(apiVersionSet);


versionedGroup.MapEndpoints();
 
app.MapHub<ChatHub>("/chatHub");

await app.RunAsync();