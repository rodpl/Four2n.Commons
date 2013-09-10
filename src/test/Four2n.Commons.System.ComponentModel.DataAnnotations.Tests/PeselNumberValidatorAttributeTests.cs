namespace Rod.Commons.System.ComponentModel.DataAnnotations
{
    using NUnit.Framework;

    [TestFixture]
    public class PeselNumberValidatorAttributeTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("02070803628")]
        public void IsValid_ForValidValues_ReturnsTrue(object value)
        {
            var sut = new PeselNumberValidatorAttribute();
            Assert.IsTrue(sut.IsValid(value));
        }

        [Test]
        [TestCase("asdad")]
        [TestCase("1234567890 ")]
        [TestCase("1234567890")]
        [TestCase("02070803628 ")]
        [TestCase(" 02070803628")]
        [TestCase("7680002466")]
        [TestCase("asdad")]
        [TestCase("1234567890 ")]
        public void IsValid_ForValidValues_ReturnsFalse(object value)
        {
            var sut = new PeselNumberValidatorAttribute();
            Assert.IsFalse(sut.IsValid(value));
        }
    }
}