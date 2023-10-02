using System.Linq.Expressions;


namespace FunctionHelper
{
    public abstract class APIBaseClass<TClass,TData,TViewModel> where TClass : class where TViewModel : new() where TData : DataModel
    {
        private readonly ILogger _logger;
        public readonly IMongoCollection<TData> Collection;

        public APIBaseClass(ILoggerFactory loggerFactory, IOptions<FlyballGameDaySettings> flyballStoreDatabaseSettings,string databaseName)
        {
            _logger = loggerFactory.CreateLogger<TClass>();
            var mongoClient = new MongoClient(
                flyballStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                flyballStoreDatabaseSettings.Value.DatabaseName);

            Collection = mongoDatabase.GetCollection<TData>(
                databaseName);
        }

        public async Task<HttpResponseData> Create(HttpRequestData request)
        {
            if (request.Body.Length == 0)
            {
                var response = request.CreateResponse(HttpStatusCode.BadRequest);
                _logger.LogInformation("The Body of the request was empty");
                return response;
            }
            
            using var cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(request.FunctionContext.CancellationToken);

            var requestBody = await new StreamReader(request.Body).ReadToEndAsync(cancellationSource.Token);
            var newItem = JsonSerializer.Deserialize<TData>(requestBody);

            _logger.LogInformation("New Item: {@NewItem}", newItem);
            
            try
            {
                await Collection.InsertOneAsync(newItem, cancellationToken: cancellationSource.Token);

                var response = request.CreateResponse(HttpStatusCode.OK);
                var insertedItem = Mapper.Map<TData, TViewModel>(newItem);
                await response.WriteAsJsonAsync(insertedItem, cancellationToken: cancellationSource.Token);

                return response;
            }
            catch (Exception ex)
            {
                var response = request.CreateResponse(HttpStatusCode.InternalServerError);
                await response.WriteStringAsync(ex.Message, cancellationSource.Token);
                return response;
            }
        }

        public async Task<HttpResponseData> GetByFilter(HttpRequestData request,
            FilterDefinition<TData> filter)
        {
            using var cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(request.FunctionContext.CancellationToken);
        
            try
            {
                var documents =  await Collection.FindAsync(filter, cancellationToken: cancellationSource.Token);
                var tournamentsData = documents.ToList();
                var tournaments = tournamentsData.MapList(Mapper.Map<TData, TViewModel>);
            
                _logger.LogInformation(@"Found Records");

                var response = request.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(tournaments, cancellationToken: cancellationSource.Token);

                return response;
            }
            catch (Exception ex)
            {
                var response = request.CreateResponse(HttpStatusCode.InternalServerError);
                await response.WriteStringAsync(ex.Message, cancellationSource.Token);
                return response;
            }
        }

        public async Task<HttpResponseData> Delete(HttpRequestData request, string id)
        {
            using var cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(request.FunctionContext.CancellationToken);
       
            try
            {
                var filter = Builders<TData>.Filter.Where(x => x.Id == id);
                var documents =  await Collection.FindAsync(filter, cancellationToken: cancellationSource.Token);
                await Collection.DeleteOneAsync(filter, cancellationToken: cancellationSource.Token);  
            
                _logger.LogInformation(@"Updated Record Records");

                var response = request.CreateResponse(HttpStatusCode.OK);
                await response.WriteStringAsync($"{id} was deleted!", cancellationToken: cancellationSource.Token);

                return response;
            }
            catch (Exception ex)
            {
                var response = request.CreateResponse(HttpStatusCode.InternalServerError);
                await response.WriteStringAsync(ex.Message, cancellationSource.Token);
                return response;
            }
        }


        public async Task<HttpResponseData> Update(HttpRequestData request, string id)
        {
            if (request.Body.Length == 0)
            {
                var response = request.CreateResponse(HttpStatusCode.BadRequest);
                _logger.LogInformation("The Body of the request was empty");
                return response;
            }
        
            using var cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(request.FunctionContext.CancellationToken);
            var requestBody = await new StreamReader(request.Body).ReadToEndAsync(cancellationSource.Token);
            var itemToUpdate = JsonSerializer.Deserialize<TData>(requestBody);
            itemToUpdate.Id = id;
        
            try
            {
                var filter = Builders<TData>.Filter.Where(x => x.Id == id);
                var documents =  await Collection.FindAsync(filter, cancellationToken: cancellationSource.Token);
                await Collection.ReplaceOneAsync(filter,itemToUpdate, cancellationToken: cancellationSource.Token);  
            
                _logger.LogInformation(@"Updated Record Records");

                var response = request.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(itemToUpdate, cancellationToken: cancellationSource.Token);

                return response;
            }
            catch (Exception ex)
            {
                var response = request.CreateResponse(HttpStatusCode.InternalServerError);
                await response.WriteStringAsync(ex.Message, cancellationSource.Token);
                return response;
            }
            
        }
       
    }
}
