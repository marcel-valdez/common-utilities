// ----------------------------------------------------------------------
// <copyright file="MethodInvocation.cs" company="Route Manager de México">
//     Copyright Route Manager de México(c) 2011. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
namespace CommonUtilities
{
    using System;

    /// <summary>
    /// Clase para invocar un método creado de manera 'tardía'
    /// </summary>
    public class MethodInvocation
    {
        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>
        /// The method.
        /// </value>
        private LateBoundMethod method 
        { 
            get;
            set;
        }

        /// <summary>
        /// Sets the method.
        /// </summary>
        /// <value>The Delegate method.</value>
        public Delegate Method
        {
            set
            {
                method = DelegateFactory.Create(value.Method);
            }
        }

        /// <summary>
        /// Gets or sets the instance on which the Method will be called.
        /// </summary>
        /// <value>The instance.</value>
        public object Instance 
        { 
            get;
            set; 
        }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public object[] Parameters 
        { 
            get;
            set;
        }


        /// <summary>
        /// Invokes the method as a callback, not using MethodInfo.
        /// </summary>
        /// <returns>The result as an object.</returns>
        public object Invoke()
        {
            return method(Instance, Parameters);
        }

        /// <summary>
        /// Gets a copy of this instance
        /// </summary>
        /// <returns>A MethodInvocation</returns>
        public MethodInvocation GetCopy()
        {
            return new MethodInvocation 
            { 
                method = this.method, 
                Instance = this.Instance, 
                Parameters = this.Parameters 
            };
        }
    }
}
