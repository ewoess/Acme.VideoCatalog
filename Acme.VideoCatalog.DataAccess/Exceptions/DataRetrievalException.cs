﻿namespace Acme.VideoCatalog.DataAccess.Exceptions;

/// <summary>
/// Represents errors that occur during data retrieval operations in the Acme.VideoCatalog application.
/// </summary>
/// <remarks>
/// This exception is thrown when an operation attempts to retrieve data and the operation fails.
/// </remarks>
/// <example>
/// The following example shows how to catch a DataRetrievalException:
/// <code>
/// try
/// {
///     // Code that could throw a DataRetrievalException
/// }
/// catch (DataRetrievalException ex)
/// {
///     // Handle or log the exception
/// }
/// </code>
/// </example>
public class DataRetrievalException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataRetrievalException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public DataRetrievalException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}