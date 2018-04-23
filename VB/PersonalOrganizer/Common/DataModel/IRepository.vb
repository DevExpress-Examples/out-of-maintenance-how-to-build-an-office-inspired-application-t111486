Imports System
Imports System.Linq
Imports System.Linq.Expressions
Namespace Common.DataModel
    Public Interface IRepository(Of TEntity As Class, TPrimaryKey)
        Inherits IReadOnlyRepository(Of TEntity)
        Function Find(ByVal key As TPrimaryKey) As TEntity
        Sub Remove(ByVal enity As TEntity)
        Function Create() As TEntity
        Function Reload(ByVal entity As TEntity) As TEntity
        ReadOnly Property GetPrimaryKeyExpression As Expression(Of Func(Of TEntity, TPrimaryKey))
        Function GetPrimaryKey(ByVal entity As TEntity) As TPrimaryKey
        Sub SetPrimaryKey(ByVal entity As TEntity, ByVal key As TPrimaryKey)
    End Interface
End Namespace
