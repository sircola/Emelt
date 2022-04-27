using System;

namespace _05_Rekurzio
{
    static class Rekurzio_01_Pelda
    {
        public static void Teszt()
        {
            Console.WriteLine($"10! = {FactorialI(10)}");
            Console.WriteLine($"10! = {FactorialR(10)}");
            Console.WriteLine("-----");
            Console.WriteLine($"Fn(10) = {FibonacciI(10)}");
            Console.WriteLine($"Fn(10) = {FibonacciR(10)}");
            Console.WriteLine("-----");
            ExceptionTeszt();
        }


        #region Factorial
        // 0! = 1
        // 1! = 1
        // 2! = 2 * 1! = 2
        // 3! = 3 * 2! = 6
        // ...
        // n! = n * (n - 1)!
        public static long FactorialI(int n) //iterative
        {
            if (n == 0)
                return 1;
            long value = 1;
            for (int i = n; i > 0; i--)
            {
                value *= i;
            }
            return value;
        }
        public static long FactorialR(int n)
        {
            if (n == 0) //The condition that limites the method for calling itself
                return 1;
            return n * FactorialR(n - 1);
        }
        #endregion


        #region Fibonacci
        // Fn = Fn-1 + Fn-2
        public static long FibonacciI(int n) //iterative
        {
            if (n < 2)
                return n;

            long n0 = 0, n1 = 1;
            for (int i = 2; i <= n; i++)
            {
                long sum = n0 + n1;
                n0 = n1;
                n1 = sum;
            }
            return n1;
        }
        public static long FibonacciR(int n)
        {
            if (n == 0 || n == 1) //satisfaction condition
                return n;
            return FibonacciR(n - 2) + FibonacciR(n - 1);
        }
        #endregion


        #region InnerException
        //a legbelső kivétel kinyerése
        public static Exception GetInnerException(Exception ex)
        {
            if (ex.InnerException == null)//Condition for limiting
                return ex;
            return GetInnerException(ex.InnerException);//Calling the method with inner exception parameter
        }
        public static void ExceptionTeszt()
        {
            try
            {
                throw new Exception("This is the exception",
                    new Exception("This is the first inner exception.",
                        new Exception("This is the last inner exception.")));
            }
            catch (Exception ex)
            {
                Console.WriteLine(GetInnerException(ex).Message);
            }
        }
        #endregion
    }
}
