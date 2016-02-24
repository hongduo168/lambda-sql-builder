using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LambdaSqlBuilder.Tests.Entities;
using LambdaSqlBuilder.Tests.Infrastructure;
using LambdaSqlBuilder.ValueObjects;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace LambdaSqlBuilder.Tests
{
    [TestClass()]
    public class ExtraTests : TestBase
    {
        /// <summary>
        /// Access the underlying SqlBuilder to directly specify the selection and the having condition
        /// </summary>
        [TestMethod()]
        public void DirectSqlBuilderAccess()
        {
            var query = from product in new SqlLambda<Product>()
                        where product.ReorderLevel > 10
                        group product by product.ReorderLevel;

            query.SqlBuilder.SelectionList.Add("MAX([Unit Price]) AS MaxPrice");
            query.SqlBuilder.HavingConditions.Add("MAX([Unit Price]) < @PriceParam");
            query.SqlBuilder.Parameters.Add("PriceParam", 25M);

            var result = Connection.Query<decimal>(query.QueryString, query.QueryParameters).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(24, result.First());
        }

        /// <summary>
        /// Load all product names without using the Dapper only with the standard Sql Command and the Data Reader
        /// </summary>
        [TestMethod()]
        public void DapperLessExecution()
        {
            var query = from product in new SqlLambda<Product>()
                        orderby product.ProductName
                        select product.ProductName;

            var selectCommand = new SqlCommand(query.QueryString, Connection);
            foreach (var param in query.QueryParameters)
                selectCommand.Parameters.AddWithValue(param.Key, param.Value);

            var result = selectCommand.ExecuteReader();

            int count = 0;
            while(result.Read())
            {
                ++count;
            }
            Assert.AreEqual(77, count);
        }

        /// <summary>
        /// Change the adapter to Sql Server 2008 and verify whether the pagination SQL string contains the specific keyword ROW_NUMBER
        /// </summary>
        [TestMethod()]
        public void ChangeSqlAdapter()
        {
            SqlLambdaBase.SetAdapter(SqlAdapter.SqlServer2008);

            var query = from product in new SqlLambda<Product>()
                        orderby product.ProductName
                        select product;

            var queryString = query.QueryStringPage(10, 1);

            SqlLambdaBase.SetAdapter(SqlAdapter.SqlServer2012);

            Assert.IsTrue(queryString.Contains("ROW_NUMBER"));
        }
    }
}
