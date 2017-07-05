using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NumberToWordService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service : IService
    {
       
        /// Array to keep the units in words.
        private static readonly string[] UnitsArray = new[]
            {
              "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN",
              "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
           };

        /// Array to keep the tens in words.
        private static readonly string[] TensArray = new[]
        {
             "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
        };


        /// <summary>
        /// Service method to convert number to word which is open for service consumers.
        /// </summary>
        /// <param name="number">number to convert in to words</param>
        /// <returns>number in words</returns>
        public string ConvertToWords(double number)
        {
            if (number == 0)
                return "ZERO DOLLAR";
            if (number < 0)
                return "MINUS " + ConvertToWords(Math.Abs(number));
            var result = new StringBuilder();
            long intPart = (long)Math.Truncate(number);

            long decimalPart = (long)((number - intPart) * 100);
            result.Append(NumberToWord(intPart) + " DOLLARS");

            if (decimalPart > 0)
            {
                result.Append(" AND " + NumberToWord((long)(decimalPart)) + " CENTS ");
            }
            return result.ToString();
        }

        //Method to change the number into words.
        private static string NumberToWord(long numberToChange)
        {
            var result = new StringBuilder();
            //Condition to check if number is in Billions
            if ((numberToChange / 1000000000) > 0)
            {
                result.Append(NumberToWord(numberToChange / 1000000) + " BILLION ");
                numberToChange %= 1000000;
            }
            //Condition to check if number is in Millions
            if ((numberToChange / 1000000) > 0)
            {
                result.Append(NumberToWord(numberToChange / 1000000) + " MILLION ");
                numberToChange %= 1000000;
            }
            //Condition to check if number is in thousands
            if ((numberToChange / 1000) > 0)
            {
                result.Append(NumberToWord(numberToChange / 1000) + " THOUSAND ");
                numberToChange %= 1000;
            }
            //Condition to check if number is in hundreds
            if ((numberToChange / 100) > 0)
            {
                result.Append(NumberToWord(numberToChange / 100) + " HUNDRED ");
                numberToChange %= 100;
            }

            if (numberToChange > 0)
            {
                if (result.Length > 0)
                    result.Append("AND ");

                if (numberToChange < 20)
                    result.Append(UnitsArray[numberToChange]);
                else
                {
                    result.Append(TensArray[numberToChange / 10]);
                    if ((numberToChange % 10) > 0)
                        result.Append("-" + UnitsArray[numberToChange % 10]);
                }
            }
            return result.ToString();
        }
    }
}
