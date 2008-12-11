//------------------------------------------------------------------------------------------------- 
// <copyright file="SimplifiedKeyDictionaryMother.cs" company="Daniel Dabrowski - rod.blogsome.com">
// Copyright (c) Daniel Dabrowski - rod.blogsome.com.  All rights reserved.
// </copyright>
// <summary>Defines the SimplifiedKeyDictionaryMother type.</summary>
//-------------------------------------------------------------------------------------------------
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