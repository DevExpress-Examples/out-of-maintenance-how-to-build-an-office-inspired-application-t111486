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
Namespace ContactContextDataModel
    Public Class ContactContextUnitOfWork
        Inherits DbUnitOfWork(Of ContactContext)
        Implements IContactContextUnitOfWork
        Private _contactsRepository As Lazy(Of IContactRepository)
        Public Sub New(ByVal context As ContactContext)
            MyBase.New(context)
            _contactsRepository = New Lazy(Of IContactRepository)(Function() New ContactRepository(Me))
        End Sub
        Private Function HasChanges() As Boolean Implements IContactContextUnitOfWork.HasChanges
            Return Context.ChangeTracker.HasChanges()
        End Function
        Private ReadOnly Property Contacts As IContactRepository Implements IContactContextUnitOfWork.Contacts
            Get
                Return _contactsRepository.Value
            End Get
        End Property
    End Class
End Namespace
