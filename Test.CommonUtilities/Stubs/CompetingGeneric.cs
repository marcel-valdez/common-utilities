using System;

namespace CommonUtilities.Test
{
    public class CompetingGeneric<T> : IGeneric<T>
            where T : class
    {
        public T Property
        {
            get;

            set;
        }
    }
}
