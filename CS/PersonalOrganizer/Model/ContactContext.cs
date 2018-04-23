using System.Data.Entity;

namespace PersonalOrganizer.Model {
    public class ContactContext : DbContext {
        public DbSet<Contact> Contacts { get; set; }
    }
}
