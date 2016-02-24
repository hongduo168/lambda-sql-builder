using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace LambdaSqlBuilder.Tests.Infrastructure
{
    public abstract class TestBase
    {
        protected System.Data.SqlClient.SqlConnection Connection;

        public void Init()
        {
            Bootstrap.Initialize();

            Connection = new SqlConnection(Bootstrap.CONNECTION_STRING);
            Connection.Open();
        }

        public void TearDown()
        {
            Connection.Close();
        }
    }
}
