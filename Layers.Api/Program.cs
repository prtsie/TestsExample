using TestsExample;

var builder = WebApplication.CreateBuilder(args);

//Регистрация сервисов с помощью Dependency Injection
builder.AddServices();

var app = builder.Build();

//Здесь настраивается обработка пользовательских запросов
app.ConfigureRequestPipeline();

app.Run();