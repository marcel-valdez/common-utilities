// ----------------------------------------------------------------------
// <copyright file="ObjectFasterFlection.cs" company="Route Manager de M�xico">
//     Copyright Route Manager de M�xico(c) 2011. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
namespace CommonUtilities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Reflection;
    using Fasterflect;

    /// <summary>
    /// Class to manage reflection operations of a given type
    /// </summary>
    /// <typeparam name="T">Type being reflected</typeparam>
    public class ObjectFasterFlection
    {
        private IEnumerable<string> getPropertyNames;
        private IEnumerable<string> setPropertyNames;
        private IEnumerable<string> setFieldNames;
        private IEnumerable<string> getFieldNames;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectFasterFlection"/> class.
        /// </summary>
        /// <param name="type">The type to reflect</param>
        public ObjectFasterFlection(Type type)
        {
            this.Initialize(type);
        }

        /// <summary>
        /// Gets the type of the object that is reflected
        /// </summary>
        /// <value>
        /// The type of the object.
        /// </value>
        public Type ObjectType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the setter methods for this type's fields and properties.
        /// </summary>
        /// <value>
        /// The setter methods
        /// </value>
        public IDictionary<string, MemberSetter> Setters
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets he getters methods for this type's fields and properties.
        /// </summary>
        /// <value>
        /// The getter methods.
        /// </value>
        public IDictionary<string, MemberGetter> Getters
        {
            get;
            private set;
        }

        /// <summary>
        /// Copies the entire object by value of fields and/or properties.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <param name="dest">The destination object.</param>
        /// <param name="onlyCopyValueTypes">if set to <c>true</c> [only copy value types].</param>
        /// <param name="doRecursiveCopy">if set to <c>true</c> [do recursive copy].</param>
        public virtual void CopyByValue(object source, object dest, bool onlyCopyValueTypes = false, bool doRecursiveCopy = true, bool includeFields = true)
        {
            Contract.Requires(source != null, "source is null.");
            Contract.Requires(dest != null, "dest is null.");
            Contract.Requires(this.source.IsInstanceOfType(ObjectType), "Reflected type does not match source object type");
            Contract.Requires(this.dest.IsInstanceOfType(ObjectType), "Reflected type does not match dest object type");

            IEnumerable<string> getterNames = this.getPropertyNames.Intersect(this.setPropertyNames);
            if (includeFields)
            {
                getterNames = getterNames.Union(this.getFieldNames.Intersect(this.setFieldNames));
            }

            this.CopyByValue(
                source,
                dest,
                getterNames, onlyCopyValueTypes, doRecursiveCopy);
        }

        /// <summary>
        /// Copies the object by value, only on the designated property or field names
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <param name="dest">The destination object.</param>
        /// <param name="property_names">The property or field names to copy from source to destination.</param>
        /// <param name="onlyCopyValueTypes">if set to <c>true</c> [only copy value types].</param>
        /// <param name="doRecursiveCopy">if set to <c>true</c> [do recursive copy].</param>
        public virtual void CopyByValue(object source, object dest, IEnumerable<string> property_names, bool onlyCopyValueTypes = false, bool doRecursiveCopy = true)
        {
            Contract.Requires(source != null, "source is null.");
            Contract.Requires(dest != null, "dest is null.");
            Contract.Requires(property_names != null, "property_names is null.");
            Contract.Requires(this.source.IsInstanceOfType(ObjectType), "Reflected type does not match source object type");
            Contract.Requires(this.dest.IsInstanceOfType(ObjectType), "Reflected type does not match dest object type");

            foreach (string name in property_names)
            {
                object currentSourceValue = this.Getters[name](source);
                if (!onlyCopyValueTypes || currentSourceValue is ValueType)
                {
                    object currentDestValue = this.Getters[name](dest);
                    if (!(currentSourceValue is ValueType || 
                        currentSourceValue is string ||
                        currentSourceValue is DateTime) &&
                        doRecursiveCopy &&
                        currentSourceValue != null && currentDestValue != null)
                    {
                        var reflect = new ObjectFasterFlection(currentDestValue.GetType());
                        reflect.CopyByValue(currentDestValue, currentSourceValue, onlyCopyValueTypes, doRecursiveCopy);
                    }
                    else
                    {
                        this.Setters[name](dest, currentSourceValue);
                    }
                }
            }
        }

        /// <summary>
        /// Compares the the objects by value.
        /// </summary>
        /// <param name="comparedA">The compared A.</param>
        /// <param name="comparedB">The compared B.</param>
        /// <param name="property_names">The property names to compare, null to compare all.</param>
        /// <param name="onlyCompareValueTypes">if set to <c>true</c> [only compare value types].</param>
        /// <param name="dorecursiveComparison">if set to <c>true</c> [do recursive comparison].</param>
        /// <returns>
        /// true if equal, false otherwise
        /// </returns>
        public virtual bool CompareByValue(object comparedA, object comparedB, bool onlyCompareValueTypes = false, bool dorecursiveComparison = true, IEnumerable<string> property_names = null)
        {
            Contract.Requires(comparedA != null, "source is null.");
            Contract.Requires(comparedB != null, "dest is null.");
            Contract.Requires(this.comparedB.IsInstanceOfType(ObjectType));
            Contract.Requires(this.comparedA.IsInstanceOfType(ObjectType));

            bool equal = false;

            if (comparedA.Equals(comparedB))
            {
                equal = true;
            }
            else
            {
                ICollection<MemberGetter> currentGetters = null;
                if (property_names == null)
                {
                    currentGetters = this.Getters.Values;
                }
                else
                {
                    // SMELL: Va tirar una excepci�n si se provee un nombre de propiedad inexistente
                    currentGetters = property_names.Select(name => this.Getters[name]).ToArray();
                }

                foreach (MemberGetter getter in currentGetters)
                {
                    object valueA = getter(comparedA);
                    object valueB = getter(comparedB);
                    if (valueA != null && valueB != null && dorecursiveComparison && !(valueA is ValueType))
                    {
                        var reflection = new ObjectFasterFlection(valueA.GetType());
                        if (!reflection.CompareByValue(valueA, valueB, onlyCompareValueTypes, dorecursiveComparison))
                        {
                            equal = false;
                            break;
                        }
                    }
                    else if (!(valueA == null && valueB == null) && valueA != valueB)
                    {
                        equal = false;
                        break;
                    }
                }
            }

            return equal;
        }

        /// <summary>
        /// Initializes this instance
        /// </summary>
        /// <param name="object_type">The object_type.</param>
        private void Initialize(Type object_type)
        {
            this.ObjectType = object_type;

            this.getPropertyNames = object_type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(pinfo => pinfo.CanRead)
                .Select(propertyinfo => propertyinfo.Name);
            this.setPropertyNames = object_type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(pinfo => pinfo.CanWrite)
                .Select(propertyinfo => propertyinfo.Name);

            if (object_type.IsInterface)
            {
                this.getFieldNames = new string[] { };
                this.setFieldNames = new string[] { };
            }
            else
            {
                this.getFieldNames = object_type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetField)
                    .Select((fieldInfo) => fieldInfo.Name);
                this.setFieldNames = object_type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetField)
                    .Select((fieldInfo) => fieldInfo.Name);
            }

            this.Setters = new Dictionary<string, MemberSetter>();
            this.Getters = new Dictionary<string, MemberGetter>();

            foreach (string name in this.getPropertyNames)
            {
                if (object_type.IsInterface || object_type.GetProperty(name).GetGetMethod().IsAbstract)
                {
                    string localname = name;
                    MemberGetter getter = (arg) => arg.GetPropertyValue(localname);
                    this.Getters.Add(name, getter);
                }
                else
                {
                    this.Getters.Add(name, object_type.DelegateForGetPropertyValue(name));
                }
            }

            foreach (string name in this.setPropertyNames)
            {
                if (object_type.IsInterface || object_type.GetProperty(name).GetSetMethod().IsAbstract)
                {
                    string localname = name;
                    MemberSetter setter = (arg, value) => arg.SetPropertyValue(localname, value);
                    this.Setters.Add(name, setter);
                }
                else
                {
                    this.Setters.Add(name, object_type.DelegateForSetPropertyValue(name));
                }
            }

            foreach (string name in this.setFieldNames)
            {
                this.Setters.Add(name, object_type.DelegateForSetFieldValue(name));
            }

            foreach (string name in this.getFieldNames)
            {
                this.Getters.Add(name, object_type.DelegateForGetFieldValue(name));
            }
        }
    }
}
