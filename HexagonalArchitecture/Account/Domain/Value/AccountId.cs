using System;


namespace HexagonalArchitecture.Account.Domain
{
    public class AccountId
    {
        private readonly long value;

        public AccountId(long value)
         =>
            this.value = value;

        public override bool Equals(object obj)
        {
            return obj is AccountId id &&
                   value == id.value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(value);
        }
    }
}
