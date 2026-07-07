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

Just pick the simple Consumption (Windows) hosting plan to keep things free:

<img width="1675" height="628" alt="image" src="https://github.com/user-attachments/assets/882ef673-3db0-4adf-85d4-449aacab8a0e" />

Then fill in the data... just trying to make obvious choices:

<img width="1056" height="1005" alt="image" src="https://github.com/user-attachments/assets/ad0c45a8-f246-40fe-90ca-0f2fef6c008b" />

And then have some patience when clicking on Create:

<img width="908" height="1011" alt="image" src="https://github.com/user-attachments/assets/09372f10-868d-457b-9a5f-42cf27f5284e" />

### Add your Cosmos DB Connection String to Azure
Go to the resource. Then Settings --> Environment variables --> Then add an entree to the App settings table. And copy the values from the Comsos DB resource. (or other places where you linked to it)

<img width="1261" height="599" alt="image" src="https://github.com/user-attachments/assets/2fcba002-cb79-4bc5-bc79-66fd75660a0a" />

### Azure Deployment Center Sync
Via Deployment Center, pick Github as source and fill the remaining fields.

<img width="1169" height="973" alt="image" src="https://github.com/user-attachments/assets/3d15e315-ff29-4f7a-8484-cb89ad69400b" />

When this is done, a workflow file will be added. However it must still be modified, so it is only triggered on the local path:

<img width="840" height="460" alt="image" src="https://github.com/user-attachments/assets/8f7dfdb9-88b9-4e29-881c-0182bb1cd54c" />


After deployment, te default domain of the function is visible in the overview page:

<img width="1099" height="758" alt="image" src="https://github.com/user-attachments/assets/b2b43cb5-b4a4-442c-b89b-e8df700a9222" />

...however when clicking add the AddWeatherForecast function, you can click on Function Keys and then copy one of those keys. Best pick is default (Function Key) as that just gives access to this specific funtion.

<img width="1447" height="354" alt="image" src="https://github.com/user-attachments/assets/6c56bb3f-9a10-42de-a602-a2b5a59730b2" />

Subsequently we can invoke this funtion via PowerShell as follows:
```
Invoke-RestMethod -Uri "LINK-TO-FUNCTION-COMES-HERE" -Method Post -ContentType "application/json" -Body '{
    "id": "4",
    "date": "2026-07-08",
    "temperatureC": 28,
    "summary": "Hot and Sunny"
}'
```

<img width="1112" height="465" alt="image" src="https://github.com/user-attachments/assets/7b3c5da2-82e7-47f8-a190-55521eca3c9a" />

And the coolest way to validate, is just look at the front-end. (as it goes FE-->API-->Cosmos DB... validating that the FUNCTION really upserted that record in the COSMOS DB.

<img width="962" height="414" alt="image" src="https://github.com/user-attachments/assets/bcfc52c4-a7db-413c-847b-f46538dec076" />














