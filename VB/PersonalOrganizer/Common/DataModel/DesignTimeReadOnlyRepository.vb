Imports System
Imports System.Linq
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports DevExpress.Mvvm
Namespace Common.DataModel
    Public MustInherit Class DesignTimeReadOnlyRepository(Of TEntity As Class)
        Implements IReadOnlyRepository(Of TEntity)
        Private _queryableEntities As IQueryable(Of TEntity)
        Protected Overridable Function GetEntitiesCore() As IQueryable(Of TEntity)
            If _queryableEntities Is Nothing Then
                _queryableEntities = DesignTimeHelper.CreateDesignTimeObjects(Of TEntity)(2).AsQueryable()
            End If
            Return _queryableEntities
        End Function
        Private Function GetEntities() As IQueryable(Of TEntity) Implements IReadOnlyRepository(Of TEntity).GetEntities
            Return GetEntitiesCore()
        End Function
        Private ReadOnly Property UnitOfWork As IUnitOfWork Implements IReadOnlyRepository(Of TEntity).UnitOfWork
            Get
                Return DesignTimeUnitOfWork.Instance
            End Get
        End Property
        Private ReadOnly Property Local As ObservableCollection(Of TEntity) Implements IReadOnlyRepository(Of TEntity).Local
            Get
                Return New ObservableCollection(Of TEntity)(GetEntitiesCore())
            End Get
        End Property
    End Class
End Namespace
