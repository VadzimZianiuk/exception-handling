using System;
using System.IO;

namespace Task1
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Write any text ang get first symbol:");

            while (true)
            {
                try
                {
                    Console.WriteLine(Console.ReadLine().GetFirstSymbol());
                }
                catch (InvalidStringException)
                {
                    Console.WriteLine("Invalid string exception");
                }
                catch (IOException)
                {
                    Console.WriteLine("IO exception");
                }
                catch (OutOfMemoryException)
                {
                    Console.WriteLine("Out of memory exception");
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Argument out of range exception");
                }
            }
        }

        private static char GetFirstSymbol(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                throw new InvalidStringException("Invalid source string", nameof(source));
            }
            return source[0];
        }
    }
}
