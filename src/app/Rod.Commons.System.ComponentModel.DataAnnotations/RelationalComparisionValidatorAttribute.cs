// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RelationalComparisionValidatorAttribute.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the RelationalComparisionValidatorAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.System.ComponentModel.DataAnnotations
{
    using global::System.ComponentModel.DataAnnotations;

    public class RelationalComparisionValidatorAttribute : ValidationAttribute
    {
        private readonly RelationComparision comparision;

        public RelationalComparisionValidatorAttribute(RelationComparision comparision)
        {
            this.comparision = comparision;
        }

        public override bool IsValid(object value)
        {
            throw new global::System.NotImplementedException();
        }
    }
}