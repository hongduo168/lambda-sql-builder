﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LambdaSqlBuilder.Tests.Entities;
using LambdaSqlBuilder.Tests.Infrastructure;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambdaSqlBuilder.Tests
{
    [TestClass()]
    public class LinqSqlQueryTests : TestBase
    {
        /// <summary>
        /// Find the product with name Tofu
        /// </summary>
        [TestMethod()]
        public void FindByFieldValue()
        {
            const string productName = "Tofu";

            var query = from product in new SqlLambda<Product>()
                        where product.ProductName == productName
                        
                        select product;

            var results = Connection.Query<Product>(query.QueryString, query.QueryParameters).ToList();

            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(productName, results.First().ProductName);
        }

        /// <summary>
        /// Get product Tofu by its Id and select the value of the Unit Price only
        /// </summary>
        [TestMethod()]
        public void SelectField()
        {
            const int productId = 14;

            var query = from product in new SqlLambda<Product>()
                        where product.ProductId == productId
                        select product.UnitPrice;

            var results = Connection.Query<decimal>(query.QueryString, query.QueryParameters).ToList();

            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(23.25, results.First());
        }

        /// <summary>
        /// Find all products for the category Beverages and the Reorder Level 25
        /// </summary>
        [TestMethod()]
        public void FindByJoinedEntityValue()
        {
            const int reorderLevel = 25;
            const string categoryName = "Beverages";
            const int categoryId = 1;

            var query = from product in new SqlLambda<Product>()
                        join category in new SqlLambda<Category>()
                        on product.CategoryId equals category.CategoryId
                        where product.ReorderLevel == reorderLevel && category.CategoryName == categoryName
                        select product;

            var results = Connection.Query<Product>(query.QueryString, query.QueryParameters).ToList();

            Assert.AreEqual(3, results.Count);
            Assert.IsTrue(results.All(p => p.CategoryId == categoryId));
        }

        /// <summary>
        /// Get categories sorted by name
        /// </summary>
        [TestMethod()]
        public void OrderEntitiesByField()
        {
            var query = from category in new SqlLambda<Category>()
                        orderby category.CategoryName
                        select category;

            var results = Connection.Query<Category>(query.QueryString, query.QueryParameters).ToList();

            for (int i = 1; i < results.Count; ++i)
            {
                Assert.IsTrue(String.CompareOrdinal(results[i - 1].CategoryName, results[i].CategoryName) <= 0);
            }
        }

        /// <summary>
        /// Get categories sorted by name descending
        /// </summary>
        [TestMethod()]
        public void OrderEntitiesByFieldDescending()
        {
            var query = from category in new SqlLambda<Category>()
                        orderby category.CategoryName descending
                        select category;

            var results = Connection.Query<Category>(query.QueryString, query.QueryParameters).ToList();

            for (int i = 1; i < results.Count; ++i)
            {
                Assert.IsTrue(String.CompareOrdinal(results[i - 1].CategoryName, results[i].CategoryName) >= 0);
            }
        }

        /// <summary>
        /// Select number of products for individual Reorder Levels
        /// </summary>
        [TestMethod()]
        public void SelectGroupedCounts()
        {
            var groupSizes = new[] { 24, 8, 7, 10, 8, 12, 8 };

            var query = from product in new SqlLambda<Product>()
                        group product by product.ReorderLevel;

            query.SelectCount(p => p.ProductId);                                               

            var results = Connection.Query<int>(query.QueryString, query.QueryParameters).ToList();

            Assert.AreEqual(groupSizes.Length, results.Count);

            for (int i = 0; i < groupSizes.Length; ++i)
            {
                Assert.AreEqual(groupSizes[i], results[i]);
            }
        }
    }
}
