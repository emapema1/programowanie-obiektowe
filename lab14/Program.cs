using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace lab14
{
    class Program
    {
        static void Main(string[] args)
        {
            //Thread thread = new Thread(work);
            //thread.Start();
            //thread.Join();
            Task task = Task.Run(work);
            TaskAwaiter taskAwaiter = task.GetAwaiter();
            taskAwaiter.OnCompleted(() => Console.WriteLine("Koniec"));
            int c = 10;


            while (c-- > 0)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Main: ");
            }
        }

        static void work()
        {
            for(int i =0;i < 10; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("\r{i}");
            }
        }

        static async Task<int> CalcAsync()
        {
            string[] lines = await FileStyleUriParser.ReadAllLinesAsync();
        }
    }
}
