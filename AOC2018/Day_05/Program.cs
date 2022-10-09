using System;
using System.IO;
using System.Text;

namespace Day_05
{
    public class Program
    {
        static void Main(string[] args)
        {
            var suitString = File.ReadAllText(args[0]);

            int charsRemoved = Int32.MaxValue;
            while (charsRemoved > 0)
            {
                suitString = Reduce(suitString, out charsRemoved);
            }
            var shrunkenSuit = suitString;

            Console.WriteLine($"The suit has {shrunkenSuit.Length} units remaining");
        }

        public static string ReduceCompletely(string suit)
        {
            var charsRemoved = Int32.MaxValue;
            while (charsRemoved > 0)
            {
                suit = Reduce(suit, out charsRemoved);
            }
            return suit;
        }

        public static string Reduce(string suit, out int charsRemoved)
        {
            var sb = new StringBuilder(suit);
            charsRemoved = 0;
            for (int i = 0; i < sb.Length - 1; ++i)
            {
                if (UnitsCanReact(sb[i], sb[i + 1]))
                {
                    sb.Remove(i, 2);
                    charsRemoved += 2;
                }
            }
            return sb.ToString();
        }

        public static bool UnitsCanReact(Char a, Char b)
        {
            return a - b == 32 || b - a == 32;
        }
    }
}
