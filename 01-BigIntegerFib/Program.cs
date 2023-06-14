using System.Numerics;

namespace BigIntegerFib;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.WindowWidth = 180;

        int count = 1;
        foreach (var number in FibonacciSeries().Take(1000))
        {
            Console.WriteLine($"{count++}\t{number} ");
        }

        if (args.Length > 0)
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }

    public static IEnumerable<BigInteger> FibonacciSeries()
    {
        BigInteger prev = 0, next = 1;
        yield return prev;
        yield return next;

        while (true)
        {
            BigInteger sum = prev + next;
            yield return sum;
            prev = next;
            next = sum;
        }
    }
}