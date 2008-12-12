using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rod.Commons.System.Collections;

namespace Rod.Commons.System.Collections
{
    public static class SimplifiedKeyIdentifiableMother
    {
        #region Nested type: Implementation

        public class Implementation : ISimplifiedKeyIdentifiable<byte, SimplifiedKeyMother.Implementation>
        {
            private readonly SimplifiedKeyMother.Implementation _key;

            public Implementation(string firstName, string sureName, DateTime birthDate)
            {
                FirstName = firstName;
                SureName = sureName;
                BirthDate = birthDate;
                _key = new SimplifiedKeyMother.Implementation(this);
            }

            public string FirstName { get; set; }
            public string SureName { get; set; }
            public DateTime BirthDate { get; set; }

            #region ISimplifiedKeyIdentifiable<byte,Implementation> Members

            public SimplifiedKeyMother.Implementation Key
            {
                get { return _key; }
            }

            #endregion

            public static Implementation Create()
            {
                return new Implementation("Daniel", "Dabrowski", new DateTime(1922, 02, 17));
            }

            public static List<Implementation> CreateListOfOne()
            {
                return new List<Implementation> { new Implementation("James", "Brown", new DateTime(1942, 02, 17)) };
            }

            public static List<Implementation> CreateListOfTwoUnique()
            {
                var one = new Implementation("Marry", "Newton", new DateTime(1832, 03, 03));
                var two = new Implementation("Phil", "Izaak", new DateTime(1748, 12, 02));
                Assert.IsFalse(one.Key.Equals(two.Key));
                Assert.IsFalse(one.Key.BusinessEquals(two.Key));
                return new List<Implementation> { one, two };
            }

            public static List<Implementation> CreateListOfThreeUnique()
            {
                var one = new Implementation("Marry", "Newton", new DateTime(1832, 03, 03));
                var two = new Implementation("Phil", "Izaak", new DateTime(1748, 12, 02));
                var three = new Implementation("Brad", "Izaak", new DateTime(1748, 12, 02));
                Assert.IsFalse(one.Key.Equals(two.Key));
                Assert.IsFalse(one.Key.Equals(three.Key));
                Assert.IsFalse(one.Key.BusinessEquals(two.Key));
                Assert.IsFalse(one.Key.BusinessEquals(three.Key));
                return new List<Implementation> { one, two, three };
            }

            public static List<Implementation> CreateListOfTwoNonUniqueSimplifiedKeyValueAndUniqueBusinessKeyValue()
            {
                var one = new Implementation("Daniel", "Dabrowski", new DateTime(1922, 02, 17));
                var two = new Implementation("John", "Doe", new DateTime(1978, 02, 10));
                Assert.IsTrue(one.Key.Equals(two.Key));
                Assert.IsFalse(one.Key.BusinessEquals(two.Key));
                return new List<Implementation> { one, two };
            }

            public static List<Implementation> CreateListOfTwoNonUniqueSimplifiedKeyValueAndNonUniqueBusinessKeyValue()
            {
                var one = new Implementation("Miriam", "Hopkins", new DateTime(1985, 12, 27));
                var two = new Implementation("Miriam", "Hopkins", new DateTime(1985, 12, 27));
                Assert.IsTrue(one.Key.Equals(two.Key));
                Assert.IsTrue(one.Key.BusinessEquals(two.Key));
                return new List<Implementation> { one, two };
            }

            public static List<Implementation> CreateListOfThreeNonUniqueSimplifiedKeyValueAndUniqueBusinessKeyValue()
            {
                var one = new Implementation("Daniel", "Dabrowski", new DateTime(1922, 02, 17));
                var two = new Implementation("John", "Doe", new DateTime(1978, 02, 10));
                var three = new Implementation("Jeremy", "Brown", new DateTime(1951, 10, 02));

                Assert.IsTrue(one.Key.Equals(two.Key));
                Assert.IsTrue(one.Key.Equals(three.Key));
                Assert.IsFalse(one.Key.BusinessEquals(two.Key));
                Assert.IsFalse(one.Key.BusinessEquals(three.Key));

                return new List<Implementation> { one, two, three };
            }
        }

        #endregion
    }
}