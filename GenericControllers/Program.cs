// TCDev.de 2022/03/31
// GenericControllers.Program.cs
// https://www.github.com/deejaytc/dotnet-utils

using System.Reflection;
using GenericControllers.Data;
using GenericControllers.Tools;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<GenericDbContext>(options => options.UseInMemoryDatabase("ApplicationDb"));

//Add Framework Services & Options, we use the current assembly to get classes. 
// Todo: Add option to add any custom assembly
builder.Services.AddMvc(o =>
      o.Conventions.Add(new GenericControllerRouteConvention()))
   .ConfigureApplicationPartManager(m =>
      m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider(new[]
      {
         Assembly.GetEntryAssembly()
            .FullName
      }))
   );


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
