using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueShit.Application.Core.Messaging;
using UniqueShit.Domain.Core.Primitives.Results;

namespace UniqueShit.Application.Features.AppUsers.Commands.UpdateAppUser
{
    public sealed record UpdateAppUserCommand(string? PhoneNumber, string? AboutMe, string? City) : ICommand<Result>;
}
