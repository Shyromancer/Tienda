var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();


// hola
app.MapOpenApi();
app.Run();
