﻿namespace PortfolioEye.Application.Features.Users;

public record UserProfileResponse(string FirstName, string LastName, string Email, string? PhotoUrl);