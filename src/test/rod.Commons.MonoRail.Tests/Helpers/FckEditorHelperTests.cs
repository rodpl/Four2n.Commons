using Castle.MonoRail.Framework;
using MbUnit.Framework;
using Rhino.Mocks;
using rod.Commons.MonoRail.Helpers;
using rod.Commons.MonoRail.Tests.Controllers;
using System;
using Castle.MonoRail.Framework.Test;
using System.Diagnostics;

namespace rod.Commons.MonoRail.Tests.Helpers
{
    [TestFixture]
    public class FckEditorHelperTests : MockedTestFixture
    {
        #region Setup/Teardown

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _model = new FckEditorHelper();

            _model.SetController(new HomeController(), new ControllerContext());
            _model.SetContext(new StubEngineContext(new UrlInfo("area", "home", "index", "/app", "sdm")));
            _model.ServerUtility = new StubServerUtility();
        }

        #endregion

        private FckEditorHelper _model;

        [Test]
        public void InstallScripts_ReturnsLinkToJavaScript()
        {
            Assert.AreEqual(@"<script type=""text/javascript"" src=""/app/content/helpers/fckeditor/fckeditor.js""></script>", _model.InstallScripts());
        }

        [Test]
        public void CreateHtmlTest_ReturnsHtmlInDivTags()
        {
            var html = _model.CreateHtml("instance");
            StringAssert.StartsWith(html, "<div>\n");
            StringAssert.EndsWith(html, "</div>\n");
        }
    }
}