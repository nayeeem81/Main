using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model;

/// <summary>
/// Represents a logged exception in the system for tracking and debugging purposes
/// </summary>
[Table ("ExceptionLogs")]
public class ExceptionLog: BaseEntity
{
    [Key]
    [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
    public long Id
    {
        get; set;
    }

    /// <summary>
    /// The type of exception that occurred
    /// </summary>
    [Required]
    [StringLength (255)]
    public string ExceptionType { get; set; } = string.Empty;

    /// <summary>
    /// HTTP status code assigned to this exception
    /// </summary>
    [Required]
    public int StatusCode
    {
        get; set;
    }

    /// <summary>
    /// Error code for internal categorization
    /// </summary>
    [Required]
    [StringLength (50)]
    public string ErrorCode { get; set; } = string.Empty;

    /// <summary>
    /// Detailed message for developers
    /// </summary>
    [Required]
    public string DetailedMessage { get; set; } = string.Empty;

    /// <summary>
    /// Stack trace of the exception for debugging
    /// </summary>
    public string? StackTrace
    {
        get; set;
    }

    /// <summary>
    /// Inner exception details (if any)
    /// </summary>
    public string? InnerException
    {
        get; set;
    }

    /// <summary>
    /// Generic message to display to users (no sensitive information)
    /// </summary>
    [Required]
    [StringLength (500)]
    public string UserMessage { get; set; } = string.Empty;

    /// <summary>
    /// Request URL where the exception occurred
    /// </summary>
    [StringLength (500)]
    public string? RequestUrl
    {
        get; set;
    }

    /// <summary>
    /// HTTP method used (GET, POST, etc.)
    /// </summary>
    [StringLength (10)]
    public string? HttpMethod
    {
        get; set;
    }

    /// <summary>
    /// Request headers for debugging
    /// </summary>
    public string? RequestHeaders
    {
        get; set;
    }

    /// <summary>
    /// Request body/parameters
    /// </summary>
    public string? RequestBody
    {
        get; set;
    }

    /// <summary>
    /// User ID who triggered the exception (if available)
    /// </summary>
    public string? UserId
    {
        get; set;
    }

    /// <summary>
    /// IP address of the client
    /// </summary>
    [StringLength (45)]
    public string? ClientIpAddress
    {
        get; set;
    }

    /// <summary>
    /// When the exception occurred
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Source of the exception (API, UI, Background Job, etc.)
    /// </summary>
    [StringLength (50)]
    public string? Source
    {
        get; set;
    }

    /// <summary>
    /// Environment where exception occurred (Development, Staging, Production)
    /// </summary>
    [StringLength (50)]
    public string? Environment
    {
        get; set;
    }

    /// <summary>
    /// Additional custom data related to the exception
    /// </summary>
    public string? CustomData
    {
        get; set;
    }

    /// <summary>
    /// Whether this exception has been reviewed/handled
    /// </summary>
    public bool IsResolved { get; set; } = false;

    /// <summary>
    /// Notes from developer/reviewer
    /// </summary>
    public string? ResolutionNotes
    {
        get; set;
    }

    /// <summary>
    /// When the exception was resolved (if applicable)
    /// </summary>
    public DateTime? ResolvedAt
    {
        get; set;
    }

    /// <summary>
    /// Number of times this exception has occurred
    /// </summary>
    [Required]
    public int OccurrenceCount { get; set; } = 1;
}
