## Create Azure Cosmos DB

Now that front-end and api are connected, it's time to replace the random data of that API by some data coming from a database.
And for fun, I picked Cosmos as I never used it before.

So Create it as follows:

<img width="1311" height="1006" alt="image" src="https://github.com/user-attachments/assets/b71378a1-4547-4797-b045-acc21112bac2" />

(first attempt showed other options... I had to refresh the page a couple of times.. I guess Azure can be buggy as well!)
and then:

<img width="782" height="980" alt="image" src="https://github.com/user-attachments/assets/e28ad5ed-1d3b-413c-8240-d510bbcfa38e" />

Then when the resource (some-cosmos-sandbox) is created, it's time to add a database and container:
<img width="779" height="936" alt="image" src="https://github.com/user-attachments/assets/74d3aa9f-2886-496e-a5d6-bb53449b92bf" />

Then add some items as follows:
<img width="1184" height="704" alt="image" src="https://github.com/user-attachments/assets/ef838480-9f91-4737-aca5-7039cadcffe4" />

## Connect to the Azure Cosmos DB from the WEB.API

First step was to add the Cosmos DB SDK (from the WEB.API folder): (and Newtonsoft.Json as well as that is needed for Cosmos)
```
dotnet add package Microsoft.Azure.Cosmos
dotnet add package Newtonsoft.Json
```
That will add some NuGet packages that are needed.
As the initial (lazy) idea is to just connect my local development environment to the live Azure Cosmos Database, it was just a matter of copying the (primary) connectopm stromg from the resouce.
<img width="854" height="938" alt="image" src="https://github.com/user-attachments/assets/1c3f3066-9942-41d8-91cb-f673080106bf" />

Then you could copy that connection string to the appsettings.json. However its better to put a placeholder at that point and hide the real secret in appsettings.Development.json that is not pushed to Git.

<img width="673" height="325" alt="image" src="https://github.com/user-attachments/assets/0e53bf4b-1424-462e-8c11-ea54df6ebf48" />

<img width="900" height="165" alt="image" src="https://github.com/user-attachments/assets/8d294176-9c96-4070-a0ac-15bae9842327" />
(and you may see the first 3 characters of the password as you won't guess the next 80 or so. 😁 )

Then in the code, it was time to split the endpoint handling from the program.cs.
This is the main part of that Endpoint (`WeatherEndpoints.cs`):
Note that instead of creating this endpoint, we could also have created a controller. But that might be the old-fashioned way.

<img width="805" height="386" alt="image" src="https://github.com/user-attachments/assets/0a0b31e2-1d15-40df-b88d-ce9e64aa430a" />

`Where SELECT * FROM C`, means read everything from the container, while C is just a standard industry convention. (D would result in the same)
So query all records from the Cosmos container, add them one by one to a list and just return that list. It is as simple as that.

Than after this runs locally, it is time to configure that correct connection srting in the `dotnet-api` resoucre in Azure as well:
<img width="1717" height="506" alt="image" src="https://github.com/user-attachments/assets/3fa7ad08-820c-4219-abae-7d6a48146a07" />

...and then it should all work both local and via the live website on the Azure Portal:

...and of course those free tiers on Azure are slow...

<img width="636" height="272" alt="image" src="https://github.com/user-attachments/assets/c804a9c7-df6e-4212-a8a1-3d085d983b1e" />

<img width="623" height="442" alt="image" src="https://github.com/user-attachments/assets/6e72e89f-c902-48bd-9257-7b7c67675990" />

