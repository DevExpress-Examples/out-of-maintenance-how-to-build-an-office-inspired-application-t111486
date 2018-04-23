Imports System
Imports System.Linq
Imports System.Linq.Expressions
Namespace Common.Utils
    Public Module ExpressionHelper
        <System.Runtime.CompilerServices.Extension> _
        Public Function SetValueAction(Of TPropertyOwner, TProperty)(ByVal getPropertyExpression As Expression(Of Func(Of TPropertyOwner, TProperty))) As Expression(Of Action(Of TPropertyOwner, TProperty))
            Dim body As MemberExpression = CType(getPropertyExpression.Body, MemberExpression)
            Dim thisParameter As ParameterExpression = getPropertyExpression.Parameters.[Single]()
            Dim propertyValueParameter As ParameterExpression = Expression.Parameter(GetType(TProperty), "propertyValue")
            Dim assignPropertyValueExpression As BinaryExpression = Expression.Assign(body, propertyValueParameter)
            Return Expression.Lambda(Of Action(Of TPropertyOwner, TProperty))(assignPropertyValueExpression, thisParameter, propertyValueParameter)
        End Function
        <System.Runtime.CompilerServices.Extension> _
        Public Function ValueEquals(Of TPropertyOwner, TProperty)(ByVal getPropertyExpression As Expression(Of Func(Of TPropertyOwner, TProperty)), ByVal constant As TProperty) As Expression(Of Func(Of TPropertyOwner, Boolean))
            Dim equalExpression As Expression = Expression.Equal(getPropertyExpression.Body, Expression.Constant(constant))
            Return Expression.Lambda(Of Func(Of TPropertyOwner, Boolean))(equalExpression, getPropertyExpression.Parameters.[Single]())
        End Function
    End Module
End Namespace
