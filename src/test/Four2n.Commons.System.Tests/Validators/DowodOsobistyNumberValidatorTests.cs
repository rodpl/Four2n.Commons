namespace Four2n.Commons.System.Validators
{
    using NUnit.Framework;

    [TestFixture]
    public class DowodOsobistyNumberValidatorTests
    {
        [Test]
        [TestCase("ABS 123456")]
        [TestCase("ABS123456")]
        [TestCase("ABA300000")]
        public void IsValid_CtorWithValidDowodOsobisty_ReturnsTrue(string number)
        {
            Assert.IsTrue(new DowodOsobistyNumberValidator(number).IsValid());
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("asdad")]
        [TestCase("ABA300001")]
        [TestCase("ABA300000 ")]
        [TestCase("abs 123456")]
        [TestCase("02070803628 ")]
        [TestCase(" 02070803628")]
        [TestCase("7680002465")]
        [TestCase("asdad")]
        [TestCase("1234567890 ")]
        public void IsValid_CtorWithInvalidDowodOsobisty_ReturnsFalse(string number)
        {
            Assert.IsFalse(new DowodOsobistyNumberValidator(number).IsValid());
        }
    }
}