using System.Configuration;
using System.Data.SqlClient;

namespace Hein.Framework.Repository.SQL
{
    public abstract class SqlCommandBase : ISqlCommand
    {
        public string Database { get { return new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings[0].ToString()).InitialCatalog; } }
        public abstract string SQL { get; }
    }
}
