using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace MyCorp.ExtendedAdapterMembershipProviders
{
    public interface IDataAccessAdapterFactory
    {
        void Initialize(string connectionStringName);

        IDataAccessAdapter CreateDataAccessAdapter();
    }
}
