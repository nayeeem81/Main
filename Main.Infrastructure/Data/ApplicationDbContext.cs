using Domain.Model;
using Main.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Main.Infrastructure;

public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
{
    public readonly Guid resolvedTenantId;
    public readonly ITenantContext _tenantContext;

    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options)
    {

    }

    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options,
    ITenantSetter tenantSetter,ITenantContext tenantContext) : base (options)
    {
        resolvedTenantId = tenantSetter.CurrentTenantId;

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

    public DbSet<UserRefreshToken> UserRefreshTokens
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

        _ = builder.Entity<Tenant> ()
       .HasOne (t => t.EmaiSmtp)
       .WithOne (e => e.Tenant)
       .HasForeignKey<EmailSmtp> (e => e.FkTenantId);

        // Tenant User
        _ = builder.Entity<TenantUser> ()
            .HasOne (ut => ut.User)
            .WithMany (au => au.TenantUsers)
            .HasForeignKey (ut => ut.UserId);


        // Tenant Invitation
        _ = builder.Entity<TenantInvitation> (static entity =>
        {
            _ = entity.Property (t => t.Email)
                  .IsRequired ();
            _ = entity.Property (t => t.Token)
                  .IsRequired ();
            _ = entity.Property (t => t.Status)
                  .IsRequired ();
            _ = entity.Property (t => t.CreatedOn)
                  .IsRequired ();
            _ = entity.Property (t => t.ExpiresOn)
                  .IsRequired ();
        });

        // 1. Locate this line in your DbContext / Configuration file:
        _ = builder.Entity<TenantInvitation> ()
            .Property (t => t.Status)
            .HasDefaultValue (InvitationStatus.Pending) // or your specific default
            .HasSentinel (( InvitationStatus ) ( -1 ));      // Add this line to silence validation [20601]


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
           .HasPrecision (18,2);

        _ = builder.Entity<Product> ()
            .Property (p => p.Price)
            .HasPrecision (18,2); // Set precision to 18 and scale to 2 digits

        _ = builder.Entity<Product> ()
            .Property (p => p.SaleCommission)
            .HasPrecision (18,2);


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
                      .IsRequired ();
            _ = entity.Property (t => t.RetryCount)
                      .IsRequired ();
        });

        _ = builder.Entity<Tenant> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<TenantInvitation> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<TenantUser> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<Page> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<Post> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<Panel> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<Product> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<ProductComment> ()
         .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<ProductImageFile> ()
         .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<AdminPost> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<AdminPostComment> ()
         .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<AdminImageFile> ()
         .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<AValue> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<ExceptionLog> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<UserRefreshToken> ()
          .HasIndex (ut => ut.MyTenantId);

        _ = builder.Entity<EmailOutboxMessage> ()
          .HasIndex (ut => ut.MyTenantId);
    }

    // TenantId based Query filters (comes from ITenantSetter.CurrentTenantId)
    // Initially, the CurrentTenantId is set in the TenantMiddleware, which is executed before the DbContext is created.
    private void TenantGlobalQueryFilter (ModelBuilder builder)
    {
        string currentTenant = resolvedTenantId.ToString();

        Guid myTenantId = new(currentTenant);

        _ = builder.Entity<Tenant> ().HasQueryFilter (p => p.MyTenantId == myTenantId);

        _ = builder.Entity<TenantUser> ().HasQueryFilter (p => p.MyTenantId == myTenantId);

        _ = builder.Entity<TenantInvitation> ().HasQueryFilter (p => p.MyTenantId == myTenantId);

        _ = builder.Entity<Product> ().HasQueryFilter (p => p.MyTenantId == myTenantId);

        _ = builder.Entity<ProductImageFile> ().HasQueryFilter (p => p.MyTenantId == myTenantId);

        _ = builder.Entity<ProductComment> ().HasQueryFilter (p => p.MyTenantId == myTenantId);

        _ = builder.Entity<AdminPost> ().HasQueryFilter (p => p.MyTenantId == myTenantId);

        _ = builder.Entity<AdminImageFile> ().HasQueryFilter (p => p.MyTenantId == myTenantId);

        _ = builder.Entity<AdminPostComment> ().HasQueryFilter (p => p.MyTenantId == myTenantId);

        _ = builder.Entity<Post> ().HasQueryFilter (p => p.MyTenantId == myTenantId);

        _ = builder.Entity<Panel> ().HasQueryFilter (p => p.MyTenantId == myTenantId);

        _ = builder.Entity<Page> ().HasQueryFilter (p => p.MyTenantId == myTenantId);

        _ = builder.Entity<AValue> ().HasQueryFilter (p => p.MyTenantId == myTenantId);

        _ = builder.Entity<ExceptionLog> ().HasQueryFilter (p => p.MyTenantId == myTenantId);

        _ = builder.Entity<UserRefreshToken> ().HasQueryFilter (p => p.MyTenantId == myTenantId);
    }

    // Apply BaseData and TenantId to entities implementing IMustHaveTenant interface before saving changes for (entries with added, modified and deleted sattus)
    private void ApplyBaseDataTenantId ()
    {
        string  currentTenant = resolvedTenantId.ToString ();
        Guid?  myTenantId = (Guid? )resolvedTenantId;


        BaseDataModel createDataModel = _tenantContext.GetCreateBaseDataModel ();
        BaseDataModel updateDataModel = _tenantContext.GetUpdateBaseDataModel ();
        BaseDataModel deleteDataModel = _tenantContext.GetDeleteBaseDataModel ();

        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is IMustHaveTenant);

        foreach ( var entry in entries )
        {
            if ( myTenantId != null )
            {
                ( ( IMustHaveTenant ) entry.Entity ).MyTenantId = myTenantId.Value;
            }

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
