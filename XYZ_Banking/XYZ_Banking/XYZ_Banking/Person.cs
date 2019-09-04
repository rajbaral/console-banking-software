using System.Collections.Generic;
namespace XYZ_Banking
{
    public class Person
    {
        private string _name;
        private List<Account> _accounts;
        private int _accountSerialNumber;
        private Money _money;
        private string _email;
        private string _userName;
        private string _password;

        public Person(string name)
        {
            _name = name;
            _accountSerialNumber = 1;
            _accounts = new List<Account>();

        }

        public Account[] Accounts => _accounts.ToArray();

        public int AccountSerialNumber => _accountSerialNumber;

        public void AddAccounts(Account newAccount) =>

            _accounts.Add(newAccount);

        public void IncrementAccountSerialNumber() => ++_accountSerialNumber;

        public string Name { get => _name; set => _name = value; }
        public string Email { get => _email; set => _email = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public string Password { get => _password; set => _password = value; }
        public Money Money { get => _money; set => _money = value; }

    }
}