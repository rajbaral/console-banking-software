using System;
using System.Collections.Generic;
using System.Linq;

namespace XYZ_Banking
{
    public class Bank : IBankable
    {
        public List<Account> _inMemoryDb;
        private string _bankName;
        private string _person1Name = "Ishwor Ojha";
        private string _person2Name = "Raj Baral";

        public Bank(string name)
        {
            BankName = name;

            // there are two existing customer for 

            Person person1 = new Person(_person1Name);
            person1.UserName = "user1";
            person1.Email = "ojha@gmail.com";
            person1.Password = "password1";

            Person person2 = new Person(_person2Name);
            person2.UserName = "user2";
            person2.Email = "baral@gmail.com";
            person2.Password = "password2";


            string accountActivity = "create new account";
            BankTransection transection = new BankTransection(accountActivity);

            Account acount1 = new Account(new Money(2500), person1);
            acount1.TransectionType.Add(transection);

            Account acount2 = new Account(new Money(1800), person2);
            acount2.TransectionType.Add(transection);

            List<Account> accountList = new List<Account>();

            accountList.Add(acount1);
            accountList.Add(acount2);

            _inMemoryDb = accountList;
        }

        public Account CreateAccount(Person customer, Money initialDeposit)
        {
            if (_validPersonWithdrawTransaction(customer, initialDeposit))
            {

                var newAccount = new Account(initialDeposit, customer);
                customer.Money = new Money(customer.Money.Value - initialDeposit.Value);
                customer.AddAccounts(newAccount);
                _inMemoryDb.Add(newAccount);
                return newAccount;
            }
            return null;
        }

        // BalanceInquiry
        //public Account[] BalanceInquiry(Person customer) => customer.Accounts;
        public Account[] BalanceInquiry(Person customer)
        {
            var accountList = _inMemoryDb.Where(a => a.Owner.UserName == customer.UserName && a.Owner.Password == customer.Password).Distinct().ToArray();
            return accountList;
        }

        // Transection History
        public Account[] TransectionHistory(Person customer)
        {
            var accountList = _inMemoryDb.Where(a => a.Owner.UserName == customer.UserName && a.Owner.Password == customer.Password).Distinct().ToArray();
            return accountList;
        }

        // This method only supports one type of currency.
        private bool _requestPersonHasSufficientFunds(Person owner, Money amount) => (owner.Money.Value >= amount.Value);

        // This method only supports one type of currency.
        private bool _requestAccountHasSufficientFunds(Account transfer, Money amount) => (transfer.Money.Value >= amount.Value);

        private bool _requestMoneyIsPositive(Money amount) => (amount.Value > 0);

        private bool _validPersonWithdrawTransaction(Person owner, Money amount)
        {
            if (!_requestMoneyIsPositive(amount))
            {
                throw new ArgumentException("Invalid value, Negative " + amount.Value);
            }

            if (!_requestPersonHasSufficientFunds(owner, amount))
            {
                throw new ArgumentException("Person has insufficient funds: " + owner.Money.Value + " < " + amount.Value);
            }

            return true;
        }

        private bool _validAccountWithdrawTransaction(Account transfer, Money amount)
        {

            if (!_requestMoneyIsPositive(amount))
            {
                throw new ArgumentException("Invalid value, Negative " + amount.Value);
            }

            if (!_requestAccountHasSufficientFunds(transfer, amount))
            {
                throw new ArgumentException("Account has insufficient funds: " + transfer.Money.Value + " < " + amount.Value);
            }

            return true;
        }

        private bool _validAccountDepositTransaction(Account transfer, Money amount)
        {
            if (!_requestMoneyIsPositive(amount))
            {
                throw new ArgumentException("Invalid value, Negative " + amount.Value);
            }

            return true;
        }

        private bool _validAccountTransferTransaction(Account from, Account to, Money amount) => (
               _validAccountDepositTransaction(to, amount)
            && _validAccountWithdrawTransaction(from, amount)
            );

        // This method only supports one type of currency.
        private void _performAccountDepositTransaction(Account transfer, Money amount) =>
            transfer.Money = new Money(transfer.Money.Value + amount.Value);

        // This method only supports one type of currency.
        private void _performAccountWithdrawTransaction(Account transfer, Money amount) =>
            transfer.Money = new Money(transfer.Money.Value - amount.Value);

        public void Deposit(Account to, Money amount)
        {
            if (_validAccountDepositTransaction(to, amount))
            {
                _performAccountDepositTransaction(to, amount);

                //update deposit on inMemoryDB
                _inMemoryDb.Add(to);
            }
        }

        public void Withdraw(Account from, Money amount)
        {
            if (_validAccountWithdrawTransaction(from, amount))
            {
                _performAccountWithdrawTransaction(from, amount);

                // update withdraw on inMemoryDB
                _inMemoryDb.Add(from);
            }
        }

        public void Transfer(Account from, Account to, Money amount)
        {
            if (_validAccountTransferTransaction(from, to, amount))
            {
                Withdraw(from, amount);
                Deposit(to, amount);

                // record deposit and withdraw for transfer
                _inMemoryDb.Add(from);
                _inMemoryDb.Add(to);
            }
        }

        public List<Account> GetAllUserAccounts()
        {
            return _inMemoryDb;
        }

        public string BankName { get => _bankName; set => _bankName = value; }
    }
}
