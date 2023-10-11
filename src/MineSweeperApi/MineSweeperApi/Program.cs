using MineSweeperApi;
using MineSweeperApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpsRedirection(options => options.HttpsPort = 5059);
builder.Services.RunCors();

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IMovementService, MovementService>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowMineSweeper");

app.UseAuthorization();

app.MapControllers();

app.Run();