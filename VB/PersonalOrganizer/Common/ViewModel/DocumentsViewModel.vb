Imports System
Imports System.Linq
Imports System.ComponentModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataAnnotations
Imports PersonalOrganizer.Common.Utils
Imports PersonalOrganizer.Common.DataModel
Namespace PersonalOrganizer.Common.ViewModel
  Public MustInherit Class DocumentsViewModel(Of TModule As ModuleDescription(Of TModule), TUnitOfWork As IUnitOfWork)
    Private _IsLoaded As Boolean
    Private _Modules As TModule()
    Protected ReadOnly unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork)
    Protected Sub New(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork))
      Me.unitOfWorkFactory = unitOfWorkFactory
      _Modules = CreateModules().ToArray()
      For Each [module] In Modules
        Messenger.[Default].Register(Of NavigateMessage(Of TModule))(Me, [module], Sub(ByVal x) Show(x.Token))
      Next
    End Sub
    Protected ReadOnly Property DocumentManagerService As IDocumentManagerService
      Get
        Return Me.GetService(Of IDocumentManagerService)()
      End Get
    End Property
    Protected ReadOnly Property WorkspaceDocumentManagerService As IDocumentManagerService
      Get
        Return Me.GetService(Of IDocumentManagerService)("WorkspaceDocumentManagerService")
      End Get
    End Property
    Public ReadOnly Property Modules As TModule()
      Get
        Return _Modules
      End Get
    End Property
    Protected Overridable ReadOnly Property DefaultModule As TModule
      Get
        Return Modules.First()
      End Get
    End Property
    Public Overridable Property SelectedModule As TModule
    Public Overridable Property ActiveModule As TModule
    Public Sub SaveAll()
      Messenger.[Default].Send(New SaveAllMessage())
    End Sub
    Public Sub OnClosing(ByVal cancelEventArgs As CancelEventArgs)
      Messenger.[Default].Send(New CloseAllMessage(cancelEventArgs))
    End Sub
    Public Sub Show(ByVal [module] As TModule)
      If [module] Is Nothing OrElse DocumentManagerService Is Nothing Then
        Return
      End If
      Dim document As IDocument = DocumentManagerService.FindDocumentByIdOrCreate([module], Function(ByVal x) CreateDocument([module]))
      document.Show()
    End Sub
    Protected ReadOnly Property IsLoaded As Boolean
      Get
        Return _IsLoaded
      End Get
    End Property
    Public Overridable Sub OnLoaded()
      _IsLoaded = True
      AddHandler DocumentManagerService.ActiveDocumentChanged, AddressOf OnActiveDocumentChanged
      Show(DefaultModule)
    End Sub
    Private Sub OnActiveDocumentChanged(ByVal sender As Object, ByVal e As ActiveDocumentChangedEventArgs)
      ActiveModule = If(e.NewDocument Is Nothing, Nothing, TryCast(e.NewDocument.Id, TModule))
    End Sub
    Protected Overridable Sub OnSelectedModuleChanged(ByVal oldModule As TModule)
      If IsLoaded Then
        Show(SelectedModule)
      End If
    End Sub
    Protected Overridable Sub OnActiveModuleChanged(ByVal oldModule As TModule)
      SelectedModule = ActiveModule
    End Sub
    Private Function CreateDocument(ByVal [module] As TModule) As IDocument
      Dim document = DocumentManagerService.CreateDocument([module].DocumentType, Nothing, Me)
      document.Title = GetModuleTitle([module])
      document.DestroyOnClose = False
      Return document
    End Function
    Protected Overridable Function GetModuleTitle(ByVal [module] As TModule) As String
      Return [module].ModuleTitle
    End Function
    Public Sub PinPeekCollectionView(ByVal [module] As TModule)
      If WorkspaceDocumentManagerService Is Nothing Then
        Return
      End If
      Dim document As IDocument = WorkspaceDocumentManagerService.FindDocumentByIdOrCreate([module], Function(ByVal x) CreatePinnedPeekCollectionDocument([module]))
      document.Show()
    End Sub
    Private Function CreatePinnedPeekCollectionDocument(ByVal [module] As TModule) As IDocument
      Dim document = WorkspaceDocumentManagerService.CreateDocument("PeekCollectionView", [module].CreatePeekCollectionViewModel())
      document.Title = [module].ModuleTitle
      Return document
    End Function
    Protected Function GetPeekCollectionViewModelFactory(Of TEntity As Class, TPrimaryKey)(ByVal getRepositoryFunc As Func(Of TUnitOfWork, IRepository(Of TEntity, TPrimaryKey))) As Func(Of TModule, Object)
      Return Function(ByVal [module]) PeekCollectionViewModel(Of TModule, TEntity, TPrimaryKey, TUnitOfWork).Create([module], unitOfWorkFactory, getRepositoryFunc).SetParentViewModel(Me)
    End Function
    Protected MustOverride Function CreateModules() As TModule()
    Protected Function CreateUnitOfWork() As TUnitOfWork
      Return unitOfWorkFactory.CreateUnitOfWork()
    End Function
    Public Overridable Property NavigationPaneVisibility As NavigationPaneVisibility
  End Class
  Public Partial MustInherit Class ModuleDescription(Of TModule As ModuleDescription(Of TModule))
    Private _DocumentType As String
    Private _ModuleGroup As String
    Private _ModuleTitle As String
    Private _peekCollectionViewModelFactory As Func(Of TModule, Object)
    Private _peekCollectionViewModel As Object
    Protected Sub New(ByVal title As String, ByVal documentType As String, ByVal group As String, ByVal peekCollectionViewModelFactory As Func(Of TModule, Object))
      _ModuleTitle = title
      _ModuleGroup = group
      Me._DocumentType = documentType
      Me._peekCollectionViewModelFactory = peekCollectionViewModelFactory
    End Sub
    Public ReadOnly Property ModuleTitle As String
      Get
        Return _ModuleTitle
      End Get
    End Property
    Public ReadOnly Property ModuleGroup As String
      Get
        Return _ModuleGroup
      End Get
    End Property
    Public ReadOnly Property DocumentType As String
      Get
        Return _DocumentType
      End Get
    End Property
    Public ReadOnly Property PeekCollectionViewModel As Object
      Get
        If _peekCollectionViewModelFactory Is Nothing Then
          Return Nothing
        End If
        If _peekCollectionViewModel Is Nothing Then
          _peekCollectionViewModel = CreatePeekCollectionViewModel()
        End If
        Return _peekCollectionViewModel
      End Get
    End Property
    Public Function CreatePeekCollectionViewModel() As Object
      Return _peekCollectionViewModelFactory(CType(Me, TModule))
    End Function
  End Class
  Public Enum NavigationPaneVisibility
    Minimized
    Normal
    Off
  End Enum
End Namespace
