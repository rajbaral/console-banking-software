using System;
namespace XYZ_Banking
{
    public class Money
    {

        private decimal _value;

        public Money(decimal value)
        {
            _value = value;
        }

        public decimal Value { get => _value; }
    }
}