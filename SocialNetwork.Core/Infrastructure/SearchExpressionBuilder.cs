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
