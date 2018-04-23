using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;

namespace PersonalOrganizer.Model {
    public class ContactContextInitializer : DropCreateDatabaseAlways<ContactContext> { //DropCreateDatabaseIfModelChanges<ContactContext> {
        protected override void Seed(ContactContext context) {
            base.Seed(context);
            List<Contact> contacts = new List<Contact>() {
                new Contact("Carolyn", "Baker") {
                    Gender = Gender.Female, Email = "carolyn.baker@example.com", Phone = "(555)349-3010",
                    Address = "1198 Theresa Cir", City = "Whitinsville", State = "MA", Zip = "01582",
                },
                new Contact("Amber", "Seaman") {
                    Gender = Gender.Female, Email = "amber.seaman@example.com", Phone = "(555)698-4285",
                    Address = "28700 S Main St", City = "Wilsonville", State = "AL", Zip = "35188",
                },
                new Contact("Annie", "Vicars") {
                    Gender = Gender.Female, Email = "annie.vicars@example.com", Phone = "(555)922-1349",
                    Address = "7267 New York Ave", City = "Jersey City", State = "NJ", Zip = "07306",
                },
                new Contact("Darlene", "Catto") {
                    Gender = Gender.Female, Email = "darlene.catto@example.com", Phone = "(555)752-0582",
                    Address = "32125 4th St NW #210", City = "Washington", State = "DC", Zip = "20001",
                },
                new Contact("Angela", "Gross") {
                    Gender = Gender.Female, Email = "angela.gross@example.com", Phone = "(555)247-1252",
                    Address = "8723 Chicago Ave", City = "Nederland", State = "TX", Zip = "77620",
                },
                new Contact("Edward", "Keck") {
                    Gender = Gender.Male, Email = "edward.keck@example.com", Phone = "(555)410-7042",
                    Address = "38202 Greens Rd", City = "Humble", State = "TX", Zip = "77397",
                },
                new Contact("Angela", "Walker") {
                    Gender = Gender.Female, Email = "angela.walker@example.com", Phone = "(555)325-6339",
                    Address = "8913 Lakeshore Dr", City = "Jacksons Gap", State = "AL", Zip = "36863",
                },
                new Contact("Alice", "Martin") {
                    Gender = Gender.Female, Email = "alice.martin@example.com", Phone = "(555)493-8440",
                    Address = "652 Avonwick Gate", City = "Toronto", State = "ON", Zip = "33125",
                },
                new Contact("Andrew", "Carter") {
                    Gender = Gender.Male, Email = "andrew.carter@example.com", Phone = "(555)578-3946",
                    Address = "7121 Bailey St", City = "Worcester", State = "MA", Zip = "01605",
                },
                new Contact("George", "Morrison") {
                    Gender = Gender.Male, Email = "george.morrison@example.com", Phone = "(555)468-3668",
                    Address = "7716 Country Woods Cir", City = "Kissimmee", State = "FL", Zip = "34747",
                },
            };
            InitializePhotos(contacts);
            contacts.ForEach(x => context.Contacts.Add(x));
            context.SaveChanges();
        }
        void InitializePhotos(IList<Contact> contacts) {
            foreach(Contact contact in contacts)
                contact.Photo = GetPhoto(contact);
        }
        byte[] GetPhoto(Contact contact) {
            string name = string.Format("{0}{1}.jpg", contact.FirstName, contact.LastName);
            string path = "pack://application:,,,/Model/Photos/" + name;
            Stream resourceStream = App.GetResourceStream(new Uri(path)).Stream;
            byte[] res;
            using(BinaryReader reader = new BinaryReader(resourceStream)) {
                res = reader.ReadBytes((int)resourceStream.Length);
            }
            return res;
        }
    }
}
