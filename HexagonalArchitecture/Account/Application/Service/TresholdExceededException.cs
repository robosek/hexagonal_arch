using System;
using HexagonalArchitecture.Account.Domain;

namespace HexagonalArchitecture.Account.Application.Service
{
    public class TresholdExceededException : Exception
    {
        public TresholdExceededException(Money treshold, Money actual)
            : base($"Maximum threshold {treshold} exceeded! Actual amount is {actual}") { }
       
    }
}
