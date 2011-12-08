// ----------------------------------------------------------------------
// <copyright file="ISerializableProxy.cs" company="Route Manager de México">
//     Copyright Route Manager de México(c) 2011. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
namespace CommonUtilities
{
    using System;

    /// <summary>
    /// Proxy serializable para el tipo <TSource>Fuente</TSource>
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    public interface ISerializableProxy<TSource>
    {
        /// <summary>
        /// Sets the source.
        /// </summary>
        /// <param name="source">The source.</param>
        void SetSource(TSource source);

        /// <summary>
        /// Returns this instance as a <TSource>instance</TSource>
        /// </summary>
        /// <returns>The <TSource>instance</TSource></returns>
        TSource AsSource();
    }
}