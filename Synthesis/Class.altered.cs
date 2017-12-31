namespace Playground
{
    using System;
    using static System.Console;

    public static class Class
    {
        static IEnumerable<int> GetData()
        {
            Console.WriteLine("Rozpoczecie metody GetData.");

            Console.WriteLine("Przygotowanie wartosci: ");
            int zmienna = 10;

            foreach (int item in zmienna)
            {
                yield return item;
            }

            Console.WriteLine("Zakonczenie metody GetData.");

            YIELD();

            YIELD<t>();

            YIELD<>();
        }

        static IEnumerable<string> GetNames(IEnumerable<string> names)
        {
            foreach (var item in names)
            {
                yield return item;
            }
        }

        public static IEnumerable<double> Numbers(double max)
        {
            if (max > 3.0)
                foreach (double item in max)
                {
                    yield return item;
                }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Pobranie danych.");
            IEnumerable<int> data = GetData();

            Console.WriteLine("Rozpoczecie przetwarzania.");
            foreach (int i in data)
            {
                Console.WriteLine("Odczyt wartosci: " + i.ToString());
                if (i == 5)
                    break;
            }

            Console.WriteLine("Zakonczenie przetwarzania.");
            Console.ReadLine();
        }
    }
}

