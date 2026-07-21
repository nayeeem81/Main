
using Main.Infrastructure.CrosscuttingHelperServices;
using Main.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore;

/// <summary>
/// Admin dashboard controller for viewing exception logs UI
/// This can be used to serve an admin dashboard page
/// </summary>
[Authorize (Roles = "Admin")]
public class ExceptionDashboardController: Controller
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IExceptionLoggingService _exceptionLoggingService;

    public ExceptionDashboardController (
        ApplicationDbContext dbContext,
        IExceptionLoggingService exceptionLoggingService)
    {
        _dbContext = dbContext;
        _exceptionLoggingService = exceptionLoggingService;
    }

    /// <summary>
    /// Displays the exception dashboard/log viewer page
    /// </summary>
    [HttpGet]
    public IActionResult Index ()
    {
        try
        {
            ViewBag.Title = "Exception Logs Dashboard";
            return View ();
        }
        catch
        {
            return RedirectToAction ("Error","Home");
        }
    }

    /// <summary>
    /// Displays detailed exception log view
    /// </summary>
    [HttpGet ("{id}")]
    public async Task<IActionResult> Details (long id)
    {
        try
        {
            var exceptionLog = await _exceptionLoggingService.GetExceptionByIdAsync(id);

            if ( exceptionLog == null )
            {
                return NotFound ();
            }

            ViewBag.ExceptionLog = exceptionLog;
            return View ();
        }
        catch
        {
            return RedirectToAction ("Error","Home");
        }
    }

    /// <summary>
    /// Displays exception statistics dashboard
    /// </summary>
    [HttpGet ("stats")]
    public async Task<IActionResult> Statistics ()
    {
        try
        {
            var (total,unresolved,today) = await _exceptionLoggingService.GetExceptionSummaryAsync ();

            ViewBag.Total = total;
            ViewBag.Unresolved = unresolved;
            ViewBag.Today = today;
            ViewBag.ResolvedPercentage = total > 0 ? Math.Round (( double ) ( total - unresolved ) / total * 100,2) : 0;

            return View ();
        }
        catch
        {
            return RedirectToAction ("Error","Home");
        }
    }

    /// <summary>
    /// Error page for dashboard
    /// </summary>
    public IActionResult Error ()
    {
        return View ();
    }
}
