namespace Main.Common;

/// <summary>
/// Enumeration of exception types that can be logged
/// </summary>
public enum ExceptionType
{
    Validation = 1,
    NotFound = 2,
    Unauthorized = 3,
    Forbidden = 4,
    Conflict = 5,
    InternalServerError = 6,
    ServiceUnavailable = 7,
    BadRequest = 8,
    DatabaseError = 9,
    NetworkError = 10,
    TimeoutError = 11,
    AuthenticationError = 12,
    InvalidOperation = 13,
    NullReference = 14,
    ArgumentException = 15,
    FileNotFound = 16,
    IOError = 17,
    Unknown = 18
}

/// <summary>
/// Enumeration of exception sources
/// </summary>
public enum ExceptionSource
{
    API = 1,
    WebUI = 2,
    BackgroundJob = 3,
    Middleware = 4,
    Service = 5,
    Repository = 6,
    Controller = 7
}

/// <summary>
/// Enumeration of environments
/// </summary>
public enum EnvironmentType
{
    Development = 1,
    Staging = 2,
    Production = 3
}
