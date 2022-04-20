using System;

namespace lab_7
{
    delegate double Operator(double a, double b);
    delegate double Calc(double x, double y);
    class Program
    {
        public static double Addition(double x, double y)
        {
            return x + y;
        }
        public static double Mul(double x, double y)
        {
            return x * y;
        }

        static public void PrintIntArray(int[] arr, Func<int, string> formatter)
        {
            foreach (var item in arr)
            {
                Console.WriteLine(formatter.Invoke(item));
            }
        }
        //zdefiniuj metode Mul typu delegate Operator

        static void Main(string[] args)
        {
            Operator operation = Addition;
            //rownowaznik Addition(4,6);
            double result = operation.Invoke(4, 6);
            Console.WriteLine(result);
            //przypisz do operation Mul
            operation = Mul;
            result = operation.Invoke(4, 6);
            Console.WriteLine(result);
            "abc".ToUpper();
            Calc c = Mul;
            Func<double, double, double> op = Mul;
            op = Addition;

            Func<int, string> Formatter = delegate (int number)
            {
                return string.Format("0x{0:x}", number);
            };

            Func<int, string> DecFormat = delegate (int number)
            {
                return string.Format("{0}", number);
            };
            Console.WriteLine(Formatter.Invoke(18));

            Predicate<string> OnlyThreeChars = delegate (string s)
            {
                return s.Length == 3;
            };

            Func<int, int, int, bool> InRange = delegate (int value, int min, int max)
            {
                return value > min && value < max;
            };
            Action<string> Print = delegate (string s)
            {
                Console.WriteLine(s);
            };

            Operator AddLambda = (a, b) => a + b;
            Action<string> PrintLambda = s => Console.WriteLine(s);
            Func<int> Lambda = () => 5;
            PrintIntArray(new int[] { 1, 5, 78, 34 }, n => string.Format("{0}", n));
        }
    }
}
