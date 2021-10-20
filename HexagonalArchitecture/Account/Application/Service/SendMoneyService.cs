using System;
using HexagonalArchitecture.Account.Application.Port.In;
using HexagonalArchitecture.Account.Application.Port.Out;

namespace HexagonalArchitecture.Account.Application.Service
{
    public class SendMoneyService : ISendMoneyUseCase
    {
        private readonly ILoadAccountPort loadAccountPort;
        private readonly IAccountLock accountLock;
        private readonly IUpdateAccountStatePort updateAccountStatePort;
        private readonly MoneyTransferProperties moneyTransferProperties;

        public SendMoneyService(ILoadAccountPort loadAccountPort,
            IAccountLock accountLock,
            IUpdateAccountStatePort updateAccountStatePort,
            MoneyTransferProperties moneyTransferProperties)
        {
            this.loadAccountPort = loadAccountPort;
            this.accountLock = accountLock;
            this.updateAccountStatePort = updateAccountStatePort;
            this.moneyTransferProperties = moneyTransferProperties;
        }

        public bool SendMoney(SendMoneyCommand command)
        {
            CheckTreshold(command);

            var dateTime = DateTime.Now.AddDays(-10);

            var sourceAccount = loadAccountPort.LoadAccount(command.SourceAccountId,dateTime);
            var targetAccount = loadAccountPort.LoadAccount(command.TargetAccountId, dateTime);

            var maybeSourceAccountId = sourceAccount.TryGetAccountId();
            var maybeTargetAccountId = sourceAccount.TryGetAccountId();

            if (!maybeSourceAccountId.HasValue())
                throw new Exception("source account has no id");

            if (!maybeTargetAccountId.HasValue())
                throw new Exception("target account has no id");

            var sourceAccountId = maybeSourceAccountId.Value();
            var targetAccountId = maybeTargetAccountId.Value();

            accountLock.LockAccount(sourceAccountId);
            if(!sourceAccount.Withdraw(command.Money,targetAccountId))
            {
                accountLock.ReleaseAccount(sourceAccountId);
                return false;
            }

            accountLock.LockAccount(targetAccountId);
            if (!targetAccount.Deposit(command.Money, sourceAccountId))
            {
                accountLock.ReleaseAccount(sourceAccountId);
                accountLock.ReleaseAccount(targetAccountId);
                return false;
            }


            updateAccountStatePort.UpdateActivities(sourceAccount);
            updateAccountStatePort.UpdateActivities(targetAccount);

            accountLock.ReleaseAccount(sourceAccountId);
            accountLock.ReleaseAccount(targetAccountId);

            return true;
        }

        private void CheckTreshold(SendMoneyCommand command)
        {
            if(command.Money.IsGreaterThan(moneyTransferProperties.MaximumTransferThreshold))
            {
                throw new TresholdExceededException(moneyTransferProperties.MaximumTransferThreshold, command.Money);
            }
        }
    }
}
