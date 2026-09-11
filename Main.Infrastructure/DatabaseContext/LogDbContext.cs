using Main.Common.Models;
using Main.Model.Base;
using Main.Model.Identity;
using Main.Model.Log;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Main.Infrastructure.DatabaseContext;

public class LogDbContext: DbContext
{
    private readonly ITenantSetter _tenantSetter;

    public LogDbContext (DbContextOptions<LogDbContext> options) : base (options)
    {
    }

    public LogDbContext
    (DbContextOptions<LogDbContext> options,ITenantSetter tenantSetter) : base (options)
    {
        _tenantSetter = tenantSetter;
    }

    public DbSet<ExceptionLogs> ExceptionLogs
    {
        get; set;
    }

    public DbSet<EmailOutboxMessage> EmailOutboxMessages
    {
        get; set;
    }



    protected override void OnModelCreating (ModelBuilder builder)
    {
        base.OnModelCreating (builder);
    }

    private void ApplyBaseMetaData ()
    {
        Guid ResolvedTenantId = _tenantSetter.CurrentTenant.ResolvedTenantId;

        BaseDataModel createDataModel = _tenantSetter.CreateMetaData;
        BaseDataModel updateDataModel = _tenantSetter.UpdateMetaData;
        BaseDataModel deleteDataModel = _tenantSetter.DeleteMetaData;

        var entries = ChangeTracker.Entries()
        .Where(e =>
               e.State == EntityState.Added
               || e.State == EntityState.Modified
               || e.State == EntityState.Deleted).ToArray();

        foreach ( var entry in entries )
        {
            var tenantEntity = (IMustHaveTenant) entry.Entity;
            tenantEntity.MyTenantId = ResolvedTenantId;

            if ( entry.State == EntityState.Added )
            {
                tenantEntity.CreateParameters (createDataModel);
            }
            else if ( entry.State == EntityState.Deleted )
            {
                tenantEntity.DeleteParameters (deleteDataModel);
            }
            else if ( entry.State == EntityState.Modified )
            {
                tenantEntity.ModifyParameters (updateDataModel);
            }

        }
    }

    public override int SaveChanges (bool acceptAllChangesOnSuccess)
    {
        ApplyBaseMetaData ();

        return base.SaveChanges (acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync (CancellationToken cancellationToken = default)
    {
        ApplyBaseMetaData ();

        return await base.SaveChangesAsync (true,cancellationToken);
    }
}
