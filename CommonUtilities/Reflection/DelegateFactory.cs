﻿// ----------------------------------------------------------------------
// <copyright file="DelegateFactory.cs" company="Route Manager de México">
//     Copyright Route Manager de México(c) 2011. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
namespace CommonUtilities
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public delegate object LateBoundMethod(object target, object[] arguments);

    public static class DelegateFactory
    {
        public static LateBoundMethod Create(MethodInfo method)
        {
            ParameterExpression instanceParameter = Expression.Parameter(typeof(object), "target");
            ParameterExpression argumentsParameter = Expression.Parameter(typeof(object[]), "arguments");

            MethodCallExpression call = Expression.Call(
              Expression.Convert(instanceParameter, method.DeclaringType),
              method,
              CreateParameterExpressions(method, argumentsParameter));

            Expression<LateBoundMethod> lambda = Expression.Lambda<LateBoundMethod>(
              Expression.Convert(call, typeof(object)),
              instanceParameter,
              argumentsParameter);

            return lambda.Compile();
        }

        private static Expression[] CreateParameterExpressions(MethodInfo method, Expression argumentsParameter)
        {
            return method.GetParameters().Select((parameter, index) =>
              Expression.Convert(
                Expression.ArrayIndex(argumentsParameter, Expression.Constant(index)), parameter.ParameterType)).ToArray();
        }
    }

}
