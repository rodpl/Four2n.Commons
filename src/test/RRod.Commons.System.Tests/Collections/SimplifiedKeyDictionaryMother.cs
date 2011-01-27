// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimplifiedKeyDictionaryMother.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the SimplifiedKeyDictionaryMother type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.System.Collections
{
    public class SimplifiedKeyDictionaryMother
    {
        public class Implementation :
            SimplifiedKeyDictionary<byte, SimplifiedKeyMother.Implementation, SimplifiedKeyIdentifiableMother.Implementation>
        {
            public static Implementation Create()
            {
                return new Implementation();
            }
        }
    }
}