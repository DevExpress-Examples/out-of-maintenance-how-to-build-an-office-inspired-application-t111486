Imports System
Imports System.Linq
Namespace PersonalOrganizer.Common.DataModel
  ''' <summary>
  ''' The IUnitOfWork interface represents the Unit Of Work pattern 
  ''' such that it can be used to query from a database and group together changes that will then be written back to the store as a unit. 
  ''' </summary>
  Public Interface IUnitOfWork  
    ''' <summary>
    ''' Saves all changes made in this unit of work to the underlying store.
    ''' </summary>
    Sub SaveChanges()    
    ''' <summary>
    ''' Checks if the unit of work is tracking any new, deleted, or changed entities or relationships that will be sent to the store if SaveChanges() is called.
    ''' </summary>
    Function HasChanges() As Boolean
  End Interface  
  ''' <summary>
  ''' Provides the method to create a unit of work of a given type.
  ''' </summary>
  ''' <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
  Public Interface IUnitOfWorkFactory(Of TUnitOfWork As IUnitOfWork)  
    ''' <summary>
    ''' Creates a new unit of work.
    ''' </summary>
    Function CreateUnitOfWork() As TUnitOfWork
  End Interface
End Namespace
