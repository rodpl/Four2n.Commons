// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionHelper.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the ReflectionHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.System.Reflection
{
    using global::System;
    using global::System.Reflection;

    /// <summary>
    /// Helper for reflection actions.
    /// </summary>
    public class ReflectionHelper
    {
        /// <summary>
        /// Instance which will be modified.
        /// </summary>
        private readonly object target;

        /// <summary>
        /// Currently used code memeber during reflection.
        /// </summary>
        private MemberInfo currentMemberInfo;

        /// <summary>
        /// Initializes a new instance of the ReflectionHelper class.
        /// </summary>
        /// <param name="target">Instance of reflected object.</param>
        /// <exception cref="ArgumentNullException">If <c>target</c> is null.</exception>
        private ReflectionHelper(object target)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            this.target = target;
        }

        /// <summary>
        /// Finds the field recursive by name.
        /// </summary>
        /// <param name="type">The class type.</param>
        /// <param name="name">The field name.</param>
        /// <returns>FieldInfo or null if there is no field with such name.</returns>
        public static FieldInfo FindField(Type type, string name)
        {
            if (type == null || type == typeof(object))
            {
                // the full inheritance chain has been walked and we could
                // not find the Field
                return null;
            }

            return type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly) ??
                    FindField(type.BaseType, name);
        }

        /// <summary>
        /// Finds the property recursive.
        /// </summary>
        /// <param name="type">The class type.</param>
        /// <param name="name">The property name.</param>
        /// <returns>PropertyInfo or null if there is no field with such name.</returns>
        public static PropertyInfo FindProperty(Type type, string name)
        {
            if (type == null || type == typeof(object))
            {
                // the full inheritance chain has been walked and we could
                // not find the Field
                return null;
            }

            return type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly) ??
                    FindProperty(type.BaseType, name);
        }

        /// <summary>
        /// Factory method which defines target instance.
        /// </summary>
        /// <param name="targetInstance">The target instance.</param>
        /// <returns> Instace of <see cref="ReflectionHelper"/>.</returns>
        public static ReflectionHelper For(object targetInstance)
        {
            return new ReflectionHelper(targetInstance);
        }

        /// <summary>
        /// Select field with the specified field name for modification.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns> ReflectionHelper for chaining </returns>
        /// <exception cref="NullReferenceException">If there is no such field.</exception>
        public ReflectionHelper Field(string fieldName)
        {
            this.currentMemberInfo = FindField(this.target.GetType(), fieldName);
            if (this.currentMemberInfo == null)
            {
                throw new NullReferenceException(
                        String.Format("Sorry, There is no such field = {0} in class type {1}", fieldName, this.target.GetType().FullName));
            }

            return this;
        }

        /// <summary>
        /// Gets the value of last selected member.
        /// </summary>
        /// <returns>Value of last selected member.</returns>
        /// <exception cref="NotSupportedException">Unsupported type of modified memeber.</exception>
        public object GetValue()
        {
            var type = this.currentMemberInfo.GetType();
            if (type.IsSubclassOf(typeof(PropertyInfo)))
            {
                return ((PropertyInfo)this.currentMemberInfo).GetValue(this.target, null);
            }

            if (type.IsSubclassOf(typeof(FieldInfo)))
            {
                return ((FieldInfo)this.currentMemberInfo).GetValue(this.target);
            }

            throw new NotSupportedException(string.Format("Unsupported type of modified memeber. {0}", type.Name));
        }

        /// <summary>
        /// Properties the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Self for chaining.</returns>
        /// <exception cref="NullReferenceException" />
        public ReflectionHelper Property(string propertyName)
        {
            this.currentMemberInfo = FindProperty(this.target.GetType(), propertyName);
            if (this.currentMemberInfo == null)
            {
                throw new NullReferenceException(
                        String.Format("Sorry, There is no such property = {0} in class type {1}", propertyName, this.target.GetType().FullName));
            }

            return this;
        }

        /// <summary>
        /// Returns this instance.
        /// </summary>
        /// <typeparam name="T">Type for casting.</typeparam>
        /// <returns>Modified instance.</returns>
        public T Return<T>()
        {
            return (T)this.target;
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Self for chaining.</returns>
        /// <exception cref="NotSupportedException">Unsupported type of modified memeber.</exception>
        /// <exception cref="NullReferenceException">Modified member is null.</exception>
        public ReflectionHelper SetValue(object value)
        {
            if (this.currentMemberInfo == null)
            {
                throw new NullReferenceException("Modified member is null.");
            }

            var type = this.currentMemberInfo.GetType();
            if (type.IsSubclassOf(typeof(PropertyInfo)))
            {
                ((PropertyInfo)this.currentMemberInfo).SetValue(this.target, value, null);
            }
            else if (type.IsSubclassOf(typeof(FieldInfo)))
            {
                ((FieldInfo)this.currentMemberInfo).SetValue(this.target, value);
            }
            else
            {
                throw new NotSupportedException(string.Format("Unsupported type of modified memeber. {0}", type.Name));
            }

            return this;
        }
    }
}