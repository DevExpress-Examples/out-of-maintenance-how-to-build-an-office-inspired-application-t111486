Imports System
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Collections.Generic
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataAnnotations
Imports Common.Utils
Imports Common.DataModel
Namespace Common.ViewModel
    Partial Public MustInherit Class ReadOnlyCollectionViewModel(Of TEntity As Class)
        Inherits ReadOnlyCollectionViewModelBase(Of TEntity)
        Public Sub New(Optional ByVal filterExpression As Expression(Of Func(Of TEntity, Boolean)) = Nothing)
            MyBase.New(filterExpression)
        End Sub
    End Class
    <POCOViewModel> _
    Public MustInherit Class ReadOnlyCollectionViewModelBase(Of TEntity As Class)
        Private _refreshOnFilterExpressionChanged As Boolean = False
        Private _repository As IReadOnlyRepository(Of TEntity)
        Private _entities As IList(Of TEntity)
        Public Sub New(ByVal filterExpression As Expression(Of Func(Of TEntity, Boolean)))
            Me.FilterExpression = filterExpression
            Me._refreshOnFilterExpressionChanged = True
            If Not Me.IsInDesignMode() Then
                OnInitializeInRuntime()
            End If
        End Sub
        Protected Overridable Sub OnInitializeInRuntime()
        End Sub
        Public Overridable Sub Refresh()
            Me._repository = GetRepository()
            Me._entities = GetEntities()
            Me.RaisePropertyChanged(Function(x) Entities)
        End Sub
        Protected ReadOnly Property Repository As IReadOnlyRepository(Of TEntity)
            Get
                If _repository Is Nothing Then
                    _repository = GetRepository()
                End If
                Return _repository
            End Get
        End Property
        Public ReadOnly Property Entities As IList(Of TEntity)
            Get
                If _entities Is Nothing Then
                    _entities = GetEntities()
                End If
                Return _entities
            End Get
        End Property
        Public Overridable Property SelectedEntity As TEntity
        Protected Overridable Sub OnSelectedEntityChanged()
        End Sub
        Public Overridable Property FilterExpression As Expression(Of Func(Of TEntity, Boolean))
        Protected Overridable Sub OnFilterExpressionChanged()
            If _refreshOnFilterExpressionChanged Then
                Refresh()
            End If
        End Sub
        Protected MustOverride Function GetRepository() As IReadOnlyRepository(Of TEntity)
        Protected Overridable Function GetEntities() As IList(Of TEntity)
            Dim queryable = Repository.GetEntities()
            If FilterExpression IsNot Nothing Then
                queryable = queryable.Where(FilterExpression)
            End If
            queryable.Load()
            Return Repository.Local
        End Function
    End Class
End Namespace
