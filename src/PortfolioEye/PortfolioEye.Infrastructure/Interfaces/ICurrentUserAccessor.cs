namespace PortfolioEye.Interfaces;

public interface ICurrentUserAccessor
{
    CurrentUser? Get();
}

public record CurrentUser(Guid Id, string? Name, string? Email);