using EcommerceAPI;
using EcommerceAPI.Shared;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers(); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var startup = new Startup(builder.Configuration,builder.Environment);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
