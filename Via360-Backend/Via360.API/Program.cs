using FirebaseAdmin;
using Via360.Api.Services;
using Google.Apis.Auth.OAuth2;

var builder = WebApplication.CreateBuilder(args);
var firebaseConfig = builder.Configuration.GetSection("FirebaseSettings");
var credentialPath = firebaseConfig["CredentialFilePath"];

if (!string.IsNullOrEmpty(credentialPath) && File.Exists(credentialPath))
{
    FirebaseApp.Create(new AppOptions()
    {
        Credential = GoogleCredential.FromFile(credentialPath),
        ProjectId = firebaseConfig["ProjectId"]
    });
}
else
{
    Console.WriteLine(">>>>> E R R O R   C R I T I C O : No se encontró la llave de Firebase en: " + credentialPath);
}

// servicios
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSingleton<FirestoreService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Via360 API V1");
    c.RoutePrefix = "swagger"; // url localhost:xxxx/swagger para acceder a la UI de Swagger
});
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseAuthorization();
app.UseCors();
app.MapControllers();

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
