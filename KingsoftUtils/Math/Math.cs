using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingsoft.Utils.Math
{
    public class Math
    {
        public static int[] Primes(int from, int to, Delegates.Templates.DelegateTemplates._1_Void<int> ifPrime = null)
        {
            List<int> primes = new List<int>();

            int num = from;

            while (true)
            {

                bool isPrime = true;

                if (num > 1)
                {
                    for (int i = 2; i <= System.Math.Sqrt(num); i++)
                        if (num % i == 0)
                            isPrime = false;
                }
                else isPrime = false;

                if (isPrime)
                {
                    primes.Add(num);
                    if (ifPrime != null)
                        ifPrime(num);
                }

                if (num >= to)
                    break;
                num++;
            }

            return primes.ToArray();
        }
        /// <summary>
        ///     computes the smallest equivalent to the input
        /// </summary>
        /// <param name="frac">input fraction (1/2)(x/y)</param>
        /// <returns>smallest equivalent from input</returns>
        public static (int, int) Frac((int, int) frac)
        {
            (int, int) result = frac;
            bool foundResult = false;

            while (!foundResult)
            {
                bool x = true;
                for (int i = 2; i < 10; i++)
                {
                    if (result.Item1 % i == 0 && result.Item2 % i == 0)
                    {
                        result.Item1 /= i;
                        result.Item2 /= i;
                        x = false;
                        break;
                    }
                }

                if (x)
                    foundResult = true;
            }

            return result;
        }
    }
}
