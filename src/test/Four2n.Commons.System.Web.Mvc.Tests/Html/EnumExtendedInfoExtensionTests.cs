// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumExtendedInfoExtensionTests.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the EnumExtendedInfoExtensionTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.System.Web.Mvc.Html
{
    using Diagnostics;

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

        public enum TestEnum
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

            var html = helper.DropDownListEnumExtendedInfo("MyName", TestEnum.Boo, new[] { TestEnum.Foo });
            Assert.AreEqual(@"<select name='MyName'><option value=""1"">Foo</option></select>", html.ToHtmlString());
        }

        [Test]
        public void EnumValueInfoDropdown_WithSampleSelectedDataAndPassedPossibleValues()
        {
            var helper = MvcHelper.GetHtmlHelper(this.viewData);

            var html = helper.DropDownListEnumExtendedInfo("MyName", TestEnum.Foo, new[] { TestEnum.Foo });
            Assert.AreEqual(@"<select name='MyName'><option value=""1"" selected='selected'>Foo</option></select>", html.ToHtmlString());
        }

        [Test]
        public void EnumValueInfoDropdown_WithSampleDataWithoutPassedPossibleValues()
        {
            var helper = MvcHelper.GetHtmlHelper(this.viewData);

            var html = helper.DropDownListEnumExtendedInfo("MyName", TestEnum.Boo);
            Assert.AreEqual(@"<select name='MyName'><option value=""0"" selected='selected'>Boos</option><option value=""1"">Foo</option></select>", html.ToHtmlString());
        }

        [Test]
        public void EnumValueInfoDropdown_WithSampleNullableData()
        {
            var helper = MvcHelper.GetHtmlHelper(this.viewData);

            MvcHtmlString html;
            using (Benchmark.InMiliseconds().ToConsole("{0}"))
            {
                html = helper.DropDownListEnumExtendedInfo("MyName", (TestEnum?)null, this.GetAll);
            }

            Assert.AreEqual(@"<select name='MyName'><option value="""" selected='selected'></option><option value=""0"">Boos</option><option value=""1"">Foo</option></select>", html.ToHtmlString());
            Console.Out.WriteLine(html.ToHtmlString());
        }

        [Test]
        public void EnumValueInfoDropdownFor_NonNullable_WithSampleValue()
        {
            var model = new SampleModel();
            model.SomeOption = TestEnum.Foo;

            var helper = MvcHelper.GetHtmlHelper(new ViewDataDictionary<SampleModel>(model));

            var html = helper.DropDownListEnumExtendedInfoFor(x => x.SomeOption);

            Assert.AreEqual(@"<select name='SomeOption'><option value=""0"">Boos</option><option value=""1"" selected='selected'>Foo</option></select>", html.ToHtmlString());
            Console.Out.WriteLine(html.ToHtmlString());
        }

        [Test]
        public void EnumValueInfoDropdownFor_Nullable_WithSampleValue()
        {
            var model = new SampleModel();
            model.SomeNullableOption = TestEnum.Foo;

            var helper = MvcHelper.GetHtmlHelper(new ViewDataDictionary<SampleModel>(model));

            var html = helper.DropDownListEnumExtendedInfoFor(x => x.SomeNullableOption);

            Assert.AreEqual(@"<select name='SomeNullableOption'><option value=""""></option><option value=""0"">Boos</option><option value=""1"" selected='selected'>Foo</option></select>", html.ToHtmlString());
            Console.Out.WriteLine(html.ToHtmlString());
        }

        [Test]
        public void EnumValueInfoDropdownFor_Nullable_WithParentSampleValue()
        {
            var model = new SampleModel();
            model.SomeNullableOption = TestEnum.Foo;
            var parent = new ParentSampleModel();
            parent.Child = model;

            var helper = MvcHelper.GetHtmlHelper(new ViewDataDictionary<ParentSampleModel>(parent));

            var html = helper.DropDownListEnumExtendedInfoFor(x => x.Child.SomeNullableOption);

            Assert.AreEqual(@"<select name='Child.SomeNullableOption'><option value=""""></option><option value=""0"">Boos</option><option value=""1"" selected='selected'>Foo</option></select>", html.ToHtmlString());
            Console.Out.WriteLine(html.ToHtmlString());
        }

        public class ParentSampleModel
        {
            public SampleModel Child { get; set; }
        }

        public class SampleModel
        {
            public TestEnum SomeOption { get; set; }

            public TestEnum? SomeNullableOption { get; set; }
        }
    }
}
