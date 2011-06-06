// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumExtendedInfoExtensionTests.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the EnumExtendedInfoExtensionTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.System.Web.Mvc.Html
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Text;
    using global::System.Web.Mvc;

    using NUnit.Framework;

    using UnitTestUtils;

    [TestFixture]
    public class EnumExtendedInfoExtensionTests
    {
        private ViewDataDictionary viewData = new ViewDataDictionary();

        internal enum TestEnum
        {
            [EnumExtendedInfo(Name = "Boos", CustomValue = 1)]
            Boo,
            [EnumExtendedInfo(CustomValue = "Jeez")]
            Foo
        }

        private IEnumerable<TestEnum> GetAll
        {
            get
            {
                yield return TestEnum.Boo;
                yield return TestEnum.Foo;
            }
        }

        private IEnumerable<TestEnum?> GetAllWithNull
        {
            get
            {
                yield return null;
                yield return TestEnum.Boo;
                yield return TestEnum.Foo;
            }
        }

        [Test]
        public void EnumValueInfoDropdown_WithSampleDataAndPassedPossibleValues()
        {
            var helper = MvcHelper.GetHtmlHelper(this.viewData);

            var html = helper.DropdownEnumExtendedInfo("MyName", TestEnum.Boo, new[] { TestEnum.Foo });
            Assert.AreEqual(@"<select name='MyName'><option value=""1"">Foo</option></select>", html.ToHtmlString());
        }

        [Test]
        public void EnumValueInfoDropdown_WithSampleSelectedDataAndPassedPossibleValues()
        {
            var helper = MvcHelper.GetHtmlHelper(this.viewData);

            var html = helper.DropdownEnumExtendedInfo("MyName", TestEnum.Foo, new[] { TestEnum.Foo });
            Assert.AreEqual(@"<select name='MyName'><option value=""1"" selected='selected'>Foo</option></select>", html.ToHtmlString());
        }

        [Test]
        public void EnumValueInfoDropdown_WithSampleDataWithoutPassedPossibleValues()
        {
            var helper = MvcHelper.GetHtmlHelper(this.viewData);

            var html = helper.DropdownEnumExtendedInfo("MyName", TestEnum.Boo);
            Assert.AreEqual(@"<select name='MyName'><option value=""0"" selected='selected'>Boos</option><option value=""1"">Foo</option></select>", html.ToHtmlString());
        }

        [Test]
        public void EnumValueInfoDropdown_WithSampleNullableData()
        {
            var helper = MvcHelper.GetHtmlHelper(this.viewData);

            var html = helper.DropdownEnumExtendedInfo("MyName", (TestEnum?)null, this.GetAll);
            Assert.AreEqual(@"<select name='MyName'><option value="""" selected='selected'></option><option value=""0"">Boos</option><option value=""1"">Foo</option></select>", html.ToHtmlString());
            Console.Out.WriteLine(html.ToHtmlString());
        }

        [Test]
        public void EnumValueInfoDropdownFor_NonNullable_WithSampleValue()
        {
            var model = new SampleModel();
            model.SomeOption = TestEnum.Foo;

            var helper = MvcHelper.GetHtmlHelper(new ViewDataDictionary<SampleModel>(model));

            var html = helper.DropdownEnumExtendedInfoFor(x => x.SomeOption);

            Assert.AreEqual(@"<select name='SomeOption'><option value=""0"">Boos</option><option value=""1"" selected='selected'>Foo</option></select>", html.ToHtmlString());
            Console.Out.WriteLine(html.ToHtmlString());
        }

        [Test]
        public void EnumValueInfoDropdownFor_Nullable_WithSampleValue()
        {
            var model = new SampleModel();
            model.SomeNullableOption = TestEnum.Foo;

            var helper = MvcHelper.GetHtmlHelper(new ViewDataDictionary<SampleModel>(model));

            var html = helper.DropdownEnumExtendedInfoFor(x => x.SomeNullableOption);

            Assert.AreEqual(@"<select name='SomeNullableOption'><option value=""""></option><option value=""0"">Boos</option><option value=""1"" selected='selected'>Foo</option></select>", html.ToHtmlString());
            Console.Out.WriteLine(html.ToHtmlString());
        }

        internal class SampleModel
        {
            internal TestEnum SomeOption { get; set; }

            internal TestEnum? SomeNullableOption { get; set; }
        }
    }
}
