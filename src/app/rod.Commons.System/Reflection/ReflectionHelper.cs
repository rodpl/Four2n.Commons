using System;
using System.Reflection;

namespace rod.Commons.System.Reflection
{
    public class ReflectionHelper
    {
        private readonly object _target;
        private FieldInfo _fieldInfo;
        private CodeElementType _lastSelectedElementType;
        private PropertyInfo _propertyInfo;

        /// <summary>
        /// Initializes a new instance of the ReflectionHelper class.
        /// </summary>
        /// <param name="target"></param>
        private ReflectionHelper(object target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            _target = target;
        }

        /// <summary>
        /// Factory method which defines target instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static ReflectionHelper For(object instance)
        {
            return new ReflectionHelper(instance);
        }

        public ReflectionHelper Field(string fieldName)
        {
            _fieldInfo = FindField(_target.GetType(), fieldName);
            if (_fieldInfo == null)
                throw new NullReferenceException(String.Format("Sorry, There is no such field = {0} in class type {1}", fieldName,
                                                               _target.GetType().FullName));
            _lastSelectedElementType = CodeElementType.Field;
            return this;
        }

        public ReflectionHelper Property(string propertyName)
        {
            _propertyInfo = FindProperty(_target.GetType(), propertyName);
            if (_propertyInfo == null)
                throw new NullReferenceException(String.Format("Sorry, There is no such property = {0} in class type {1}",
                                                               propertyName, _target.GetType().FullName));
            _lastSelectedElementType = CodeElementType.Property;
            return this;
        }

        public ReflectionHelper SetValue(object value)
        {
            switch (_lastSelectedElementType)
            {
                case CodeElementType.Property:
                    _propertyInfo.SetValue(_target, value, null);
                    break;
                case CodeElementType.Field:
                    _fieldInfo.SetValue(_target, value);
                    break;
            }
            return this;
        }

        public object GetValue()
        {
            switch (_lastSelectedElementType)
            {
                case CodeElementType.Property:
                    return _propertyInfo.GetValue(_target, null);
                case CodeElementType.Field:
                    return _fieldInfo.GetValue(_target);
            }
            return null;
        }

        public T Return<T>()
        {
            return (T)_target;
        }

        /// <summary>
        /// Finds the field recursive.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static FieldInfo FindField(Type type, string name)
        {
            if (type == null || type == typeof (object))
            {
                // the full inheritance chain has been walked and we could
                // not find the Field
                return null;
            }

            FieldInfo field =
                type.GetField(name,
                              BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly) ??
                FindField(type.BaseType, name);

            return field;
        }

        /// <summary>
        /// Finds the property recursive.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static PropertyInfo FindProperty(Type type, string name)
        {
            if (type == null || type == typeof (object))
            {
                // the full inheritance chain has been walked and we could
                // not find the Field
                return null;
            }

            PropertyInfo property =
                type.GetProperty(name,
                                 BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly) ??
                FindProperty(type.BaseType, name);

            return property;
        }

        #region Nested type: CodeElementType

        internal enum CodeElementType
        {
            Property,
            Field
        }

        #endregion
    }
}