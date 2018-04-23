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
    Public Class ContactRepository
        Inherits DbRepository(Of Contact, Integer, ContactContext)
        Implements IContactRepository
        Public Sub New(ByVal unitOfWork As DbUnitOfWork(Of ContactContext))
            MyBase.New(unitOfWork, Function(context) context.[Set](Of Contact)(), Function(x) x.Id)
        End Sub
    End Class
End Namespace
