using Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.InjectAppServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
await app.InitializePersistance();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

