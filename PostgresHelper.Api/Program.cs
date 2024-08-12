using PostgresHelper.DataAccess;
using PostgresHelper.PostgresNuget;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPostgresDatabase<TestPostgresDbContext>(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();