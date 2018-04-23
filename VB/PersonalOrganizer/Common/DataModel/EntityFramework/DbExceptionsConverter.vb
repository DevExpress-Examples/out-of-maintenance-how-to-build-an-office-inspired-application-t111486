Imports System
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Validation
Imports System.Linq
Imports System.Text
Namespace Common.DataModel.EntityFramework
    Public Module DbExceptionsConverter
        Public Function Convert(ByVal exception As DbUpdateException) As DbException
            Dim originalException As Exception = exception
            While originalException.InnerException IsNot Nothing
                originalException = originalException.InnerException
            End While
            Return New DbException(originalException.Message, "Update Error", exception)
        End Function
        Public Function Convert(ByVal exception As DbEntityValidationException) As DbException
            Dim stringBuilder As StringBuilder = New StringBuilder()
            For Each validationResult As DbEntityValidationResult In exception.EntityValidationErrors
                For Each [error] As DbValidationError In validationResult.ValidationErrors
                    If stringBuilder.Length > 0 Then
                        stringBuilder.AppendLine()
                    End If
                    stringBuilder.Append([error].PropertyName + ": " + [error].ErrorMessage)
                Next
            Next
            Return New DbException(stringBuilder.ToString(), "Validation Error", exception)
        End Function
    End Module
End Namespace
