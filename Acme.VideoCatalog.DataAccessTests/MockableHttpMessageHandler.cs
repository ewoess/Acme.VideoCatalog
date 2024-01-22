namespace Acme.VideoCatalog.DataAccessTests;

public class MockableHttpMessageHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        return base.SendAsync(request, cancellationToken);
    }
}