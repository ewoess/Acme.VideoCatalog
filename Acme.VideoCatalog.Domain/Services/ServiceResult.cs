namespace Acme.VideoCatalog.Domain.Services;
/// <summary>
/// Represents the result of a service operation, encapsulating the returned data, 
/// a success flag, and an optional error message.
/// </summary>
/// <typeparam name="T">The type of data returned by the service operation.</typeparam>
public class ServiceResult<T>
{
    /// <summary>
    /// Gets or sets the data returned by the service operation.
    /// </summary>
    public T Data { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the service operation was successful.
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Gets or sets the error message if the service operation fails.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Creates a success result object with the provided data.
    /// </summary>
    /// <param name="data">The data to be included in the result.</param>
    /// <returns>A success result object containing the provided data.</returns>
    public static ServiceResult<T> Success(T data)
    {
        return new ServiceResult<T>
        {
            Data = data,
            IsSuccess = true
        };
    }

    /// <summary>
    /// Creates a failure result object with the provided error message.
    /// </summary>
    /// <param name="message">The error message describing the failure.</param>
    /// <returns>A failure result object containing the error message.</returns>
    public static ServiceResult<T> Failure(string message)
    {
        return new ServiceResult<T> { ErrorMessage = message, IsSuccess = false };
    }
}