using ConwayGameOfLife.API.Middlewares;
using ConwayGameOfLife.API.Profiles;
using Ioc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ResolveDependencies(builder.Configuration);
builder.Services.AddTransient<ErrorHandlerMiddleware>();
builder.Services.AddAutoMapper(typeof(BoardProfile));
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

app.UseMiddleware<ErrorHandlerMiddleware>();

app.Run();
