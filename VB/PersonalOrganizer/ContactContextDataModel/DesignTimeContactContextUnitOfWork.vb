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
    ''' A ContactContextDesignTimeUnitOfWork instance that represents the design-time implementation of the IContactContextUnitOfWork interface.
    ''' </summary>
    Public Class ContactContextDesignTimeUnitOfWork
        Inherits DesignTimeUnitOfWork
        Implements IContactContextUnitOfWork

        ''' <summary>
        ''' Initializes a new instance of the ContactContextDesignTimeUnitOfWork class.
        ''' </summary>
        Public Sub New()
        End Sub

        Private ReadOnly Property IContactContextUnitOfWork_Contacts() As IRepository(Of Contact, Integer) Implements IContactContextUnitOfWork.Contacts
            Get
                Return GetRepository(Function(x As Contact) x.Id)
            End Get
        End Property
    End Class
End Namespace
