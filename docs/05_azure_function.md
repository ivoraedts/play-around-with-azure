## Getting Started with Azure Functions

Before locally using Azure Function related stuff a few actions are needed:

Install Azure Functions Core Tools:

```
winget install Microsoft.Azure.FunctionsCoreTools
# Or: npm install -g azure-functions-core-tools@4 --unsafe-perm true
```

Install .NET Templates
```
dotnet new install Microsoft.Azure.Functions.Worker.ProjectTemplates
```

## Create the Azure Function

First I created the folder in which I wanted to store the Azure function, which is: `play-around-with-azure\02-funtion\01-simple-function`.
Then generate the Isolated Worker Project in that folder:

```
dotnet new func -n WeatherFunctionApp -F net10.0
```

Then ensure the NuGet Packages are installed:
- Microsoft.Azure.Functions.Worker.Extensions.CosmosDB
- Microsoft.Azure.Functions.Worker.Extensions.Http.AspNetCore
- Microsoft.Azure.Functions.Worker.Sdk

Then I added the `WeatherRecord.cs` class, which matches the database entries in COSMOS DB.

`AddWeatherForecast.cs` contains the real functionality. The method tagged with: `[Function("AddWeatherForecast")]` will be discoverd and invoked via source generators that are included in Azure Funtions 2.x Worked SDK.
Unless specified different in `host.json`, Azure Functions by default reserves the root prefix `api` on all functions. As the function contains the Route = "weather" prefix, it means that calls to `/api/weather` are mapped to this method.

```
        [Function("AddWeatherForecast")]
        public async Task<MultiResponse> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "weather")] HttpRequest req)
```

The method returns a multiresponse:

```
            return new MultiResponse
            {
                CosmosOutput = weatherData,
                HttpResponse = new OkObjectResult(weatherData)
            };
```
`HttpResponse` ──► Sends 200 OK back to User

`CosmosOutput` ──► Intercepted by Azure Runtime ──► Azure Runtime automatically inserts object into Cosmos DB


`HttpResult`: This tells the Azure Functions engine: "Take whatever object is assigned to this specific property and send it back across the internet to the client browser or tool that initiated the HTTP POST request."

`CosmosDBOutput(...)`: This tells the engine: "Take whatever object is assigned to this specific property, look up the connection string named CosmosDBConnectionSetting, connect to WeatherDatabase -> WeatherForecasts, and execute an Upsert (Insert/Update) operation using that object."


Then we needed the `local.settings.json` to contain a fake well-formed mock storage structure in `AzureWebJobsStorage` and the real connection to the Cosmos DB in `CosmosDBConnectionSetting`:
<img width="1043" height="309" alt="image" src="https://github.com/user-attachments/assets/3d0f1f1d-66e6-4e9a-b1cb-6250195dc108" />

## Run the Function locally

If all goes well, the function can be started as following:
```
func start
```
or (more modern)
```
dotnet run
```

It will output tje URL that it is monitoring to receive data:

<img width="1091" height="220" alt="image" src="https://github.com/user-attachments/assets/c43d08d8-0d5d-480c-93c8-220323779155" />

And when it runs, you can invoke it like:
```
Invoke-RestMethod -Uri "http://localhost:7071/api/weather" -Method Post -ContentType "application/json" -Body '{
    "id": "3",
    "date": "2026-07-07",
    "temperatureC": 25,
    "summary": "Warm and Sunny"
}'

```

## Create the Azure Function App Resource

<img width="1675" height="628" alt="image" src="https://github.com/user-attachments/assets/882ef673-3db0-4adf-85d4-449aacab8a0e" />

<img width="1056" height="1005" alt="image" src="https://github.com/user-attachments/assets/ad0c45a8-f246-40fe-90ca-0f2fef6c008b" />

<img width="908" height="1011" alt="image" src="https://github.com/user-attachments/assets/09372f10-868d-457b-9a5f-42cf27f5284e" />

### Add your Cosmos DB Connection String to Azure
Go to the resource. Then Settings --> Environment variables --> Then add an entree to the App settings table:

<img width="1261" height="599" alt="image" src="https://github.com/user-attachments/assets/2fcba002-cb79-4bc5-bc79-66fd75660a0a" />

### Azure Deployment Center Sync
<img width="1169" height="973" alt="image" src="https://github.com/user-attachments/assets/3d15e315-ff29-4f7a-8484-cb89ad69400b" />

<img width="1099" height="758" alt="image" src="https://github.com/user-attachments/assets/b2b43cb5-b4a4-442c-b89b-e8df700a9222" />

<img width="1447" height="354" alt="image" src="https://github.com/user-attachments/assets/6c56bb3f-9a10-42de-a602-a2b5a59730b2" />

<img width="1112" height="465" alt="image" src="https://github.com/user-attachments/assets/7b3c5da2-82e7-47f8-a190-55521eca3c9a" />

<img width="962" height="414" alt="image" src="https://github.com/user-attachments/assets/bcfc52c4-a7db-413c-847b-f46538dec076" />














