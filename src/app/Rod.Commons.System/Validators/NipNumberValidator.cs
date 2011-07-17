namespace Rod.Commons.System.Validators
{
    using global::System;
    using global::System.Text.RegularExpressions;

    public class NipNumberValidator
    {
        private readonly string number;
        private readonly int[] validNums = { 6, 5, 7, 2, 3, 4, 5, 6, 7 };

        public NipNumberValidator(string number)
        {
            this.number = number;
        }

        public static bool IsValid(string number)
        {
            return new NipNumberValidator(number).IsValid();
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(this.number))
            {
                return false;
            }

            if (!Regex.IsMatch(this.number, @"^[\d]{10}$"))
            {
                return false;
            }

            return this.CheckSum();
        }

        private bool CheckSum()
        {
            int sum = 0;
            for (int t = 8; t >= 0; t--)
            {
                sum += this.validNums[t] * Convert.ToInt32(this.number.Substring(t, 1));
            }

            return (sum % 11) != 10 && ((sum % 11) == Convert.ToInt32(this.number.Substring(9, 1)));
        }
    }
}