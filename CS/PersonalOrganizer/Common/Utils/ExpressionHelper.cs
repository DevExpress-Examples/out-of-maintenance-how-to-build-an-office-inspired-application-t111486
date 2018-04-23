using System;
using System.Linq;
using System.Linq.Expressions;

namespace PersonalOrganizer.Common.Utils {
    public static class ExpressionHelper {
        public static Expression<Action<TPropertyOwner, TProperty>> SetValueAction<TPropertyOwner, TProperty>(this Expression<Func<TPropertyOwner, TProperty>> getPropertyExpression) {
            MemberExpression body = (MemberExpression)getPropertyExpression.Body;
            ParameterExpression thisParameter = getPropertyExpression.Parameters.Single();
            ParameterExpression propertyValueParameter = Expression.Parameter(typeof(TProperty), "propertyValue");
            BinaryExpression assignPropertyValueExpression = Expression.Assign(body, propertyValueParameter);
            return Expression.Lambda<Action<TPropertyOwner, TProperty>>(assignPropertyValueExpression, thisParameter, propertyValueParameter);
        }
        public static Expression<Func<TPropertyOwner, bool>> ValueEquals<TPropertyOwner, TProperty>(this Expression<Func<TPropertyOwner, TProperty>> getPropertyExpression, TProperty constant) {
            Expression equalExpression = Expression.Equal(getPropertyExpression.Body, Expression.Constant(constant));
            return Expression.Lambda<Func<TPropertyOwner, bool>>(equalExpression, getPropertyExpression.Parameters.Single());
        }
    }
}
