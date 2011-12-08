using System;

namespace CommonUtilities.Test
{
    public abstract class BaseGeneric<T> : IGeneric<T>
            where T : class
    {
        public BaseGeneric(T arg)
        {
            this.Property = arg;
        }
        abstract public T Property
        {
            get;
            set;
        }
    }
}
