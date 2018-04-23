Imports System
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Linq.Expressions
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataAnnotations
Imports Common.Utils
Imports Common.DataModel
Imports MessageBoxButton = System.Windows.MessageBoxButton
Imports MessageBoxImage = System.Windows.MessageBoxImage
Imports MessageBoxResult = System.Windows.MessageBoxResult
Namespace Common.ViewModel
    Public Interface ISingleObjectViewModel(Of TEntity, TPrimaryKey)
        ReadOnly Property Entity As TEntity
        ReadOnly Property PrimaryKey As TPrimaryKey
    End Interface
    Public Interface IDetailEntityInfo
    End Interface
    Public Class DetailEntityInfo(Of TDetailEntity As Class)
        Implements IDetailEntityInfo
        Private _DetailEntityKey As Object
        Public ReadOnly Property DetailEntityKey As Object
            Get
                Return _DetailEntityKey
            End Get
        End Property
        Public Sub New(ByVal detailEntityKey As Object)
            Me._DetailEntityKey = detailEntityKey
        End Sub
    End Class
End Namespace
