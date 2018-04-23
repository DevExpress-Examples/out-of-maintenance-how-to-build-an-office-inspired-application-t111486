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
        <[ReadOnly](True), Key> _
        Public Property Id() As Integer
            Get
                Return privateId
            End Get
            Protected Set(ByVal value As Integer)
                privateId = value
            End Set
        End Property
        Public Property Gender() As Gender
        <Required> _
        Public Property FirstName() As String
        <Required> _
        Public Property LastName() As String
        <DataType(DataType.EmailAddress), DisplayFormat(NullDisplayText := "<empty>")> _
        Public Property Email() As String
        <DataType(DataType.PhoneNumber), DisplayFormat(NullDisplayText := "<empty>")> _
        Public Property Phone() As String
        <DisplayFormat(NullDisplayText := "<empty>")> _
        Public Property Address() As String
        <DisplayFormat(NullDisplayText := "<empty>")> _
        Public Property City() As String
        <DisplayFormat(NullDisplayText := "<empty>")> _
        Public Property State() As String
        <DisplayFormat(NullDisplayText := "<empty>")> _
        Public Property Zip() As String
        Public Property Photo() As Byte()

        Public Sub New()
        End Sub
        Public Sub New(ByVal firstName As String, ByVal lastName As String)
            Me.FirstName = firstName
            Me.LastName = lastName
        End Sub
    End Class
End Namespace
