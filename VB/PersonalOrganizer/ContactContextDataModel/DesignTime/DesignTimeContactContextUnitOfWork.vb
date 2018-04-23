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
    Public Class ContactContextDesignTimeUnitOfWork
        Inherits DesignTimeUnitOfWork
        Implements IContactContextUnitOfWork
        Private Shared _contactsRepository As DesignTimeContactRepository = New DesignTimeContactRepository()
        Public Sub New()
        End Sub
        Private Function HasChanges() As Boolean Implements IContactContextUnitOfWork.HasChanges
            Return False
        End Function
        Private ReadOnly Property Contacts As IContactRepository Implements IContactContextUnitOfWork.Contacts
            Get
                Return _contactsRepository
            End Get
        End Property
        Public Sub Detach(ByVal entity As Object)
        End Sub
    End Class
End Namespace
