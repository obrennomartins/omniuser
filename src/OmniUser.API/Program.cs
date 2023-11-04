using Microsoft.EntityFrameworkCore;
using OmniUser.API.Configurations;
using OmniUser.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerConfig();
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegistrarDependencias();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<OmniUserDbContext>(options =>
    options.UseNpgsql(builder.Configuration["OmniUserDb"]));


var app = builder.Build();

app.UseSwaggerConfig();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();