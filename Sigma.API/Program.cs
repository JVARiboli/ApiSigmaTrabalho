using AutoMapper;
using Sigma.Infra.CrossCutting.IoC;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationContext(configuration.GetValue<string>("ConnectionStrings:Database")!);

MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
{
    cfg.AddMaps(new[] { "Sigma.Application" });
});
builder.Services.AddSingleton(mapperConfiguration.CreateMapper());

ContainerService.AddApplicationServicesCollentions(builder.Services);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

ContainerService.AddApplicationAuthentication(builder.Services, configuration);

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
