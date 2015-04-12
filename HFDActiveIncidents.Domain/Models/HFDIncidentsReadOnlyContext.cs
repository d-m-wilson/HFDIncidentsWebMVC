using System;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace HFDActiveIncidents.Domain.Models
{
    public class HFDIncidentsReadOnlyContext : DbContext, IIncidentDataSource
    {
        public HFDIncidentsReadOnlyContext()
            : base("Name=HFDActiveIncidentsReadOnlyContext")
        {
            System.Data.Entity.Database.SetInitializer<Models.HFDIncidentsReadOnlyContext>(null);
        }

        System.Linq.IQueryable<ArchivedIncident> IIncidentDataSource.ArchivedIncidents
        {
            get
            {
                return Set<ArchivedIncident>()
                    .AsNoTracking()
                    .Include(ai => ai.IncidentType.Agency);
            }
        }

        System.Linq.IQueryable<IncidentType> IIncidentDataSource.IncidentTypes
        {
            get 
            {
                return Set<IncidentType>()
                    .AsNoTracking()
                    .Include(it => it.Agency);
            }
        }

        public override int SaveChanges()
        {
            throw new InvalidOperationException("This context is read-only.");
        }

        public override System.Threading.Tasks.Task<int> SaveChangesAsync()
        {
            throw new InvalidOperationException("This context is read-only.");
        }

        public override System.Threading.Tasks.Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            throw new InvalidOperationException("This context is read-only.");
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArchivedIncident>();
            modelBuilder.Entity<IncidentType>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
