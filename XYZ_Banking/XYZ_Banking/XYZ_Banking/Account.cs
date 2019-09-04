using System.Collections.Generic;

namespace XYZ_Banking
{
    public class Account
    {
        private Money _amount;
        private Person _owner;
        private string _name;
        private List<BankTransection> _transectionType;
        public Account(Money money, Person owner)
        {
            owner.IncrementAccountSerialNumber();
            _amount = money;
            _owner = owner;
            _name = owner.Name + "-" + owner.AccountSerialNumber;
            _transectionType = new List<BankTransection>();

        }

        public Money Money { get => _amount; set => _amount = value; }

        public Person Owner { get => _owner; set => _owner = value; }
        public List<BankTransection> TransectionType => _transectionType;

        public void AddTransections(BankTransection newTransections) =>

            _transectionType.Add(newTransections);

        public string Name => _name;
    }
}