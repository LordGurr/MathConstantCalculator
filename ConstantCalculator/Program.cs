using System;

namespace ConstantCalculator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            decimal phi = (decimal)(0.5 + (Math.Pow(5, 0.5)) * 0.5);
            decimal otherPhi = (decimal)((1 + Math.Sqrt(5)) / 2);
            decimal e = 1;
            var a = fact(3);
            var b = fact(4);
            var c = fact(5);
            var d = fact(6);
            var f = fact((decimal)0.5);
            a = factorial(3);
            b = factorial(4);
            c = factorial(5);
            d = factorial(6);
            f = factorial(0.5);
            decimal PI = 2 * F(1, 100);
            string chamb = Champernowne(5000);
            for (int i = 0; i < 20; i++)
            {
                e += fact((decimal)1 / ((decimal)i + (decimal)1));
            }
            Console.WriteLine(PI);
            Console.ReadKey(true);
        }

        private static decimal fact(decimal temp)
        {
            decimal factorial = 2 > temp ? 1 : 2;
            for (int i = 3; i <= temp; i++)
            {
                factorial *= i;
            }
            return factorial;
            factorial = 2 > temp ? 1 : 2;

            factorial *= fact(factorial - 1);
        }

        private static decimal factorial(double d)
        {
            if (d == 0.0)
            {
                return (decimal)1.0;
            }

            double abs = Math.Abs(d);
            double decimalen = abs - Math.Floor(abs);
            double result = 1.0;

            for (double i = Math.Floor(abs); i > decimalen; --i)
            {
                result *= (i + decimalen);
            }
            if (d < 0.0)
            {
                result = -result;
            }

            return (decimal)result;
        }

        private decimal CalcPi(int iterations)
        {
            decimal q = 1;
            decimal r = 0;
            decimal t = 1;
            decimal k = 1;
            decimal n = 3;
            decimal l = 3;
            for (int i = 0; i < iterations; i++)
            {
                if (4 * q + r - t < n * t)
                {
                }
            }
            return q;
        }

        private static string Champernowne(int iterations)
        {
            string temp = "0.";
            for (int i = 1; i < iterations; i++)
            {
                try
                {
                    temp += i;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }
            }
            return temp;
        }

        private static decimal F(Int64 i, int iterations)
        {
            try
            {
                if (iterations > 0)
                {
                    decimal temp = (decimal)1 + (decimal)i / ((decimal)2 * (decimal)i + (decimal)1) * F(i + 1, iterations - 1);
                    return temp;
                }

                return i;
            }
            catch (Exception)
            {
                return i;
            }
        }
    }
}