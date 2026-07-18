using Main.Infrastructure.CrosscuttingHelperServices;
using Main.Services;
using Main.WebAppCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore;

[Authorize]
public class TenantInvitationController: BaseController
{
    private readonly ITenantInvitationService _invitationService;
    private readonly ITenantSetter _tenantSetter;

    public TenantInvitationController (ITenantInvitationService invitationService,ITenantSetter tenantSetter)
    {
        _invitationService = invitationService;
        _tenantSetter = tenantSetter;
    }

    [HttpGet]
    public IActionResult Invite ()
        => View (new InviteUserViewModel ());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Invite (InviteUserViewModel model)
    {
        if ( !ModelState.IsValid )
        {
            return View (model);
        }

        var tenantId = Guid.Parse(User.FindFirst("TenantId")?.Value ?? Guid.Empty.ToString());
        var invitedByUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        var result = await _invitationService.InviteUserAsync(_tenantSetter.CurrentTenantId, model.Email, model.TenantRole, invitedByUserId);

        TempData["Message"] = result;
        return RedirectToAction (nameof (Invite));
    }

    [HttpGet ("account/accept-invitation")]
    public IActionResult AcceptInvitation (string token)
        => View (new AcceptInvitationViewModel { Token = token });

    [HttpPost ("account/accept-invitation")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AcceptInvitation (AcceptInvitationViewModel model)
    {
        if ( !ModelState.IsValid )
        {
            return View (model);
        }

        var ok = await _invitationService.AcceptInvitationAsync(model.Token, model.FullName);
        if ( !ok )
        {
            ModelState.AddModelError (string.Empty,"Invitation is invalid or expired.");
            return View (model);
        }

        return RedirectToAction ("Login","Account");
    }
}
