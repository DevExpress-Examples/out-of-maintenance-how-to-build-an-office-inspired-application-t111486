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
    Public Interface IContactContextUnitOfWork
        Inherits IUnitOfWork
        Function HasChanges() As Boolean
        ReadOnly Property Contacts As IContactRepository
    End Interface
End Namespace
