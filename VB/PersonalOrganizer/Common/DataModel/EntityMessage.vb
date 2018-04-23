Imports System
Imports System.Linq
Namespace Common.DataModel
    Public Enum EntityState
        Detached = 1
        Unchanged = 2
        Added = 4
        Deleted = 8
        Modified = 16
    End Enum
    Public Enum EntityMessageType
        Added
        Deleted
        Changed
    End Enum
    Public Class EntityMessage(Of TEntity)
        Private _MessageType As EntityMessageType
        Private _Entity As TEntity
        Public ReadOnly Property Entity As TEntity
            Get
                Return _Entity
            End Get
        End Property
        Public ReadOnly Property MessageType As EntityMessageType
            Get
                Return _MessageType
            End Get
        End Property
        Public Sub New(ByVal entity As TEntity, ByVal messageType As EntityMessageType)
            Me._Entity = entity
            Me._MessageType = messageType
        End Sub
    End Class
End Namespace
