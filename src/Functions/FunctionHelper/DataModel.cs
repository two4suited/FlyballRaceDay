namespace FunctionHelper;

public class DataModel
{
    [BsonId]
    public string Id { get; set; } = Guid.NewGuid().ToString();
}