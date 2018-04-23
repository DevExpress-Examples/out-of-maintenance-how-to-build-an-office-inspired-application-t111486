Imports System
Imports System.Linq
Imports System.Collections.ObjectModel
Namespace Common.DataModel
    Public Interface IReadOnlyRepository(Of TEntity As Class)
        Function GetEntities() As IQueryable(Of TEntity)
        ReadOnly Property UnitOfWork As IUnitOfWork
        ReadOnly Property Local As ObservableCollection(Of TEntity)
    End Interface
End Namespace
