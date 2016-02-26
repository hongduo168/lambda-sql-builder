/* License: http://www.apache.org/licenses/LICENSE-2.0 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using LambdaSqlBuilder.Builder;
using System.Collections.Concurrent;

namespace LambdaSqlBuilder.Resolver
{
    partial class LambdaResolver
    {
        private Dictionary<ExpressionType, string> _operationDictionary = new Dictionary<ExpressionType, string>()
                                                                              {
                                                                                  { ExpressionType.Equal, "="},
                                                                                  { ExpressionType.NotEqual, "!="},
                                                                                  { ExpressionType.GreaterThan, ">"},
                                                                                  { ExpressionType.LessThan, "<"},
                                                                                  { ExpressionType.GreaterThanOrEqual, ">="},
                                                                                  { ExpressionType.LessThanOrEqual, "<="}
                                                                              };

        private SqlQueryBuilder _builder { get; set; }

        public LambdaResolver(SqlQueryBuilder builder)
        {
            _builder = builder;
        }

        #region helpers
        public static string GetColumnName<T>(Expression<Func<T, object>> selector)
        {
            return GetColumnName(GetMemberExpression(selector.Body));
        }

        private static ConcurrentDictionary<object, string> columnName = new ConcurrentDictionary<object, string>();
        public static string GetColumnName(Expression expression)
        {
            var name = string.Empty;
            if (!columnName.TryGetValue(expression, out name))
            {
                var member = GetMemberExpression(expression);
                var column = member.Member.GetCustomAttributes(false).OfType<SqlLambdaColumnAttribute>().FirstOrDefault();
                if (column != null)
                    name = column.Name;
                else
                    name = member.Member.Name;

                columnName[expression] = name;
            }
            return name;
        }

        public static string GetTableName<T>()
        {
            return GetTableName(typeof(T));
        }

        private static ConcurrentDictionary<object, string> tableName = new ConcurrentDictionary<object, string>();
        public static string GetTableName(Type type)
        {
            var name = string.Empty;
            if (!tableName.TryGetValue(type, out name))
            {
                var column = type.GetCustomAttributes(false).OfType<SqlLambdaTableAttribute>().FirstOrDefault();
                if (column != null)
                    name = column.Name;
                else
                    name = type.Name;

                tableName[type] = name;
            }

            return name;
        }

        private static string GetTableName(MemberExpression expression)
        {
            return GetTableName(expression.Member.DeclaringType);
        }

        private static BinaryExpression GetBinaryExpression(Expression expression)
        {
            if (expression is BinaryExpression)
                return expression as BinaryExpression;

            throw new ArgumentException("Binary expression expected");
        }

        private static MemberExpression GetMemberExpression(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return expression as MemberExpression;
                case ExpressionType.Convert:
                    return GetMemberExpression((expression as UnaryExpression).Operand);
            }

            throw new ArgumentException("Member expression expected");
        }

        #endregion
    }
}
