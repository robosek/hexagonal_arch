namespace HexagonalArchitecture.Account.Domain
{
    public class Activity
    {
        private ActivityId id;
        private AccountId ownerId;
        private AccountId sourceId;
        private AccountId targetId;
        private Money money;
        private long timestamp;

        public Activity(
            AccountId ownerId, 
            AccountId sourceId, 
            AccountId targetId, 
            Money money, 
            long timestamp)
        {
  
            this.ownerId = ownerId;
            this.sourceId = sourceId;
            this.targetId = targetId;
            this.money = money;
            this.timestamp = timestamp;
        }

        public long GetTimestamp()
        {
            return timestamp;
        }

        public AccountId GetTargetAccountId()
        {
            return targetId;
        }

        public AccountId GetSourceAccountId()
        {
            return sourceId;
        }

        public Money GetMoney()
        {
            return money;
        }
    }

    public class ActivityId
    {
        private readonly int value;

        public ActivityId(int value)
        {
            this.value = value;
        }
    }
}
