// ----------------------------------------------------------------------
// <copyright file="ObjectFasterFlectionT.cs" company="Route Manager de México">
//     Copyright Route Manager de México(c) 2011. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
namespace CommonUtilities
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Fasterflect;

    /// <summary>
    /// Class to manage reflection operations of a given type
    /// </summary>
    /// <typeparam name="T">Type being reflected</typeparam>
    public class ObjectFasterFlection<T>
    {
        private readonly ObjectFasterFlection wrapped = new ObjectFasterFlection(typeof(T));

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectFasterFlection&lt;T&gt;"/> class.
        /// </summary>
        public ObjectFasterFlection()
        {
        }

        /// <summary>
        /// Gets the setters.
        /// </summary>
        public IDictionary<string, MemberSetter> Setters
        {
            get
            {
                Contract.Ensures(Contract.Result<IDictionary<string, MemberSetter>>() != null);
                return this.wrapped.Setters;
            }
        }

        /// <summary>
        /// Gets the member getters.
        /// </summary>
        public IDictionary<string, MemberGetter> Getters
        {
            get
            {
                Contract.Ensures(Contract.Result<IDictionary<string, MemberGetter>>() != null);
                return this.wrapped.Getters;
            }
        }

        /// <summary>
        /// Compares both objects by value.
        /// </summary>
        /// <param name="comparedA">The compared A.</param>
        /// <param name="comparedB">The compared B.</param>
        /// <param name="onlyCompareValueTypes">if set to <c>true</c> [only compare value types].</param>
        /// <param name="doRecursiveComparison">if set to <c>true</c> [do recursive comparison].</param>
        /// <param name="propertyNames">The property names to compare</param>
        /// <returns>
        /// true if equal, false otherwise
        /// </returns>
        public bool CompareByValue(T comparedA, T comparedB, bool onlyCompareValueTypes = false, bool doRecursiveComparison = true, IEnumerable<string> propertyNames = null)
        {
            Contract.Requires(comparedA != null, "comparedA is null.");
            Contract.Requires(comparedB != null, "comparedB is null.");

            return this.wrapped.CompareByValue(comparedA, comparedB, onlyCompareValueTypes, doRecursiveComparison, propertyNames);
        }

        /// <summary>
        /// Copies the object by value, only on the designated property or field names
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <param name="dest">The destination object.</param>
        /// <param name="propertyNames">The property or field names to copy from source to destination.</param>
        /// <param name="onlyCopyValueTypes">if set to <c>true</c> [only copy value types].</param>
        /// <param name="doRecursiveCopy">if set to <c>true</c> [do recursive copy].</param>
        public void CopyByValue(T source, T dest, IEnumerable<string> propertyNames, bool onlyCopyValueTypes = false, bool doRecursiveCopy = true)
        {
            Contract.Requires(source != null, "source is null.");
            Contract.Requires(dest != null, "dest is null.");
            Contract.Requires(propertyNames != null, "property_names is null.");
            this.wrapped.CopyByValue(source, dest, propertyNames, onlyCopyValueTypes, doRecursiveCopy);
        }

        /// <summary>
        /// Copies the entire object by value of fields and properties.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <param name="dest">The destination object.</param>
        /// <param name="onlyCopyValueTypes">if set to <c>true</c> [only copy value types].</param>
        /// <param name="doRecursiveCopy">if set to <c>true</c> [do recursive copy].</param>
        public void CopyByValue(T source, T dest, bool onlyCopyValueTypes = false, bool doRecursiveCopy = true, bool includeFields = true)
        {
            Contract.Requires(source != null, "source is null.");
            Contract.Requires(dest != null, "dest is null.");

            this.wrapped.CopyByValue(source, dest, onlyCopyValueTypes, doRecursiveCopy, includeFields);
        }

        /// <summary>
        /// Ases the object faster flection.
        /// </summary>
        /// <returns>The underlying ObjectFasterFlection</returns>
        public ObjectFasterFlection AsObjectFasterFlection()
        {
            Contract.Ensures(Contract.Result<ObjectFasterFlection>() != null);

            return this.wrapped;
        }
    }
}
