using Sigma.API;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var serviceConfigurator = new ServicesConfig(builder.Services, configuration);
serviceConfigurator.ConfigureServices();

var app = builder.Build();

var swaggerConfigurator = new SwaggerConfig(app);
swaggerConfigurator.ConfigurePipeline();

app.Run();