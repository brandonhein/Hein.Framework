using System.Configuration;
using System.Data.SqlClient;

namespace Hein.Framework.Repository
{
    public class RepositoryContextBase
    {
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings[0].ToString());
        }
    }
}
