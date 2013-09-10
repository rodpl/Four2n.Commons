namespace Four2n.Commons.System.ComponentModel.DataAnnotations
{
    using NUnit.Framework;

    [TestFixture]
    public class RegonNumberValidatorAttributeTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("732065814")]
        [TestCase("23511332857188")]
        public void IsValid_ForValidValues_ReturnsTrue(object value)
        {
            var sut = new RegonNumberValidatorAttribute();
            Assert.IsTrue(sut.IsValid(value));
        }

        [Test]
        [TestCase("02070803628 ")]
        [TestCase(" 02070803628")]
        [TestCase("7680002465")]
        [TestCase("asdad")]
        [TestCase("1234567890 ")]
        [TestCase("732065815")]
        [TestCase("23511332857180")]
        public void IsValid_ForValidValues_ReturnsFalse(object value)
        {
            var sut = new RegonNumberValidatorAttribute();
            Assert.IsFalse(sut.IsValid(value));
        }
    }
}