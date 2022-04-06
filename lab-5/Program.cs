using System;
using System.Collections;
using System.Collections.Generic;

namespace lab_5
{
    class Ingredient
    {
        public double Calories { get; init; }
        public string Name { get; init; }
    }

    class Sandwitch: IEnumerable<Ingredient>
    {
        public Ingredient Bread { get; init; }
        public Ingredient Butter { get; init; }
        public Ingredient Salad { get; init; }
        public Ingredient Ham { get; init; }

        public IEnumerator<Ingredient> GetEnumerator()
        {
            //return new SandwitchEnumerator(this);
            yield return Bread; //zwrocone w Current po pierwszm wywolaniu MoveNext
            yield return Butter; //zwrócone w Current po drugim wywołaniu Movenext
            yield return Salad; //zwrócone w Current po trzecim wywołaniu Movenext
            yield return Ham; 
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    
    class Parking : IEnumerable<string>
    {
        private String[] _arr = { "GL789", null, null, "TK3789", "IMN819" };
        public string this[char slot]
        {
            get
            {
                //test poprawnosci,czy slot jst miedzy 'A' a 'Z'
               return _arr[slot - 'A'];
            }
            set
            {
                _arr[slot - 'A'] = value;
            }
        }
        public string this[int index]
        {
            get
            {
                return _arr[index];
            }
            set
            {

            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            foreach (string car in _arr)
            {
                if (car != null)
                {
                    yield return car;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }







    class SandwitchEnumerator : IEnumerator<Ingredient>
    {
        private Sandwitch _sandwitch;
        int counter = -1;
        private Sandwitch sandwitch;

        public SandwitchEnumerator(Sandwitch sandwitch)
        {
            _sandwitch = sandwitch;
        }

        public Ingredient Current
        {
            get
            {
                return counter switch
                {
                    0 => _sandwitch.Bread,
                    1 => _sandwitch.Butter,
                    2 => _sandwitch.Salad,
                    3 => _sandwitch.Ham
                };
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            return ++counter < 3;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Sandwitch sandwitch = new Sandwitch()
            {
                Bread = new Ingredient() { Calories = 100, Name = "Bułka wrocławska"},
                Ham = new Ingredient() { Calories = 400, Name = "Z kotła" },
                Salad = new Ingredient() { Calories = 10,Name = "Lodowa"},
                Butter = new Ingredient() { Calories = 120,Name = "Śmietankowe"}
            };

            IEnumerator<Ingredient> enumerator = sandwitch.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }

            foreach(Ingredient ingredient in sandwitch)
            {
                Console.WriteLine(ingredient);
            }


            Parking parking = new Parking();
            foreach(string car in parking)
            {
                Console.WriteLine(car);
            }
            Console.WriteLine(string.Join(", ", parking));
            Console.WriteLine(string.Join(", ", sandwitch));
            Console.WriteLine(parking['E']);
            parking['A'] = "TT23234";

        }
    }
}
