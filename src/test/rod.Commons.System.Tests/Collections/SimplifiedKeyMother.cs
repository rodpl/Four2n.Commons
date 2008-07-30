using System;
using rod.Commons.System.Collections;

namespace rod.Commons.System.Collections
{
    public static class SimplifiedKeyMother
    {
        #region Nested type: Implementation

        public class Implementation : SimplifiedKey<byte>
        {
            private readonly SimplifiedKeyIdentifiableMother.Implementation _parent;

            internal Implementation()
            {
            }

            public Implementation(SimplifiedKeyIdentifiableMother.Implementation parent)
            {
                _parent = parent;
            }

            protected override string GenerateBussinessKeyValue()
            {
                return String.Concat(_parent.FirstName, _parent.SureName, _parent.BirthDate.ToShortDateString());
            }

            protected override byte GenerateSimplifiedKeyValue()
            {
                var result = _parent.FirstName.GetHashCode();
                result = result*29 + _parent.SureName.GetHashCode();
                result = result*29 + _parent.BirthDate.GetHashCode();
                return (byte)(result % 8);
            }
        }

        #endregion
    }
}