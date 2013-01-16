using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace MyCorp.ExtendedAdapterMembershipProviders.Providers
{
    public class DataAccessAdapterFactory: IDataAccessAdapterFactory
    {
        private static Func<IDataAccessAdapter> creator = null;

        public IDataAccessAdapter CreateDataAccessAdapter()
        {
            return creator.Invoke();
        }

        public void Initialize(string connectionStringName)
        {
            if(creator == null) {

                var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName];
                if (connectionString == null)
                    throw new ArgumentException(string.Format("ConnectionString {0} does not exist!", connectionStringName));

                Type dataAccessAdapterType;
                switch (connectionString.ProviderName)
                {
                    case "System.Data.SqlClient":
                        dataAccessAdapterType = Type.GetType("MyCorp.ExtendedAdapterMembershipProviders.SqlServer.DatabaseSpecific.DataAccessAdapter, MyCorp.ExtendedAdapterMembershipProviders.SqlServerDBSpecific");
                        break;
                    case "Devart.Data.MySql":
                    case "MySql.Data.MySqlClient":
                        dataAccessAdapterType = Type.GetType("MyCorp.ExtendedAdapterMembershipProviders.MySql.DatabaseSpecific.DataAccessAdapter, MyCorp.ExtendedAdapterMembershipProviders.MySqlDBSpecific");
                        break;
                    case "System.Data.OleDb":
                        dataAccessAdapterType = Type.GetType("MyCorp.ExtendedAdapterMembershipProviders.MsAccess.DatabaseSpecific.DataAccessAdapter, MyCorp.ExtendedAdapterMembershipProviders.MsAccessDBSpecific");
                        break;
                    case "Oracle.DataAccess.Client":
                        dataAccessAdapterType = Type.GetType("MyCorp.ExtendedAdapterMembershipProviders.Oracle.DatabaseSpecific.DataAccessAdapter, MyCorp.ExtendedAdapterMembershipProviders.OracleDBSpecific");
                        break;
                    default:
                        throw new Exception("Unrecognized provider name on connection string!");
                }

                creator = () => { 
                    return (IDataAccessAdapter)Activator.CreateInstance(dataAccessAdapterType, new object[] { connectionString.ConnectionString });
                };
            }
        }
    }
}
