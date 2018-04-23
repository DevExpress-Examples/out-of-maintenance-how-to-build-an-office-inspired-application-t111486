Imports System
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Collections.Generic
Imports DevExpress.Mvvm
Imports PersonalOrganizer.Common.Utils
Namespace PersonalOrganizer.Common.DataModel
  ''' <summary>
  ''' DesignTimeRepository is an IRepository interface implementation representing the collection of entities of a given type for design-time mode. 
  ''' DesignTimeRepository objects are created from a DesignTimeUnitOfWork class instance using the GetRepository method. 
  ''' Write operations against entities of a given type are not supported in this implementation and throw InvalidOperationException.
  ''' </summary>
  ''' <typeparam name="TEntity">A repository entity type.</typeparam>
  ''' <typeparam name="TPrimaryKey">An entity primary key type.</typeparam>
  Public Class DesignTimeRepository(Of TEntity As Class, TPrimaryKey)
    Inherits DesignTimeReadOnlyRepository(Of TEntity)
    Implements IRepository(Of TEntity, TPrimaryKey)
    Private ReadOnly _getPrimaryKeyExpression As Expression(Of Func(Of TEntity, TPrimaryKey))
    Private ReadOnly _entityTraits As EntityTraits(Of TEntity, TPrimaryKey)    
    ''' <summary>
    ''' Initializes a new instance of the DesignTimeRepository class.
    ''' </summary>
    ''' <param name="getPrimaryKeyExpression">A lambda-expression that returns the entity primary key.</param>
    Public Sub New(ByVal unitOfWork As DesignTimeUnitOfWork, ByVal getPrimaryKeyExpression As Expression(Of Func(Of TEntity, TPrimaryKey)))
      MyBase.New(unitOfWork)
      Me._getPrimaryKeyExpression = getPrimaryKeyExpression
      Me._entityTraits = ExpressionHelper.GetEntityTraits(Me, getPrimaryKeyExpression)
    End Sub
    Protected Overridable Function CreateCore() As TEntity
      Return DesignTimeHelper.CreateDesignTimeObject(Of TEntity)()
    End Function
    Protected Overridable Sub AttachCore(ByVal entity As TEntity)
    End Sub
    Protected Overridable Sub DetachCore(ByVal entity As TEntity)
    End Sub
    Protected Overridable Sub UpdateCore(ByVal entity As TEntity)
    End Sub
    Protected Overridable Function GetStateCore(ByVal entity As TEntity) As EntityState
      Return EntityState.Detached
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
      Return _entityTraits.GetPrimaryKey(entity)
    End Function
    Protected Overridable Sub SetPrimaryKeyCore(ByVal entity As TEntity, ByVal key As TPrimaryKey)
      Dim setPrimaryKeyaction = _entityTraits.SetPrimaryKey
      setPrimaryKeyaction(entity, key)
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
    Private Sub Attach(ByVal entity As TEntity) Implements IRepository(Of TEntity, TPrimaryKey).Attach
      AttachCore(entity)
    End Sub
    Private Sub Detach(ByVal entity As TEntity) Implements IRepository(Of TEntity, TPrimaryKey).Detach
      DetachCore(entity)
    End Sub
    Private Sub Update(ByVal entity As TEntity) Implements IRepository(Of TEntity, TPrimaryKey).Update
      UpdateCore(entity)
    End Sub
    Private Function GetState(ByVal entity As TEntity) As EntityState Implements IRepository(Of TEntity, TPrimaryKey).GetState
      Return GetStateCore(entity)
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
    Private Function HasPrimaryKey(ByVal entity As TEntity) As Boolean Implements IRepository(Of TEntity, TPrimaryKey).HasPrimaryKey
      Return _entityTraits.HasPrimaryKey(entity)
    End Function
    Private Sub SetPrimaryKey(ByVal entity As TEntity, ByVal key As TPrimaryKey) Implements IRepository(Of TEntity, TPrimaryKey).SetPrimaryKey
      SetPrimaryKeyCore(entity, key)
    End Sub
  End Class
End Namespace
