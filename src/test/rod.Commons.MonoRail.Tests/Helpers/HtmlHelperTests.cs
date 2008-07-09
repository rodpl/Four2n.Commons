using MbUnit.Framework;
using rod.Commons.MonoRail.Helpers;

namespace rod.Commons.MonoRail.Tests.Helpers
{
    [TestFixture]
    public class HtmlHelperTests
    {
        private HtmlHelper _model;

        [SetUp]
        public void SetUp()
        {
            _model = new HtmlHelper();
        }

        [RowTest]
        [Row(null, HtmlHelper.NBSP)]
        [Row("", HtmlHelper.NBSP)]
        [Row(" ", HtmlHelper.NBSP)]
        [Row("  ", HtmlHelper.NBSP + HtmlHelper.NBSP)]
        [Row("   ", HtmlHelper.NBSP + HtmlHelper.NBSP + HtmlHelper.NBSP)]
        [Row(" text", " text")]
        [Row("text ", "text ")]
        [Row("text", "text")]
        [Row(1, "1")]
        public void ToNbsp_StringInputs_ReturnsInputOrNbsp(object input, string expected)
        {
            Assert.AreEqual(expected, _model.ToNbsp(input));
        }
    }
}