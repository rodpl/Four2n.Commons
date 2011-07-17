namespace Rod.Commons.System.ComponentModel.DataAnnotations
{
    using NUnit.Framework;

    [TestFixture]
    public class DowodOsobistyNumberValidatorAttributeTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("ABS 123456")]
        [TestCase("ABS123456")]
        [TestCase("ABA300000")]
        public void IsValid_ForValidValues_ReturnsTrue(object value)
        {
            var sut = new DowodOsobistyNumberValidatorAttribute();
            Assert.IsTrue(sut.IsValid(value));
        }

        [Test]
        [TestCase("asdad")]
        [TestCase("ABA300001")]
        [TestCase("ABA300000 ")]
        [TestCase("abs 123456")]
        public void IsValid_ForValidValues_ReturnsFalse(object value)
        {
            var sut = new DowodOsobistyNumberValidatorAttribute();
            Assert.IsFalse(sut.IsValid(value));
        }
    }
}