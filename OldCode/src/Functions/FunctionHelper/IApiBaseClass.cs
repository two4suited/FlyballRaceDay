namespace FunctionHelper;

public interface IApiBaseClass
{
    Task<HttpResponseData> Create(HttpRequestData request);
}