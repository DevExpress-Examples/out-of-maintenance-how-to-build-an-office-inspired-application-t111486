Imports System
Imports System.Linq
Imports System.Collections.ObjectModel
Imports System.Linq.Expressions
Imports System.Collections
Imports System.Collections.Generic
Namespace PersonalOrganizer.Common.DataModel
  ''' <summary>
  ''' The IReadOnlyRepository interface represents the read-only implementation of the Repository pattern 
  ''' such that it can be used to query entities of a given type. 
  ''' </summary>
  ''' <typeparam name="TEntity">Repository entity type.</typeparam>
  Public Interface IReadOnlyRepository(Of TEntity As Class)
    Inherits IRepositoryQuery(Of TEntity)    
    ''' <summary>
    ''' The owner unit of work.
    ''' </summary>
    ReadOnly Property UnitOfWork As IUnitOfWork
  End Interface
  Public Interface IRepositoryQuery(Of T)
    Inherits IQueryable(Of T)
    Function Include(Of TProperty)(ByVal path As Expression(Of Func(Of T, TProperty))) As IRepositoryQuery(Of T)
    Function Where(ByVal predicate As Expression(Of Func(Of T, Boolean))) As IRepositoryQuery(Of T)
  End Interface
  Public MustInherit Class RepositoryQueryBase(Of T)
    Implements IQueryable(Of T)
    Private ReadOnly _queryable As Lazy(Of IQueryable(Of T))
    Protected ReadOnly Property Queryable As IQueryable(Of T)
      Get
        Return _queryable.Value
      End Get
    End Property
    Protected Sub New(ByVal getQueryable As Func(Of IQueryable(Of T)))
      Me._queryable = New Lazy(Of IQueryable(Of T))(getQueryable)
    End Sub
    Private ReadOnly Property ElementType As Type Implements IQueryable.ElementType
      Get
        Return Me.Queryable.ElementType
      End Get
    End Property
    Private ReadOnly Property Expression As Expression Implements IQueryable.Expression
      Get
        Return Me.Queryable.Expression
      End Get
    End Property
    Private ReadOnly Property Provider As IQueryProvider Implements IQueryable.Provider
      Get
        Return Me.Queryable.Provider
      End Get
    End Property
    Private Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
      Return Me.Queryable.GetEnumerator()
    End Function
    Private Function GetEnumerator_Impl() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
      Return Me.Queryable.GetEnumerator()
    End Function
  End Class
  Public Module ReadOnlyRepositoryExtensions
    <System.Runtime.CompilerServices.Extension> _
    Public Function GetFilteredEntities(Of TEntity As Class, TProjection)(ByVal repository As IReadOnlyRepository(Of TEntity), ByVal filterExpression As Expression(Of Func(Of TEntity, Boolean)), Optional ByVal projection As Func(Of IRepositoryQuery(Of TEntity), IQueryable(Of TProjection)) = Nothing) As IQueryable(Of TProjection)
      Dim filtered = If(filterExpression IsNot Nothing, repository.Where(filterExpression), repository)
      Return If(projection IsNot Nothing, projection(filtered), CType(filtered, IQueryable(Of TProjection)))
    End Function
    <System.Runtime.CompilerServices.Extension> _
    Public Function GetFilteredEntities(Of TEntity As Class)(ByVal repository As IReadOnlyRepository(Of TEntity), ByVal filterExpression As Expression(Of Func(Of TEntity, Boolean))) As IQueryable(Of TEntity)
      Return repository.GetFilteredEntities(filterExpression, Function(ByVal x) x)
    End Function
  End Module
End Namespace
