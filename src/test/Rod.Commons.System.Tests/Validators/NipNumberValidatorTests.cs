namespace Rod.Commons.System.Validators
{
    using NUnit.Framework;

    [TestFixture]
    public class NipNumberValidatorTests
    {
        [Test]
        [TestCase("7680002466")]
        public void IsValid_CtorWithValidNip_ReturnsTrue(string number)
        {
            Assert.IsTrue(new NipNumberValidator(number).IsValid());
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("02070803628 ")]
        [TestCase(" 02070803628")]
        [TestCase("7680002465")]
        [TestCase("asdad")]
        [TestCase("1234567890 ")]
        public void IsValid_CtorWithInvalidNip_ReturnsFalse(string number)
        {
            Assert.IsFalse(new NipNumberValidator(number).IsValid());
        }
    }
}