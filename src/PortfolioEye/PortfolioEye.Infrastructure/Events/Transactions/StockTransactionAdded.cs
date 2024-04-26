using MediatR;

namespace PortfolioEye.Infrastructure.Events;

public record StockTransactionAdded(Guid Id) : INotification;