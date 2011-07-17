namespace Rod.Commons.System.Validators
{
    using global::System;
    using global::System.Text.RegularExpressions;

    public class PeselNumberValidator
    {
        private readonly string peselNumber;
        private readonly byte[] peselArray = new byte[11];

        public PeselNumberValidator(string peselNumber)
        {
            this.peselNumber = peselNumber;
        }

        public static bool IsValid(string peselNumber)
        {
            return new PeselNumberValidator(peselNumber).IsValid();
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(peselNumber))
            {
                return false;
            }

            if (!Regex.IsMatch(peselNumber, @"^[\d]{11}$"))
            {
                return false;
            }

            this.FillPeselArray();

            return this.CheckSum();
        }

        private bool CheckSum()
        {
            int sum =
                1 * peselArray[0] +
                3 * peselArray[1] +
                7 * peselArray[2] +
                9 * peselArray[3] +
                1 * peselArray[4] +
                3 * peselArray[5] +
                7 * peselArray[6] +
                9 * peselArray[7] +
                1 * peselArray[8] +
                3 * peselArray[9];
            sum %= 10;
            sum = 10 - sum;
            sum %= 10;

            return sum == this.peselArray[10];
        }

        private void FillPeselArray()
        {
            for (int i = 0; i < 11; i++)
            {
                peselArray[i] = Byte.Parse(peselNumber.Substring(i, 1));
            }
        }
    }
}
