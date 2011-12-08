using System;

namespace CommonUtilities.Test
{
    public interface IGeneric<T>
            where T : class
    {
        T Property
        {
            get;
            set;
        }
    }
}
