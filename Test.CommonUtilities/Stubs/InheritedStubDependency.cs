using System;

namespace CommonUtilities.Test
{
    public class InheritedStubDependency : ConcreteStubDependency
    {
        public InheritedStubDependency(string data)
            : base(data)
        {
        }

        public InheritedStubDependency()
        {
            
        }

        public InheritedStubDependency(string data, int moredata, string postfix)
            : base(data, moredata, postfix)
        {   
        }
    }
}