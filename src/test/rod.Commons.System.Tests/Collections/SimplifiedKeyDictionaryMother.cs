using rod.Commons.System.Collections;

namespace rod.Commons.System.Tests.Collections
{
    public class SimplifiedKeyDictionaryMother
    {
        #region Nested type: Implementation

        public class Implementation :
            SimplifiedKeyDictionary<byte, SimplifiedKeyMother.Implementation, SimplifiedKeyIdentifiableMother.Implementation>
        {
            public static Implementation Create()
            {
                return new Implementation();
            }
        }

        #endregion
    }
}