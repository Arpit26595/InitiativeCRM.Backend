using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Project.Infrastructure.ExtensionMethods
{
    public static class DbContextExtensions
    {
        public static IEnumerable<EntityEntry> SaveChangeState(this DbContext context)
        {
            return new List<EntityEntry>(context.ChangeTracker.Entries());
        }

        public static void DetachNotInChangeState(this DbContext context, IEnumerable<EntityEntry> changeState)
        {
            foreach (var entry in context.ChangeTracker.Entries().ToList())
            {
                if (!changeState.Contains(entry))
                    entry.State = EntityState.Detached;
            }
        }
    }

}
