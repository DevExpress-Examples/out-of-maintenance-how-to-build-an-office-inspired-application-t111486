Imports System
Imports System.Linq
Imports System.Data.Entity
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Namespace Common.DataModel.EntityFramework
    Public MustInherit Class DbReadOnlyRepository(Of TEntity As Class, TDbContext As DbContext)
        Implements IReadOnlyRepository(Of TEntity)
        Private ReadOnly _dbSetAccessor As Func(Of TDbContext, DbSet(Of TEntity))
        Private ReadOnly _unitOfWork As DbUnitOfWork(Of TDbContext)
        Private _dbSet As DbSet(Of TEntity)
        Public Sub New(ByVal unitOfWork As DbUnitOfWork(Of TDbContext), ByVal dbSetAccessor As Func(Of TDbContext, DbSet(Of TEntity)))
            Me._dbSetAccessor = dbSetAccessor
            Me._unitOfWork = unitOfWork
        End Sub
        Protected ReadOnly Property DbSet As DbSet(Of TEntity)
            Get
                If _dbSet Is Nothing Then
                    _dbSet = _dbSetAccessor(_unitOfWork.Context)
                    'dbSet.Load();
                End If
                Return _dbSet
            End Get
        End Property
        Protected ReadOnly Property Context As TDbContext
            Get
                Return _unitOfWork.Context
            End Get
        End Property
        Protected Overridable Function GetEntities() As IQueryable(Of TEntity)
            Return DbSet
        End Function
        Private Function GetEntities_Impl() As IQueryable(Of TEntity) Implements IReadOnlyRepository(Of TEntity).GetEntities
            Return GetEntities()
        End Function
        Private ReadOnly Property UnitOfWork As IUnitOfWork Implements IReadOnlyRepository(Of TEntity).UnitOfWork
            Get
                Return _unitOfWork
            End Get
        End Property
        Private ReadOnly Property Local As ObservableCollection(Of TEntity) Implements IReadOnlyRepository(Of TEntity).Local
            Get
                Return DbSet.Local
            End Get
        End Property
    End Class
End Namespace
