namespace Rod.Commons.System.ComponentModel.DataAnnotations
{
    using Validators;

    using global::System.ComponentModel.DataAnnotations;

    public class RegonNumberValidatorAttribute : ValidationAttribute
    {
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

            var nip = value.ToString();

            if (string.Empty.Equals(nip))
            {
                return true;
            }

            return new RegonNumberValidator(nip).IsValid();
        }
    }
}