namespace Functions.Tests;

public static class TestHelpers
{
    public static T DeserializeHttpResponseData<T>(HttpResponseData httpResponseData)
    {
        httpResponseData.Body.Seek(0, SeekOrigin.Begin);
        var reader = new StreamReader(httpResponseData.Body);
        var text = reader.ReadToEnd();
        return JsonSerializer.Deserialize<T>(text);
    }
}