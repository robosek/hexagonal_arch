using System;

namespace HexagonalArchitecture.Account.Domain
{
    public class Account
    {
        private readonly AccountId accountId;

        private readonly Money baselineBalacne;

        private readonly ActivityWindow activityWindow;

        private Account(AccountId accountId, Money baselineBalacne, ActivityWindow activityWindow)
        {
            this.accountId = accountId;
            this.baselineBalacne = baselineBalacne;
            this.activityWindow = activityWindow;
        }

        public static Account WithoutId(Money baselineBalacne, ActivityWindow activityWindow)
        {
            return new Account(null,baselineBalacne, activityWindow);
        }

        public static Account WithId(AccountId accountId,Money baselineBalacne, ActivityWindow activityWindow)
        {
            return new Account(accountId, baselineBalacne, activityWindow);
        }

        public MaybeAccountId TryGetAccountId()
        {
            return new MaybeAccountId(accountId);
        }

        public Money CalculateBalance()
        {
            return Money.Add(baselineBalacne, activityWindow.CalculateBalance(accountId));
        }

        public bool Withdraw(Money money, AccountId targetId)
        {
            if (!MayWithdraw(money))
                return false;

            var withdrawalActivity = new Activity(this.accountId,this.accountId, targetId, money, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds());
            activityWindow.AddActivity(withdrawalActivity);

            return true;
        }

        public bool Deposit(Money money, AccountId sourceId)
        {
            var depositActivity = new Activity(this.accountId, sourceId, this.accountId, money, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds());
            activityWindow.AddActivity(depositActivity);

            return true;
        }

        private bool MayWithdraw(Money money)
        {
            return Money.Subtract(CalculateBalance(), money)
                .IsPositiveOrZero();
        }

    }

    public class MaybeAccountId
    {
        private readonly AccountId accountId;

        public MaybeAccountId(AccountId accountId)
        {
            this.accountId = accountId;
        }

        public bool HasValue()
        {
            return accountId != null;
        }

        public AccountId Value()
        {
            return accountId;
        }
    }
}
