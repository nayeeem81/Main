using Domain.Model;
using Main.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.Data;

namespace Main.Infrastructure;

public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
{
    public readonly string tenantId;
    public readonly ITenantContext _tenantContext;

    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) :
    base (options)
    {
    }

    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options,
    ITenantSetter tenantSetter,ITenantContext tenantContext) : base (options)
    {
        tenantId = tenantSetter.CurrentTenantId;
        _tenantContext = tenantContext;
    }

    public DbSet<ApplicationUser> ApplicationUsers
    {
        get; set;
    }

    public DbSet<Tenant> Tenants
    {
        get; set;
    }

    public DbSet<EmailSmtp> EmailSmtps
    {
        get; set;
    }

    public DbSet<TenantUser> TenantUsers
    {
        get; set;
    }

    public DbSet<TenantInvitation> TenantInvitations
    {
        get; set;
    }

    public DbSet<EmailOutboxMessage> EmailOutboxMessages
    {
        get; set;
    }

    public DbSet<Page> Pages
    {
        get; set;
    }

    public DbSet<Panel> Panels
    {
        get; set;
    }

    public DbSet<Post> Posts
    {
        get; set;
    }

    public DbSet<Product> Products
    {
        get; set;
    }

    public DbSet<ProductImageFile> ProductImageFiles
    {
        get; set;
    }

    public DbSet<ProductComment> ProductComments
    {
        get; set;
    }

    public DbSet<AdminPost> AdPosts
    {
        get; set;
    }

    public DbSet<AdminPostComment> AdPostComments
    {
        get; set;
    }

    public DbSet<AdminImageFile> AdImageFiles
    {
        get; set;
    }

    public DbSet<AValue> AValues
    {
        get; set;
    }

    public DbSet<ExceptionLog> ExceptionLogs
    {
        get; set;
    }

    protected override void OnModelCreating (ModelBuilder builder)
    {
        base.OnModelCreating (builder);

        FluentApiConfiguration (builder);

        TenantGlobalQueryFilter (builder);
    }

    private void FluentApiConfiguration (ModelBuilder builder)
    {
        // Tenant 
        _ = builder.Entity<Tenant> (static entity =>
        {
            _ = entity.HasKey (t => t.TenantId);
            _ = entity.Property (t => t.TenantId)
                  .HasValueGenerator<SequentialGuidValueGenerator> ();

        });

        // Tenant User
        _ = builder.Entity<TenantUser> ()
                   .HasKey (ut => new { ut.UserId,ut.TenantId,ut.TenantRole });
        _ = builder.Entity<TenantUser> ()
           .HasOne (ut => ut.User)
           .WithMany (au => au.TenantUsers)
           .HasForeignKey (ut => ut.UserId);

        // Tenant Invitation
        _ = builder.Entity<TenantInvitation> (static entity =>
        {
            _ = entity.HasKey (t => t.InviteId);
            _ = entity.Property (t => t.InviteId)
                  .HasValueGenerator<SequentialGuidValueGenerator> ();
            _ = entity.Property (t => t.Email)
                  .IsRequired ();
            _ = entity.Property (t => t.Token)
                  .IsRequired ();
            _ = entity.Property (t => t.Status)
                  .HasDefaultValue (InvitationStatus.Pending)
                  .IsRequired ();
            _ = entity.Property (t => t.CreatedOn)
                  .HasDefaultValueSql ("GETUTCDATE()")
                  .IsRequired ();
            _ = entity.Property (t => t.ExpiresOn)
                  .HasDefaultValueSql ("DATEADD(day, 5, GETUTCDATE())")
                  .IsRequired ();
        });

        // Application User
        _ = builder.Entity<ApplicationUser> ()
            .HasIndex (u => u.Email)
            .IsUnique ();

        // Post
        _ = builder.Entity<Post> ()
            .Property (p => p.Price)
            .HasColumnType ("decimal(18,2)")
            .IsRequired ();

        // Product
        _ = builder.Entity<Product> ()
           .Property (p => p.Discount)
           .HasColumnType ("decimal(18,2)");
        _ = builder.Entity<Product> ()
            .Property (p => p.SaleCommission)
            .HasColumnType ("decimal(18,2)");


        // Email Outbox Message
        _ = builder.Entity<EmailOutboxMessage> (static entity =>
        {
            _ = entity.HasKey (t => t.Id);
            _ = entity.Property (t => t.ReceiverEmail)
                      .IsRequired ();
            _ = entity.Property (t => t.Subject)
                      .IsRequired ();
            _ = entity.Property (t => t.Body)
                      .IsRequired ();
            _ = entity.Property (t => t.CreatedOnUtc)
                      .HasDefaultValueSql ("GETUTCDATE()")
                      .IsRequired ();
            _ = entity.Property (t => t.RetryCount)
                      .IsRequired ();
        });
    }

    // TenantId based Query filters (comes from ITenantSetter.CurrentTenantId)
    // Initially, the CurrentTenantId is set in the TenantMiddleware, which is executed before the DbContext is created.
    private void TenantGlobalQueryFilter (ModelBuilder builder)
    {
        string currentTenant = tenantId;

        _ = builder.Entity<Tenant> ().HasQueryFilter (p => p.TenantId == currentTenant);

        _ = builder.Entity<TenantUser> ().HasQueryFilter (p => p.TenantId == currentTenant);

        _ = builder.Entity<TenantInvitation> ().HasQueryFilter (p => p.TenantId == currentTenant);

        _ = builder.Entity<Product> ().HasQueryFilter (p => p.TenantId == currentTenant);

        _ = builder.Entity<ProductImageFile> ().HasQueryFilter (p => p.TenantId == currentTenant);

        _ = builder.Entity<ProductComment> ().HasQueryFilter (p => p.TenantId == currentTenant);

        _ = builder.Entity<AdminPost> ().HasQueryFilter (p => p.TenantId == currentTenant);

        _ = builder.Entity<AdminImageFile> ().HasQueryFilter (p => p.TenantId == currentTenant);

        _ = builder.Entity<AdminPostComment> ().HasQueryFilter (p => p.TenantId == currentTenant);

        _ = builder.Entity<Post> ().HasQueryFilter (p => p.TenantId == currentTenant);

        _ = builder.Entity<Panel> ().HasQueryFilter (p => p.TenantId == currentTenant);

        _ = builder.Entity<Page> ().HasQueryFilter (p => p.TenantId == currentTenant);

        _ = builder.Entity<AValue> ().HasQueryFilter (p => p.TenantId == currentTenant);

        _ = builder.Entity<ExceptionLog> ().HasQueryFilter (p => p.TenantId == currentTenant);
    }

    // Apply BaseData and TenantId to entities implementing IMustHaveTenant interface before saving changes for (entries with added, modified and deleted sattus)
    private void ApplyBaseDataTenantId ()
    {
        string currentTenantId = tenantId;

        BaseDataModel createDataModel = _tenantContext.GetCreateBaseDataModel ();
        BaseDataModel updateDataModel = _tenantContext.GetUpdateBaseDataModel ();
        BaseDataModel deleteDataModel = _tenantContext.GetDeleteBaseDataModel ();

        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added && e.Entity is IMustHaveTenant);

        foreach ( var entry in entries )
        {
            ( ( IMustHaveTenant ) entry.Entity ).TenantId = currentTenantId;

            var entryState = entry.State;

            if ( entryState == EntityState.Added )
            {
                ( ( IMustHaveTenant ) entry.Entity ).CreateParameters (createDataModel);
            }
            else if ( entryState == EntityState.Modified )
            {
                ( ( IMustHaveTenant ) entry.Entity ).ModifyParameters (updateDataModel);
            }
            else if ( entryState == EntityState.Deleted )
            {
                ( ( IMustHaveTenant ) entry.Entity ).ModifyParameters (deleteDataModel);
            }
        }
    }

    public override int SaveChanges (bool acceptAllChangesOnSuccess)
    {
        ApplyBaseDataTenantId ();

        return base.SaveChanges (acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync (bool acceptAllChangesOnSuccess,CancellationToken cancellationToken = default)
    {
        ApplyBaseDataTenantId ();

        return base.SaveChangesAsync (acceptAllChangesOnSuccess,cancellationToken);
    }

}
