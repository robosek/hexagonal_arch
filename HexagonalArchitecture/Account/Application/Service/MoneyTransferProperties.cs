using HexagonalArchitecture.Account.Domain;

namespace HexagonalArchitecture.Account.Application.Service
{
    public class MoneyTransferProperties
    {
        public Money MaximumTransferThreshold { get; } = Money.Of(1000000);
    }
}
