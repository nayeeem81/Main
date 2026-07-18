using Domain.Model;

namespace Main.Infrastructure.CrosscuttingHelperServices;

public interface IExceptionLoggingService
{

    Task LogExceptionAsync (
        Exception exception,
        string errorCode,
        int statusCode,
        string userMessage,
        string? userId = null,
        string? clientIpAddress = null,
        string? requestUrl = null,
        string? httpMethod = null,
        string? requestHeaders = null,
        string? requestBody = null,
        string? customData = null,
        string source = "API");


    Task<List<ExceptionLog>> GetExceptionsAsync (
        int? statusCode = null,
        string? errorCode = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        bool? isResolved = null,
        int pageNumber = 1,
        int pageSize = 20);


    Task<(int Total,int Unresolved,int Today)> GetExceptionSummaryAsync ();

    Task MarkAsResolvedAsync (long exceptionId,string? notes = null);

    Task<ExceptionLog?> GetExceptionByIdAsync (long id);
}
