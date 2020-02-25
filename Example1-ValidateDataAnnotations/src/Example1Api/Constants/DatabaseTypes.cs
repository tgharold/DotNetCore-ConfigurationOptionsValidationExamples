using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Example1Api.Constants
{
    public static class DatabaseTypes
    {
        public const string PostgreSQL = "PostgreSQL";
        public const string SQLite = "SQLite";
        public const string SQLiteMemory = "SQLiteMemory";
        public const string SQLServer = "SQLServer";

        public static Lazy<IEnumerable<string>> All = new Lazy<IEnumerable<string>>(() =>
        {
            var stringConstants = typeof(DatabaseTypes)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(string))
                .Where(x => x.IsLiteral)
                .Select(x => x.GetValue(null))
                .Cast<string>()
                .OrderBy(x => x)
                .ToList();

            return stringConstants;
        });        
    }
}