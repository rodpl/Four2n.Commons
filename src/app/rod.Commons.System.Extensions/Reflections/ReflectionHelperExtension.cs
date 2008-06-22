using rod.Commons.System.Reflection;

namespace rod.Commons.System.Extensions.Reflections
{
    public static class ReflectionHelperExtension
    {
        /// <summary>
        /// Reflects the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>ReflectionHelper instance</returns>
        public static ReflectionHelper Reflect(this object target)
        {
            return ReflectionHelper.For(target);
        }
    }
}
