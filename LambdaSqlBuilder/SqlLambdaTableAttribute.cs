using System;

namespace LambdaSqlBuilder
{
    /// <summary>
    /// Configures the name of the db table related to this entity. If the attribute is not specified, the class name is used instead.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SqlLambdaTableAttribute : Attribute
    {
        public string Name { get; set; }

        public SqlLambdaTableAttribute(string name = "")
        {
            this.Name = name;
        }
    }
}
