using api;
using infrastructure;
using infrastructure.Repositories;
using Microsoft.AspNetCore.HttpOverrides;
using service;
using service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataSource();

var frontEndRelativePath = builder.Environment.IsDevelopment() ? "./../frontend/dist" : "../Budget_app_frontend";
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<PasswordHashRepository>();
builder.Services.AddSingleton<AccountService>();
builder.Services.AddSingleton<BudgetRepository>();
builder.Services.AddSingleton<BudgetService>();
builder.Services.AddJwtService();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});
var app = builder.Build();

app.UseForwardedHeaders();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
});

app.UseAuthorization();

app.MapControllers();
var frontendOrigin = app.Services.GetService<IConfiguration>()!["FrontendOrigin"];
app.UseCors(policy =>
    policy
        .SetIsOriginAllowed(origin => origin == frontendOrigin)
        .AllowAnyMethod()
        .AllowAnyHeader()
);

app.Run();
