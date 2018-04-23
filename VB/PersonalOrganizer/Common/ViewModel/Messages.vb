Imports System
Imports System.Linq
Imports System.ComponentModel
Namespace PersonalOrganizer.Common.ViewModel
  ''' <summary>
  ''' Represents the type of entity state change notification when the IUnitOfWork.SaveChanges method has been called.
  ''' </summary>
  Public Enum EntityMessageType  
    ''' <summary>
    ''' The new entity has been added to the unit of work. 
    ''' </summary>
    Added    
    ''' <summary>
    ''' The object has been deleted from the unit of work.
    ''' </summary>
    Deleted    
    ''' <summary>
    ''' One of the properties on the object has been modified. 
    ''' </summary>
    Changed
  End Enum
  Public Class EntityMessage(Of TEntity, TPrimaryKey)
    Private _MessageType As EntityMessageType
    Private _PrimaryKey As TPrimaryKey
    Public ReadOnly Property PrimaryKey As TPrimaryKey
      Get
        Return _PrimaryKey
      End Get
    End Property    
    ''' <summary>
    ''' The entity state change notification type.
    ''' </summary>
    Public ReadOnly Property MessageType As EntityMessageType
      Get
        Return _MessageType
      End Get
    End Property
    Public Sub New(ByVal primaryKey As TPrimaryKey, ByVal messageType As EntityMessageType)
      Me._PrimaryKey = primaryKey
      Me._MessageType = messageType
    End Sub
  End Class
  Public Class SaveAllMessage
  End Class
  Public Class CloseAllMessage
    Private ReadOnly _cancelEventArgs As CancelEventArgs
    Public Sub New(ByVal cancelEventArgs As CancelEventArgs)
      Me._cancelEventArgs = cancelEventArgs
    End Sub
    Public Property Cancel As Boolean
      Get
        Return _cancelEventArgs.Cancel
      End Get
      Set
        _cancelEventArgs.Cancel = value
      End Set
    End Property
  End Class
  Public Class NavigateMessage(Of TNavigationToken)
    Private _Token As TNavigationToken
    Public Sub New(ByVal token As TNavigationToken)
      Me._Token = token
    End Sub
    Public ReadOnly Property Token As TNavigationToken
      Get
        Return _Token
      End Get
    End Property
  End Class
End Namespace
