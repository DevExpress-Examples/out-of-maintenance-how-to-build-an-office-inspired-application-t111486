Imports System
Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.IO

Namespace PersonalOrganizer.Model
    Public Class ContactContextInitializer 'DropCreateDatabaseIfModelChanges<ContactContext> {
        Inherits DropCreateDatabaseAlways(Of ContactContext)

        Protected Overrides Sub Seed(ByVal context As ContactContext)
            MyBase.Seed(context)
            Dim contacts As New List(Of Contact)() From { _
                New Contact("Carolyn", "Baker") With {.Gender = Gender.Female, .Email = "carolyn.baker@example.com", .Phone = "(555)349-3010", .Address = "1198 Theresa Cir", .City = "Whitinsville", .State = "MA", .Zip = "01582"}, _
                New Contact("Amber", "Seaman") With {.Gender = Gender.Female, .Email = "amber.seaman@example.com", .Phone = "(555)698-4285", .Address = "28700 S Main St", .City = "Wilsonville", .State = "AL", .Zip = "35188"}, _
                New Contact("Annie", "Vicars") With {.Gender = Gender.Female, .Email = "annie.vicars@example.com", .Phone = "(555)922-1349", .Address = "7267 New York Ave", .City = "Jersey City", .State = "NJ", .Zip = "07306"}, _
                New Contact("Darlene", "Catto") With {.Gender = Gender.Female, .Email = "darlene.catto@example.com", .Phone = "(555)752-0582", .Address = "32125 4th St NW #210", .City = "Washington", .State = "DC", .Zip = "20001"}, _
                New Contact("Angela", "Gross") With {.Gender = Gender.Female, .Email = "angela.gross@example.com", .Phone = "(555)247-1252", .Address = "8723 Chicago Ave", .City = "Nederland", .State = "TX", .Zip = "77620"}, _
                New Contact("Edward", "Keck") With {.Gender = Gender.Male, .Email = "edward.keck@example.com", .Phone = "(555)410-7042", .Address = "38202 Greens Rd", .City = "Humble", .State = "TX", .Zip = "77397"}, _
                New Contact("Angela", "Walker") With {.Gender = Gender.Female, .Email = "angela.walker@example.com", .Phone = "(555)325-6339", .Address = "8913 Lakeshore Dr", .City = "Jacksons Gap", .State = "AL", .Zip = "36863"}, _
                New Contact("Alice", "Martin") With {.Gender = Gender.Female, .Email = "alice.martin@example.com", .Phone = "(555)493-8440", .Address = "652 Avonwick Gate", .City = "Toronto", .State = "ON", .Zip = "33125"}, _
                New Contact("Andrew", "Carter") With {.Gender = Gender.Male, .Email = "andrew.carter@example.com", .Phone = "(555)578-3946", .Address = "7121 Bailey St", .City = "Worcester", .State = "MA", .Zip = "01605"}, _
                New Contact("George", "Morrison") With {.Gender = Gender.Male, .Email = "george.morrison@example.com", .Phone = "(555)468-3668", .Address = "7716 Country Woods Cir", .City = "Kissimmee", .State = "FL", .Zip = "34747"} _
            }
            InitializePhotos(contacts)
            contacts.ForEach(Sub(x) context.Contacts.Add(x))
            context.SaveChanges()
        End Sub
        Private Sub InitializePhotos(ByVal contacts As IList(Of Contact))
            For Each contact As Contact In contacts
                contact.Photo = GetPhoto(contact)
            Next contact
        End Sub
        Private Function GetPhoto(ByVal contact As Contact) As Byte()
            Dim name As String = String.Format("{0}{1}.jpg", contact.FirstName, contact.LastName)
            Dim path As String = "pack://application:,,,/Model/Photos/" & name
            Dim resourceStream As Stream = App.GetResourceStream(New Uri(path)).Stream
            Dim res() As Byte
            Using reader As New BinaryReader(resourceStream)
                res = reader.ReadBytes(CInt(resourceStream.Length))
            End Using
            Return res
        End Function
    End Class
End Namespace
