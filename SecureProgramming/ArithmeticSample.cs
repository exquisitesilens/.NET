using System;

namespace ArithmeticSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = int.MaxValue;
            int b = 1;
            int c = a + b;
            Console.WriteLine(c.ToString());
            Console.WriteLine(calculateResult(2, 7).ToString());

            Console.ReadLine();
        }

        private static int calculateResult(int a, int b)
        {
            int result = 0;
            try
            {
                result = checked(a + b);
            }
            catch (OverflowException)
            {
                Console.WriteLine("Arithmetic Overflow");
            }
            return result;
        }
    }
}
