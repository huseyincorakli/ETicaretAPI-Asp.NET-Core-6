using ETicaretAPI_V2.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceService();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy=>
policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
