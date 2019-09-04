namespace XYZ_Banking
{
    public interface IBankable
    {
        Account CreateAccount(Person customer, Money initialDeposit);
        Account[] BalanceInquiry(Person customer);
        void Deposit(Account to, Money amount);
        void Withdraw(Account from, Money amount);
        void Transfer(Account from, Account to, Money amount);
    }
}
