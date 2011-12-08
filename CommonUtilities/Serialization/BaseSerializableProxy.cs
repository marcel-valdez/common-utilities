// ----------------------------------------------------------------------
// <copyright file="BaseSerializableProxy.cs" company="Route Manager de México">
//     Copyright Route Manager de México(c) 2011. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
namespace CommonUtilities
{
    using System;

    /// <summary>
    /// Base clase for the SerializableProxy
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    public abstract class BaseSerializableProxy<TSource> : ISerializableProxy<TSource>
            where TSource : class
    {
        protected TSource source;

        /// <summary>
        /// Sets the source.
        /// </summary>
        /// <param name="source">The source.</param>
        public virtual void SetSource(TSource source)
        {
            this.source = source;
        }

        /// <summary>
        /// Ases the source.
        /// </summary>
        /// <returns></returns>
        public virtual TSource AsSource()
        {
            return this.source;
        }
    }
}
