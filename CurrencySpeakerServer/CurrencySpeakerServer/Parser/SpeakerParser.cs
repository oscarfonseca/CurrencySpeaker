using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencySpeakerServer.Parser
{
    public class SpeakerParser : ISpeakerParser
    {
        public SpeakerParser()
        {
            Units = new Dictionary<int, string>
            {
                {0, ""}, {1, "one"}, {2, "two"}, {3, "three"}, {4, "four"},
                {5, "five"}, {6, "six"}, {7, "seven"}, {8, "eight"}, {9, "nine"},
                {10, "ten"}, {11, "eleven"}, {12, "twelve"}, {13, "thirteen"}, {14, "fourteen"},
                {15, "fifteen"}, {16, "sixteen"}, {17, "seventeen"}, {18, "eighteen"}, {19, "nineteen"}
            };

            Tens = new Dictionary<int, string>
            {
                {2, "twenty"}, {3, "thirty"}, {4, "forty"}, {5, "fifty"}, {6, "sixty"}, {7, "seventy"},
                {8, "eighty"}, {9, "ninety"}
            };
        }

        public string Parse(string amount)
        {
            if (!double.TryParse(amount.Replace(',', '.'),
                out var amountValue))
                throw new ArgumentException("invalid amount");

            var millions = Math.Truncate(amountValue / 1E6);
            var thousands = Math.Truncate((amountValue - millions * 1E6) / 1E3);
            var units = Math.Truncate(amountValue - millions * 1E6 - thousands * 1E3);
            var cents = Math.Round((amountValue % 1) * 100);

            var result = Parse((int)millions, (int)thousands, (int)units, (int)cents);
            return result;
        }

        private string Parse(int millions, int thousands, int units, int cents)
        {
            var parts = new List<string>();

            if (millions == 0 && thousands == 0 && units == 0)
            {
                parts.Add("zero dollars");
                parts.Add(ConvertCents(cents));
            }
            else
            {
                parts.Add(ParseHundreds(millions));
                parts.Add(MillionsUnits(millions));
                parts.Add(ParseHundreds(thousands));
                parts.Add(ThousandUnits(thousands));
                parts.Add(ParseHundreds(units));
                parts.Add(DollarUnits(millions, thousands, units));
                parts.Add(ConvertCents(cents));
            }

            return string.Join(' ', parts.Where(x => x != string.Empty));
        }

        private string ParseHundreds(int amount)
        {
            var hundreds = Math.Truncate((double) amount / 100);
            var tens = Math.Truncate((amount - hundreds * 100) / 10);
            var units = Math.Truncate(amount - hundreds * 100 - tens * 10);

            return ParseHundreds((int) hundreds, (int) tens, (int) units);
        }

        private string ParseHundreds(int hundreds, int tens, int units)
        {
            var outParts = new List<string>
            {
                ConvertHundreds(hundreds), ConvertTens(hundreds, tens, units)
            };

            return string.Join(" ", outParts.Where(x => x != string.Empty));
        }

        private string MillionsUnits(int millions)
        {
            return millions switch
            {
                0 => string.Empty,
                _ => "million"
            };
        }

        private string ThousandUnits(int thousands)
        {
            return thousands switch
            {
                0 => string.Empty,
                _ => "thousand"
            };
        }

        private string DollarUnits(int millions, int thousands, int units)
        {
            if (millions == 0 && thousands == 0 && units == 1)
                return "dollar";

            return "dollars";
        }

        private string CentUnits(int finalCents)
        {
            return finalCents switch
            {
                0 => string.Empty,
                1 => "cent",
                _ => "cents"
            };
        }

        private string ConvertCents(int cents)
        {
            if (cents == 0)
                return string.Empty;

            var tens = Math.Truncate((double) cents / 10);
            var units = cents - tens * 10;

            return $"and {ConvertTens(0, (int) tens, (int) units)} {CentUnits(cents)}";
        }

        private string ConvertTens(int hundreds, int tens, int units)
        {
            if (hundreds == 0 && tens == 0 && units == 0)
                return string.Empty;

            if (tens is 0)
                return Units[units];

            return tens is 1
                ? Units[10 + units]
                : Tens[tens] +
                  (units != 0 ? $"-{Units[units]}" : string.Empty);
        }

        private string ConvertHundreds(int hundreds)
        {
            return hundreds == 0
                ? string.Empty
                : $"{Units[hundreds]} hundred";
        }

        private IDictionary<int, string> Units { get; }
        private IDictionary<int, string> Tens { get; }
    }
}