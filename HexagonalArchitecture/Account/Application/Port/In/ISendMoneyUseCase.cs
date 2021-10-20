namespace HexagonalArchitecture.Account.Application.Port.In
{
    public interface ISendMoneyUseCase
    {
        bool SendMoney(SendMoneyCommand command);
    }
}
