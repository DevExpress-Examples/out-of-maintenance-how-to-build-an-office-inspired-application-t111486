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
    Public Class DesignTimeContactRepository
        Inherits DesignTimeRepository(Of Contact, Integer)
        Implements IContactRepository
        Public Sub New()
            MyBase.New(Function(x) x.Id)
        End Sub
    End Class
End Namespace
