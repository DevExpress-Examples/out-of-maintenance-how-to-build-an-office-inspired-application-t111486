Imports System
Imports System.Linq
Imports System.ComponentModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System.Collections.ObjectModel
Imports PersonalOrganizer.Common.Utils
Imports PersonalOrganizer.Common.DataModel
Namespace PersonalOrganizer.Common.ViewModel
  Public Class LookUpEntitiesViewModel(Of TEntity As Class, TProjection As Class, TPrimaryKey, TUnitOfWork As IUnitOfWork)
    Inherits EntitiesViewModel(Of TEntity, TProjection, TUnitOfWork)
    Implements IDocumentContent
    Public Shared Function Create(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IReadOnlyRepository(Of TEntity)), Optional ByVal projection As Func(Of IRepositoryQuery(Of TEntity), IQueryable(Of TProjection)) = Nothing) As LookUpEntitiesViewModel(Of TEntity, TProjection, TPrimaryKey, TUnitOfWork)
      Return ViewModelSource.Create(Function() New LookUpEntitiesViewModel(Of TEntity, TProjection, TPrimaryKey, TUnitOfWork)(unitOfWorkFactory, getRepositoryFunc, projection))
    End Function
    Protected Sub New(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IReadOnlyRepository(Of TEntity)), ByVal projection As Func(Of IRepositoryQuery(Of TEntity), IQueryable(Of TProjection)))
      MyBase.New(unitOfWorkFactory, getRepositoryFunc, projection)
    End Sub
    Protected Overrides Function CreateEntitiesChangeTracker() As IEntitiesChangeTracker
      Return New EntitiesChangeTracker(Of TPrimaryKey)(Me)
    End Function
  End Class
End Namespace
