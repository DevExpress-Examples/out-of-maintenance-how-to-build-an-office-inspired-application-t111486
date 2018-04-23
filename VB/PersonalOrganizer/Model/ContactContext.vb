Imports Microsoft.VisualBasic
Imports System.Data.Entity

Namespace PersonalOrganizer.Model
	Public Class ContactContext
		Inherits DbContext
		Private privateContacts As DbSet(Of Contact)
		Public Property Contacts() As DbSet(Of Contact)
			Get
				Return privateContacts
			End Get
			Set(ByVal value As DbSet(Of Contact))
				privateContacts = value
			End Set
		End Property
	End Class
End Namespace
