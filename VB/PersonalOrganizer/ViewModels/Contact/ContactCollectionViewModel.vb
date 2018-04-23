Imports System
Imports System.Linq
Imports DevExpress.Mvvm.POCO
Imports PersonalOrganizer.Common.Utils
Imports PersonalOrganizer.ContactContextDataModel
Imports PersonalOrganizer.Common.DataModel
Imports PersonalOrganizer.Model
Imports PersonalOrganizer.Common.ViewModel

Namespace PersonalOrganizer.ViewModels
    ''' <summary>
    ''' Represents the Contacts collection view model.
    ''' </summary>
    Partial Public Class ContactCollectionViewModel
        Inherits CollectionViewModel(Of Contact, Integer, IContactContextUnitOfWork)

        ''' <summary>
        ''' Creates a new instance of ContactCollectionViewModel as a POCO view model.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        Public Shared Function Create(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IContactContextUnitOfWork) = Nothing) As ContactCollectionViewModel
            Return ViewModelSource.Create(Function() New ContactCollectionViewModel(unitOfWorkFactory))
        End Function

        ''' <summary>
        ''' Initializes a new instance of the ContactCollectionViewModel class.
        ''' This constructor is declared protected to avoid undesired instantiation of the ContactCollectionViewModel type without the POCO proxy factory.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        Protected Sub New(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IContactContextUnitOfWork) = Nothing)
            MyBase.New(If(unitOfWorkFactory, UnitOfWorkSource.GetUnitOfWorkFactory()), Function(x) x.Contacts)
        End Sub
    End Class
End Namespace