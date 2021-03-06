using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Musicalog.Domain.Services;
using Musicalog.Models.Validators;
using Musicalog.Repository.DataContexts;
using Musicalog.Services;

using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IArtistServices, ArtistServices>();
builder.Services.AddScoped<IAlbumServices, AlbumServices>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var services = builder.Services;

// AutoMapper
services.AddAutoMapper(typeof(Musicalog.Models.MappingProfiles.AlbumMapperProfile).Assembly);

// Database 
services.AddDbContext<MusicLogDBDataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MusicLogDb")).EnableSensitiveDataLogging());

// Add Validator
services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<ArtistValitador>());

// Add Enum Validations 
services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                }).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve)
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Versioning
services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = ApiVersion.Default;
});

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
