using NUnit.Framework;
using rod.Commons.MonoRail.Helpers;

namespace rod.Commons.MonoRail.Helpers
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

        [TestCase(null, HtmlHelper.NBSP)]
        [TestCase("", HtmlHelper.NBSP)]
        [TestCase(" ", HtmlHelper.NBSP)]
        [TestCase("  ", HtmlHelper.NBSP + HtmlHelper.NBSP)]
        [TestCase("   ", HtmlHelper.NBSP + HtmlHelper.NBSP + HtmlHelper.NBSP)]
        [TestCase(" text", " text")]
        [TestCase("text ", "text ")]
        [TestCase("text", "text")]
        [TestCase(1, "1")]
        public void ToNbsp_StringInputs_ReturnsInputOrNbsp(object input, string expected)
        {
            Assert.AreEqual(expected, _model.ToNbsp(input));
        }
    }
}