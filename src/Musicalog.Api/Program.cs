using Musicalog.Domain;
using Musicalog.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IArtistServices, ArtistServices>();
builder.Services.AddScoped<IAlbumServices, AlbumServices>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var services = builder.Services;
// Health Check
services.AddHealthChecks();

// AutoMapper
services.AddAutoMapper(typeof(Musicalog.Models.MappingProfiles.AlbumMapperProfile).Assembly);

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
