namespace Four2n.Commons.System.Validators
{
    using NUnit.Framework;

    [TestFixture]
    public class RegonNumberValidatorTests
    {
        [Test]
        [TestCase("732065814")]
        [TestCase("23511332857188")]
        public void IsValid_CtorWithValidRegon_ReturnsTrue(string number)
        {
            Assert.IsTrue(new RegonNumberValidator(number).IsValid());
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("02070803628 ")]
        [TestCase(" 02070803628")]
        [TestCase("7680002465")]
        [TestCase("asdad")]
        [TestCase("1234567890 ")]
        [TestCase("732065815")]
        [TestCase("23511332857180")]
        public void IsValid_CtorWithInvalidRegon_ReturnsFalse(string number)
        {
            Assert.IsFalse(new RegonNumberValidator(number).IsValid());
        }
    }
}