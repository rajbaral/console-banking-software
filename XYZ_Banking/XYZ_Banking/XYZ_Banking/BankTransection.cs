using System;

namespace XYZ_Banking
{
    public class BankTransection
    {
        private string _transectionType;
        private DateTime _transectionDate;
        private Money _money;

        public BankTransection(string typeOfTransection, Money money)
        {
            _transectionType = typeOfTransection;
            _money = money;
            _transectionDate = DateTime.Now;
        }

        public string TransectionType { get => _transectionType; set => _transectionType = value; }
        public DateTime TransectionDate { get => _transectionDate; set => _transectionDate = value; }
        public Money Money { get => _money; set => _money = value; }
    }
}
