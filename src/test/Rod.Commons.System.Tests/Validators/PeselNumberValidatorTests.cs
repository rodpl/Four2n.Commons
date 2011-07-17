namespace Rod.Commons.System.Validators
{
    using NUnit.Framework;

    [TestFixture]
    public class PeselNumberValidatorTests
    {
        [Test]
        [TestCase("02070803628")]
        public void IsValid_CtorWithValidPesel_ReturnsTrue(string number)
        {
            Assert.IsTrue(new PeselNumberValidator(number).IsValid());
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("02070803628 ")]
        [TestCase(" 02070803628")]
        [TestCase("7680002466")]
        [TestCase("asdad")]
        [TestCase("1234567890 ")]
        public void IsValid_CtorWithInvalidPesel_ReturnsFalse(string number)
        {
            Assert.IsFalse(new PeselNumberValidator(number).IsValid());
        }
    }
}
