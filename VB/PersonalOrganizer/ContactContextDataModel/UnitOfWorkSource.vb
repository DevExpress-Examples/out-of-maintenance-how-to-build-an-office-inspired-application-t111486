Imports System
Imports System.Linq
Imports System.Data
Imports System.Data.Entity
Imports System.Linq.Expressions
Imports System.Collections.Generic
Imports Common.Utils
Imports Common.DataModel
Imports Common.DataModel.EntityFramework
Imports PersonalOrganizer.Model
Imports DevExpress.Mvvm
Namespace ContactContextDataModel
    Public Module UnitOfWorkSource
        Friend Class DbUnitOfWorkFactory
            Implements IUnitOfWorkFactory
            Public Shared ReadOnly Instance As IUnitOfWorkFactory = New DbUnitOfWorkFactory()
            Private Sub New()
            End Sub
            Private Function CreateUnitOfWork() As IContactContextUnitOfWork Implements IUnitOfWorkFactory(Of IContactContextUnitOfWork).CreateUnitOfWork
                Return New ContactContextUnitOfWork(New ContactContext())
            End Function
        End Class
        Friend Class DesignUnitOfWorkFactory
            Implements IUnitOfWorkFactory
            Public Shared ReadOnly Instance As IUnitOfWorkFactory = New DesignUnitOfWorkFactory()
            Private ReadOnly _UnitOfWork As IContactContextUnitOfWork = New ContactContextDesignTimeUnitOfWork()
            Private Sub New()
            End Sub
            Private Function CreateUnitOfWork() As IContactContextUnitOfWork Implements IUnitOfWorkFactory(Of IContactContextUnitOfWork).CreateUnitOfWork
                Return _UnitOfWork
            End Function
        End Class
        Public Function GetUnitOfWorkFactory() As IUnitOfWorkFactory
            Return GetUnitOfWorkFactory(ViewModelBase.IsInDesignMode)
        End Function
        Public Function GetUnitOfWorkFactory(ByVal isInDesignTime As Boolean) As IUnitOfWorkFactory
            Return If(isInDesignTime, DesignUnitOfWorkFactory.Instance, DbUnitOfWorkFactory.Instance)
        End Function
        Public Function CreateUnitOfWork(Optional ByVal isInDesignTime As Boolean = False) As IContactContextUnitOfWork
            Return GetUnitOfWorkFactory(isInDesignTime).CreateUnitOfWork()
        End Function
    End Module
End Namespace
