Imports System
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports DevExpress.Mvvm
Namespace PersonalOrganizer.Common.DataModel
  ''' <summary>
  ''' DesignTimeReadOnlyRepository is an IReadOnlyRepository interface implementation that represents the collection of entities of a given type for design-time mode. 
  ''' DesignTimeReadOnlyRepository objects are created from a DesignTimeInitOfWork class instance using the GetReadOnlyRepository method. 
  ''' DesignTimeReadOnlyRepository provides only read-only operations against entities of a given type.
  ''' </summary>
  ''' <typeparam name="TEntity">A repository entity type.</typeparam>
  Public Class DesignTimeReadOnlyRepository(Of TEntity As Class)
    Inherits DesignTimeRepositoryQuery(Of TEntity)
    Implements IReadOnlyRepository(Of TEntity)
    Private Shared Function CreateSampleQueryable() As IQueryable(Of TEntity)
      Return DesignTimeHelper.CreateDesignTimeObjects(Of TEntity)(2).AsQueryable()
    End Function
    Private ReadOnly _unitOfWork As DesignTimeUnitOfWork
    Public Sub New(ByVal unitOfWork As DesignTimeUnitOfWork)
      MyBase.New(CreateSampleQueryable())
      Me._unitOfWork = unitOfWork
    End Sub
    Private ReadOnly Property UnitOfWork As IUnitOfWork Implements IReadOnlyRepository(Of TEntity).UnitOfWork
      Get
        Return _unitOfWork
      End Get
    End Property
  End Class
  Public Class DesignTimeRepositoryQuery(Of TEntity)
    Inherits RepositoryQueryBase(Of TEntity)
    Implements IRepositoryQuery(Of TEntity)
    Public Sub New(ByVal queryable As IQueryable(Of TEntity))
      MyBase.New(Function() queryable)
    End Sub
    Private Function Include(Of TProperty)(ByVal path As Expression(Of Func(Of TEntity, TProperty))) As IRepositoryQuery(Of TEntity) Implements IRepositoryQuery(Of TEntity).Include
      Return Me
    End Function
    Private Function Where(ByVal predicate As Expression(Of Func(Of TEntity, Boolean))) As IRepositoryQuery(Of TEntity) Implements IRepositoryQuery(Of TEntity).Where
      Return Me
    End Function
  End Class
End Namespace
