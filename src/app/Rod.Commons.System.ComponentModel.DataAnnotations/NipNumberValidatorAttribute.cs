// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NipNumberValidatorAttribute.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the NipNumberValidatorAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.System.ComponentModel.DataAnnotations
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.ComponentModel;
    using global::System.ComponentModel.DataAnnotations;
    using global::System.Text;
    using global::System.Text.RegularExpressions;

    /// <summary>
    /// Validator for checkin Polish VAT number.
    /// </summary>
    public class NipNumberValidatorAttribute : ValidationAttribute
    {
        private readonly int[] validNums = { 6, 5, 7, 2, 3, 4, 5, 6, 7 };

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
            var nip = value.ToString();

            if (!Regex.IsMatch(nip, @"^[\d]{10}$"))
            {
                return false;
            }

            this.sum = 0;
            for (int t = 8; t >= 0; t--)
            {
                this.sum += this.validNums[t] * Convert.ToInt32(nip.Substring(t, 1));
            }

            return (this.sum % 11) == 10 ? false : ((this.sum % 11) == Convert.ToInt32(nip.Substring(9, 1)));
        }
    }
}
