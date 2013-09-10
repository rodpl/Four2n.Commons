namespace Four2n.Commons.System.ComponentModel.DataAnnotations
{
    using Validators;

    using global::System;
    using global::System.ComponentModel.DataAnnotations;
    using global::System.Text.RegularExpressions;

    /// <summary>
    /// Validator for checkin Polish PESEL number.
    /// </summary>
    public class PeselNumberValidatorAttribute : ValidationAttribute
    {
        private int sum;

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var number = value.ToString();

            if (string.Empty.Equals(number))
            {
                return true;
            }

            return new PeselNumberValidator(number).IsValid();
        }
    }
}