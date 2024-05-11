using Mapster;
using PortfolioEye.Application.Features.Transactions.Queries;
using PortfolioEye.Domain.Entities;

namespace PortfolioEye.Infrastructure.MapProfiles;


    public class TransactionProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config
                .NewConfig<Transaction, RetrieveTransactionsByUserIdQuery.Transaction>()
                .Map(dest => dest.PortfolioName, src => src.Portfolio!.Name)
                .Map(dest => dest.AccountName, src => src.Account!.Name);
        }
    }
