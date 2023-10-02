using System.Linq.Expressions;
using System.Net;
using Microsoft.Azure.CosmosRepository;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionHelper
{
    public abstract class APIBaseClass<T> where T : Item
    {
        private readonly ILogger _logger;
        private readonly IRepository<T> _repository;

        public APIBaseClass(ILoggerFactory loggerFactory, IRepositoryFactory repositoryFactory)
        {
            _logger = loggerFactory.CreateLogger<APIBaseClass<T>>();
            _repository = repositoryFactory.RepositoryOf<T>();
        }

        public async Task<HttpResponseData> Create(HttpRequestData request)
        {
            using var cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(request.FunctionContext.CancellationToken);

            var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
            T newItem = JsonConvert.DeserializeObject<T>(requestBody);

            _logger.LogInformation("New Item: {NewItem}", newItem);

            try
            {
                var savedItem = await _repository.CreateAsync(newItem, cancellationSource.Token);

                var response = request.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(savedItem, cancellationToken: cancellationSource.Token);

                return response;
            }
            catch (Exception ex)
            {
                var response = request.CreateResponse(HttpStatusCode.InternalServerError);
                await response.WriteStringAsync(ex.Message, cancellationSource.Token);
                return response;
            }

            
        }

        public async Task<HttpResponseData> GetAll(HttpRequestData request)
        {
            using var cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(request.FunctionContext.CancellationToken);

            try
            {
                var item = await _repository.PageAsync(pageNumber: 1, cancellationToken: cancellationSource.Token, returnTotal: true);
                var response = request.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(item.Items, cancellationToken: cancellationSource.Token);

                return response;
            }
            catch (Exception ex)
            {
                var response = request.CreateResponse(HttpStatusCode.InternalServerError);
                await response.WriteStringAsync(ex.Message, cancellationSource.Token);
                return response;
            }
        }

        public async Task<HttpResponseData> GetByID(HttpRequestData request, string id)
        {
            using var cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(request.FunctionContext.CancellationToken);

            var returnRecords = await _repository.GetAsync(k => k.Id == id, cancellationSource.Token);

            var enumerable = returnRecords as T[] ?? returnRecords.ToArray();
            if (!enumerable.Any())
            {
                var response = request.CreateResponse(HttpStatusCode.NotFound);
                await response.WriteStringAsync($"Record with id: {id} was not found", cancellationSource.Token);

                return response;
            }
            else
            {
                var record = enumerable.First();
                var response = request.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(record, cancellationToken: cancellationSource.Token);

                return response;
            }
            
        }

        public async Task<HttpResponseData> GetByQuery(HttpRequestData request,Expression<Func<T,bool>> query)
        {
            using var cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(request.FunctionContext.CancellationToken);

            var items = await _repository.GetAsync(query, cancellationSource.Token);

            var response = request.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(items, cancellationToken: cancellationSource.Token);

            return response;
        }

        public async Task<HttpResponseData> Update(HttpRequestData request, string id)
        {
            using var cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(request.FunctionContext.CancellationToken);
            
            var returnRecords = await _repository.GetAsync(k => k.Id == id, cancellationSource.Token);
            
            var enumerable = returnRecords as T[] ?? returnRecords.ToArray();
            if (!enumerable.Any())
            {
                var response = request.CreateResponse(HttpStatusCode.NotFound);
                await response.WriteStringAsync($"Record with id: {id} was not found", cancellationSource.Token);

                return response;
            }
            else
            {
                var requestBody = await new StreamReader(request.Body).ReadToEndAsync(cancellationSource.Token);
                var updatedItem = JsonConvert.DeserializeObject<T>(requestBody);
                updatedItem.Id = id;

                _logger.LogInformation("Updated Item: {UpdatedItem}", updatedItem);

                var club = await _repository.UpdateAsync(updatedItem);

                var response = request.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(club, cancellationToken: cancellationSource.Token);

                return response;
            }
            
        }
    }
}
