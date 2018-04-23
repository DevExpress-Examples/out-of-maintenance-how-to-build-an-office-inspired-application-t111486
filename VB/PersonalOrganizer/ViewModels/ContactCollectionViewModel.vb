Imports System
Imports System.Linq
Imports Common.Utils
Imports ContactContextDataModel
Imports Common.DataModel
Imports PersonalOrganizer.Model
Imports Common.ViewModel
Namespace PersonalOrganizer.ViewModels
    Partial Public Class ContactCollectionViewModel
        Inherits CollectionViewModel(Of Contact, Integer)
        Private ReadOnly _unitOfWorkFactory As IUnitOfWorkFactory
        Public Sub New()
            MyClass.New(UnitOfWorkSource.GetUnitOfWorkFactory())
        End Sub
        Public Sub New(ByVal unitOfWorkFactory As IUnitOfWorkFactory)
            Me._unitOfWorkFactory = unitOfWorkFactory
        End Sub
        Protected Overrides Function GetRepository() As IReadOnlyRepository(Of Contact)
            Return _unitOfWorkFactory.CreateUnitOfWork().Contacts
        End Function
    End Class
End Namespace
