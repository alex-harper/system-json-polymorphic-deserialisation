using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace PolymorphicDeserialisationDemo
{
    internal static class PropertyHelper
    {
        public static string GetMemberName<T>(this Expression<T> expression) =>
        expression.Body switch
        {
            MemberExpression m =>
                m.Member.Name,
            UnaryExpression u when u.Operand is MemberExpression m =>
                m.Member.Name,
            _ =>
                throw new NotImplementedException(expression.GetType().ToString())
        };



        /// <summary>
        /// Returns the JsonPropertyName if available, otherwsie defaults the member name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetMemberJsonName<T>(this Expression<T> expression)
        {
            switch (expression.Body)
            {
                case MemberExpression m:
                    {
                        var jsonPropNameAttribute = m.Member.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false).FirstOrDefault();
                        if (jsonPropNameAttribute != null) return ((JsonPropertyNameAttribute)jsonPropNameAttribute).Name;
                        return m.Member.Name;
                    }
                case UnaryExpression u when u.Operand is MemberExpression m:
                    {
                        var jsonPropNameAttribute = m.Member.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false).FirstOrDefault();
                        if (jsonPropNameAttribute != null) return ((JsonPropertyNameAttribute)jsonPropNameAttribute).Name;
                        return m.Member.Name;
                    }
                default:
                    throw new NotImplementedException(expression.GetType().ToString());
            }
        }
    }
}
