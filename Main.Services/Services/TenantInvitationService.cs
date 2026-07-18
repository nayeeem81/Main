using Domain.Model;
using Main.Common;
using Main.Infrastructure;
using Main.Infrastructure.CrosscuttingHelperServices;
using Main.IRepository;

namespace Main.Services;

public class TenantInvitationService: ITenantInvitationService
{
    private readonly IApplicationUserRepository _userRepository;
    private readonly ITenantInvitationRepository _invitationRepository;
    private readonly ITenantUserRepository _tenantUserRepository;
    private readonly ITenantRepository _tenantRepository;
    private readonly IEmailSenderService _emailSender;
    private readonly Guid tenantId;

    public TenantInvitationService (
       IApplicationUserRepository userRepository,
        ITenantInvitationRepository invitationRepository,
        ITenantUserRepository tenantUserRepository,
        IEmailSenderService emailSender,
       ITenantRepository tenantRepository,
       ITenantSetter tenantSetter)
    {
        _userRepository = userRepository;
        _invitationRepository = invitationRepository;
        _tenantUserRepository = tenantUserRepository;
        _emailSender = emailSender;
        tenantId = tenantSetter.CurrentTenantId;
        _tenantRepository = tenantRepository;
    }

    public async Task<string> InviteUserAsync (Guid tenantId,string email,string tenantRole,string invitedByUserId,CancellationToken ct = default)
    {
        Tenant? tenant = await _tenantRepository.GetTenantByIdAsync (tenantId);

        string? host = tenant?.HostName != null ? tenant.HostName : "";

        _ = tenant?.Name != null ? tenant.Name : "";


        email = email.Trim ().ToLowerInvariant ();

        var existing = await _invitationRepository.GetByEmailAndTenantAsync(tenantId, email, ct);

        if ( existing is { Status: InvitationStatus.Pending } )
        {
            return "Invitation already pending";
        }

        var token = Guid.NewGuid().ToString("N");
        var invitation = new TenantInvitation
        {
            InviteId = Guid.NewGuid(),
            MyTenantId = tenantId,
            Email = email,
            InvitedByUserId = invitedByUserId,
            TenantRole = tenantRole,
            Token = token,
            Status = InvitationStatus.Pending,
            ExpiresOn = DateTime.UtcNow.AddDays(7)
        };

        await _invitationRepository.AddAsync (invitation,ct);

        var acceptUrl = $"https://{host}/account/accept-invitation?token={token}";
        var emailBody = InvitationEmailTemplate.BuildInvitationEmail (
            recipientEmail: email,
            inviterName: "Tenant Admin",
            tenantName: "Your Tenant",
            acceptUrl: acceptUrl);

        await _emailSender.SendEmailAsync (email,"You're invited to join a tenant",emailBody,ct);

        return "Invitation sent";
    }

    public async Task<bool> AcceptInvitationAsync (string token,string? fullName,CancellationToken ct = default)
    {
        var invitation = await _invitationRepository.GetByTokenAsync(token, ct);
        if ( invitation is null || invitation.Status != InvitationStatus.Pending || invitation.ExpiresOn < DateTime.UtcNow )
        {
            return false;
        }

        var user = await _userRepository.FindByEmailAsync(invitation.Email);


        if ( user is null )
        {
            user = new ApplicationUser
            {
                UserName = invitation.Email,
                Email = invitation.Email,
                NormalizedUserName = fullName,
                EmailConfirmed = true
            };

            var createResult = await _userRepository.CreateAsync(user, "Msainc@1nm");

            if ( !createResult )
            {
                return false;
                //throw new Exception (string.Join (", ",createResult.Errors.Select (e => e.Description)));
            }

            _ = await _userRepository.AddToRoleAsync (user.Email,"User");
        }

        var alreadyMember = await _tenantUserRepository.ExistsAsync(invitation.MyTenantId, user.Id);


        if ( !alreadyMember )
        {
            await _tenantUserRepository.AddAsync (new TenantUser
            {
                UserId = user.Id,
                TenantRole = invitation.TenantRole ?? "ContentManager"
            });
        }

        invitation.Status = InvitationStatus.Accepted;

        invitation.AcceptedOn = DateTime.UtcNow;

        await _invitationRepository.UpdateAsync (invitation,ct);

        return true;
    }
}
