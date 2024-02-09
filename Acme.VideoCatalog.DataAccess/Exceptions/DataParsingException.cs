namespace Acme.VideoCatalog.DataAccess.Exceptions;

/// <summary>
/// Represents errors that occur during data parsing in the video catalog services.
/// </summary>
/// <remarks>
/// This exception is thrown when there is an error parsing the data from the remote service.
/// </remarks>
public class DataParsingException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataParsingException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
    public DataParsingException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}