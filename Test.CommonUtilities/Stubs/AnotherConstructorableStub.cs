using System;

namespace CommonUtilities.Test
{
    public class AnotherConstructorableStub : ConstructorableStub
    {
        public AnotherConstructorableStub(IStubDependency dependency)
            : base(dependency)
        {
            
        }

        public AnotherConstructorableStub(IStubDependency dependency, IConstructorableStub another)
            : base(dependency, another)
        {
            
        }
    }
}
