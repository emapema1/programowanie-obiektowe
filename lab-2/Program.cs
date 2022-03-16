using System;

namespace lab_2
{
    class Program
    {
        abstract class Product
        {
            public decimal Price { get; init; }

            public abstract decimal GetVatPrice();
        }

        class Computer : Product
        {
            public decimal Vat { get; init; }
            public override decimal GetVatPrice()
            {
                return Price * Vat / 100.0m;
            }
        }


        class Paint : Product
        {
            public decimal Vat { get; init; }

            public decimal Capacity { get; init; }
            static void Main(string[] args)
            {

            }

            public override decimal GetVatPrice()
            {
                return Price * Capacity * Vat / 100.0m;
            }
        }
    }
}
