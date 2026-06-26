using Microsoft.AspNetCore.Mvc;
using Model;

namespace FineArtsWebApp
{
    public class UserSessionLogController : BaseController
    {
        public readonly ILoggingService _LoggingService;

        public UserSessionLogController(ILoggingService loggingService)
        {
            _LoggingService = loggingService;
        }

        [HttpPost]
        public async Task<JsonResult> LogSession(UserSessionViewModel sessionModel)
        {
            try
            {
                var userSessionEntity = GetUserSessionObject(sessionModel);
                var result = await _LoggingService.LogUserSession(userSessionEntity);
                return Json("OK");
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return Json("FAILED");
            }
        }

        private LogUserSession GetUserSessionObject(UserSessionViewModel sessionModel)
        {
            LogUserSession userSession = new LogUserSession(StaticAppSettings.Country)
            {
                BrowserWidth = sessionModel.BrowserWidth,
                BrowserHeight = sessionModel.BrowserHeight,
                ElementId = sessionModel.ElementId,
                ElementTagName = sessionModel.ElementTagName,
                ElementClass = sessionModel.ElementClass,
                TargetUrl = sessionModel.TargetUrl,
                HostCountry = StaticAppSettings.Country,
                BrowserLogId = (long?)GetBrowserId(),
                ActiveUrl = sessionModel.ActiveUrl

            };
            userSession.AddPositions(sessionModel.ListMousePosition);
            return userSession;
        }
    }
}