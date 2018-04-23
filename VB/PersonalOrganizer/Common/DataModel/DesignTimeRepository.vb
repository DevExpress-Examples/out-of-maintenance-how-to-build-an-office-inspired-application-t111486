Imports System
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Collections.Generic
Imports DevExpress.Mvvm
Imports Common.Utils
Namespace Common.DataModel
    Public MustInherit Class DesignTimeRepository(Of TEntity As Class, TPrimaryKey)
        Inherits DesignTimeReadOnlyRepository(Of TEntity)
        Implements IRepository(Of TEntity, TPrimaryKey)
        Private _getPrimaryKeyExpression As Expression(Of Func(Of TEntity, TPrimaryKey))
        Private _getPrimaryKeyFunction As Func(Of TEntity, TPrimaryKey)
        Private _setPrimaryKeyAction As Action(Of TEntity, TPrimaryKey)
        Public Sub New(ByVal getPrimaryKeyExpression As Expression(Of Func(Of TEntity, TPrimaryKey)), Optional ByVal setPrimaryKeyAction As Action(Of TEntity, TPrimaryKey) = Nothing)
            Me._getPrimaryKeyExpression = getPrimaryKeyExpression
            Me._setPrimaryKeyAction = setPrimaryKeyAction
            If Me._setPrimaryKeyAction Is Nothing Then
                Me._setPrimaryKeyAction = getPrimaryKeyExpression.SetValueAction().Compile()
            End If
        End Sub
        Protected Overridable Function CreateCore() As TEntity
            Return DesignTimeHelper.CreateDesignTimeObject(Of TEntity)()
        End Function
        Protected Overridable Function FindCore(ByVal key As TPrimaryKey) As TEntity
            Throw New InvalidOperationException()
        End Function
        Protected Overridable Sub RemoveCore(ByVal entity As TEntity)
            Throw New InvalidOperationException()
        End Sub
        Protected Overridable Function ReloadCore(ByVal entity As TEntity) As TEntity
            Throw New InvalidOperationException()
        End Function
        Protected Overridable Function GetPrimaryKeyCore(ByVal entity As TEntity) As TPrimaryKey
            If _getPrimaryKeyFunction Is Nothing Then
                _getPrimaryKeyFunction = _getPrimaryKeyExpression.Compile()
            End If
            Return _getPrimaryKeyFunction(entity)
        End Function
        Protected Overridable Sub SetPrimaryKeyCore(ByVal entity As TEntity, ByVal key As TPrimaryKey)
            _setPrimaryKeyAction(entity, key)
        End Sub
        Private Function Find(ByVal key As TPrimaryKey) As TEntity Implements IRepository(Of TEntity, TPrimaryKey).Find
            Return FindCore(key)
        End Function
        Private Sub Remove(ByVal entity As TEntity) Implements IRepository(Of TEntity, TPrimaryKey).Remove
            RemoveCore(entity)
        End Sub
        Private Function Create() As TEntity Implements IRepository(Of TEntity, TPrimaryKey).Create
            Return CreateCore()
        End Function
        Private Function Reload(ByVal entity As TEntity) As TEntity Implements IRepository(Of TEntity, TPrimaryKey).Reload
            Return ReloadCore(entity)
        End Function
        Private ReadOnly Property GetPrimaryKeyExpression As Expression(Of Func(Of TEntity, TPrimaryKey)) Implements IRepository(Of TEntity, TPrimaryKey).GetPrimaryKeyExpression
            Get
                Return _getPrimaryKeyExpression
            End Get
        End Property
        Private Function GetPrimaryKey(ByVal entity As TEntity) As TPrimaryKey Implements IRepository(Of TEntity, TPrimaryKey).GetPrimaryKey
            Return GetPrimaryKeyCore(entity)
        End Function
        Private Sub SetPrimaryKey(ByVal entity As TEntity, ByVal key As TPrimaryKey) Implements IRepository(Of TEntity, TPrimaryKey).SetPrimaryKey
            SetPrimaryKeyCore(entity, key)
        End Sub
    End Class
End Namespace
