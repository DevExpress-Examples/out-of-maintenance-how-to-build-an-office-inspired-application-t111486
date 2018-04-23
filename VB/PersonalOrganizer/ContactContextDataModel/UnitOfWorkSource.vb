Imports System
Imports System.Linq
Imports System.Data
Imports System.Data.Entity
Imports System.Linq.Expressions
Imports System.Collections.Generic
Imports PersonalOrganizer.Common.Utils
Imports PersonalOrganizer.Common.DataModel
Imports PersonalOrganizer.Common.DataModel.EntityFramework
Imports PersonalOrganizer.Model
Imports DevExpress.Mvvm

Namespace PersonalOrganizer.ContactContextDataModel
    ''' <summary>
    ''' Provides methods to obtain the relevant IUnitOfWorkFactory.
    ''' </summary>
    Public NotInheritable Class UnitOfWorkSource

        Private Sub New()
        End Sub


        #Region "inner classes"
        Private Class DbUnitOfWorkFactory
            Implements IUnitOfWorkFactory(Of IContactContextUnitOfWork)

            Public Shared ReadOnly Instance As IUnitOfWorkFactory(Of IContactContextUnitOfWork) = New DbUnitOfWorkFactory()
            Private Sub New()
            End Sub
            Private Function IUnitOfWorkFactoryGeneric_CreateUnitOfWork() As IContactContextUnitOfWork Implements IUnitOfWorkFactory(Of IContactContextUnitOfWork).CreateUnitOfWork
                Return New ContactContextUnitOfWork(Function() New ContactContext())
            End Function
        End Class

        Private Class DesignUnitOfWorkFactory
            Implements IUnitOfWorkFactory(Of IContactContextUnitOfWork)

            Public Shared ReadOnly Instance As IUnitOfWorkFactory(Of IContactContextUnitOfWork) = New DesignUnitOfWorkFactory()
            Private Sub New()
            End Sub
            Private Function IUnitOfWorkFactoryGeneric_CreateUnitOfWork() As IContactContextUnitOfWork Implements IUnitOfWorkFactory(Of IContactContextUnitOfWork).CreateUnitOfWork
                Return New ContactContextDesignTimeUnitOfWork()
            End Function
        End Class
        #End Region

        ''' <summary>
        ''' Returns the IUnitOfWorkFactory implementation based on the current mode (run-time or design-time).
        ''' </summary>
        Public Shared Function GetUnitOfWorkFactory() As IUnitOfWorkFactory(Of IContactContextUnitOfWork)
            Return GetUnitOfWorkFactory(ViewModelBase.IsInDesignMode)
        End Function

        ''' <summary>
        ''' Returns the IUnitOfWorkFactory implementation based on the given mode (run-time or design-time).
        ''' </summary>
        ''' <param name="isInDesignTime">Used to determine which implementation of IUnitOfWorkFactory should be returned.</param>
        Public Shared Function GetUnitOfWorkFactory(ByVal isInDesignTime As Boolean) As IUnitOfWorkFactory(Of IContactContextUnitOfWork)
            Return If(isInDesignTime, DesignUnitOfWorkFactory.Instance, DbUnitOfWorkFactory.Instance)
        End Function
    End Class
End Namespace