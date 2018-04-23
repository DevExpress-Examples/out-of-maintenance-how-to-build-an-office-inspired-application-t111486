Imports System
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Collections.Generic
Imports System.Linq
Imports System.Linq.Expressions
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataAnnotations
Imports PersonalOrganizer.Common.Utils
Imports PersonalOrganizer.Common.DataModel
Namespace PersonalOrganizer.Common.ViewModel
  Public Partial Class PeekCollectionViewModel(Of TNavigationToken, TEntity As Class, TPrimaryKey, TUnitOfWork As IUnitOfWork)
    Inherits CollectionViewModelBase(Of TEntity, TEntity, TPrimaryKey, TUnitOfWork)
    Public Shared Function Create(ByVal navigationToken As TNavigationToken, ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IRepository(Of TEntity, TPrimaryKey)), Optional ByVal projection As Func(Of IRepositoryQuery(Of TEntity), IQueryable(Of TEntity)) = Nothing) As PeekCollectionViewModel(Of TNavigationToken, TEntity, TPrimaryKey, TUnitOfWork)
      Return ViewModelSource.Create(Function() New PeekCollectionViewModel(Of TNavigationToken, TEntity, TPrimaryKey, TUnitOfWork)(navigationToken, unitOfWorkFactory, getRepositoryFunc, projection))
    End Function
    Private _navigationToken As TNavigationToken
    Private _pickedEntity As TEntity
    Protected Sub New(ByVal navigationToken As TNavigationToken, ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IRepository(Of TEntity, TPrimaryKey)), Optional ByVal projection As Func(Of IRepositoryQuery(Of TEntity), IQueryable(Of TEntity)) = Nothing)
      MyBase.New(unitOfWorkFactory, getRepositoryFunc, projection, Nothing, True)
      Me._navigationToken = navigationToken
    End Sub
    <Display(AutoGenerateField := False)> _
    Public Sub Navigate(ByVal projectionEntity As TEntity)
      _pickedEntity = projectionEntity
      SendSelectEntityMessage()
      Messenger.[Default].Send(New NavigateMessage(Of TNavigationToken)(_navigationToken), _navigationToken)
    End Sub
    Public Function CanNavigate(ByVal projectionEntity As TEntity) As Boolean
      Return projectionEntity IsNot Nothing
    End Function
    Protected Overrides Sub OnInitializeInRuntime()
      MyBase.OnInitializeInRuntime()
      Messenger.[Default].Register(Of SelectedEntityRequest)(Me, Sub(ByVal x) SendSelectEntityMessage())
    End Sub
    Private Sub SendSelectEntityMessage()
      If IsLoaded AndAlso _pickedEntity IsNot Nothing Then
        Messenger.[Default].Send(New SelectEntityMessage(CreateRepository().GetProjectionPrimaryKey(_pickedEntity)))
      End If
    End Sub
  End Class
End Namespace
