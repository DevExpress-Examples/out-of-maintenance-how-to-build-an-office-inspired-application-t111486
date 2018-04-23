Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations

Namespace PersonalOrganizer.ViewModels
    <POCOViewModel> _
    Public Class MainViewModel
        Protected Overridable ReadOnly Property CurrentWindowService() As ICurrentWindowService
            Get
                Return Nothing
            End Get
        End Property
        Public Sub [Exit]()
            CurrentWindowService.Close()
        End Sub
    End Class
End Namespace