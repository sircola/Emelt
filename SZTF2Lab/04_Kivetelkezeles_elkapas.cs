using System;

namespace Kivetelkezeles
{
    class x_DemoKivetelkezeles1_elkapas
    {
        public static void Teszt()
        {
            // NullReferenceException 
            //              SomeClass x=null; x.ToString(); 
            //              SomeType[] arr=null; x[20] 
            // IndexOutOfRangeException
            //              int[] arr=new int[5];
            //              int[10]=42;
            // ArgumentException
            //              string s="hello";
            //              s.Substring(0, 20);
            // DivideByZeroException
            //              int a = 5, b = 0;
            //              int c = a / b;
            // FormatException
            // OverflowException

            //Exception
            //ApplicationException
            //SystemException
            //ArithmeticException
            //DivideByZeroException
            try
            {
                Console.WriteLine("Kérek 2 számot!");
                int num = int.Parse(Console.ReadLine());
                int num2 = int.Parse(Console.ReadLine());

                Console.WriteLine("1. szám: " + num);
                Console.WriteLine("2. szám: " + num2);
                Console.WriteLine("DIV: " + num / num2);
            }
            catch (DivideByZeroException) // Sorrendben történik az elkapás...legutoljára az 'Exception'
            {
            }
            catch (FormatException)
            {
                Console.WriteLine("NO NUMBER");
                throw new Exception("tovább bodás..."); //de itt nem fogjuk tudni elkapni...
            }
            catch (OverflowException e)
            {
                Console.WriteLine("BIG NUMBER");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.StackTrace);
            }
            catch (SystemException ex)
            {
                Console.WriteLine("SYSTEM ERROR: " + ex.Message);
            }
            //catch (Exception ex)
            //{
            //    Console.WriteLine("ERROR: " + ex.Message);
            //}
            finally // Finally block: Catch blokkok után írható, de nem kötelező. Minden esetben lefut, akkor is, ha a try blokkban nem keletkezett kivétel.
            {
                Console.ReadLine();
            }
        }
    }
}
