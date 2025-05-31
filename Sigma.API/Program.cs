using Sigma.API;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var serviceConfigurator = new ServicesConfig(builder.Services, configuration);
serviceConfigurator.ConfigureServices();

var app = builder.Build();

var pipelineConfigurator = new SwaggerConfig(app);
pipelineConfigurator.ConfigurePipeline();

app.Run();