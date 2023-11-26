# FlyballRaceDay


## Create API Client

https://github.com/RicoSuter/NSwag/wiki/CommandLine
nswag openapi2csclient /input:http://localhost:5458/swagger/v1/swagger.json /className:ApiServiceClient /namespace:FlyballRaceDay.Web /output:ApServiceClient.cs /UseBaseUrl:false