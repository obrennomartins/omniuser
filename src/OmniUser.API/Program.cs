using Microsoft.AspNetCore.Cors.Infrastructure;
using OmniUser.API.Configurations;
using OmniUser.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerConfig();
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegistrarDependencias();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddDbContext<OmniUserDbContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirCorsFrontEnd",
        corsPolicyBuilder => corsPolicyBuilder
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

app.UseSwaggerConfig();
// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("PermitirCorsFrontEnd");
app.UseMiddleware<CorsMiddleware>();

app.Run();
