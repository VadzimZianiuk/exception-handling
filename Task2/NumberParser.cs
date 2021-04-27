using System;
using System.Diagnostics;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        public int Parse(string stringValue)
        {
            if(stringValue is null)
            {
                throw new ArgumentNullException(nameof(stringValue));
            }

            if(string.IsNullOrWhiteSpace(stringValue))
            {
                throw new FormatException($"{nameof(stringValue)} is empty or white space.");
            }

            stringValue = stringValue.Trim();
            int start = 0;
            bool isNegative = false;
            switch (stringValue[start])
            {
                case '-':
                    start++;
                    isNegative = true;
                    break;
                case '+':
                    start++;
                    break;
            }

            if (start == stringValue.Length)
            {
                throw new FormatException($"{nameof(stringValue)} contains only '+' or '-'.");
            }

            int negativeResult = 0;
            while (start < stringValue.Length)
            {
                negativeResult = checked(negativeResult * 10 - GetDigit(stringValue[start++]));
            } 

            return isNegative ? negativeResult : checked(-negativeResult);
        }

        private static int GetDigit(char digit) => char.IsDigit(digit) 
            ? digit - '0' 
            : throw new FormatException($"{nameof(digit)} is not digit.");
    }
}