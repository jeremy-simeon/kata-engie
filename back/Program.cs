using Microsoft.AspNetCore.Server.Kestrel.Core;
using NetTopologySuite.Features;
using NetTopologySuite.IO;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);

builder.Services.AddCors(options => options.AddDefaultPolicy(corsBuilder => corsBuilder.AllowAnyOrigin()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.MapGet("/api/buildings.geojson", async context =>
    {
        var text = await File.ReadAllTextAsync("Buildings.geojson");
        var serializer = GeoJsonSerializer.Create();
        using (var stringReader = new StringReader(text))
        using (var jsonReader = new JsonTextReader(stringReader))
        {
            context.Response.ContentType = "application/geo+json";
            var featureCollection = serializer.Deserialize<FeatureCollection>(jsonReader);
            foreach (var feature in featureCollection)
            {
                feature.Attributes.Add("consumption", Helpers.ComputeArea(feature.Geometry) * (feature.Attributes.Exists("levels") ? (long?)feature.Attributes["levels"] ?? 1 : 1) * 60);
            }
            using var jsonWriter = new JsonTextWriter(new StreamWriter(context.Response.Body));
            serializer.Serialize(jsonWriter, featureCollection);
        }
    }
);
app.UseHttpsRedirection();
app.UseCors();
app.Run();
