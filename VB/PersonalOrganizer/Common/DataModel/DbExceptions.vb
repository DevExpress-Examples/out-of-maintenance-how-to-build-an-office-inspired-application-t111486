Imports System
Imports System.Linq
Namespace Common.DataModel
    Public Class DbException
        Inherits Exception
        Private _ErrorCaption As String
        Private _ErrorMessage As String
        Public Sub New(ByVal errorMessage As String, ByVal errorCaption As String, ByVal innerException As Exception)
            MyBase.New(innerException.Message, innerException)
            Me._ErrorMessage = errorMessage
            Me._ErrorCaption = errorCaption
        End Sub
        Public ReadOnly Property ErrorMessage As String
            Get
                Return _ErrorMessage
            End Get
        End Property
        Public ReadOnly Property ErrorCaption As String
            Get
                Return _ErrorCaption
            End Get
        End Property
    End Class
End Namespace
