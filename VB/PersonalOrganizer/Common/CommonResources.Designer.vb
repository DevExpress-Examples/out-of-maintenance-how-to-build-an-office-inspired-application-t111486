Imports System
Imports System.ComponentModel
' This class was auto-generated.
' To add or remove a member, edit your .ResX file then rerun ResGen
' with the /str option, or rebuild your VS project.
Namespace PersonalOrganizer.Common
  ''' <summary>
  ''' A strongly-typed resource class, for looking up localized strings, etc.
  ''' </summary>
  <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")> _
  <System.Diagnostics.DebuggerNonUserCodeAttribute> _
  <System.Runtime.CompilerServices.CompilerGeneratedAttribute> _
  Friend Class CommonResources
    Private Shared _resourceMan As System.Resources.ResourceManager
    Private Shared _resourceCulture As System.Globalization.CultureInfo
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")> _
    Friend Sub New()
    End Sub    
    ''' <summary>
    ''' Returns the cached ResourceManager instance used by this class.
    ''' </summary>
    <System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Advanced)> _
    Friend Shared ReadOnly Property ResourceManager As System.Resources.ResourceManager
      Get
        If Object.ReferenceEquals(_resourceMan, Nothing) Then
          Dim temp As System.Resources.ResourceManager = New System.Resources.ResourceManager("CommonResources", GetType(CommonResources).[Assembly])
          _resourceMan = temp
        End If
        Return _resourceMan
      End Get
    End Property    
    ''' <summary>
    ''' Overrides the current thread's CurrentUICulture property for all
    ''' resource lookups using this strongly typed resource class.
    ''' </summary>
    <System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Advanced)> _
    Friend Shared Property Culture As System.Globalization.CultureInfo
      Get
        Return _resourceCulture
      End Get
      Set
        _resourceCulture = value
      End Set
    End Property    
    ''' <summary>
    ''' Looks up a localized string similar to Do you want to delete this {0}?.
    ''' </summary>
    Friend Shared ReadOnly Property Confirmation_Delete As String
      Get
        Return ResourceManager.GetString("Confirmation_Delete", _resourceCulture)
      End Get
    End Property    
    ''' <summary>
    ''' Looks up a localized string similar to Do you want to save changes?.
    ''' </summary>
    Friend Shared ReadOnly Property Confirmation_Save As String
      Get
        Return ResourceManager.GetString("Confirmation_Save", _resourceCulture)
      End Get
    End Property    
    ''' <summary>
    ''' Looks up a localized string similar to Click OK to reload the entity and lose unsaved changes. Click Cancel to continue editing data..
    ''' </summary>
    Friend Shared ReadOnly Property Confirmation_Reset As String
      Get
        Return ResourceManager.GetString("Confirmation_Reset", _resourceCulture)
      End Get
    End Property    
    ''' <summary>
    ''' Looks up a localized string similar to Confirmation.
    ''' </summary>
    Friend Shared ReadOnly Property Confirmation_Caption As String
      Get
        Return ResourceManager.GetString("Confirmation_Caption", _resourceCulture)
      End Get
    End Property    
    ''' <summary>
    ''' Looks up a localized string similar to Warning.
    ''' </summary>
    Friend Shared ReadOnly Property Warning_Caption As String
      Get
        Return ResourceManager.GetString("Warning_Caption", _resourceCulture)
      End Get
    End Property    
    ''' <summary>
    ''' Looks up a localized string similar to Some fields contain invalid data. Click OK to close the page and lose unsaved changes. Press Cancel to continue editing data..
    ''' </summary>
    Friend Shared ReadOnly Property Warning_SomeFieldsContainInvalidData As String
      Get
        Return ResourceManager.GetString("Warning_SomeFieldsContainInvalidData", _resourceCulture)
      End Get
    End Property    
    ''' <summary>
    ''' Looks up a localized string similar to Update Error.
    ''' </summary>
    Friend Shared ReadOnly Property Exception_UpdateErrorCaption As String
      Get
        Return ResourceManager.GetString("Exception_UpdateErrorCaption", _resourceCulture)
      End Get
    End Property    
    ''' <summary>
    ''' Looks up a localized string similar to Validation Error.
    ''' </summary>
    Friend Shared ReadOnly Property Exception_ValidationErrorCaption As String
      Get
        Return ResourceManager.GetString("Exception_ValidationErrorCaption", _resourceCulture)
      End Get
    End Property    
    ''' <summary>
    ''' Looks up a localized string similar to DataService Request Error.
    ''' </summary>
    Friend Shared ReadOnly Property Exception_DataServiceRequestErrorCaption As String
      Get
        Return ResourceManager.GetString("Exception_DataServiceRequestErrorCaption", _resourceCulture)
      End Get
    End Property    
    ''' <summary>
    ''' Looks up a localized string similar to  *.
    ''' </summary>
    Friend Shared ReadOnly Property Entity_Changed As String
      Get
        Return ResourceManager.GetString("Entity_Changed", _resourceCulture)
      End Get
    End Property    
    ''' <summary>
    ''' Looks up a localized string similar to  (New).
    ''' </summary>
    Friend Shared ReadOnly Property Entity_New As String
      Get
        Return ResourceManager.GetString("Entity_New", _resourceCulture)
      End Get
    End Property
  End Class
End Namespace
