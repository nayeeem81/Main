namespace Main.Common;

/// <summary>
/// Exception error codes and their status codes
/// </summary>
public static class ExceptionErrorCodes
{
    // Validation Errors (4xx - 400-419)
    public const string VALIDATION_ERROR = "ERR_VALIDATION_001";
    public const int VALIDATION_ERROR_CODE = 400;

    // Bad Request (400)
    public const string BAD_REQUEST = "ERR_BAD_REQUEST_001";
    public const int BAD_REQUEST_CODE = 400;

    // Authentication Error (401)
    public const string AUTHENTICATION_ERROR = "ERR_AUTH_001";
    public const int AUTHENTICATION_ERROR_CODE = 401;

    public const string TOKEN_EXPIRED = "ERR_AUTH_002";
    public const int TOKEN_EXPIRED_CODE = 401;

    public const string INVALID_CREDENTIALS = "ERR_AUTH_003";
    public const int INVALID_CREDENTIALS_CODE = 401;

    // Authorization/Forbidden (403)
    public const string FORBIDDEN_ERROR = "ERR_FORBIDDEN_001";
    public const int FORBIDDEN_ERROR_CODE = 403;

    public const string INSUFFICIENT_PERMISSIONS = "ERR_FORBIDDEN_002";
    public const int INSUFFICIENT_PERMISSIONS_CODE = 403;

    // Not Found (404)
    public const string NOT_FOUND = "ERR_NOT_FOUND_001";
    public const int NOT_FOUND_CODE = 404;

    public const string RESOURCE_NOT_FOUND = "ERR_NOT_FOUND_002";
    public const int RESOURCE_NOT_FOUND_CODE = 404;

    // Conflict (409)
    public const string CONFLICT = "ERR_CONFLICT_001";
    public const int CONFLICT_CODE = 409;

    public const string DUPLICATE_RESOURCE = "ERR_CONFLICT_002";
    public const int DUPLICATE_RESOURCE_CODE = 409;

    // Unprocessable Entity (422)
    public const string INVALID_OPERATION = "ERR_INVALID_OP_001";
    public const int INVALID_OPERATION_CODE = 422;

    // Database Errors (5xx - 500-599)
    public const string DATABASE_ERROR = "ERR_DB_001";
    public const int DATABASE_ERROR_CODE = 500;

    public const string DATABASE_TIMEOUT = "ERR_DB_002";
    public const int DATABASE_TIMEOUT_CODE = 500;

    public const string DATA_INTEGRITY_ERROR = "ERR_DB_003";
    public const int DATA_INTEGRITY_ERROR_CODE = 500;

    // Internal Server Error (500)
    public const string INTERNAL_SERVER_ERROR = "ERR_INTERNAL_001";
    public const int INTERNAL_SERVER_ERROR_CODE = 500;

    public const string NULL_REFERENCE_ERROR = "ERR_INTERNAL_002";
    public const int NULL_REFERENCE_ERROR_CODE = 500;

    public const string INVALID_ARGUMENT_ERROR = "ERR_INTERNAL_003";
    public const int INVALID_ARGUMENT_ERROR_CODE = 500;

    // Network/Service Errors (503)
    public const string SERVICE_UNAVAILABLE = "ERR_SERVICE_001";
    public const int SERVICE_UNAVAILABLE_CODE = 503;

    public const string NETWORK_ERROR = "ERR_NETWORK_001";
    public const int NETWORK_ERROR_CODE = 503;

    public const string TIMEOUT_ERROR = "ERR_TIMEOUT_001";
    public const int TIMEOUT_ERROR_CODE = 504;

    public const string EXTERNAL_SERVICE_ERROR = "ERR_EXT_SERVICE_001";
    public const int EXTERNAL_SERVICE_ERROR_CODE = 502;

    // File Errors (500)
    public const string FILE_NOT_FOUND = "ERR_FILE_001";
    public const int FILE_NOT_FOUND_CODE = 500;

    public const string IO_ERROR = "ERR_IO_001";
    public const int IO_ERROR_CODE = 500;

    // Tenant Errors (400-403)
    public const string TENANT_NOT_FOUND = "ERR_TENANT_001";
    public const int TENANT_NOT_FOUND_CODE = 404;

    public const string INVALID_TENANT = "ERR_TENANT_002";
    public const int INVALID_TENANT_CODE = 403;

    public const string TENANT_INACTIVE = "ERR_TENANT_003";
    public const int TENANT_INACTIVE_CODE = 403;

    // Unknown Error (500)
    public const string UNKNOWN_ERROR = "ERR_UNKNOWN_001";
    public const int UNKNOWN_ERROR_CODE = 500;
}

/// <summary>
/// User-friendly error messages (never expose details)
/// </summary>
public static class UserFriendlyMessages
{
    public const string VALIDATION_ERROR = "Your request contains invalid data. Please check your input and try again.";
    public const string BAD_REQUEST = "Your request could not be processed. Please verify the information and try again.";
    public const string AUTHENTICATION_ERROR = "Your authentication credentials are invalid. Please log in again.";
    public const string TOKEN_EXPIRED = "Your session has expired. Please log in again.";
    public const string INSUFFICIENT_PERMISSIONS = "You do not have permission to perform this action.";
    public const string NOT_FOUND = "The requested resource was not found.";
    public const string CONFLICT = "A conflict occurred while processing your request. Please try again.";
    public const string INVALID_OPERATION = "The operation you requested cannot be completed at this time.";
    public const string DATABASE_ERROR = "A system error occurred while processing your request. Our team has been notified.";
    public const string SERVICE_UNAVAILABLE = "The service is currently unavailable. Please try again later.";
    public const string NETWORK_ERROR = "A network error occurred. Please check your connection and try again.";
    public const string TIMEOUT_ERROR = "The request took too long to complete. Please try again.";
    public const string UNKNOWN_ERROR = "An unexpected error occurred. Our team has been notified and will investigate.";
}
