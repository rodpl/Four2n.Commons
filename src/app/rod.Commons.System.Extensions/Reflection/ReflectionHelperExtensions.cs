using Rod.Commons.System.Reflection;

namespace Rod.Commons.System.Extensions.Reflection
{
    using System.Reflection;

    public static class ReflectionHelperExtensions
    {
        /// <summary>
        /// Reflects the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns><see cref="ReflectionHelper"/> instance</returns>
        public static ReflectionHelper Reflect(this object target)
        {
            return ReflectionHelper.For(target);
        }
    }
}