﻿using MediatR;
using PortfolioEye.Application.Features.Accounts.Common;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Accounts.Commands;

public record AddAccountCommand(string Name, string Description, string Currency, AccountType Type)
    : IRequest<IResult>;

public record EditAccountCommand(Guid Id, string Name, string Description)
    : IRequest<IResult>;

public record AddAccountForUserCommand(Guid UserId, AddAccountCommand BaseCommand)
    : AddAccountCommand(BaseCommand);

public record EditAccountForUserCommand(Guid UserId, EditAccountCommand BaseEditCommand)
    : EditAccountCommand(BaseEditCommand);

