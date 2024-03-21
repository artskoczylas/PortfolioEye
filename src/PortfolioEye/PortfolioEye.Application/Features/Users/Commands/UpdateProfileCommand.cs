using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Users.Commands;

public record UpdateProfileCommand(string FirstName, string LastName, string PhotoUrl);