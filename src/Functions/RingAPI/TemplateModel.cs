using Microsoft.Azure.CosmosRepository;
using Microsoft.Azure.CosmosRepository.Attributes;

namespace Template
{
    [Container("templates")]
    [PartitionKeyPath("/id")]
    public class TemplateModel : Item
    {
    }
}
