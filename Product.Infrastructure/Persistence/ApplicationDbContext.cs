using Microsoft.EntityFrameworkCore;
using Product.Application.Interfaces;
using Product.Domain.Contracts;
using Product.Domain.Entities;

namespace Product.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IEventPublisher? _events;
        //private readonly Guid _userId;

        public ApplicationDbContext(DbContextOptions options /*,ICurrentUser currentUser*/, IEventPublisher? events)
            : base(options)
        {
            //_userId = currentUser.GetUserId();
            _events = events;
        }

        public DbSet<Domain.Entities.Product> Products { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.AppendGlobalQueryFilter<ISoftDelete>(s => s.DeletedOn == null);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        //entry.Entity.CreatedBy = _userId;
                        //entry.Entity.LastModifiedBy = _userId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = DateTime.UtcNow;
                        //entry.Entity.LastModifiedBy = _userId;
                        break;

                    case EntityState.Deleted:
                        if (entry.Entity is ISoftDelete softDelete)
                        {
                            //softDelete.DeletedBy = _userId;
                            softDelete.DeletedOn = DateTime.UtcNow;
                            entry.State = EntityState.Modified;
                        }
                        break;
                }
            }

            int result = await base.SaveChangesAsync(cancellationToken);
            await SendDomainEventsAsync();
            return result;
        }

        private async Task SendDomainEventsAsync()
        {
            if(_events is null)
            return;
            
            var entitiesWithEvents = ChangeTracker.Entries<IEntity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents != null && e.DomainEvents.Any());

            // copy all domain events
            var domainEvents = entitiesWithEvents
                .SelectMany(x => x.DomainEvents)
                .ToList();

            // clear entity domain events
            entitiesWithEvents.ToList()
                .ForEach(entity => entity.DomainEvents.Clear());

            foreach (var domainEvent in domainEvents)
                await _events.PublishAsync(domainEvent);
        }
    }
}
