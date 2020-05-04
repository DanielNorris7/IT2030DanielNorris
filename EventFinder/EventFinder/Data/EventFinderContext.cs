using System.Data.Entity;

namespace EventFinder.Data
{
    public class EventFinderContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public EventFinderContext() : base("name=EventFinderContext")
        {
        }

        public virtual System.Data.Entity.DbSet<EventFinder.Models.Event> Events { get; set; }

        public virtual System.Data.Entity.DbSet<EventFinder.Models.EventType> EventTypes { get; set; }

        public virtual System.Data.Entity.DbSet<EventFinder.Models.Cart> Carts { get; set; }
    }
}
