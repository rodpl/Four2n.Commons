// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NipNumberValidatorAttribute.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the NipNumberValidatorAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.System.ComponentModel.DataAnnotations
{
    using Validators;

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

            return new NipNumberValidator(nip).IsValid();
        }
    }
}
