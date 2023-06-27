using Microsoft.EntityFrameworkCore;
using ParkingGarages_API.Data;
using ParkingGarages_API.Repositories;
using ParkingGarages_API.Repositories.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PGSQLConnection"));
});
builder.Services.AddControllers().AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
builder.Services.AddScoped<IParkingGarageRepository, ParkingGarageRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IParkingRepository, ParkingRepository>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
