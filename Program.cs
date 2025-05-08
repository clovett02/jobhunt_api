using JobHunt_API.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using MySql.EntityFrameworkCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddEntityFrameworkMySQL().AddDbContext<JobhuntContext>(options => {
//     options.UseMySQL(builder.Configuration.GetConnectionString("Default"));
// });

builder.Services.AddCors(options => 
{
        options.AddPolicy("MyPolicy", 
        
        policy =>
        {
            policy.WithOrigins("http://thor.jobhunt", "http://localhost:5000", "http://127.0.0.1:5000")
                        .WithMethods("GET", "POST", "PUT", "DELETE")
                        .WithHeaders(HeaderNames.ContentType);
        });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseRouting();

app.UseCors("MyPolicy");

// app.UseAuthorization();

app.MapControllers();

app.Run();
