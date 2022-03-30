using System;

namespace lab_2
{

    abstract class Product
    {
        public virtual decimal Price { get; init; }

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

    interface IFly
    {
        void Fly();
    }

    interface ISwim
    {
        void Swim();
    }

    abstract class Animal
    {

    }


    class Duck : ISwim, IFly
    {
        public void Fly()
        {
            throw new NotImplementedException();
        }

        public void Swim()
        {
            throw new NotImplementedException();
        }
    }

    class Hydroplane : IFly, ISwim
    {
        public void Fly()
        {
            throw new NotImplementedException();
        }

        public void Swim()
        {
            throw new NotImplementedException();
        }
    }

    ///////////////////////////////////////////////////////////

    class Paint : Product
    {
        public decimal Vat { get; init; }

        public decimal Capacity { get; init; }

        public decimal PriceUnit { get; init; }

        public override decimal GetVatPrice()
        {
            return Price * Capacity * Vat / 100.0m;
        }

        public override decimal Price
        {
            get
            {
                return PriceUnit * Capacity;
            }
        }
    }

    class Butter : Product
    {
        public override decimal GetVatPrice()
        {
            return 2m;
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            Object[] poorShop = new Object[1];

            Product[] shop = new Product[4];
            shop[0] = new Computer() { Price = 2000m, Vat = 23m };
            shop[1] = new Paint() { PriceUnit = 12, Capacity = 5, Vat = 8 };
            shop[2] = new Computer() { Price = 2400m, Vat = 23m };
            shop[3] = new Butter();

            decimal sumVat = 0;
            decimal sumPrice = 0;
            foreach (var product in shop)
            {
                sumVat += product.GetVatPrice(); ///
                sumPrice += product.Price;
                //starsza wersja testowania czy jest instancja
                if (product is Computer)
                {
                    Computer comp = (Computer)product;
                }
                //nowsza wersja testowania czy jest instancja
                Computer computer = product as Computer;
                Console.WriteLine(computer?.Vat);

            }

            Console.WriteLine(sumVat);
            Console.WriteLine(sumPrice);

            IFly[] flyingObject = new IFly[2];
            Duck duck = new Duck();
            flyingObject[0] = duck;
            flyingObject[1] = new Hydroplane();

            ISwim[] swimmingObjects = new ISwim[2];
            swimmingObjects[0] = duck;


            IAggregate aggregate;
            IIterator iterator = aggregate.CreateIterator();
            while (iterator.HasNext())
            {
                Console.WriteLine(iterator.GetNext());
            }
        }
    }

    interface IAggregate
    {
        IIterator CreateIterator();
    }

    interface IIterator
    {
        bool HasNext();
        int GetNext();
    }
}
