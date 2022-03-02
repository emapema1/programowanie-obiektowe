using System;

namespace lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = Person.OfName("Adam");
            Console.WriteLine(person.FirstName);
            //
            Money money = Money.Of(12, Currency.PLN) ?? Money.Of(0, Currency.PLN);
            Console.WriteLine(money.Value + " " + money.Currency);
            Money result = money * 0.22m;
            Console.WriteLine(result.Value);

        }
    }

    public class Person
    {
        private string firstName;

        public Person(string _firstName)
        {
            this.FirstName = _firstName;
        }

        public static Person OfName(string name)
        {
            if (name != null && name.Length >= 2)
            {
                return new Person(name);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Imię zbyt krótkie");
            }
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (value != null && value.Length >= 2)
                {
                    firstName = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Imię zbyt krótkie");
                }
            }
        }
    }

    public enum Currency
    {
        PLN = 1,
        USD = 2,
        EUR = 3
    }


    public class Money
    {
        private readonly decimal _value;
        private readonly Currency _currency;
        private Money(decimal value, Currency currency)
        {
            _value = value;
            _currency = currency;
        }

        public static Money operator *(Money money, decimal factor)
        {
            return Money.Of(money.Value * factor, money.Currency);
        }

        public decimal Value
        {
            get { return _value; }
        }

        public Currency Currency
        {
            get
            {
                return _currency;
            }
        }

        public static Money? Of(decimal value, Currency currency)
        {
            return value < 0 ? null : new Money(value, currency);
        }

        public static Money? OfName(decimal value, Currency currency)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                return new Money(value, currency);
            }
        }
    }

}
