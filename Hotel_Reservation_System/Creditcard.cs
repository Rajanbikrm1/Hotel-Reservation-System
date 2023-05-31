using System;
using System.Collections.Generic;
using System.Text;

namespace InitialTestApp
{
    public static class Creditcard
    {
        // Validate the given credit card number
        public static bool IsValid(long number)
        {
            return (GetSize(number) >= 13 && GetSize(number) <= 16) &&
                    (PrefixMatched(number, 4) || PrefixMatched(number, 5) ||
                    PrefixMatched(number, 37) || PrefixMatched(number, 6)) &&
                    ((SumOfDoubleEvenPlace(number) + SumOfOddPlace(number)) % 10 == 0);
        }

        // Add all single-digit numbers from given value
        private static int SumOfDoubleEvenPlace(long number)
        {
            int sum = 0;
            String num = number + "";
            for (int i = GetSize(number) - 2; i >= 0; i -= 2)
                sum += GetDigit(int.Parse(num[i] + "") * 2);

            return sum;
        }

        // Get an individual digit from the card number
        // If is single digit number, return number
        // If is two dogit value, return summation of 2 digits
        private static int GetDigit(int number)
        {
            if (number < 9)
                return number;
            return number / 10 + number % 10;
        }

        // Return sum of odd-place digits in the given card number
        private static int SumOfOddPlace(long number)
        {
            int sum = 0;
            String num = number + "";
            for (int i = GetSize(number) - 1; i >= 0; i -= 2)
                sum += int.Parse(num[i] + "");
            return sum;
        }

        // Check if the digit d is a prefix in number
        private static bool PrefixMatched(long number, int d)
        {
            return GetPrefix(number, GetSize(d)) == d;
        }

        // Return the number of digits in d
        private static int GetSize(long d)
        {
            String num = d + "";    // Convert to a string
            return num.Length;      // Get the length of that now converted string
        }

        // Get the firsk k numbers of digits from number
        private static long GetPrefix(long number, int k)
        {
            if (GetSize(number) > k)
            {
                String num = number + "";
                return long.Parse(num.Substring(0, k));
            }
            return number;
        }
    }
}
