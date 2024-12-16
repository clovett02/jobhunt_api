var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

/*builder.Services.AddCors(o => o.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));*/

builder.Services.AddCors(options => 
{
        options.AddPolicy("MyPolicy", 
        
        policy =>
        {
            policy.WithOrigins("http://thor.jobhunt")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
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
//app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
