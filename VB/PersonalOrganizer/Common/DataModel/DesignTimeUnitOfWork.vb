Imports System
Imports System.Linq
Imports System.Collections.Generic
Imports System.Linq.Expressions
Namespace PersonalOrganizer.Common.DataModel
  ''' <summary>
  ''' A DbUnitOfWork class instance represents the implementation of the Unit Of Work pattern for design-time mode. 
  ''' </summary>
  Public Class DesignTimeUnitOfWork
    Inherits UnitOfWorkBase
    Implements IUnitOfWork
    Private Sub SaveChanges() Implements IUnitOfWork.SaveChanges
    End Sub
    Private Function HasChanges() As Boolean Implements IUnitOfWork.HasChanges
      Return False
    End Function
    Protected Function GetRepository(Of TEntity As Class, TPrimaryKey)(ByVal getPrimaryKeyExpression As Expression(Of Func(Of TEntity, TPrimaryKey))) As IRepository(Of TEntity, TPrimaryKey)
      Return GetRepositoryCore(Of IRepository(Of TEntity, TPrimaryKey), TEntity)(Function() New DesignTimeRepository(Of TEntity, TPrimaryKey)(Me, getPrimaryKeyExpression))
    End Function
    Protected Function GetReadOnlyRepository(Of TEntity As Class)() As IReadOnlyRepository(Of TEntity)
      Return GetRepositoryCore(Of IReadOnlyRepository(Of TEntity), TEntity)(Function() New DesignTimeReadOnlyRepository(Of TEntity)(Me))
    End Function
  End Class
End Namespace
