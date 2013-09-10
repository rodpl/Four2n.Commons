namespace Four2n.Commons.System.Validators
{
    using global::System;
    using global::System.Text.RegularExpressions;

    public class DowodOsobistyNumberValidator
    {
        private string number;
        private static readonly char[] letterValues = {
                                                     '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E'
                                                     , 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S',
                                                     'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
                                             };

        public DowodOsobistyNumberValidator(string number)
        {
            this.number = number;
        }

        public static bool IsValid(string number)
        {
            return new DowodOsobistyNumberValidator(number).IsValid();
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(this.number))
            {
                return false;
            }

            if (!Regex.IsMatch(number, @"^[A-Z]{3}\s?[\d]{6}$"))
            {
                return false;
            }

            // Delete inner spaces
            this.number = number.Replace(" ", string.Empty);

            return this.CheckSum();
        }

        private bool CheckSum()
        {
            int checkSum = 0;

            checkSum = 7 * GetLetterValue(number[0]);
            checkSum += 3 * GetLetterValue(number[1]);
            checkSum += 1 * GetLetterValue(number[2]);
            checkSum += 7 * GetLetterValue(number[4]);
            checkSum += 3 * GetLetterValue(number[5]);
            checkSum += 1 * GetLetterValue(number[6]);
            checkSum += 7 * GetLetterValue(number[7]);
            checkSum += 3 * GetLetterValue(number[8]);
            checkSum %= 10;
            return checkSum == GetLetterValue(number[3]);
        }

        private static int GetLetterValue(char letter)
        {
            int i;
            for (i = 0; i < letterValues.Length; i++)
            {
                if (letter == letterValues[i])
                {
                    return i;
                }
            }
            return -1;
        }

    }
}