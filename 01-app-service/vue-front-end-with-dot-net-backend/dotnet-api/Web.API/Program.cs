using Microsoft.Azure.Cosmos;
using Web.API.Endpoints; // Brings in your new endpoint mapping namespace

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// --- CORS Configuration ---
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
var allowedOrigins = builder.Configuration.GetSection("FrontendSettings:AllowedOrigins").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
                      policy =>
                      {
                          if (allowedOrigins != null && allowedOrigins.Length > 0)
                          {
                              policy.WithOrigins(allowedOrigins)
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                          }
                      });
});

// --- Register the Cosmos DB Client Singleton ---
var connectionString = builder.Configuration["CosmosSettings:ConnectionString"];
var dbId = builder.Configuration["CosmosSettings:DatabaseId"] ?? "WeatherDatabase";
var containerId = builder.Configuration["CosmosSettings:ContainerId"] ?? "WeatherForecasts";

builder.Services.AddSingleton<CosmosClient>(sp => new CosmosClient(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors(policyName: myAllowSpecificOrigins);

// --- Execute Your Extracted Database Route ---
app.MapWeatherEndpoints(dbId, containerId);

app.Run();
