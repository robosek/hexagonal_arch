namespace HexagonalArchitecture.Account.Domain
{
    public class Money
    {
		private int amount;

        public Money(int amount)
        {
			this.amount = amount;
        }

		public bool IsPositiveOrZero()
		{
			return this.amount.CompareTo(0) >= 0;
		}

		public bool IsNegative()
		{
			return this.amount.CompareTo(0) < 0;
		}

		public bool IsPositive()
		{
			return this.amount.CompareTo(0) > 0;
		}

		public bool IsGreaterThanOrEqualTo(Money money)
		{
			return this.amount.CompareTo(money.amount) >= 0;
		}

		public bool IsGreaterThan(Money money)
		{
			return this.amount.CompareTo(money.amount) >= 1;
		}

		public static Money Of(int value)
		{
			return new Money(value);
		}

		public static Money Add(Money a, Money b)
		{
			return new Money(a.amount + b.amount);
		}

		public Money Minus(Money money)
		{
			return new Money(this.amount - money.amount);
		}

		public Money Plus(Money money)
		{
			return new Money(this.amount + (money.amount));
		}

		public static Money Subtract(Money a, Money b)
		{
			return new Money(a.amount - (b.amount));
		}

		public Money Negate()
		{
			return new Money(amount - amount - amount);
		}
	}
}
