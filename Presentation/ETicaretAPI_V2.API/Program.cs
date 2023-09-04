using ETicaretAPI_V2.API.Configurations.ColumnWriters;
using ETicaretAPI_V2.API.Extensions;
using ETicaretAPI_V2.Application;
using ETicaretAPI_V2.Application.Validators.Products;
using ETicaretAPI_V2.Infrastructure;
using ETicaretAPI_V2.Infrastructure.Filters;
using ETicaretAPI_V2.Infrastructure.Services.Storage.Local;
using ETicaretAPI_V2.Persistence;
using ETicaretAPI_V2.SignalR;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceService();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationService();
builder.Services.AddStorage<LocalStorage>();
builder.Services.AddSignalRServices();
builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
));

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/logs.txt")
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"), "logs",
    needAutoCreateTable: true, columnOptions: new Dictionary<string, ColumnWriterBase>
    {
        {"message",new RenderedMessageColumnWriter()},
        {"message_template",new MessageTemplateColumnWriter()},
        {"level", new LevelColumnWriter()},
        {"time_stamp",new TimestampColumnWriter()},
        {"exceptions",new ExceptionColumnWriter()},
        {"log_event",new LogEventSerializedColumnWriter()},
        {"user_name",new UsernameColumnWriter()}
    })
    .WriteTo.Seq("http://localhost:8888")//Seq ui
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, //hangi originler berlirli?
            ValidateIssuer = true, //kim daðýtýyor?
            ValidateLifetime = true, // token süresi?
            ValidateIssuerSigningKey = true, // security key

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
            NameClaimType = ClaimTypes.Name//JWT ÜZERÝNDEN NAME CLAIMINE DENK GELEN DEÐERÝ User.Identity.Name alabiliriz.
        };
    });

var app = builder.Build();
app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseSerilogRequestLogging();
app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthorization();


app.Use(async (contex, next) =>
{   // user_name'e karþýlýk gelen deðeri identityden yakalayýp  LogContext claasýnýn Pushpropertyisini kullanarak
    //push ediyoruz UsernameColumnWriterda key'e gelecek isimle ayný olmasý gerekmektedir.
    var username = contex.User?.Identity?.IsAuthenticated != null || true ? contex.User.Identity.Name : null;
    LogContext.PushProperty("user_name", username);
    await next();
});

app.MapControllers();

app.MapHubs();

app.Run();




#region NOT 1
//User.Identity.Name herhangi bir servisten yada katmandan ulaþmak için httpcontext e ihtyiacýmýz vardýr
//bu servis ile ulaþabiliriz builder.Services.AddHttpContextAccessor()
//Clienttan gelen istekler neticesinde oluþturulan HttpContext nesnesine katmanlardaki classlar üzerinden eriþebilmemizi saðlaar..
#endregion