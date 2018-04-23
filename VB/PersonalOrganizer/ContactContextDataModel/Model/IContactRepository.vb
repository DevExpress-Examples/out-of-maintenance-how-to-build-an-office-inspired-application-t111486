Imports System
Imports System.Linq
Imports System.Data
Imports System.Data.Entity
Imports System.Linq.Expressions
Imports System.Collections.Generic
Imports Common.Utils
Imports PersonalOrganizer.Model
Imports Common.DataModel
Imports Common.DataModel.EntityFramework
Namespace ContactContextDataModel
    Public Interface IContactRepository
        Inherits IRepository(Of Contact, Integer)
    End Interface
End Namespace
