namespace Acme.VideoCatalog.DataAccessTests;

/// <summary>
/// A mockable HTTP message handler for testing purposes. It extends <see cref="DelegatingHandler"/>
/// and can be used to intercept and modify HTTP requests and responses.
/// </summary>
public class MockableHttpMessageHandler : DelegatingHandler
{
    /// <summary>
    /// Sends an HTTP request asynchronously.
    /// </summary>
    /// <param name="request">The HTTP request message to send.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects
    /// or threads to receive notice of cancellation.</param>
    /// <returns>
    /// The task object representing the asynchronous operation. The <see cref="Task{TResult}"/> object's 
    /// <see cref="Task{TResult}.Result"/> property on completion contains the <see cref="HttpResponseMessage"/> 
    /// sent by the server.
    /// </returns>
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        return base.SendAsync(request, cancellationToken);
    }
}