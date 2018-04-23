Imports System
Imports System.Linq
Imports System.Collections.Generic
Namespace Common.DataModel
    Public Class DesignTimeUnitOfWork
        Implements IUnitOfWork
        Public Shared ReadOnly Instance As IUnitOfWork = New DesignTimeUnitOfWork()
        Private Sub SaveChanges() Implements IUnitOfWork.SaveChanges
        End Sub
        Private Function GetState(ByVal entity As Object) As EntityState Implements IUnitOfWork.GetState
            Return EntityState.Detached
        End Function
        Private Sub Update(ByVal entity As Object) Implements IUnitOfWork.Update
        End Sub
        Private Sub Detach(ByVal entity As Object) Implements IUnitOfWork.Detach
        End Sub
    End Class
End Namespace
