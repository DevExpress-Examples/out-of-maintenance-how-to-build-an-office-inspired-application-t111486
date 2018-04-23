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

Namespace PersonalOrganizer.ContactContextDataModel
    ''' <summary>
    ''' A ContactContextUnitOfWork instance that represents the run-time implementation of the IContactContextUnitOfWork interface.
    ''' </summary>
    Public Class ContactContextUnitOfWork
        Inherits DbUnitOfWork(Of ContactContext)
        Implements IContactContextUnitOfWork

        Public Sub New(ByVal contextFactory As Func(Of ContactContext))
            MyBase.New(contextFactory)
        End Sub

        Private ReadOnly Property IContactContextUnitOfWork_Contacts() As IRepository(Of Contact, Integer) Implements IContactContextUnitOfWork.Contacts
            Get
                Return GetRepository(Function(x) x.Set(Of Contact)(), Function(x) x.Id)
            End Get
        End Property
    End Class
End Namespace
