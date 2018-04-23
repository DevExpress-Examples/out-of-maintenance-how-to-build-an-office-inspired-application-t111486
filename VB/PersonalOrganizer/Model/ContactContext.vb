Imports System.Data.Entity

Namespace PersonalOrganizer.Model
    Public Class ContactContext
        Inherits DbContext

        Public Property Contacts() As DbSet(Of Contact)
    End Class
End Namespace
