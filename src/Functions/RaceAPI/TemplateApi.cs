using FunctionHelper;
using Microsoft.Azure.CosmosRepository;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Template
{
    public class TemplateApi : APIBaseClass<TemplateModel>
    {
        public TemplateApi(ILoggerFactory loggerFactory, IRepositoryFactory repositoryFactory) : base(loggerFactory, repositoryFactory)
        { }

        [Function("Create")]
        public async Task<HttpResponseData> CreatePerson([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            return await Create(req);
        }
    }
}
