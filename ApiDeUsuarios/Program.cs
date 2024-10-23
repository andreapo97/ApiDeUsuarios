var builder = WebApplication.CreateBuilder(args);

var misReglas = "ReglasCors";
builder.Services.AddCors(option =>
 option.AddPolicy(name: misReglas,
 builder =>
 {
     builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
 }
 )
    );

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(misReglas);

app.UseAuthorization();

app.MapControllers();

app.Run();
