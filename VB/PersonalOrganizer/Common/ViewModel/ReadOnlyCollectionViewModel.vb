Imports System
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataAnnotations
Imports System.Collections.ObjectModel
Imports System.Threading
Imports System.Threading.Tasks
Imports PersonalOrganizer.Common.Utils
Imports PersonalOrganizer.Common.DataModel
Namespace PersonalOrganizer.Common.ViewModel
  Public Partial Class ReadOnlyCollectionViewModel(Of TEntity As Class, TUnitOfWork As IUnitOfWork)
    Inherits ReadOnlyCollectionViewModel(Of TEntity, TEntity, TUnitOfWork)
    Public Shared Function CreateReadOnlyCollectionViewModel(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IReadOnlyRepository(Of TEntity)), Optional ByVal projection As Func(Of IRepositoryQuery(Of TEntity), IQueryable(Of TEntity)) = Nothing) As ReadOnlyCollectionViewModel(Of TEntity, TUnitOfWork)
      Return ViewModelSource.Create(Function() New ReadOnlyCollectionViewModel(Of TEntity, TUnitOfWork)(unitOfWorkFactory, getRepositoryFunc, projection))
    End Function
    Protected Sub New(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IReadOnlyRepository(Of TEntity)), Optional ByVal projection As Func(Of IRepositoryQuery(Of TEntity), IQueryable(Of TEntity)) = Nothing)
      MyBase.New(unitOfWorkFactory, getRepositoryFunc, projection)
    End Sub
  End Class  
  ''' <summary>
  ''' The base class for POCO view models exposing a read-only collection of entities of a given type. 
  ''' This is a partial class that provides the extension point to add custom properties, commands and override methods without modifying the auto-generated code.
  ''' </summary>
  ''' <typeparam name="TEntity">An entity type.</typeparam>
  ''' <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
  Public Partial Class ReadOnlyCollectionViewModel(Of TEntity As Class, TProjection As Class, TUnitOfWork As IUnitOfWork)
    Inherits ReadOnlyCollectionViewModelBase(Of TEntity, TProjection, TUnitOfWork)
    Public Shared Function CreateReadOnlyProjectionCollectionViewModel(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IReadOnlyRepository(Of TEntity)), ByVal projection As Func(Of IRepositoryQuery(Of TEntity), IQueryable(Of TProjection))) As ReadOnlyCollectionViewModel(Of TEntity, TProjection, TUnitOfWork)
      Return ViewModelSource.Create(Function() New ReadOnlyCollectionViewModel(Of TEntity, TProjection, TUnitOfWork)(unitOfWorkFactory, getRepositoryFunc, projection))
    End Function    
    ''' <summary>
    ''' Initializes a new instance of the ReadOnlyCollectionViewModel class.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    ''' <param name="getRepositoryFunc">A function that returns the repository representing entities of a given type.</param>
    Protected Sub New(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IReadOnlyRepository(Of TEntity)), Optional ByVal projection As Func(Of IRepositoryQuery(Of TEntity), IQueryable(Of TProjection)) = Nothing)
      MyBase.New(unitOfWorkFactory, getRepositoryFunc, projection)
    End Sub
  End Class  
  ''' <summary>
  ''' The base class for POCO view models exposing a read-only collection of entities of a given type. 
  ''' It is not recommended to inherit directly from this class. Use the ReadOnlyCollectionViewModel class instead.
  ''' </summary>
  ''' <typeparam name="TEntity">An entity type.</typeparam>
  ''' <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
  <POCOViewModel> _
  Public MustInherit Class ReadOnlyCollectionViewModelBase(Of TEntity As Class, TProjection As Class, TUnitOfWork As IUnitOfWork)
    Inherits EntitiesViewModel(Of TEntity, TProjection, TUnitOfWork)    
    ''' <summary>
    ''' Initializes a new instance of the ReadOnlyCollectionViewModelBase class.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    ''' <param name="getRepositoryFunc">A function that returns the repository representing entities of a given type.</param>
    Public Sub New(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IReadOnlyRepository(Of TEntity)), ByVal projection As Func(Of IRepositoryQuery(Of TEntity), IQueryable(Of TProjection)))
      MyBase.New(unitOfWorkFactory, getRepositoryFunc, projection)
    End Sub
    Protected Overrides Sub OnEntitiesAssigned(ByVal getSelectedEntityCallback As Func(Of TProjection))
      MyBase.OnEntitiesAssigned(getSelectedEntityCallback)
      SelectedEntity = If(getSelectedEntityCallback(), Entities.FirstOrDefault())
    End Sub
    Protected Overrides Function GetSelectedEntityCallback() As Func(Of TProjection)
      Dim selectedItemIndex As Integer = Entities.IndexOf(SelectedEntity)
      Return Function() If((selectedItemIndex >= 0 AndAlso selectedItemIndex < Entities.Count), Entities(selectedItemIndex), Nothing)
    End Function    
    ''' <summary>
    ''' The selected enity.
    ''' Since ReadOnlyCollectionViewModelBase is a POCO view model, this property will raise INotifyPropertyChanged.PropertyEvent when modified so it can be used as a binding source in views.
    ''' </summary>
    Public Overridable Property SelectedEntity As TProjection    
    ''' <summary>
    ''' The lambda expression used to filter which entities will be loaded locally from the unit of work.
    ''' </summary>
    Public Overridable Property FilterExpression As Expression(Of Func(Of TEntity, Boolean))    
    ''' <summary>
    ''' Recreates the unit of work and reloads entities.
    ''' Since CollectionViewModelBase is a POCO view model, an instance of this class will also expose the RefreshCommand property that can be used as a binding source in views.
    ''' </summary>
    Public Overridable Sub Refresh()
      LoadEntities(False)
    End Sub
    Public Function CanRefresh() As Boolean
      Return Not IsLoading
    End Function
    Protected Overrides Sub OnIsLoadingChanged()
      MyBase.OnIsLoadingChanged()
      Me.RaiseCanExecuteChanged(Sub(ByVal x) x.Refresh())
    End Sub
    Protected Overridable Sub OnSelectedEntityChanged()
    End Sub
    Protected Overridable Sub OnFilterExpressionChanged()
      If IsLoaded OrElse IsLoading Then
        LoadEntities(True)
      End If
    End Sub
    Protected Overrides Function GetFilterExpression() As Expression(Of Func(Of TEntity, Boolean))
      Return FilterExpression
    End Function
  End Class
End Namespace
