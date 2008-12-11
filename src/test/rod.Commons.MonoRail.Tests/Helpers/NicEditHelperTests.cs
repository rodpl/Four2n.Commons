namespace Rod.Commons.MonoRail.Helpers
{
    using NUnit.Framework;

    [TestFixture]
    public class NicEditHelperTests : HelperTestFixture<NicEditHelper>
    {
        private NicEditHelper _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new NicEditHelper();
            InitializeSut(_sut);
        }

        [Test]
        public void CreateHtmlTest_ReturnsHtmlInDivTags()
        {
            var html = _sut.CreateHtml("instance");
            Assert.That(html, Text.StartsWith("<div>\n"));
            Assert.That(html, Text.EndsWith("</div>\n"));
        }
    }
}