using System;

namespace XYZ_Banking
{
    public class BankTransection
    {
        private string _transectionType;
        private DateTime _transectionDate;

        public BankTransection(string typeOfTransection)
        {
            _transectionType = typeOfTransection;
            _transectionDate = DateTime.Now;
        }

        public string TransectionType { get => _transectionType; set => _transectionType = value; }
        public DateTime TransectionDate { get => _transectionDate; set => _transectionDate = value; }
    }
}
