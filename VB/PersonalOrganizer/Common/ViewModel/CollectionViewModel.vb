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
    Partial Public MustInherit Class CollectionViewModel(Of TEntity As Class, TPrimaryKey)
        Inherits CollectionViewModelBase(Of TEntity, TPrimaryKey)
        Protected Sub New(Optional ByVal filterExpression As Expression(Of Func(Of TEntity, Boolean)) = Nothing)
            MyBase.New(filterExpression)
        End Sub
    End Class
    Public MustInherit Class CollectionViewModelBase(Of TEntity As Class, TPrimaryKey)
        Inherits ReadOnlyCollectionViewModel(Of TEntity)
        Protected Sub New(ByVal filterExpression As Expression(Of Func(Of TEntity, Boolean)))
            MyBase.New(filterExpression)
        End Sub
        Protected Overrides Sub OnInitializeInRuntime()
            MyBase.OnInitializeInRuntime()
            Messenger.[Default].Register(Of EntityMessage(Of TEntity))(Me, Sub(x) OnMessage(x))
        End Sub
        Protected Shadows ReadOnly Property Repository As IRepository(Of TEntity, TPrimaryKey)
            Get
                Return CType(MyBase.Repository, IRepository(Of TEntity, TPrimaryKey))
            End Get
        End Property
        <Required> _
        Protected Overridable ReadOnly Property MessageBoxService As IMessageBoxService
            Get
                Return Nothing
            End Get
        End Property
        <ServiceProperty(ServiceSearchMode.PreferParents)> _
        Protected Overridable ReadOnly Property DocumentManagerService As IDocumentManagerService
            Get
                Return Nothing
            End Get
        End Property
        Public Overridable Sub [New]()
            Dim document As IDocument = CreateDocument(Nothing)
            If document IsNot Nothing Then
                document.Show()
            End If
        End Sub
        Public Overridable Sub Edit(ByVal entity As TEntity)
            Dim primaryKey As TPrimaryKey = GetPrimaryKey(entity)
            entity = Repository.Reload(entity)
            If entity Is Nothing OrElse Repository.UnitOfWork.GetState(entity) = EntityState.Detached Then
                DestroyDocument(FindEntityDocument(primaryKey))
                Return
            End If
            ShowDocument(GetPrimaryKey(entity))
        End Sub
        Public Function CanEdit(ByVal entity As TEntity) As Boolean
            Return entity IsNot Nothing
        End Function
        Public Sub Delete(ByVal entity As TEntity)
            If MessageBoxService.Show(String.Format("Do you want to delete this {0}?", GetType(TEntity).Name), "Confirmation", MessageBoxButton.YesNo) <> MessageBoxResult.Yes Then
                Return
            End If
            Try
                Entities.Remove(entity)
                Repository.Remove(entity)
                Repository.UnitOfWork.SaveChanges()
                Messenger.[Default].Send(New EntityMessage(Of TEntity)(entity, EntityMessageType.Deleted))
            Catch e As DbException
                Refresh()
                MessageBoxService.Show(e.ErrorMessage, e.ErrorCaption, MessageBoxButton.OK, MessageBoxImage.[Error])
            End Try
        End Sub
        Public Function CanDelete(ByVal entity As TEntity) As Boolean
            Return entity IsNot Nothing
        End Function
        Public Overrides Sub Refresh()
            Dim entity As TEntity = SelectedEntity
            MyBase.Refresh()
            If entity IsNot Nothing AndAlso EntityHasPrimaryKey(entity) Then
                SelectedEntity = FindEntity(GetPrimaryKey(entity))
            End If
        End Sub
        <Display(AutoGenerateField:=False)> _
        Public Sub Save(ByVal entity As TEntity)
            Try
                Repository.UnitOfWork.Update(entity)
                Repository.UnitOfWork.SaveChanges()
                Messenger.[Default].Send(New EntityMessage(Of TEntity)(entity, EntityMessageType.Changed))
            Catch e As DbException
                MessageBoxService.Show(e.ErrorMessage, e.ErrorCaption, MessageBoxButton.OK, MessageBoxImage.[Error])
            End Try
        End Sub
        Public Function CanSave(ByVal entity As TEntity) As Boolean
            Return entity IsNot Nothing
        End Function
        <Display(AutoGenerateField:=False)> _
        Public Overridable Sub UpdateSelectedEntity()
            Me.RaisePropertyChanged(Function(x) x.SelectedEntity)
        End Sub
        Private Sub OnMessage(ByVal message As EntityMessage(Of TEntity))
            If Not EntityHasPrimaryKey(message.Entity) Then
                Return
            End If
            Dim key As TPrimaryKey = GetPrimaryKey(message.Entity)
            Select Case message.MessageType
                Case EntityMessageType.Added
                    OnEntityAdded(key)
                    Exit Select
                Case EntityMessageType.Changed
                    OnEntityChanged(key)
                    Exit Select
                Case EntityMessageType.Deleted
                    OnEntityDeleted(key)
                    Exit Select
            End Select
        End Sub
        Protected Overridable Function OnEntityAdded(ByVal key As TPrimaryKey) As TEntity
            Return FindEntity(key)
        End Function
        Protected Overridable Function OnEntityChanged(ByVal key As TPrimaryKey) As TEntity
            Dim entity As TEntity = FindEntity(key)
            If entity Is Nothing Then
                Return Nothing
            End If
            entity = Repository.Reload(entity)
            Dim index As Integer = Repository.Local.IndexOf(entity)
            If index >= 0 Then
                Repository.Local.Move(index, index)
            End If
            If Object.ReferenceEquals(entity, SelectedEntity) Then
                UpdateSelectedEntity()
            End If
            Return entity
        End Function
        Protected Overridable Sub OnEntityDeleted(ByVal key As TPrimaryKey)
            Dim entity As TEntity = Repository.Local.FirstOrDefault(Function(x) Object.Equals(Repository.GetPrimaryKey(x), key))
            If entity IsNot Nothing Then
                Repository.Remove(entity)
                Repository.UnitOfWork.Detach(entity)
            End If
        End Sub
        Protected Function FindEntity(ByVal key As TPrimaryKey) As TEntity
            If FilterExpression Is Nothing Then
                Return Repository.Find(key)
            End If
            Return Repository.GetEntities().Where(Repository.GetPrimaryKeyExpression.ValueEquals(key)).Where(FilterExpression).FirstOrDefault()
        End Function
        Protected Overrides Sub OnSelectedEntityChanged()
            MyBase.OnSelectedEntityChanged()
            Me.RaiseCanExecuteChanged(Sub(x) x.Edit(SelectedEntity))
            Me.RaiseCanExecuteChanged(Sub(x) x.Delete(SelectedEntity))
            Me.RaiseCanExecuteChanged(Sub(x) x.Save(SelectedEntity))
        End Sub
        Protected Overridable Function EntityHasPrimaryKey(ByVal entity As TEntity) As Boolean
            Return True
        End Function
        Protected Overridable Function GetPrimaryKey(ByVal entity As TEntity) As TPrimaryKey
            Return Repository.GetPrimaryKey(entity)
        End Function
        Private Sub ShowDocument(ByVal key As TPrimaryKey)
            Dim document As IDocument = If(FindEntityDocument(key), CreateDocument(key))
            If document IsNot Nothing Then
                document.Show()
            End If
        End Sub
        Protected Overridable Function CreateDocument(ByVal parameter As Object) As IDocument
            If DocumentManagerService Is Nothing Then
                Return Nothing
            End If
            Return DocumentManagerService.CreateDocument(GetType(TEntity).Name + "View", parameter, Me)
        End Function
        Protected Sub DestroyDocument(ByVal document As IDocument)
            If document IsNot Nothing Then
                document.Close()
            End If
        End Sub
        Protected Function FindEntityDocument(ByVal key As TPrimaryKey) As IDocument
            If DocumentManagerService Is Nothing Then
                Return Nothing
            End If
            For Each document As IDocument In DocumentManagerService.Documents
                Dim entityViewModel As ISingleObjectViewModel(Of TEntity, TPrimaryKey) = TryCast(document.Content, ISingleObjectViewModel(Of TEntity, TPrimaryKey))
                If entityViewModel IsNot Nothing AndAlso Object.Equals(entityViewModel.PrimaryKey, key) Then
                    Return document
                End If
            Next
            Return Nothing
        End Function
    End Class
End Namespace
