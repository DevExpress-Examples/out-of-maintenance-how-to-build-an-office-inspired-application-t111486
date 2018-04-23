Imports Microsoft.VisualBasic
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Namespace PersonalOrganizer.Model
	Public Enum Gender
		Female = 0
		Male = 1
	End Enum

	Public Class Contact
		Private privateId As Integer
        <Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)> _
        Public Property Id() As Integer
            Get
                Return privateId
            End Get
            Protected Set(ByVal value As Integer)
                privateId = value
            End Set
        End Property
		Private privateGender As Gender
		Public Property Gender() As Gender
			Get
				Return privateGender
			End Get
			Set(ByVal value As Gender)
				privateGender = value
			End Set
		End Property
		Private privateFirstName As String
		<Required> _
		Public Property FirstName() As String
			Get
				Return privateFirstName
			End Get
			Set(ByVal value As String)
				privateFirstName = value
			End Set
		End Property
		Private privateLastName As String
		<Required> _
		Public Property LastName() As String
			Get
				Return privateLastName
			End Get
			Set(ByVal value As String)
				privateLastName = value
			End Set
		End Property
		Private privateEmail As String
		<DataType(DataType.EmailAddress), DisplayFormat(NullDisplayText := "<empty>")> _
		Public Property Email() As String
			Get
				Return privateEmail
			End Get
			Set(ByVal value As String)
				privateEmail = value
			End Set
		End Property
		Private privatePhone As String
		<DataType(DataType.PhoneNumber), DisplayFormat(NullDisplayText := "<empty>")> _
		Public Property Phone() As String
			Get
				Return privatePhone
			End Get
			Set(ByVal value As String)
				privatePhone = value
			End Set
		End Property
		Private privateAddress As String
		<DisplayFormat(NullDisplayText := "<empty>")> _
		Public Property Address() As String
			Get
				Return privateAddress
			End Get
			Set(ByVal value As String)
				privateAddress = value
			End Set
		End Property
		Private privateCity As String
		<DisplayFormat(NullDisplayText := "<empty>")> _
		Public Property City() As String
			Get
				Return privateCity
			End Get
			Set(ByVal value As String)
				privateCity = value
			End Set
		End Property
		Private privateState As String
		<DisplayFormat(NullDisplayText := "<empty>")> _
		Public Property State() As String
			Get
				Return privateState
			End Get
			Set(ByVal value As String)
				privateState = value
			End Set
		End Property
		Private privateZip As String
		<DisplayFormat(NullDisplayText := "<empty>")> _
		Public Property Zip() As String
			Get
				Return privateZip
			End Get
			Set(ByVal value As String)
				privateZip = value
			End Set
		End Property
		Private privatePhoto As Byte()
		Public Property Photo() As Byte()
			Get
				Return privatePhoto
			End Get
			Set(ByVal value As Byte())
				privatePhoto = value
			End Set
		End Property

		Public Sub New()
		End Sub
		Public Sub New(ByVal firstName As String, ByVal lastName As String)
			FirstName = firstName
			LastName = lastName
		End Sub
	End Class
End Namespace
