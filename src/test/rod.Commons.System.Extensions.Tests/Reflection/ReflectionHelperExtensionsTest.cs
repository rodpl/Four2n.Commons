using NUnit.Framework;
using rod.Commons.System.Reflection;

namespace rod.Commons.System.Extensions.Reflection
{
    [TestFixture]
    public class ReflectionHelperExtensionsTest
    {
        private const string TEXT = "Some string";

        [Test]
        public void ReflectExtension_OnObject_ReturnsReflectionHelper()
        {
            var model = new ReflectionHelperTests.B();
            Assert.AreEqual(model.Reflect().GetType(), typeof(ReflectionHelper));

            model
                .Reflect()
                .Field("_protectedString")
                .SetValue(TEXT);
            Assert.AreEqual(TEXT, model.ProtectedString);
        }
    }
}