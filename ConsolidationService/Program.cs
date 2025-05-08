using Microsoft.EntityFrameworkCore;
using ConsolidationService.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<TransactionDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Consolidation Service", Version = "v1" });
});

var app = builder.Build();

app.Urls.Add("http://0.0.0.0:5001");

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Consolidation Service v1"));
app.UseAuthorization();
app.MapControllers();

app.Run();