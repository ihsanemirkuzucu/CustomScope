using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ScopeMiddlewareSample.Models.DbContext
{
    public class ConnectionHelper 
    {
        private readonly IConfiguration _conf;

        public ConnectionHelper(IConfiguration conf)
        {
            _conf = conf;
        }

        public IDbConnection GetConnection()
        {
          return new SqlConnection( _conf.GetConnectionString("dbcon"));
        }
    }
}
