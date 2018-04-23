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
    ''' IContactContextUnitOfWork extends the IUnitOfWork interface with repositories representing specific entities.
    ''' </summary>
    Public Interface IContactContextUnitOfWork
        Inherits IUnitOfWork

        ''' <summary>
        ''' The Contact entities repository.
        ''' </summary>
        ReadOnly Property Contacts() As IRepository(Of Contact, Integer)
    End Interface
End Namespace
