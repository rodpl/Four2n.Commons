using Castle.MonoRail.Framework;
using MbUnit.Framework;
using Rhino.Mocks;
using rod.Commons.MonoRail.Helpers;
using rod.Commons.MonoRail.Tests.Controllers;

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

            var controller = new HomeController();
            var controllerContext = new ControllerContext();
            var context = Mocks.DynamicMock<IEngineContext>();

            _model.SetController(controller, controllerContext);
            _model.SetContext(context);
        }

        #endregion

        private FckEditorHelper _model;

        [Test]
        public void InstallScriptsTest()
        {
            _model.Context.Stub(x => x.ApplicationPath).Return(@"/app");

            Assert.AreEqual(@"<script type=""text/javascript"" src=""/app/content/helpers/fckeditor/fckeditor.js""></script>",
                            _model.InstallScripts());
        }
    }
}