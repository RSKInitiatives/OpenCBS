using System.Text;

namespace OpenCBS.Stringifier
{
    public class IndianEnglish
    {

        private readonly string[] _1To19 =
        {
            "",
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine",
            "ten",
            "eleven",
            "twelve",
            "thirteen",
            "fourteen",
            "fifteen",
            "sixteen",
            "seventeen",
            "eighteen",
            "nineteen",
        };

        private readonly string[] _tens =
        {
            "",
            "twenty",
            "thirty",
            "fourty",
            "fifty",
            "sixty",
            "seventy",
            "eighty",
            "ninety"
        };

        public string Stringify(int amount)
        {
            var text = amount.ToString("D9");
            var result = new StringBuilder();

            // Crores
            var chunk = text.Substring(0, 2);
            chunk = Stringify1To99(int.Parse(chunk));
            if (!string.IsNullOrEmpty(chunk)) result.Append(chunk + " crore ");

            // Lacs
            chunk = text.Substring(2, 2);
            chunk = Stringify1To99(int.Parse(chunk));
            if (!string.IsNullOrEmpty(chunk)) result.Append(chunk + " lac ");

            // Thousands
            chunk = text.Substring(4, 2);
            chunk = Stringify1To99(int.Parse(chunk));
            if (!string.IsNullOrEmpty(chunk)) result.Append(chunk + " thousand ");

            // Hundreds
            chunk = text.Substring(6, 1);
            chunk = Stringify1To19(int.Parse(chunk));
            if (!string.IsNullOrEmpty(chunk)) result.Append(chunk + " hundred ");

            // 1 to 99
            chunk = text.Substring(7, 2);
            chunk = Stringify1To99(int.Parse(chunk));
            if (!string.IsNullOrEmpty(chunk)) result.Append(chunk);

            return result.ToString().Trim();
        }

        private string Stringify1To19(int amount)
        {
            return amount < 20 ? _1To19[amount] : string.Empty;
        }

        private string Stringify1To99(int amount)
        {
            if (amount < 20) return Stringify1To19(amount);
            var part1 = amount / 10;
            var part2 = amount % 10;
            return _tens[part1 - 1] + " " + Stringify1To19(part2);
        }
    }
}
