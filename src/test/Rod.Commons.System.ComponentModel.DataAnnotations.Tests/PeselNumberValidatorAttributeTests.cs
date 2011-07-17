namespace Rod.Commons.System.ComponentModel.DataAnnotations
{
    using NUnit.Framework;

    [TestFixture]
    public class PeselNumberValidatorAttributeTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("7680002466")]
        public void IsValid_ForValidValues_ReturnsTrue(object value)
        {
            var sut = new PeselNumberValidatorAttribute();
            Assert.IsTrue(sut.IsValid(value));
        }

        [Test]
        [TestCase("asdad")]
        [TestCase("1234567890 ")]
        public void IsValid_ForValidValues_ReturnsFalse(object value)
        {
            var sut = new PeselNumberValidatorAttribute();
            Assert.IsFalse(sut.IsValid(value));
        }
    }
}