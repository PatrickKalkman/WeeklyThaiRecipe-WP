using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeeklyThaiRecipeManagement.Repositories
{
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.Configuration;

    public class BaseRepository
    {
        protected static IDbConnection OpenConnection()
        {
            IDbConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["WeeklyThaiRecipe"].ConnectionString);
            connection.Open();
            return connection;
        }
    }
}