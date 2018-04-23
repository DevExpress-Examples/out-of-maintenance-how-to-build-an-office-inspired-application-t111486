Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO

Namespace PersonalOrganizer.ViewModels
    <POCOViewModel> _
    Public Class MainViewModel
        Protected ReadOnly Property CurrentWindowService() As ICurrentWindowService
            Get
                Return Me.GetService(Of ICurrentWindowService)()
            End Get
        End Property
        Public Sub [Exit]()
            CurrentWindowService.Close()
        End Sub
    End Class
End Namespace