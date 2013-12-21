using System;

namespace FeatureToggle
{
    internal static class ObjectValidationExtensions
    {
        public static void CheckNull(this object target, string parameterName)
        {
            if (target == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static void CheckNull<T>(this object target, Func<T> validator) where T : Exception
        {
            if (target == null)
            {
                throw validator();
            }
        }

        public static void CheckNullOrEmpty<T>(this string target, Func<T> invoker) where T : Exception
        {
            if (string.IsNullOrEmpty(target))
            {
                throw invoker();
            }
        }

        public static void WithNotNull<T>(this T target, Action<T> invoker) where T : class
        {
            if (target == null)
            {
                return;
            }

            invoker(target);
        }
    }
}
