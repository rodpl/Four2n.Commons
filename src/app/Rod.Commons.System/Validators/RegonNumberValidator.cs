namespace Rod.Commons.System.Validators
{
    using global::System;
    using global::System.Text.RegularExpressions;

    public class RegonNumberValidator
    {
        private readonly string number;
        private readonly byte[] numberArray = new byte[14];

        public RegonNumberValidator(string number)
        {
            this.number = number;
        }

        public static bool IsValid(string number)
        {
            return new RegonNumberValidator(number).IsValid();
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(this.number))
            {
                return false;
            }

            if (!Regex.IsMatch(this.number, @"^([\d]{9}|[\d]{14})$"))
            {
                return false;
            }

            this.FillNumberArray();

            return this.CheckSum();
        }

        private bool CheckSum()
        {
            return this.number.Length == 9 ? this.CheckSum9() : this.CheckSum9() && this.CheckSum14();
        }

        private bool CheckSum14()
        {
            int sum =
                2 * numberArray[0] +
                4 * numberArray[1] +
                8 * numberArray[2] +
                5 * numberArray[3] +
                0 * numberArray[4] +
                9 * numberArray[5] +
                7 * numberArray[6] +
                3 * numberArray[7] +
                6 * numberArray[8] +
                1 * numberArray[9] +
                2 * numberArray[10] +
                4 * numberArray[11] +
                8 * numberArray[12];
            sum %= 11;
            if (sum == 10)
            {
                sum = 0;
            }

            return sum == this.numberArray[13];
        }

        private bool CheckSum9()
        {
            int sum =
                8 * numberArray[0] +
                9 * numberArray[1] +
                2 * numberArray[2] +
                3 * numberArray[3] +
                4 * numberArray[4] +
                5 * numberArray[5] +
                6 * numberArray[6] +
                7 * numberArray[7];
            sum %= 11;
            if (sum == 10)
            {
                sum = 0;
            }

            return sum == this.numberArray[8];
        }

        private void FillNumberArray()
        {
            for (int i = 0; i < number.Length; i++)
            {
                numberArray[i] = Byte.Parse(number.Substring(i, 1));
            }
        }
    }
}