Imports System
Imports System.Collections
Imports System.Linq
Namespace Common.Utils
    Public Module DbExtensions
        <System.Runtime.CompilerServices.Extension> _
        Public Sub Load(ByVal source As IQueryable)
            Dim enumerator As IEnumerator = source.GetEnumerator()
            While enumerator.MoveNext()
            End While
        End Sub
    End Module
End Namespace
