using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManakerAlgorythm
{
    class Program
    {
        static void Main(string[] args)
        { 
                try { 
                string str=File.ReadAllText(args[0]);
                int[] arr = GetNumberOfPalindroms(str);
                int numberOfOddPalindroms = arr[0];
                int numberOfEvenPalindroms = arr[1];
                File.WriteAllText(args[1],$"В строке {str} всего {numberOfEvenPalindroms + numberOfOddPalindroms} палиндромов, из них: " +
                    $"{numberOfEvenPalindroms} чётной длины, {numberOfOddPalindroms} нечётной длины");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
        }

        public static int[] GetNumberOfPalindroms(string str)
        {
            /*
             * Суть оптимизации алгоритма в добавлении фиктивных символов (например, нуля) между каждыми двумя символами строки.  Тогда все палиндромы переходят
             * в палиндромы нечётной длины, это позволяет не рассматривать случай палиндромов чётной длины в алгоритме. По индукции легко доказывается, что 
             * число палиндромов нечётной длины в изначальной строке равно сумме (palindroms[i] + 1) / 2 для всех нефективных символов и число палиндромов
             * чётной длины в изначльной строке равно  сумме (palindroms[i] / 2) для всех фективных символов.
             * Лучше использовать вместо int тип BigInteger
             */
            string strZero = "";
            int length = str.Length;
            for (int i = 0; i < length - 1; i++)
            {
                strZero += (str[i] + "0");
            }
            if (str != "")
                strZero += str[length - 1];

            int left = 0;
            int right = -1;
            int numberOfOddPalindroms = 0;
            int numberOfEvenPalindroms = 0;
            int lengthZero = strZero.Length;
            int[] palindroms = new int[lengthZero];
            for (int i = 0; i < lengthZero; i++)
            {
                int radius = (i > right) ? 1 : Math.Min(right - i + 1, palindroms[left + right - i]);
                palindroms[i] = radius;
                while (i - radius >= 0 && lengthZero - i - radius > 0 && strZero[i - radius] == strZero[i + radius])
                    palindroms[i] = ++radius;
                if (i + radius - 1 > right)
                {
                    right = i + radius - 1;
                    left = i - radius + 1;
                }
                numberOfOddPalindroms += i % 2 == 0 ? (palindroms[i] + 1) / 2 : 0;
                numberOfEvenPalindroms += i % 2 == 1 ? (palindroms[i] / 2) : 0;
            }
            int[] arr = new int[2] { numberOfOddPalindroms, numberOfEvenPalindroms };
            return arr;
        }
    }
}
