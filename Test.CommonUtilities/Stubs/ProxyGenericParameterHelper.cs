using CommonUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CommonUtilities.Test
{
    public class ProxyGenericParameterHelper : BaseSerializableProxy<GenericParameterHelper>
    {
        private static Random rand = new Random(10000);

        public ProxyGenericParameterHelper()
        {
            this.source = new GenericParameterHelper();
            this.source.Data = rand.Next(10000);
        }

        public int Data
        {
            get
            {
                return source.Data;
            }

            set
            {
                source.Data = value;
            }
        }
    }
}
