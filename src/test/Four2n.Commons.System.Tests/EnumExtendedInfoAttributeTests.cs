// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumExtendedInfoAttributeTests.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the ExtendedEnumAttributeTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.System
{
    using Diagnostics;

    using global::System;
    using global::System.Globalization;

    using NUnit.Framework;

    [TestFixture]
    public class EnumExtendedInfoAttributeTests
    {
        public enum TestEnum
        {
            [EnumExtendedInfo(Name = "waiting", CustomValue = "...")]
            Pending = 1,
            [EnumExtendedInfo(Name = "misc")]
            Misc = 2,
            [EnumExtendedInfo(CustomValue = "dbName")]
            Something,
            NonPending,
            [EnumExtendedInfo(CustomValue = 12L)]
            Numeric,
            [EnumExtendedInfo(CustomValue = 23)]
            Integer,
            [EnumExtendedInfo(CustomValue = "0.2")]
            Decimal
        }

        public enum AnoterTestEnum
        {
            [EnumExtendedInfo(Name = "some male", CustomValue = "M")]
            Male = 1,
            [EnumExtendedInfo(Name = "just female", CustomValue = "F")]
            Female = 2
        }

        [Flags]
        public enum TestFlagEnum
        {
            [EnumExtendedInfo(Name = "waiting", CustomValue = "...")]
            Pending = 1,
            [EnumExtendedInfo(Name = "misc")]
            Misc = 2,
            [EnumExtendedInfo(CustomValue = "dbName")]
            Something = 4,
            NonPending = 8,
            [EnumExtendedInfo(CustomValue = 12L)]
            Numeric = 16,
            [EnumExtendedInfo(CustomValue = 23)]
            Integer = 32
        }

        [Test]
        [Description("Name tests")]
        public void GetExtendedInfoByEnumValue_ValueWithAttributeWithName_ReturnsAttributeInstanceWithNameAsPassedIntoAttribute()
        {
            Assert.AreEqual("waiting", EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestEnum.Pending).Name);
            Assert.AreEqual("misc", EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestEnum.Misc).Name);

            Assert.AreEqual("waiting", EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestFlagEnum.Pending).Name);
            Assert.AreEqual("misc", EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestFlagEnum.Misc).Name);

            Assert.AreEqual("waiting, misc", EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestFlagEnum.Pending | TestFlagEnum.Misc).Name);

            // Names for flag enum
            var names = EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestFlagEnum.Pending | TestFlagEnum.Misc).GetNames();
            Assert.Contains("waiting", names);
            Assert.Contains("misc", names);
        }

        [Test]
        [Description("Name tests")]
        public void GetExtendedInfoByEnumValue_ValueWithAttributeWithoutName_ReturnsAttributeInstanceWithValueAsName()
        {
            Assert.AreEqual("Something", EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestEnum.Something).Name);
        }

        [Test]
        [Description("Name tests")]
        public void GetExtendedInfoByEnumValue_ValueWithoutAttribute_ReturnsAttriburteInstanceWithValueAsName()
        {
            Assert.AreEqual("NonPending", EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestEnum.NonPending).Name);
        }

        [Test]
        [Description("Value tests")]
        public void GetExtendedInfoByEnumValue_ValueWithAttributeWithNameAndWithValueName_ReturnsAttributeInstanceWithValueNameAsPassedIntoAttribute()
        {
            Assert.AreEqual("...", EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestEnum.Pending).CustomValue as string);

            var customValues = EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestFlagEnum.Pending | TestFlagEnum.Integer).GetCustomValues();

            Assert.Contains("...", customValues);
            Assert.Contains(23, customValues);
            Assert.That(customValues[0], Is.EqualTo("..."));
            Assert.That(customValues.Length, Is.EqualTo(2));
        }

        [Test]
        [Description("Value tests")]
        public void GetExtendedInfoByEnumValue_CustomValueLooksLikeDecimal_ReturnsDecimal()
        {
            Assert.AreEqual(0.2m, EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestEnum.Decimal).CustomValue);
            Assert.AreEqual(typeof(decimal), EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestEnum.Decimal).CustomValue.GetType());
        }

        [Test]
        [Description("Value tests")]
        public void GetExtendedInfoByEnumValue_ValueWithAttributeWithoutNameAndWithValueName_ReturnsAttributeInstanceWithValueNameAsPassedIntoAttribute()
        {
            Assert.AreEqual("dbName", EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestEnum.Something).CustomValue as string);

            Assert.AreEqual("dbName", EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestFlagEnum.Something).CustomValue as string);
        }

        [Test]
        [Description("Value tests")]
        public void GetExtendedInfoByEnumValue_ValueWithAttributeWithNameAndWithoutValueName_ReturnsAttributeInstanceWithValueNameAsName()
        {
            Assert.AreEqual("misc", EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestEnum.Misc).CustomValue as string);

            Assert.AreEqual("misc", EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestFlagEnum.Misc).CustomValue as string);
        }

        [Test]
        [Description("Value tests")]
        public void GetExtendedInfoByEnumValue_ValueWithoutAttribute_ReturnsAttriburteInstanceWithValueAsValueName()
        {
            Assert.AreEqual("NonPending", EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestEnum.NonPending).CustomValue as string);

            Assert.AreEqual("NonPending", EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestFlagEnum.NonPending).CustomValue as string);
        }

        [Test]
        public void GetExtendedInfoByEnumValue_NullValue_ReturnsEmptyAttributeWithNullValuesAndNames()
        {
            Assert.AreEqual(null, EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue((TestEnum?)null).CustomValue);
            Assert.AreEqual(null, EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue((TestEnum?)null).Name);
        }

        [Test]
        [Description("Value tests")]
        [TestCase("...", TestEnum.Pending)]
        [TestCase("misc", TestEnum.Misc)]
        [TestCase("dbName", TestEnum.Something)]
        [TestCase("NonPending", TestEnum.NonPending)]
        [TestCase(12L, TestEnum.Numeric)]
        public void GetEnumValueByCustomValue_ValidValueName_ReturnsValidValue(object databaseRepresentation, TestEnum expectedValue)
        {
            Assert.AreEqual(expectedValue, EnumExtendedInfoAttribute.GetEnumValueByCustomValue<TestEnum>(databaseRepresentation));
        }

        [Test]
        public void GetEnumValueByCustomValue_NonExistingValueName_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(delegate { EnumExtendedInfoAttribute.GetEnumValueByCustomValue<TestEnum>("nie ma"); });
        }

        [Test]
        public void GetEnumValueByCustomValue_InvalidTypeNotEnum_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(delegate { EnumExtendedInfoAttribute.GetEnumValueByCustomValue<string>("nie ma"); });
        }

        [Test]
        public void GetEnumValueByCustomValue_Decimal_FindsStringRepresentation()
        {
            Assert.AreEqual(TestEnum.Decimal, EnumExtendedInfoAttribute.GetEnumValueByCustomValue<TestEnum>(0.2M));
            Assert.Throws<ArgumentException>(delegate { EnumExtendedInfoAttribute.GetEnumValueByCustomValue<TestEnum>(12M); });
        }

        [Test]
        public void GetExtendedInfoByEnumValue_TwoDifferentEnums()
        {
            Assert.AreEqual("...", EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestEnum.Pending).CustomValue as string);
            Assert.AreEqual("misc", EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestEnum.Misc).CustomValue);

            Assert.AreEqual("M", EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(AnoterTestEnum.Male).CustomValue as string);
            Assert.AreEqual("F", EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(AnoterTestEnum.Female).CustomValue as string);
        }

        [Test]
        [Explicit]
        public void GetExtendedInfoByEnumValue_Performance_Test()
        {
            /* 1 times takes 3 ms
             * 100 times takes 0 ms
             * 100.000 times takes 89 ms
             * TryGetValue in dictionary
             * 1 times takes 1 ms
             * 100 times takes 0 ms
             * 100.000 times takes 40 ms
             */
            using (Benchmark.InMiliseconds().ToConsole("1 times takes {0} ms"))
            {
                var a = EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestEnum.Decimal).CustomValue;
            }

            using (Benchmark.InMiliseconds().ToConsole("100 times takes {0} ms"))
            {
                for (int i = 0; i < 100; i++)
                {
                    var a = EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestEnum.Decimal).CustomValue;
                }
            }

            using (Benchmark.InMiliseconds().ToConsole("100.000 times takes {0} ms"))
            {
                for (int i = 0; i < 100000; i++)
                {
                    var a = EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestEnum.Decimal).CustomValue;
                }
            }
        }

        [Test]
        [Explicit]
        public void GetExtendedInfoByEnumValueAndGetCustomValues_Performance_Test()
        {
            /* 1 times takes 3 ms
             * 100 times takes 0 ms
             * 100.000 times takes 38 ms
             */
            using (Benchmark.InMiliseconds().ToConsole("1 times takes {0} ms"))
            {
                EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestFlagEnum.Pending | TestFlagEnum.Integer).GetCustomValues();
            }

            using (Benchmark.InMiliseconds().ToConsole("100 times takes {0} ms"))
            {
                for (int i = 0; i < 100; i++)
                {
                    EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestFlagEnum.Pending | TestFlagEnum.Integer).GetCustomValues();
                }
            }

            using (Benchmark.InMiliseconds().ToConsole("100.000 times takes {0} ms"))
            {
                for (int i = 0; i < 100000; i++)
                {
                    EnumExtendedInfoAttribute.GetExtendedInfoByEnumValue(TestFlagEnum.Pending | TestFlagEnum.Integer).GetCustomValues();
                }
            }
        }

        [Test]
        [Explicit]
        public void GetEnumValueByCustomValue_Performance_Test()
        {
            /* 1 times takes 4 ms
             * 100 times takes 9 ms
             * 100.000 times takes 10200 ms
             * with caching
             * 1 times takes 5 ms
             * 100 times takes 0 ms
             * 100.000 times takes 1214 ms
             * TryGetValue in dictionary
             * 1 times takes 5 ms
             * 100 times takes 0 ms
             * 100.000 times takes 750 ms
             */
            using (Benchmark.InMiliseconds().ToConsole("1 times takes {0} ms"))
            {
                EnumExtendedInfoAttribute.GetEnumValueByCustomValue<TestEnum>(23);
            }

            using (Benchmark.InMiliseconds().ToConsole("100 times takes {0} ms"))
            {
                for (int i = 0; i < 100; i++)
                {
                    EnumExtendedInfoAttribute.GetEnumValueByCustomValue<TestEnum>(23);
                }
            }

            using (Benchmark.InMiliseconds().ToConsole("100.000 times takes {0} ms"))
            {
                for (int i = 0; i < 100000; i++)
                {
                    EnumExtendedInfoAttribute.GetEnumValueByCustomValue<TestEnum>(23);
                }
            }
        }
    }
}