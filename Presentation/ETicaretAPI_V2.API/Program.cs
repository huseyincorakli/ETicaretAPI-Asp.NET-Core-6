using ETicaretAPI_V2.Application;
using ETicaretAPI_V2.Application.Validators.Products;
using ETicaretAPI_V2.Infrastructure;
using ETicaretAPI_V2.Infrastructure.Filters;
using ETicaretAPI_V2.Infrastructure.Services.Storage.Azure;
using ETicaretAPI_V2.Infrastructure.Services.Storage.Local;
using ETicaretAPI_V2.Persistence;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceService();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationService();
builder.Services.AddStorage<AzureStorage>();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200", "http://localhost:5173", "https://localhost:5173").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
));

builder.Services.AddControllers(options=>options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();
app.Run();

