using HexagonalArchitecture.Account.Domain;

namespace HexagonalArchitecture.Account.Application.Port.In
{
    public class SendMoneyCommand
    {
        public AccountId SourceAccountId { get; }
        public AccountId TargetAccountId { get; }
        public Money Money { get; }

        public SendMoneyCommand(AccountId sourceAccountId, AccountId targetAccountId, Money money)
        {
            SourceAccountId = sourceAccountId;
            TargetAccountId = targetAccountId;
            Money = money;
        }
    }
}
