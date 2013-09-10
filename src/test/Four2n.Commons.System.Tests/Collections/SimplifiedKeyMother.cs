// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimplifiedKeyMother.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the SimplifiedKeyMother type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.System.Collections
{
    using global::System;

    public static class SimplifiedKeyMother
    {
        #region Nested type: Implementation

        public class Implementation : SimplifiedKey<byte>
        {
            private readonly SimplifiedKeyIdentifiableMother.Implementation parent;

            public Implementation(SimplifiedKeyIdentifiableMother.Implementation parent)
            {
                this.parent = parent;
            }

            internal Implementation()
            {
            }

            protected override string GenerateBussinessKeyValue()
            {
                return String.Concat(this.parent.FirstName, this.parent.SureName, this.parent.BirthDate.ToShortDateString());
            }

            protected override byte GenerateSimplifiedKeyValue()
            {
                var result = this.parent.FirstName.GetHashCode();
                result = (result * 29) + this.parent.SureName.GetHashCode();
                result = (result * 29) + this.parent.BirthDate.GetHashCode();
                return (byte)(result % 8);
            }
        }

        #endregion
    }
}