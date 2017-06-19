using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace SocialNetwork.Core.Infrastructure
{
    public static class SearchExpressionBuilder
    {
        /// <summary>
        /// Builds an expression to search for entities by property provided
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to search</typeparam>
        /// <typeparam name="TProperty">the type of the property</typeparam>
        /// <param name="propertyName">The name of the property</param>
        /// <param name="propertyValue">The value to search</param>
        /// <returns>Expression requested</returns>
        public static Expression<Func<TEntity, bool>> ByProperty<TEntity, TProperty>(string propertyName, TProperty propertyValue)
        {
            if (typeof(TEntity).GetProperty(propertyName) == null)
                throw new ArgumentException();
            ParameterExpression parameterExp = Expression.Parameter(typeof(TEntity));
            MemberExpression parameterValue = Expression.Property(parameterExp, typeof(TEntity).GetProperty(propertyName));
            ConstantExpression valueToSearch = Expression.Constant(propertyValue, typeof(TProperty));
            BinaryExpression equalityExp = Expression.Equal(parameterValue, valueToSearch);
            var predicateExpression = Expression.Lambda<Func<TEntity, bool>>(equalityExp, parameterExp);
            return predicateExpression;
        }
    }
}
