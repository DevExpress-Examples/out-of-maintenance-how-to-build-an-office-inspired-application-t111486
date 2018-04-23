Imports Microsoft.VisualBasic
Imports DevExpress.Xpf.Core
Imports PersonalOrganizer.Model
Imports System.Data.Entity
Imports System.Windows

Namespace PersonalOrganizer
    Partial Public Class App
        Inherits Application
        Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
            ThemeManager.ApplicationThemeName = Theme.Office2013.Name
            Database.SetInitializer(Of ContactContext)(New ContactContextInitializer())
            MyBase.OnStartup(e)
        End Sub
    End Class
End Namespace
