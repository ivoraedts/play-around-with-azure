Azure Trigger Function

After implementing that first function, I realised there are different kind of triggers that can activate a function. In the previous example it was triggered by a HTTP call. So now it's time to repeat this for a timer trigger.
Next to these Core triggers, it can react on: Data & Storage triggers, Messaging & Event Triggers, Enterprise & Database Triggers and even some Special System Triggers. But doing the two core actions is good enough for now.

After creating a new folder, its time to ensure the template is there (not needed if you just did the previous steps) and scaffold a native .NET 10 Isolated Function App structure:

```
dotnet new install Microsoft.Azure.Functions.Worker.ProjectTemplates
dotnet new func -n HourlyUpdaterApp -F net10.0
```

Then install the NuGet Packages:
```
dotnet add package Microsoft.Azure.Functions.Worker.Extensions.Timer
dotnet add package Microsoft.Azure.Functions.Worker.Extensions.CosmosDB
```

As the HTTP Trigger function is inbound traffic-driven. It needs no memory, no state, and no storage.
The Timer Trigger function is Clock-driven. So it requires state tracking, multi-instance lock synchronization, and mandatory storage access.
So in order to run locally, we must use a live Azure storage account or use an emulator.
...or as I don't feel like that: create a (free) Azure storage account that I will also use in `production`.

Create a storage account:

<img width="800" height="842" alt="image" src="https://github.com/user-attachments/assets/1283fe61-8862-413b-9fa1-a32648eccd77" />

Then see the details before confirming:

<img width="809" height="864" alt="image" src="https://github.com/user-attachments/assets/c17107e7-f731-4a19-93ef-7486708bad58" />

<img width="537" height="696" alt="image" src="https://github.com/user-attachments/assets/ce3b84ce-87c4-46be-b38f-06a08b6dfe39" />

Then I need to wait for a while.. deployment in progress..

Then go to the created resource and copy the connection string of key 1:

<img width="1014" height="892" alt="image" src="https://github.com/user-attachments/assets/15932dfe-71d1-4b14-bc56-cfb95eeef80a" />

And then copy that (together with the earlier used connection string of cosmos db) into the `local.settings.json` file:
<img width="850" height="215" alt="image" src="https://github.com/user-attachments/assets/a21bacd7-f9a6-4efe-966c-ccc2e670cdd5" />

Then similar to the previous function, I added `WeatherFunctionApp.cs`.
And next to this, there is the logic for handling the trigger in `HourlyWeatherUpdater.cs`

As shown from the header it writes results to the CosmosDB, and it is activated each hour. The time is being kept in the storage account that we just created.

<img width="544" height="178" alt="image" src="https://github.com/user-attachments/assets/1923e5e2-ca5f-48c9-b1e5-3517d71bd79d" />

Then the first action is to read from the CosmosDB and just get the first record in a variable named `existingRecord`:

<img width="538" height="141" alt="image" src="https://github.com/user-attachments/assets/bf7ca920-3cdf-4491-80b9-eebe43bedc9d" />

Finally that record is modified and then returned. The returned record will be upserted in the CosmosDB.

Validating that it works can be done via the usual steps:
```
dotnet clean
dotnet build
dotnet run
```

<img width="678" height="141" alt="image" src="https://github.com/user-attachments/assets/b853223f-5394-4110-a2ac-e21969b82657" />

